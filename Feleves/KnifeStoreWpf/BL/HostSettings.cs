using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnifeStoreWpf.BL
{
    public class HostSettings : IHostSettings
    {
        public string Address()
        {
            return "https://knifestore.azurewebsites.net/";
        }
    }
}
