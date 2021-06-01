using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnifeStoreWpf.Data
{
    public class Knife : ObservableObject
    {
        private string serialNumber;
        private string maker;
        private string model;
        private bool coated;
        private int bladeLength;
        private string handle;
        private string steel;
        private int price;
        private string storageId;

        public string StorageId
        {
            get { return this.storageId; }
            set { this.Set(ref this.storageId, value); }
        }

        public string SerialNumber
        {
            get { return this.serialNumber; }
            set { this.Set(ref this.serialNumber, value); }
        }
        public string Handle
        {
            get { return this.handle; }
            set { this.Set(ref this.handle, value); }
        }

        public int Price
        {
            get { return this.price; }
            set { this.Set(ref this.price, value); }
        }
        public string Maker
        {
            get { return this.maker; }
            set { this.Set(ref this.maker, value); }
        }

        public string Model
        {
            get { return this.model; }
            set { this.Set(ref this.model, value); }
        }

        public bool Coated
        {
            get { return this.coated; }
            set { this.Set(ref this.coated, value); }
        }

        public int BladeLength
        {
            get { return this.bladeLength; }
            set { this.Set(ref this.bladeLength, value); }
        }

        public string Steel
        {
            get { return this.steel; }
            set { this.Set(ref this.steel, value); }
        }

        /// <summary>
        /// Copies an object for which is a temporary element.
        /// </summary>
        /// <param name="other">The other element where copy is intended.</param>
        public void CopyFrom(Knife other)
        {
            this.GetType().GetProperties().ToList().ForEach(
                property => property.SetValue(this, property.GetValue(other)));
        }
    }
}
