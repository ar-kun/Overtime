namespace API.Contracts
{
    // Defines a generic interface IGeneralRepository that has a type parameter TEntity
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        // Defines method GetAll, GetByGuid, Create, Update, and Delete
        IEnumerable<TEntity> GetAll(); 
        TEntity? GetByGuid(Guid guid); 
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity); 
        bool Delete(TEntity entity);
    }
}
