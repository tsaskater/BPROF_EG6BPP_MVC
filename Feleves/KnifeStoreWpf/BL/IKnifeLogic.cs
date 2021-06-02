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
        void AddKnife(IList<Knife> list, string selectedKnifeStoreId);
        public void DelKnife(IList<Knife> list, Knife knife);
        public void ModKnife(Knife knifeToModify);
        public IList<Knife> GetAllKnivesForStore(string knifeStoreId);


    }
}
