// <copyright file="IEditorService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

[assembly: System.CLSCompliant(false)]

namespace KnifeStoreWpf.BL
{
    using KnifeStoreWpf.Data;

    /// <summary>
    /// Editor service must implement certain methods.
    /// </summary>
    internal interface IEditorService
    {
        /// <summary>
        /// Checks if KnifeStore entity was edited.
        /// </summary>
        /// <param name="k">Entity where edit occured.</param>
        /// <returns>Entity was editer or not.</returns>
        bool EditKnifeStore(KnifeStore k);
        bool EditKnife(Knife k);
        bool EditReview(Review r);
        
    }
}