using GalaSoft.MvvmLight.Messaging;
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
        private IEditorService editorService;
        private IMessenger messengerService;
        private IHostSettings hostSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnifeStoreLogic"/> class.
        /// Initialize the components which are needed.
        /// </summary>
        /// <param name="editorService">Editor service component.</param>
        /// <param name="messengerService">Messenger service component.</param>
        public ReviewLogic(IEditorService editorService, IMessenger messengerService,IHostSettings hostSettings)
        {
            this.editorService = editorService;
            this.messengerService = messengerService;
            this.hostSettings = hostSettings;
        }

        /// <summary>
        /// Adds a new KnifeStore to it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be added.</param>
        public void AddReview(IList<Review> list, string selectedKnifeId,string token)
        {
            try
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
                    string api = hostSettings.Address() + $"Review";
                    WebClient wc = new WebClient();
                    var json = JsonConvert.SerializeObject(kb);
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                    wc.UploadString(api, "POST", json);
                    this.messengerService.Send("HOZZÁADÁS SIKERES", "LogicResult");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("403"))
                {
                    this.messengerService.Send("HOZZÁADÁS SIKERTELEN\nNINCS ENGEDÉLYE EHHEZ", "LogicResult");
                    return;
                }
                this.messengerService.Send("HOZZÁADÁS SIKERTELEN", "LogicResult");
                return;
            }

        }

        /// <summary>
        /// Deletes a Review from it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be deleted from.</param>
        /// <param name="review">The entity which is intended to delete.</param>
        public void DelReview(IList<Review> list, Review review,string token)
        {
            try
            {
                if (review != null)
                {
                    string api = hostSettings.Address() + $"Review" + $"/{review.ReviewId}";
                    WebRequest request = WebRequest.Create(api);
                    request.Method = "DELETE";
                    request.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains("403"))
                        {
                            this.messengerService.Send("TÖRLÉS SIKERTELEN\nNINCS ENGEDÉLYE EHHEZ", "LogicResult");
                            return;
                        }
                        this.messengerService.Send(ex.ToString(), "LogicResult");
                        return;
                    }
                    list.Remove(review);
                    this.messengerService.Send("TÖRLÉS SIKERES", "LogicResult");
                    return;
                }
            }
            catch 
            { 
                this.messengerService.Send("TÖRLÉS SIKERTELEN", "LogicResult");
                return;
            }

        }

        /// <summary>
        /// Modifies an element of the knifeStore list as intended.
        /// </summary>
        /// <param name="reviewToModify">The Reveiew entity which should be modified.</param>
        public void ModReview(Review reviewToModify,string token)
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
                try
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


                    string api = hostSettings.Address() + $"Review" + $"/{reviewToModify.ReviewId}";
                    try
                    {
                        WebClient wc = new WebClient();
                        var json = JsonConvert.SerializeObject(kb);
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                        wc.UploadString(api, "PUT", json);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains("403"))
                        {
                            this.messengerService.Send("MÓDOSÍTÁS SIKERTELEN\nNINCS ENGEDÉLYE EHHEZ", "LogicResult");
                            return;
                        }
                        this.messengerService.Send("MÓDOSÍTÁS SIKERTELEN", "LogicResult");
                        return;
                    }
                    //this.knifeStoreLogic.UpdateKes_Bolt(knifeStoreToModify.StorageId, kb);
                    this.messengerService.Send("MÓDOSÍTÁS SIKERES", "LogicResult");
                    return;
                }
                catch
                {

                    this.messengerService.Send("MÓDOSÍTÁS SIKERTELEN", "LogicResult");
                    return;
                }
            }

        }

        public IList<Review> GetAllReviewsForKnife(string knifeId,string token)
        {
            ObservableCollection<Review> reviews = new ObservableCollection<Review>();
            try
            {
                string api = hostSettings.Address() + $"Review/GetAllReviewsForKnife/{knifeId}";
                WebClient wc = new WebClient();
                wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                string jsonContent = wc.DownloadString(api);
                IQueryable<Velemeny> ks = JsonConvert.DeserializeObject<List<Velemeny>>(jsonContent).AsQueryable();

                //IQueryable<Kes_Bolt> kbs = this.knifeStoreLogic.GetAllKes_Bolt();
                if (ks == null)
                {
                    return reviews;
                }

                foreach (var item in ks)
                {
                    Review k = new Review()
                    {
                        ReviewId = item.Velemeny_Id,
                        ReviewText = item.VelemenySzovege,
                        Rating = item.Elegedettseg,
                        Author = item.Szerzo,
                        SerialNumber = item.Gyartasi_Cikkszam,
                    };
                    reviews.Add(k);
                }

            }
            catch
            {
                this.messengerService.Send("A SZERVER NEM TALÁLHATÓ", "LogicResult");
            }
            return reviews;
        }
    }
}
