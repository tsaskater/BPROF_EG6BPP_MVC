// <copyright file="EditorViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.VM
{
    using GalaSoft.MvvmLight;
    using KnifeStoreWpf.Data;

    /// <summary>
    /// View Model for the window Edit.
    /// </summary>
    internal class EditorViewModel : ViewModelBase
    {
        private KnifeStore knifeStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// Constructor which sets up a new entity fills it in designer mode.
        /// </summary>
        public EditorViewModel()
        {
            this.knifeStore = new KnifeStore();
            if (this.IsInDesignMode)
            {
                // knifeStore.StorageId = Guid.NewGuid(); //this is not needed for preview
                this.knifeStore.Name = "BladeHQ";
                this.knifeStore.Address = "Random strt 8.";
                this.KnifeStore.Website = "www.bladehq.com";
            }
        }

       /// <summary>
       /// Gets or sets knifestore.
       /// </summary>
        public KnifeStore KnifeStore
        {
            get { return this.knifeStore; } set { this.Set(ref this.knifeStore, value); }
        }
    }
}