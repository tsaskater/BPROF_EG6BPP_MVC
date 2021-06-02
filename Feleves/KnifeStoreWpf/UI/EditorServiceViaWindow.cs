// <copyright file="EditorServiceViaWindow.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.UI
{
    using KnifeStoreWpf.BL;
    using KnifeStoreWpf.Data;

    /// <summary>
    /// UI communication between the editor window.
    /// </summary>
    public class EditorServiceViaWindow : IEditorService
    {
        /// <inheritdoc/>
        public bool EditKnifeStore(KnifeStore k)
        {
            KnifeStoreEditorWindow win = new KnifeStoreEditorWindow(k);
            return win.ShowDialog() ?? false;
        }
        /// <inheritdoc/>
        public bool EditKnife(Knife k)
        {
            KnifeEditorWindow win = new KnifeEditorWindow(k);
            return win.ShowDialog() ?? false;
        }
        /// <inheritdoc/>
        public bool EditReview(Review r)
        {
            ReviewEditorWindow win = new ReviewEditorWindow(r);
            return win.ShowDialog() ?? false;
        }
    }
}