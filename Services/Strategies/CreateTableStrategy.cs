using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Interfaces.Strategies;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Services.Strategies
{
    public class CreateTableStrategy : ICreateTableStrategy
    {
        private readonly ITableService _tableService;

        public CreateTableStrategy(ITableService tableService)
        {
            _tableService = tableService;
        }

        public void Invoke(Command command)
        {
            string tableName = command.TableName;
            var columnNames = command.ColumnNames;
            var columnTypes = command.ColumnTypes;

            _tableService.CreateTable(tableName, columnNames, columnTypes);
        }

        //TODO: add validation
    }
}
