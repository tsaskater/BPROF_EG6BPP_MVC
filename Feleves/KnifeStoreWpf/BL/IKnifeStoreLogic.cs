// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

namespace KnifeStoreWpf.BL
{
    using System.Collections.Generic;
    using KnifeStoreWpf.Data;

    /// <summary>
    /// Requires the logic methods for entity KnifeStore.
    /// </summary>
    internal interface IKnifeStoreLogic
    {
        /// <summary>
        /// Defines the method for creating an instance with the type of KnifeStore.
        /// </summary>
        /// <param name="list">The list where the entity will be added.</param>
        void AddKnifeStore(IList<KnifeStore> list);

        /// <summary>
        /// Defines the method for modifying an instance with the type of KnifeStore.
        /// </summary>
        /// <param name="knifeStoreToModify">Entity which is intended to modify.</param>
        void ModKnifeStore(KnifeStore knifeStoreToModify);

        /// <summary>
        /// Defines the method for deleting an instance with the type of KnifeStore.
        /// </summary>
        /// <param name="list">List where the entity is deleted from.</param>
        /// <param name="knifeStore">Entity which is intended to deleted.</param>
        void DelKnifeStore(IList<KnifeStore> list, KnifeStore knifeStore);

        /// <summary>
        /// Gets all the entities from the database which and converts them into List of KnifeStore-s.
        /// </summary>
        /// <returns>KnifeStore List from database.</returns>
        IList<KnifeStore> GetAllKnifeStore();
    }
}