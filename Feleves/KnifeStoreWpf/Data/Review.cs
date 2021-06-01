using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnifeStoreWpf.Data
{
    public class Review : ObservableObject
    {
        private string reviewId;
        private string author;
        private int rating;
        private string reviewText;
        private string serialNumber;

        public string SerialNumber
        {
            get { return this.serialNumber; }
            set { this.Set(ref this.serialNumber, value); }
        }

        public string ReviewId
        {
            get { return this.reviewId; }
            set { this.Set(ref this.reviewId, value); }
        }

        public string Author
        {
            get { return this.author; }
            set { this.Set(ref this.author, value); }
        }

        public int Rating
        {
            get { return this.rating; }
            set { this.Set(ref this.rating, value); }
        }

        public string ReviewText
        {
            get { return this.reviewText; }
            set { this.Set(ref this.reviewText, value); }
        }

        /// <summary>
        /// Copies an object for which is a temporary element.
        /// </summary>
        /// <param name="other">The other element where copy is intended.</param>
        public void CopyFrom(Review other)
        {
            this.GetType().GetProperties().ToList().ForEach(
                property => property.SetValue(this, property.GetValue(other)));
        }
    }
}
