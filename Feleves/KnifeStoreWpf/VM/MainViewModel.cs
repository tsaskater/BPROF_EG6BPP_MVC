// <copyright file="MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.VM
{
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Windows;
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
        private IReviewLogic reviewLogic;
        private KnifeStore selectedKnifeStore;
        private Knife selectedKnife;
        private Review selectedReview;
        private ObservableCollection<KnifeStore> knifeStores;
        private ObservableCollection<Knife> knives;
        private ObservableCollection<Review> reviews;

        public MainViewModel(IKnifeStoreLogic knifeStoreLogic, IKnifeLogic knifeLogic, IReviewLogic reviewLogic)
        {
            this.knifeStoreLogic = knifeStoreLogic;
            this.knifeLogic = knifeLogic;
            this.reviewLogic = reviewLogic;

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

            this.AddKnifeCmd = new RelayCommand(() => {
                    this.knifeLogic.AddKnife(this.Knives,selectedKnifeStore == null ? string.Empty : selectedKnifeStore.StorageId);
                    RefreshKnives();
            }, true);
            this.ModKnifeCmd = new RelayCommand(() => this.knifeLogic.ModKnife(SelectedKnife), true);
            this.DelKnifeCmd = new RelayCommand(() => this.knifeLogic.DelKnife(Knives,SelectedKnife), true);

            this.AddReviewCmd = new RelayCommand(() => {
                    this.reviewLogic.AddReview(this.Reviews,selectedKnife == null ? string.Empty : SelectedKnife.SerialNumber);
                    RefreshReview();
            }, true);
            this.ModReviewCmd = new RelayCommand(() => this.reviewLogic.ModReview(SelectedReview), true);
            this.DelReviewCmd = new RelayCommand(() => this.reviewLogic.DelReview(Reviews, SelectedReview), true);

            this.GenerateCmd = new RelayCommand(() => GenerateSample(), true);
            this.UpdateCmd = new RelayCommand(() => RefreshKnifeStores(), true);
            this.GetKnives = new RelayCommand(() => { RefreshKnives(); Reviews = new ObservableCollection<Review>(); selectedReview = null; },true);
            this.GetReviews = new RelayCommand(() => RefreshReview(), true);
        }

        private void GenerateSample()
        {
            if (knifeStores.Count > 0)
            {
                if (MessageBox.Show("Már van generált adatod!\nSzeretnél ismét generálni?", "Hoppá!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            string api = "http://localhost:5000/SampleDataGenerator";
            WebRequest request = WebRequest.Create(api);
            request.Method = "POST";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                RefreshKnifeStores();
                Knives = new ObservableCollection<Knife>();
                Reviews = new ObservableCollection<Review>();
            }
            catch
            {
                return;
            }
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
        private void RefreshReview()
        {
            if (selectedKnife != null)
            {
                this.Reviews = new ObservableCollection<Review>(reviewLogic.GetAllReviewsForKnife(selectedKnife.SerialNumber));
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IKnifeStoreLogic>(),
                  IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IKnifeLogic>(),
                  IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IReviewLogic>()) // IoC
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
        public ObservableCollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.Set(ref this.reviews, value); }
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
        public Review SelectedReview
        {
            get { return this.selectedReview; }
            set { this.Set(ref this.selectedReview, value); }
        }

        public ICommand AddKnifeStoreCmd { get; private set; }

        public ICommand ModKnifeStoreCmd { get; private set; }

        public ICommand DelKnifeStoreCmd { get; private set; }

        public ICommand AddKnifeCmd { get; private set; }

        public ICommand ModKnifeCmd { get; private set; }

        public ICommand DelKnifeCmd { get; private set; }

        public ICommand AddReviewCmd { get; private set; }

        public ICommand ModReviewCmd { get; private set; }

        public ICommand DelReviewCmd { get; private set; }

        public ICommand UpdateCmd { get; private set; }

        public ICommand GenerateCmd { get; private set; }

        public ICommand GetKnives { get; private set; }

        public ICommand GetReviews { get; private set; }
    }
}