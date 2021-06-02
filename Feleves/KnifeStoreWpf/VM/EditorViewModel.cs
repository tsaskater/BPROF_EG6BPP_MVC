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
        private Knife knife;
        private Review review;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// Constructor which sets up a new entity fills it in designer mode.
        /// </summary>
        public EditorViewModel()
        {
            this.knifeStore = new KnifeStore();
            this.knife = new Knife();
            this.review = new Review();
            if (this.IsInDesignMode)
            {
                // KnifeStore sample
                // knifeStore.StorageId = Guid.NewGuid(); //this is not needed for preview
                this.knifeStore.Name = "BladeHQ";
                this.knifeStore.Address = "Random strt 8.";
                this.KnifeStore.Website = "www.bladehq.com";
                // Knife sample
                this.knife.Steel = "S30V";
                this.knife.Model = "Paramilitary 2";
                this.knife.Maker = "Spyderco";
                this.knife.Coated = false;
                this.Knife.Price = 65000;
                this.knife.BladeLength = 90;
                this.knife.Handle = "G10";
            }
        }

        /// <summary>
        /// Gets or sets review.
        /// </summary>
        public Review Review
        {
            get { return this.review; }
            set { this.Set(ref this.review, value); }
        }

        /// <summary>
        /// Gets or sets knife.
        /// </summary>
        public Knife Knife
        {
            get { return this.knife; }
            set { this.Set(ref this.knife, value); }
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