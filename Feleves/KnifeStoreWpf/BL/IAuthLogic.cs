using KnifeStoreWpf.Data;
using System.Security;

namespace KnifeStoreWpf.BL
{
    public interface IAuthLogic
    {
        SimpUser Auth(User u);
    }
}