using RealEstateRefactored.Enums;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Interfaces.Strategies;
using RealEstateRefactored.Models;
using System.Text.RegularExpressions;
using RealEstateRefactored.Interfaces.Strategies.Base;

namespace RealEstateRefactored.Infrastructure
{
    /// <inheritdoc/>
    public class CommandContext : ICommandContext
    {
        private readonly List<Command> _commands;
        private readonly IServiceProvider _services;

        public CommandContext(IServiceProvider services)
        {
            _services = services;
        }

        /// <inheritdoc/>
        public void ProcessCommands(string rawCommand)
        {
            // Split raw command into multiple commands.
            string[] subCommands = rawCommand.Split(";", StringSplitOptions.RemoveEmptyEntries);

            // Format multiple commands.
            for (int i = 0; i < subCommands.Length; i++)
            {
                subCommands[i] = subCommands[i].Replace(Environment.NewLine, "").Trim();
            }

            //Process each.
            foreach(string subCommand in subCommands)
            {
                var command = IdentifyCommand(subCommand);
                if (command.Type is CommandType.UNKNOWN)
                {
                    Console.WriteLine();
                }

                SetStrategy(command);
            }

        }

        private Command IdentifyCommand(string rawCommand)
        {
            var command = new Command();

            if (rawCommand == null)
            {
                rawCommand = "";
            }
           
            if (rawCommand == "SHOW TABLES")
            {
                command.Type = CommandType.SHOW_TABLES;
            }
            else if (new Regex(@"^CREATE TABLE [a-zA-Z0-9_-]+ \(([a-zA-Z0-9_-]+ [a-zA-Z]+,? ?)*\)$").IsMatch(rawCommand))
            {
                command.Type = CommandType.CREATE_TABLE;
                Regex regex = new Regex(@"^CREATE TABLE ([a-zA-Z0-9_-]+) ?\((?:([a-zA-Z0-9_-]+) ([a-zA-Z]+),? ?)*\)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.ColumnNames = new List<string>();
                command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                command.ColumnTypes = new List<string>();
                command.ColumnTypes.AddRange(match.Groups[3].Captures.Select(x => x.Value));
            }
            else if (new Regex(@"^INSERT INTO [a-zA-Z0-9_-]+ ?(\(([a-zA-Z0-9_-]+,? ?)*\))? VALUES ?\(('[^']+',? ?)*\)$").IsMatch(rawCommand))
            {
                command.Type = CommandType.INSERT_INTO;
                Regex regex = new Regex(@"^INSERT INTO ([a-zA-Z0-9_-]+) ?(?:\((?:([a-zA-Z0-9_-]+),? ?)*\))? VALUES ?\((?:'([^']+)',? ?)*\)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.ColumnNames = new List<string>();
                command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                command.ColumnValues = new List<string>();
                command.ColumnValues.AddRange(match.Groups[3].Captures.Select(x => x.Value));
            }
            else if (new Regex(@"^DELETE FROM ([a-zA-Z0-9_-]+)( WHERE ([a-zA-Z0-9_-]+) = '([^']+)')?$").IsMatch(rawCommand))
            {
                command.Type = CommandType.DELETE_FROM;
                Regex regex = new Regex(@"^DELETE FROM ([a-zA-Z0-9_-]+)(?: WHERE ([a-zA-Z0-9_-]+) = '([^']+)')?$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.SingleColumnName = match.Groups[2].Value;
                command.SingleValue = match.Groups[3].Value;
            }
            else if (new Regex(@"^DROP TABLE [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                command.Type = CommandType.DROP_TABLE;
                command.TableName = rawCommand.Substring(10).Trim();
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ RENAME TO [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                command.Type = CommandType.ALTER_TABLE_RENAME_TO;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) RENAME TO ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.TableNameNew = match.Groups[2].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ RENAME COLUMN [a-zA-Z0-9_-]+ TO [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                command.Type = CommandType.ALTER_TABLE_RENAME_COLUMN;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) RENAME COLUMN ([a-zA-Z0-9_-]+) TO ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.SingleColumnName = match.Groups[2].Value;
                command.SingleColumnNameNew = match.Groups[3].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ DELETE COLUMN [a-zA-Z0-9_-]+$").IsMatch(rawCommand))
            {
                command.Type = CommandType.ALTER_TABLE_DELETE_COLUMN;
                Regex regex = new Regex(@"^ALTER TABLE ([a-zA-Z0-9_-]+) DELETE COLUMN ([a-zA-Z0-9_-]+)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.SingleColumnName = match.Groups[2].Value;
            }
            else if (new Regex(@"^ALTER TABLE [a-zA-Z0-9_-]+ ADD COLUMN [a-zA-Z0-9_-]+ [a-zA-Z]+$").IsMatch(rawCommand))
            {
                command.Type = CommandType.ALTER_TABLE_ADD_COLUMN;
                Regex regex = new(@"^ALTER TABLE ([a-zA-Z0-9_-]+) ADD COLUMN ([a-zA-Z0-9_-]+) ([a-zA-Z]+)$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.ColumnNames = new List<string>();
                command.ColumnNames.Add(match.Groups[2].Value);
                command.ColumnTypes = new List<string>();
                command.ColumnTypes.Add(match.Groups[3].Value);
            }
            else if (new Regex(@"^UPDATE [a-zA-Z0-9_-]+ SET ([a-zA-Z0-9_-]+ ?= ?'[^']+',? ?)+ WHERE [a-zA-Z0-9_-]+ ?= ?'[^']+'$").IsMatch(rawCommand))
            {
                command.Type = CommandType.UPDATE;
                Regex regex = new Regex(@"^UPDATE ([a-zA-Z0-9_-]+) SET (?:([a-zA-Z0-9_-]+) ?= ?'([^']+)',? ?)+ WHERE ([a-zA-Z0-9_-]+) = '([^']+)'$");
                Match match = regex.Match(rawCommand);
                command.TableName = match.Groups[1].Value;
                command.ColumnNames = new List<string>();
                command.ColumnNames.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                command.ColumnValues = new List<string>();
                command.ColumnValues.AddRange(match.Groups[3].Captures.Select(x => x.Value));
                command.SingleColumnName = match.Groups[4].Value;
                command.SingleValue = match.Groups[5].Value;
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
                    command.Type = CommandType.SELECT;
                    Match match = regexSelect.Match(commandPartSelect);
                    command.ColumnNames = new List<string>();
                    command.ColumnNames.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    command.ColumnValues = new List<string>();
                    command.ColumnValues.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                    command.TableName = match.Groups[3].Value;
                }

                if (regexJoin.IsMatch(commandPartJoin))
                {
                    Match match = regexJoin.Match(commandPartJoin);
                    command.TableNameJoin = match.Groups[1].Value;
                    command.SingleColumnName = match.Groups[2].Value;
                    command.SingleColumnNameJoin = match.Groups[3].Value;
                }

                if (regexWhere.IsMatch(commandPartWhere))
                {
                    Match match = regexWhere.Match(commandPartWhere);
                    command.ColumnNamesWhere = new List<string>();
                    command.ColumnNamesWhere.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    command.ColumnConditionWhere = new List<string>();
                    command.ColumnConditionWhere.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                    command.ColumnValuesWhere = new List<string>();
                    command.ColumnValuesWhere.AddRange(match.Groups[3].Captures.Select(x => x.Value));
                }
                if (regexOrderBy.IsMatch(commandPartOrderBy))
                {
                    Match match = regexOrderBy.Match(commandPartOrderBy);
                    command.ColumnNamesOrderBy = new List<string>();
                    command.ColumnNamesOrderBy.AddRange(match.Groups[1].Captures.Select(x => x.Value));
                    command.OrderByTypes = new List<string>();
                    command.OrderByTypes.AddRange(match.Groups[2].Captures.Select(x => x.Value));
                }
                #endregion
            }

            return command;
        }

        private void SetStrategy(Command command)
        {
            var strategy = ResolveStrategy(command);
            strategy.Invoke(command);

        }

        private ICommandStrategy ResolveStrategy(Command command)
        {
            return command.Type switch
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
                CommandType.SELECT => (ICommandStrategy)_services.GetService(typeof(ISelectStrategy)),
            };
        }
    }
}
