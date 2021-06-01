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
            /*EditorWindow win = new EditorWindow(r);
            return win.ShowDialog() ?? false;*/
            return false;
        }
        /// <inheritdoc/>
        public bool EditReview(Review r)
        {
            /*EditorWindow win = new EditorWindow(r);
            return win.ShowDialog() ?? false;*/
            return false;
        }
    }
}