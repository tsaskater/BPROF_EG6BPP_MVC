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
        public void AddKnife(IList<Knife> list,string selectedKnifeStoreId,string token)
        {
            if (selectedKnifeStoreId==null)
            {
                this.messengerService.Send("HOZZÁADÁS SIKERTELEN", "LogicResult");
            }
            Knife newKnife = new Knife();
            if ((selectedKnifeStoreId != null && selectedKnifeStoreId != string.Empty) && this.editorService.EditKnife(newKnife) == true)
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
                string api = url + $"Knife";
                WebClient wc = new WebClient();
                var json = JsonConvert.SerializeObject(kb);
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                try
                {
                    wc.UploadString(api, "POST", json);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString().Contains("403"))
                    {
                        this.messengerService.Send("TÖRLÉS SIKERTELEN\nNINCS ENGEDÉLYE EHHEZ", "LogicResult");
                        return;
                    }
                    this.messengerService.Send("HOZZÁADÁS SIKERTELEN", "LogicResult");
                    return;
                }
                this.messengerService.Send("HOZZÁADÁS SIKERES", "LogicResult");
                return;
            }

            this.messengerService.Send("HOZZÁADÁS MEGSZAKÍTVA", "LogicResult");
        }

        /// <summary>
        /// Deletes a Knife from it's list and calls the database operation to syncronhize them.
        /// </summary>
        /// <param name="list">The entity list where the entity should be deleted from.</param>
        /// <param name="knife">The entity which is intended to delete.</param>
        public void DelKnife(IList<Knife> list, Knife knife,string token)
        {
            try
            {
                if (knife != null)
                {
                    string api = url + $"Knife" + $"/{knife.SerialNumber}";
                    WebRequest request = WebRequest.Create(api);
                    request.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                    request.Method = "DELETE";
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
                        this.messengerService.Send("TÖRLÉS SIKERTELEN", "LogicResult");
                        return;
                    }
                    list.Remove(knife);
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
        /// <param name="knifeToModify">The KnifeStore entity which should be modified.</param>
        public void ModKnife(Knife knifeToModify,string token)
        {
            if (knifeToModify == null)
            {
                this.messengerService.Send("MÓDOSÍTÁS SIKERTELEN", "LogicResult");
                return;
            }

            Knife clone = new Knife();
            clone.CopyFrom(knifeToModify);
            if (this.editorService.EditKnife(clone) == true)
            {
                knifeToModify.CopyFrom(clone);
                 Kes kb = new Kes()
                 {
                    Gyartasi_Cikkszam = clone.SerialNumber,
                    Raktar_Id = clone.StorageId,
                    Gyarto = clone.Maker == null ? string.Empty : clone.Maker,
                    Bevont_Penge = clone.Coated,
                    Acel = clone.Steel == null ? string.Empty : clone.Steel,
                    Penge_Hossz = clone.BladeLength,
                    Ar = clone.Price,
                    Markolat = clone.Handle == null ? string.Empty : clone.Handle,
                    Modell_nev = clone.Model == null ? string.Empty : clone.Model,
                 };


                string api = url + $"Knife" + $"/{knifeToModify.SerialNumber}";
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

            this.messengerService.Send("MÓDOSÍTÁS MEGSZAKÍTVA", "LogicResult");
        }

        public IList<Knife> GetAllKnivesForStore(string knifeStoreId,string token)
        {
            ObservableCollection<Knife> knifeStores = new ObservableCollection<Knife>();
            try
            {
                string api = url + $"Knife/AllKnifesForKnifeStore/{knifeStoreId}";
                WebClient wc = new WebClient();
                wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";
                string jsonContent = wc.DownloadString(api);
                IQueryable<Kes> ks = JsonConvert.DeserializeObject<List<Kes>>(jsonContent).AsQueryable();

                //IQueryable<Kes_Bolt> kbs = this.knifeStoreLogic.GetAllKes_Bolt();
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
                        Coated = item.Bevont_Penge,
                        Handle = item.Markolat,
                        Model = item.Modell_nev,
                        Price = item.Ar

                    };
                    knifeStores.Add(k);
                }
            }
            catch
            {
                this.messengerService.Send("A SZERVER NEM ELÉRHETŐ", "LogicResult");
            }
            return knifeStores;
        }
    }
}
