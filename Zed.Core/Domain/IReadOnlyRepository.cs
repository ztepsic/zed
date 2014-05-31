using System.Collections.Generic;

namespace Zed.Core.Domain {

    /// <summary>
    /// A general repository that allows only read operations
    /// for entities with an identifier of type int
    /// </summary>
    /// <typeparam name="TEntity">Type of entity (with an identifier of type int) with wich the repository works.</typeparam>
    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, int> where TEntity : Entity { }

    /// <summary>
    /// A general repository that allows only read operations
    /// </summary>
    /// <typeparam name="TEntity">Type of entity with which the repository works.</typeparam>
    /// <typeparam name="TId">Type of entity identifier</typeparam>
    public interface IReadOnlyRepository<out TEntity, in TId> where TEntity : Entity<TId> {

        /// <summary>
        /// Gets the entity/aggregate based on provided identifier
        /// </summary>
        /// <param name="id">Entity/aggregate identity</param>
        /// <returns>Entity/aggregate</returns>
        TEntity GetById(TId id);


        /// <summary>
        /// Gets all persistent entites/aggregates
        /// </summary>
        /// <returns>All persistent entities/aggregates</returns>
        IEnumerable<TEntity> GetAll();
    }
}
