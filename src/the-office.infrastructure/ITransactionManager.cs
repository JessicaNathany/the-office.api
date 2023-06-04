namespace the_office.infrastructure
{
    /// <summary>
    /// The contract that define behavior of an transaction manager
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Method that define transaction open
        /// </summary>
        /// <returns></returns>
        IDisposable Begin();

        /// <summary>
        /// Method that define transaction commit
        /// </summary>
        /// <returns></returns>
        Task Commit();

        /// <summary>
        /// Method that defines the contract for discarding operations
        /// </summary>
        /// <returns></returns>
        void Rollback();

        void CloseConnection();
    }
}
