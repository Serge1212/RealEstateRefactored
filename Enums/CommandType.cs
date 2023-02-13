namespace RealEstateRefactored.Enums
{
    public enum CommandType
    {
        UNKNOWN,
        SHOW_TABLES,
        CREATE_TABLE,
        INSERT_INTO,
        DELETE_FROM,
        DROP_TABLE,
        ALTER_TABLE_RENAME_TO,
        ALTER_TABLE_RENAME_COLUMN,
        ALTER_TABLE_DELETE_COLUMN,
        ALTER_TABLE_ADD_COLUMN,
        UPDATE,
        SELECT
    }
}
