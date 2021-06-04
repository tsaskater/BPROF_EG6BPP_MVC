using KnifeStoreWpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnifeStoreWpf.BL
{
    public interface IKnifeLogic
    {
        void AddKnife(IList<Knife> list, string selectedKnifeStoreId, string token);
        public void DelKnife(IList<Knife> list, Knife knife, string token);
        public void ModKnife(Knife knifeToModify, string token);
        public IList<Knife> GetAllKnivesForStore(string knifeStoreId, string token);


    }
}
