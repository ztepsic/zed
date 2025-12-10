using System.Threading;
using System.Threading.Tasks;

namespace Zed.Domain {
    /// <summary>
    /// A general repository that provides basuc CRUD operations
    /// for entities with an identifier of type int
    /// </summary>
    /// <typeparam name="TEntity">Type of entity (with an identifier of type int) with wich the repository works.</typeparam>
    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, int> where TEntity : Entity { }

    /// <summary>
    /// A general repository that provides basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">Type of entity with which the repository works</typeparam>
    /// <typeparam name="TId">Type of entity identifier</typeparam>
    public interface ICrudRepository<TEntity, in TId> : IReadOnlyRepository<TEntity, TId> where TEntity : Entity<TId> {

        /// <summary>
        /// Saves a new or updates an existing entity/aggregate from the repository
        /// </summary>
        /// <param name="entity">Entity/aggregate root which is saved or updated</param>
        void SaveOrUpdate(TEntity entity);

        /// <summary>
        /// This is the asynchronous version of <see cref="SaveOrUpdate"/>.
        /// Saves a new or updates an existing entity/aggregate from the repository
        /// </summary>
        /// <param name="entity">Entity/aggregate root which is saved or updated</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        Task SaveOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="SaveOrUpdate"/>.
        /// Saves a new or updates an existing entity/aggregate from the repository
        /// </summary>
        /// <param name="entity">Entity/aggregate root which is saved or updated</param>
        Task SaveOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes the provided entity/aggregate root from the repository.
        /// </summary>
        /// <param name="entity">Entity/aggregate which needs to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// This is the asynchronous version of <see cref="Delete"/>.
        /// Deletes the provided entity/aggregate root from the repository.
        /// </summary>
        /// <param name="entity">Entity/aggregate which needs to be deleted.</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="Delete"/>.
        /// Deletes the provided entity/aggregate root from the repository.
        /// </summary>
        /// <param name="entity">Entity/aggregate which needs to be deleted.</param>
        Task DeleteAsync(TEntity entity);

    }
}
