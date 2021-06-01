// <copyright file="EditorWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.UI
{
    using System.Windows;
    using KnifeStoreWpf.Data;
    using KnifeStoreWpf.VM;

    /// <summary>
    /// Interaction logic for EditorWindow.xaml .
    /// </summary>
    public partial class KnifeStoreEditorWindow : Window
    {
        private EditorViewModel vM;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnifeStoreEditorWindow"/> class.
        /// </summary>
        public KnifeStoreEditorWindow()
        {
            this.InitializeComponent();
            this.vM = this.FindResource("VM") as EditorViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnifeStoreEditorWindow"/> class.
        /// Constructor needed to edit elements.
        /// </summary>
        /// <param name="oldKnifeStore">The item which is edited.</param>
        public KnifeStoreEditorWindow(KnifeStore oldKnifeStore)
            : this()
        {
            this.vM.KnifeStore = oldKnifeStore;
        }

        /// <summary>
        /// Gets KnifeStore.
        /// </summary>
        public KnifeStore KnifeStore { get => this.vM.KnifeStore; }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}