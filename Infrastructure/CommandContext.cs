using RealEstateRefactored.Enums;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Interfaces.Strategies;
using RealEstateRefactored.Models;
using System.Text.RegularExpressions;
using RealEstateRefactored.Interfaces.Strategies.Base;

namespace RealEstateRefactored.Infrastructure
{
    public class CommandContext : ICommandContext
    {
        private readonly Command _command;
        private readonly IServiceProvider _services;

        public CommandContext(IServiceProvider services)
        {
            _services = services;
        }

        public Command IdentifyCommand(string rawCommand)
        {
            var _command = new Command();

            if (rawCommand == null)
            {
                rawCommand = "";
            }
           
            if (rawCommand == "SHOW TABLES")
            {
                _command.Type = CommandType.SHOW_TABLES;
            }
            else if (new Regex(@"^CREATE TABLE [a-zA-Z0-9_-]+ \(([a-zA-Z0-9_-]+ [a-zA-Z]+,? ?)*\)$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.CREATE_TABLE;
                Regex regex = new Regex(@"^CREATE TABLE ([a-zA-Z0-9_-]+) ?\((?:([a-zA-Z0-9_-]+) ([a-zA-Z]+),? ?)*\)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.ColumnNames = new List<string>();
                _command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                _command.ColumnTypes = new List<string>();
                _command.ColumnTypes.AddRange(match.Groups[3].Captures.Select(x => x.Value));
            }
            else if (new Regex(@"^INSERT INTO [a-zA-Z0-9_-]+ ?(\(([a-zA-Z0-9_-]+,? ?)*\))? VALUES ?\(('[^']+',? ?)*\)$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.INSERT_INTO;
                Regex regex = new Regex(@"^INSERT INTO ([a-zA-Z0-9_-]+) ?(?:\((?:([a-zA-Z0-9_-]+),? ?)*\))? VALUES ?\((?:'([^']+)',? ?)*\)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.ColumnNames = new List<string>();
                _command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                _command.ColumnValues = new List<string>();
                _command.ColumnValues.AddRange(match.Groups[3].Captures.Select(x => x.Value));
            }
            else if (new Regex(@"^DELETE FROM ([a-zA-Z0-9_-]+)( WHERE ([a-zA-Z0-9_-]+) = '([^']+)')?$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.DELETE_FROM;
                Regex regex = new Regex(@"^DELETE FROM ([a-zA-Z0-9_-]+)(?: WHERE ([a-zA-Z0-9_-]+) = '([^']+)')?$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.SingleColumnName = match.Groups[2].Value;
                _command.SingleValue = match.Groups[3].Value;
            }
            else if (new Regex(@"^DROP TABLE [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.DROP_TABLE;
                _command.TableName = rawCommand.Substring(10).Trim();
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ RENAME TO [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.ALTER_TABLE_RENAME_TO;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) RENAME TO ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.TableNameNew = match.Groups[2].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ RENAME COLUMN [a-zA-Z0-9_-]+ TO [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.ALTER_TABLE_RENAME_COLUMN;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) RENAME COLUMN ([a-zA-Z0-9_-]+) TO ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.SingleColumnName = match.Groups[2].Value;
                _command.SingleColumnNameNew = match.Groups[3].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ DELETE COLUMN [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.ALTER_TABLE_DELETE_COLUMN;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) DELETE COLUMN ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.SingleColumnName = match.Groups[2].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ ADD COLUMN [a-zA-Z0-9_-]+ [a-zA-Z]+$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.ALTER_TABLE_ADD_COLUMN;
                Regex regex = new(@"^ALTER TABLE ([a-zA-Z0-9_-]+) ADD COLUMN ([a-zA-Z0-9_-]+) ([a-zA-Z]+)$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.ColumnNames = new List<string>();
                _command.ColumnNames.Add(match.Groups[2].Value);
                _command.ColumnTypes = new List<string>();
                _command.ColumnTypes.Add(match.Groups[3].Value);
            }
            else if (new Regex(@"^UPDATE [a-zA-Z0-9_-]+ SET ([a-zA-Z0-9_-]+ ?= ?'[^']+',? ?)+ WHERE [a-zA-Z0-9_-]+ ?= ?'[^']+'$").IsMatch(rawCommand))
            {
                _command.Type = CommandType.UPDATE;
                Regex regex = new Regex(@"^UPDATE ([a-zA-Z0-9_-]+) SET (?:([a-zA-Z0-9_-]+) ?= ?'([^']+)',? ?)+ WHERE ([a-zA-Z0-9_-]+) = '([^']+)'$");
                Match match = regex.Match(rawCommand);
                _command.TableName = match.Groups[1].Value;
                _command.ColumnNames = new List<string>();
                _command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                _command.ColumnValues = new List<string>();
                _command.ColumnValues.AddRange(match.Groups[3].Captures.Select(x => x.Value));
                _command.SingleColumnName = match.Groups[4].Value;
                _command.SingleValue = match.Groups[5].Value;
            }
            else if (rawCommand.StartsWith("SELECT "))
            {
                #region split select command parts
                int joinPosition = rawCommand.IndexOf(" JOIN ");
                int wherePosition = rawCommand.IndexOf(" WHERE ");
                int orderbyPosition = rawCommand.IndexOf(" ORDER BY ");
                string commandPartSelect = rawCommand;
                if (joinPosition > 0 || wherePosition > 0 || orderbyPosition > 0)
                {
                    commandPartSelect = rawCommand.Remove(new List<int>() { joinPosition, wherePosition, orderbyPosition }.Where(x => x > 0).Min()).Trim();
                    rawCommand = rawCommand.Substring(commandPartSelect.Length).Trim();
                }
                else
                    rawCommand = "";

                wherePosition = rawCommand.IndexOf("WHERE ");
                orderbyPosition = rawCommand.IndexOf("ORDER BY ");
                string commandPartJoin = "";
                if (joinPosition >= 0)
                {
                    commandPartJoin = rawCommand;
                    if (wherePosition > 0 || orderbyPosition > 0)
                    {
                        commandPartJoin = rawCommand.Remove(new List<int>() { wherePosition, orderbyPosition }.Where(x => x > 0).Min()).Trim();
                        rawCommand = rawCommand.Substring(commandPartJoin.Length).Trim();
                    }
                    else
                        rawCommand = "";
                }
                string commandPartWhere = "";
                orderbyPosition = rawCommand.IndexOf("ORDER BY ");
                if (wherePosition >= 0)
                {
                    commandPartWhere = rawCommand;
                    if (orderbyPosition > 0)
                    {
                        commandPartWhere = rawCommand.Remove(orderbyPosition).Trim();
                        rawCommand = rawCommand.Substring(commandPartWhere.Length).Trim();
                    }
                    else
                        rawCommand = "";
                }
                string commandPartOrderBy = rawCommand;
                #endregion

                Regex regexSelect = new Regex(@"^SELECT \((?:(?:(?:([a-zA-Z0-9_-]+)(?: ?= ?'([^']+)')?,? ?)+)|[\*])\) FROM ([a-zA-Z0-9_-]+)$");
                Regex regexJoin = new Regex(@"^JOIN ([a-zA-Z0-9_-]+) ON ([a-zA-Z0-9_-]+) = ([a-zA-Z0-9_-]+)$");
                Regex regexWhere = new Regex(@"^WHERE (?:([a-zA-Z0-9_-]+) ?([=<>]{1,2}) ?'([^']+)',? ?)+$");
                Regex regexOrderBy = new Regex(@"^ORDER BY (?:([a-zA-Z0-9_-]+) (ASC|DESC),? ?)+$");

                #region match parts
                if (regexSelect.IsMatch(commandPartSelect))
                {
                    _command.Type = CommandType.SELECT;
                    Match match = regexSelect.Match(commandPartSelect);
                    _command.ColumnNames = new List<string>();
                    _command.ColumnNames.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    _command.ColumnValues = new List<string>();
                    _command.ColumnValues.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                    _command.TableName = match.Groups[3].Value;
                }

                if (regexJoin.IsMatch(commandPartJoin))
                {
                    Match match = regexJoin.Match(commandPartJoin);
                    _command.TableNameJoin = match.Groups[1].Value;
                    _command.SingleColumnName = match.Groups[2].Value;
                    _command.SingleColumnNameJoin = match.Groups[3].Value;
                }

                if (regexWhere.IsMatch(commandPartWhere))
                {
                    Match match = regexWhere.Match(commandPartWhere);
                    _command.ColumnNamesWhere = new List<string>();
                    _command.ColumnNamesWhere.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    _command.ColumnConditionWhere = new List<string>();
                    _command.ColumnConditionWhere.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                    _command.ColumnValuesWhere = new List<string>();
                    _command.ColumnValuesWhere.AddRange(match.Groups[3].Captures.Select(x => x.Value));
                }
                if (regexOrderBy.IsMatch(commandPartOrderBy))
                {
                    Match match = regexOrderBy.Match(commandPartOrderBy);
                    _command.ColumnNamesOrderBy = new List<string>();
                    _command.ColumnNamesOrderBy.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    _command.OrderByTypes = new List<string>();
                    _command.OrderByTypes.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                }
                #endregion
            }

            return _command;
        }

        private void SetStrategy()
        {

        }

        private ICommandStrategy ResolveStrategy()
        {
            return _command.Type switch
            {
                CommandType.SHOW_TABLES => (ICommandStrategy)_services.GetService(typeof(IShowTablesStrategy)),
                CommandType.CREATE_TABLE => (ICommandStrategy)_services.GetService(typeof(ICreateTableStrategy)),
                CommandType.INSERT_INTO => (ICommandStrategy)_services.GetService(typeof(IInsertIntoStrategy)),
                CommandType.DELETE_FROM => (ICommandStrategy)_services.GetService(typeof(IDeleteFromStrategy)),
                CommandType.DROP_TABLE => (ICommandStrategy)_services.GetService(typeof(IDropTableStrategy)),
                CommandType.ALTER_TABLE_RENAME_TO => (ICommandStrategy)_services.GetService(typeof(IAlterRenameTableToStrategy)),
                CommandType.ALTER_TABLE_RENAME_COLUMN => (ICommandStrategy)_services.GetService(typeof(IAlterRenameColumnStrategy)),
                CommandType.ALTER_TABLE_DELETE_COLUMN => (ICommandStrategy)_services.GetService(typeof(IAlterDeleteColumnStrategy)),
                CommandType.ALTER_TABLE_ADD_COLUMN => (ICommandStrategy)_services.GetService(typeof(IAlterAddColumnStrategy)),
                CommandType.UPDATE => (ICommandStrategy)_services.GetService(typeof(IUpdateStrategy)),
                CommandType.SELECT => (ICommandStrategy)_services.GetService(typeof(ISelectStrategy))
            };
        }
    }
}
