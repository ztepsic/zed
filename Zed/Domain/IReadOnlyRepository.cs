using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Zed.Domain {

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
    public interface IReadOnlyRepository<TEntity, in TId> where TEntity : Entity<TId> {

        /// <summary>
        /// Gets the entity/aggregate based on provided identifier
        /// </summary>
        /// <param name="id">Entity/aggregate identity</param>
        /// <returns>Entity/aggregate</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// This is the asynchronous version of <see cref="GetById"/>.
        /// Gets entity/aggregate root bases on it's identity.
        /// </summary>
        /// <param name="id">Entity/Aggregat root identifier</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Entity/aggregate root</returns>
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets entity/aggregate root bases on it's identity.
        /// </summary>
        /// <param name="id">Entity/Aggregat root identifier</param>
        /// <returns>Entity/aggregate root</returns>
        Task<TEntity> GetByIdAsync(TId id);


        /// <summary>
        /// Gets all persistent entites/aggregates
        /// </summary>
        /// <returns>All persistent entities/aggregates</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// This is the asynchronous version of <see cref="GetAll()"/>.
        /// Gets all persisted entities/aggregate roots
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>All persisted entities/aggregate roots</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// This is the asynchronous version of <see cref="GetAll()"/>.
        /// Gets all persisted entities/aggregate roots
        /// </summary>
        /// <returns>All persisted entities/aggregate roots</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
