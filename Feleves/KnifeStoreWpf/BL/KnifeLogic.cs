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
    internal class KnifeLogic:IKnifeLogic
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
        public KnifeLogic(IEditorService editorService, IMessenger messengerService)
        {
            this.editorService = editorService;
            this.messengerService = messengerService;
        }

        /// <summary>
        /// Adds a new KnifeStore to it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be added.</param>
        public void AddKnife(IList<Knife> list,string selectedKnifeStoreId)
        {
            Knife newKnife = new Knife();
            if (this.editorService.EditKnife(newKnife) == true)
            {
                Kes kb = new Kes()
                {
                    Gyartasi_Cikkszam = string.Empty,
                    Raktar_Id = selectedKnifeStoreId,
                    Gyarto = newKnife.Maker == null ? string.Empty : newKnife.Maker,
                    Bevont_Penge = newKnife.Coated,
                    Acel = newKnife.Steel == null ? string.Empty : newKnife.Steel,
                    Penge_Hossz = newKnife.BladeLength,
                    Ar = newKnife.Price,
                    Markolat = newKnife.Handle == null ? string.Empty : newKnife.Handle,
                    Modell_nev= newKnife.Model == null ? string.Empty : newKnife.Model,
                };
                string api = url + $"KnifeStore";
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
        /// Deletes a Knife from it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be deleted from.</param>
        /// <param name="knife">The entity which is intended to delete.</param>
        public void DelKnife(IList<Knife> list, Knife knife)
        {
            if (knife != null)
            {
                string api = url + $"KnifeStore" + $"/{knife.StorageId}";
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
                list.Remove(knife);
                this.messengerService.Send("DELETE OK", "LogicResult");
                return;
            }

            this.messengerService.Send("DELETE FAILED", "LogicResult");
        }

        /// <summary>
        /// Modifies an element of the knifeStore list as intended.
        /// </summary>
        /// <param name="knifeToModify">The KnifeStore entity which should be modified.</param>
        public void ModKnifeStore(Knife knifeToModify)
        {
            if (knifeToModify == null)
            {
                this.messengerService.Send("EDIT FAILED", "LogicResult");
                return;
            }

            Knife clone = new Knife();
            clone.CopyFrom(knifeToModify);
            if (this.editorService.EditKnife(clone) == true)
            {
                knifeToModify.CopyFrom(clone);
                 Kes kb = new Kes()
                 {
                    Gyartasi_Cikkszam = string.Empty,
                    Raktar_Id = clone.StorageId,
                    Gyarto = clone.Maker == null ? string.Empty : clone.Maker,
                    Bevont_Penge = clone.Coated,
                    Acel = clone.Steel == null ? string.Empty : clone.Steel,
                    Penge_Hossz = clone.BladeLength,
                    Ar = clone.Price,
                    Markolat = clone.Handle == null ? string.Empty : clone.Handle,
                    Modell_nev = clone.Model == null ? string.Empty : clone.Model,
                 };


                string api = url + $"KnifeStore" + $"/{knifeToModify.StorageId}";
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

        public IList<Knife> GetAllKnivesForStore(string knifeStoreId)
        {
            string api = url + $"Knife/AllKnifesForKnifeStore/{knifeStoreId}";
            WebClient wc = new WebClient();
            string jsonContent = wc.DownloadString(api);
            IQueryable<Kes> ks = JsonConvert.DeserializeObject<List<Kes>>(jsonContent).AsQueryable();

            //IQueryable<Kes_Bolt> kbs = this.knifeStoreLogic.GetAllKes_Bolt();
            ObservableCollection<Knife> knifeStores = new ObservableCollection<Knife>();
            if (ks == null)
            {
                return knifeStores;
            }

            foreach (var item in ks)
            {
                Knife k = new Knife()
                {
                    StorageId = item.Raktar_Id,
                    Maker = item.Gyarto,
                    Steel = item.Acel,
                    BladeLength = item.Penge_Hossz,
                    SerialNumber = item.Gyartasi_Cikkszam,
                    Coated=item.Bevont_Penge,
                    Handle=item.Markolat,
                    Model=item.Modell_nev,
                    Price=item.Ar
                    
                };
                knifeStores.Add(k);
            }

            return knifeStores;
        }
    }
}
