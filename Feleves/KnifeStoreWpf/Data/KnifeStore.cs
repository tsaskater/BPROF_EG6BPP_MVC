// <copyright file="KnifeStore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.Data
{
    using System.Linq;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// KnifeStore entity.
    /// </summary>
    public class KnifeStore : ObservableObject
    {
        private string storageId;
        private string name;
        private string address;
        private string website;

        /// <summary>
        /// Gets or sets creates object reference which throws param when modified for StorageId.
        /// </summary>
        public string StorageId
        {
            get { return this.storageId; } set { this.Set(ref this.storageId, value); }
        }

        /// <summary>
        /// Gets or sets creates object reference which throws param when modified for Name.
        /// </summary>
        public string Name
        {
            get { return this.name; } set { this.Set(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets creates object reference which throws param when modified for Address.
        /// </summary>
        public string Address
        {
            get { return this.address; } set { this.Set(ref this.address, value); }
        }

        /// <summary>
        /// Gets or sets creates object reference which throws param when modified for Website.
        /// </summary>
        public string Website
        {
            get { return this.website; } set { this.Set(ref this.website, value); }
        }

        /// <summary>
        /// Copies an object for which is a temporary element.
        /// </summary>
        /// <param name="other">The other element where copy is intended.</param>
        public void CopyFrom(KnifeStore other)
        {
            this.GetType().GetProperties().ToList().ForEach(
                property => property.SetValue(this, property.GetValue(other)));
        }
    }
}