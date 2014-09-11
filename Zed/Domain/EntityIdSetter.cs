using System;
using System.Reflection;

namespace Zed.Domain {
    /// <summary>
    /// Static class EntityIdSetter allows to manually set the identifier
    /// for the entity using reflection.
    /// </summary>
    public static class EntityIdSetter {

        /// <summary>
        /// Identifier name
        /// </summary>
        private const string IDENTIFIER_NAME = "Id";

        /// <summary>
        /// Sets the identifier of the provided entity
        /// </summary>
        /// <typeparam name="TId">Identifier (Id) type</typeparam>
        /// <param name="entity">entity instance</param>
        /// <param name="id">identifier (Id) value</param>
        public static void SetIdOf<TId>(Entity<TId> entity, TId id) {
            PropertyInfo idPropertyInfo = entity.GetType().GetProperty(IDENTIFIER_NAME, BindingFlags.Public | BindingFlags.Instance);
            if (idPropertyInfo == null) {
                throw new MissingMemberException(String.Format("No identifier property {0}.", IDENTIFIER_NAME));
            }

            idPropertyInfo.SetValue(entity, id, null);
        }

        /// <summary>
        /// Sets the identifier of the provided entity
        /// </summary>
        /// <typeparam name="TId">Identifier (Id) type</typeparam>
        /// <param name="entity">entity instance</param>
        /// <param name="id">identifier (Id) value</param>
        public static void SetIdTo<TId>(this Entity<TId> entity, TId id) {
            SetIdOf(entity, id);
        }


    }
}
