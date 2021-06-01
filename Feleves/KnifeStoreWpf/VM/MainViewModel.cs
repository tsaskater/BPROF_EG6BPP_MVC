// <copyright file="MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.VM
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using KnifeStoreWpf.BL;
    using KnifeStoreWpf.Data;

    /// <summary>
    /// View Model for main window.
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        private IKnifeStoreLogic knifeStoreLogic;
        private IKnifeLogic knifeLogic;
        private KnifeStore selectedKnifeStore;
        private Knife selectedKnife;
        private ObservableCollection<KnifeStore> knifeStores;
        private ObservableCollection<Knife> knives;

        public MainViewModel(IKnifeStoreLogic knifeStoreLogic, IKnifeLogic knifeLogic)
        {
            this.knifeStoreLogic = knifeStoreLogic;
            this.knifeLogic = knifeLogic;

            if (!this.IsInDesignMode)
            {
                this.KnifeStores = new ObservableCollection<KnifeStore>();
            }
            else
            {
                this.KnifeStores = new ObservableCollection<KnifeStore>();
                KnifeStore ks1 = new KnifeStore() { Address = "Random Strt 8.", Name = "BladeHQ", Website = "www.bladehq.com" };
                KnifeStore ks2 = new KnifeStore() { Address = "Random Strt 69.", Name = "KnifeCenter", Website = "www.knifecenter.com" };
                KnifeStore ks3 = new KnifeStore() { Address = "Random Strt 12.", Name = "Boker", Website = "www.boker.de" };
                KnifeStore ks4 = new KnifeStore() { Address = "Random Strt 9.", Name = "BladeShop", Website = "www.bladeshop.hu" };
                KnifeStore ks5 = new KnifeStore() { Address = "Random Strt 617.", Name = "KnivesAndtools", Website = "www.knivesandtools.uk" };
                this.KnifeStores.Add(ks1);
                this.KnifeStores.Add(ks2);
                this.KnifeStores.Add(ks3);
                this.KnifeStores.Add(ks4);
                this.KnifeStores.Add(ks5);
            }

            this.AddKnifeStoreCmd = new RelayCommand(() => {
                this.knifeStoreLogic.AddKnifeStore(this.KnifeStores);
                RefreshKnifeStores();
            }, true);
            this.ModKnifeStoreCmd = new RelayCommand(() => this.knifeStoreLogic.ModKnifeStore(this.SelectedKnifeStore), true);
            this.DelKnifeStoreCmd = new RelayCommand(() => this.knifeStoreLogic.DelKnifeStore(this.KnifeStores, this.SelectedKnifeStore), true);
            this.UpdateCmd = new RelayCommand(() => RefreshKnifeStores(), true);
            this.GetKnives = new RelayCommand(() => RefreshKnives(),true);


        }

        private void RefreshKnifeStores()
        {
            this.KnifeStores = new ObservableCollection<KnifeStore>(knifeStoreLogic.GetAllKnifeStore());
        }
        private void RefreshKnives()
        {
            if (selectedKnifeStore!=null)
            {
                this.Knives = new ObservableCollection<Knife>(knifeLogic.GetAllKnivesForStore(selectedKnifeStore.StorageId));
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IKnifeStoreLogic>(),
                  IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IKnifeLogic>()) // IoC
        {
        }

        /// <summary>
        /// Gets ts the property of KnifeStore collection.
        /// </summary>
        public ObservableCollection<KnifeStore> KnifeStores
        {
            get { return this.knifeStores; }
            set { this.Set(ref this.knifeStores, value); }
        }
        public ObservableCollection<Knife> Knives
        {
            get { return this.knives; }
            set { this.Set(ref this.knives, value); }
        }

        /// <summary>
        /// Gets or sets the KnifeStore which is currently selected from the frontend list.
        /// </summary>
        public KnifeStore SelectedKnifeStore
        {
            get { return this.selectedKnifeStore; } set { this.Set(ref this.selectedKnifeStore, value); }
        }

        public Knife SelectedKnife
        {
            get { return this.selectedKnife; }
            set { this.Set(ref this.selectedKnife, value); }
        }

        public ICommand AddKnifeStoreCmd { get; private set; }

        public ICommand ModKnifeStoreCmd { get; private set; }

        public ICommand DelKnifeStoreCmd { get; private set; }

        public ICommand UpdateCmd { get; private set; }

        public ICommand GetKnives { get; private set; }
    }
}