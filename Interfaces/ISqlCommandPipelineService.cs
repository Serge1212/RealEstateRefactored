namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// A service that build SQL command pipelines.
    /// </summary>
    public interface ISqlCommandPipelineService
    {
        /// <summary>
        /// Collects the pipeline to show tables in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetShowTablesPipelineAsync();

        /// <summary>
        /// Collects the pipeline to create a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetCreateTablePipelineAsync();

        /// <summary>
        /// Collects the pipeline to insert into a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetInsertPipelineAsync();

        /// <summary>
        /// Collects the pipeline to delete from a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetDeletePipelineAsync();

        /// <summary>
        /// Collects the pipeline to update table rows in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetUpdatePipelineAsync();

        /// <summary>
        /// Collects the pipeline to rename a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetAlterTableRenameToPipelineAsync();

        /// <summary>
        /// Collects the pipeline to rename a table column in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetAlterTableColumnRenameToPipelineAsync();

        /// <summary>
        /// Collects the pipeline to delete a column from a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetAlterTableDeleteColumnPipelineAsync();

        /// <summary>
        /// Collects the pipeline to add a column to a table in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetAlterTableAddColumnPipelineAsync();

        /// <summary>
        /// Collects the pipeline to perform select operation in the database.
        /// </summary>
        Task<ISqlCommandOperation> GetSelectPipelineAsync();
    }
}
