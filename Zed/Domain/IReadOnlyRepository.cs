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
        /// <returns>Entity/aggregate, null if the object does not exist.</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// This is the asynchronous version of <see cref="GetById"/>.
        /// Gets entity/aggregate root bases on it's identity.
        /// </summary>
        /// <param name="id">Entity/Aggregat root identifier</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Entity/aggregate, null if the object does not exist.</returns>
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets entity/aggregate root bases on it's identity.
        /// </summary>
        /// <param name="id">Entity/Aggregat root identifier</param>
        /// <returns>Entity/aggregate, null if the object does not exist.</returns>
        Task<TEntity> GetByIdAsync(TId id);

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        /// assuming that the instnace exists.
        /// </summary>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <returns>Entity/aggregate root</returns>
        /// <remarks>
        /// Load never return null. It will always return an entity or throw an exception.
        /// It is permissible for Load to not hit the database (no query/select against a database) when it is called,
        /// it is free to return a proxy instead.
        /// </remarks>
        TEntity Load(TId id);

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        /// assuming that the instnace exists.
        /// </summary>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Entity/aggregate root</returns>
        /// <remarks>
        /// Load never return null. It will always return an entity or throw an exception.
        /// It is permissible for Load to not hit the database (no query/select against a database) when it is called,
        /// it is free to return a proxy instead.
        /// </remarks>
        Task<TEntity> LoadAsync(TId id, CancellationToken cancellationToken);

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        /// assuming that the instnace exists.
        /// </summary>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <returns>Entity/aggregate root</returns>
        /// <remarks>
        /// Load never return null. It will always return an entity or throw an exception.
        /// It is permissible for Load to not hit the database (no query/select against a database) when it is called,
        /// it is free to return a proxy instead.
        /// </remarks>
        Task<TEntity> LoadAsync(TId id);

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
