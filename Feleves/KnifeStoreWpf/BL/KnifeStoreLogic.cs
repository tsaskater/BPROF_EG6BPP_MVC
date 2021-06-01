namespace KnifeStoreWpf.BL
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using GalaSoft.MvvmLight.Messaging;
    using KnifeStoreWpf.Data;
    using Logic;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Logic Portion for the frontend methods which are required such as cruds.
    /// </summary>
    internal class KnifeStoreLogic : IKnifeStoreLogic
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
        public KnifeStoreLogic(IEditorService editorService, IMessenger messengerService)
        {
            this.editorService = editorService;
            this.messengerService = messengerService;
        }

        /// <summary>
        /// Adds a new KnifeStore to it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be added.</param>
        public void AddKnifeStore(IList<KnifeStore> list)
        {
            KnifeStore newKnifeStore = new KnifeStore();
            if (this.editorService.EditKnifeStore(newKnifeStore) == true)
            {
                Kes_Bolt kb = new Kes_Bolt()
                {
                    Raktar_Id = string.Empty,
                    Bolt_Nev = newKnifeStore.Name,
                    Cim = newKnifeStore.Address,
                    Weboldal = newKnifeStore.Website,
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
        /// Deletes a KnifeStore from it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be deleted from.</param>
        /// <param name="knifeStore">The entity which is intended to delete.</param>
        public void DelKnifeStore(IList<KnifeStore> list, KnifeStore knifeStore)
        {
            if (knifeStore != null)
            {
                string api = url + $"KnifeStore"+$"/{knifeStore.StorageId}";
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
                list.Remove(knifeStore);
                this.messengerService.Send("DELETE OK", "LogicResult");
                return;
            }

            this.messengerService.Send("DELETE FAILED", "LogicResult");
        }

        /// <summary>
        /// Modifies an element of the knifeStore list as intended.
        /// </summary>
        /// <param name="knifeStoreToModify">The KnifeStore entity which should be modified.</param>
        public void ModKnifeStore(KnifeStore knifeStoreToModify)
        {
            if (knifeStoreToModify == null)
            {
                this.messengerService.Send("EDIT FAILED", "LogicResult");
                return;
            }

            KnifeStore clone = new KnifeStore();
            clone.CopyFrom(knifeStoreToModify);
            if (this.editorService.EditKnifeStore(clone) == true)
            {
                knifeStoreToModify.CopyFrom(clone);
                Kes_Bolt kb = new Kes_Bolt()
                {
                    Raktar_Id = clone.StorageId,
                    Bolt_Nev = clone.Name,
                    Cim = clone.Address,
                    Weboldal = clone.Website,
                };


                string api = url + $"KnifeStore" + $"/{knifeStoreToModify.StorageId}";
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

        /// <summary>
        /// Gets all the KesBolt elements from the database converts it to KnifeStore objects.
        /// </summary>
        /// <returns>All the KnifeStore entities from the database.</returns>
        public IList<KnifeStore> GetAllKnifeStore()
        {
            string api = url + $"KnifeStore";
            WebClient wc = new WebClient();
            string jsonContent = wc.DownloadString(api);
            IQueryable<Kes_Bolt> kbs = JsonConvert.DeserializeObject<List<Kes_Bolt>>(jsonContent).AsQueryable();

            //IQueryable<Kes_Bolt> kbs = this.knifeStoreLogic.GetAllKes_Bolt();
            ObservableCollection<KnifeStore> knifeStores = new ObservableCollection<KnifeStore>();
            if (kbs == null)
            {
                return knifeStores;
            }

            foreach (var item in kbs)
            {
                KnifeStore ks = new KnifeStore()
                {
                    StorageId = item.Raktar_Id,
                    Name = item.Bolt_Nev,
                    Address = item.Cim,
                    Website = item.Weboldal,
                };
                knifeStores.Add(ks);
            }

            return knifeStores;
        }
    }
}