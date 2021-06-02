using KnifeStoreWpf.Data;
using System.Security;

namespace KnifeStoreWpf.BL
{
    public interface IAuthLogic
    {
        string Auth(User u);
    }
}