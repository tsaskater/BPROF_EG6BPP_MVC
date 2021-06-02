﻿using GalaSoft.MvvmLight.Messaging;
using KnifeStoreWpf.Data;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KnifeStoreWpf.BL
{
    internal class ReviewLogic:IReviewLogic
    {
        const string url = "http://localhost:5000/";
        private IEditorService editorService;
        private IMessenger messengerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnifeStoreLogic"/> class.
        /// Initialize the components which are needed.
        /// </summary>
        /// <param name="editorService">Editor service component.</param>
        /// <param name="messengerService">Messenger service component.</param>
        public ReviewLogic(IEditorService editorService, IMessenger messengerService)
        {
            this.editorService = editorService;
            this.messengerService = messengerService;
        }

        /// <summary>
        /// Adds a new KnifeStore to it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be added.</param>
        public void AddReview(IList<Review> list, string selectedKnifeId)
        {
            Review newReview = new Review();
            if ((selectedKnifeId != null && selectedKnifeId != string.Empty) && this.editorService.EditReview(newReview) == true)
            {
                Velemeny kb = new Velemeny()
                {
                    Velemeny_Id = string.Empty,
                    VelemenySzovege = newReview.ReviewText == null ? string.Empty : newReview.ReviewText,
                    Elegedettseg = newReview.Rating,
                    Szerzo = newReview.Author == null ? string.Empty : newReview.Author,
                    Gyartasi_Cikkszam = selectedKnifeId,
                };
                string api = url + $"Review";
                WebClient wc = new WebClient();
                var json = JsonConvert.SerializeObject(kb);
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.UploadString(api, "POST", json);
                this.messengerService.Send("ADD OK", "LogicResult");
                return;
            }

            this.messengerService.Send("ADD CANCEL", "LogicResult");
        }

        /// <summary>
        /// Deletes a Review from it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be deleted from.</param>
        /// <param name="review">The entity which is intended to delete.</param>
        public void DelReview(IList<Review> list, Review review)
        {
            if (review != null)
            {
                string api = url + $"Review" + $"/{review.ReviewId}";
                WebRequest request = WebRequest.Create(api);
                request.Method = "DELETE";
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    this.messengerService.Send(ex.ToString(), "LogicResult");
                    return;
                }
                list.Remove(review);
                this.messengerService.Send("DELETE OK", "LogicResult");
                return;
            }

            this.messengerService.Send("DELETE FAILED", "LogicResult");
        }

        /// <summary>
        /// Modifies an element of the knifeStore list as intended.
        /// </summary>
        /// <param name="reviewToModify">The Reveiew entity which should be modified.</param>
        public void ModReview(Review reviewToModify)
        {
            if (reviewToModify == null)
            {
                this.messengerService.Send("EDIT FAILED", "LogicResult");
                return;
            }

            Review clone = new Review();
            clone.CopyFrom(reviewToModify);
            if (this.editorService.EditReview(clone) == true)
            {
                reviewToModify.CopyFrom(clone);
                Velemeny kb = new Velemeny()
                {
                    Velemeny_Id = string.Empty,
                    VelemenySzovege = clone.ReviewText == null ? string.Empty : clone.ReviewText,
                    Elegedettseg = clone.Rating,
                    Szerzo = clone.Author == null ? string.Empty : clone.Author,
                    Gyartasi_Cikkszam = clone.SerialNumber,
                };


                string api = url + $"Review" + $"/{reviewToModify.ReviewId}";
                try
                {
                    WebClient wc = new WebClient();
                    var json = JsonConvert.SerializeObject(kb);
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.UploadString(api, "PUT", json);
                }
                catch (Exception ex)
                {
                    this.messengerService.Send(ex.ToString(), "LogicResult");
                    return;
                }
                //this.knifeStoreLogic.UpdateKes_Bolt(knifeStoreToModify.StorageId, kb);
                this.messengerService.Send("MODIFY OK", "LogicResult");
                return;
            }

            this.messengerService.Send("MODIFY CANCEL", "LogicResult");
        }

        public IList<Review> GetAllReviewsForKnife(string knifeId)
        {
            string api = url + $"Review/GetAllReviewsForKnife/{knifeId}";
            WebClient wc = new WebClient();
            string jsonContent = wc.DownloadString(api);
            IQueryable<Velemeny> ks = JsonConvert.DeserializeObject<List<Velemeny>>(jsonContent).AsQueryable();

            //IQueryable<Kes_Bolt> kbs = this.knifeStoreLogic.GetAllKes_Bolt();
            ObservableCollection<Review> reviews = new ObservableCollection<Review>();
            if (ks == null)
            {
                return reviews;
            }

            foreach (var item in ks)
            {
                Review k = new Review()
                {
                    ReviewId=item.Velemeny_Id,
                    ReviewText=item.VelemenySzovege,
                    Rating=item.Elegedettseg,
                    Author=item.Szerzo,
                    SerialNumber=item.Gyartasi_Cikkszam,
                };
                reviews.Add(k);
            }

            return reviews;
        }
    }
}
