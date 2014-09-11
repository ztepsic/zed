namespace Zed.Domain {

    /// <summary>
    /// EntityWithOcc is an entity class with optimistic concurrency control (OCC) enabled property EntityVersion.
    /// It is subclass of Entity class which is a base class for objects (entities) which will be persisted to the database.
    /// Benefits include the addition of an Id property along with a consistent manner for comparing
    /// entities.
    /// 
    /// Entities of this class will have a int type for idnetifier (Id) and EntityVersion. For entities wih a type other than int,
    /// then use <see cref="EntityWithOcc{TId,TVersion}"/> instead.
    /// </summary>
    public class EntityWithOcc : EntityWithOcc<int, int> { }

    /// <summary>
    /// EntityWithOcc is an entity class with optimistic concurrency control (OCC) enabled property EntityVersion.
    /// It is subclass of <see cref="Entity{TId}"/> class which is a base class for objects (entities) which will be persisted to the database.
    /// Benefits include the addition of an Id property along with a consistent manner for comparing
    /// entities.
    /// </summary>
    /// <typeparam name="TId">Identifier (Id) type</typeparam>
    /// <typeparam name="TVersion">EntityVersion type</typeparam>
    public class EntityWithOcc<TId, TVersion> : Entity<TId> {
        /// <summary>
        /// Version property is used for versioning state of the entity.
        /// </summary>
        public TVersion EntityVersion { get; protected set; }
    }

}
