using GalaSoft.MvvmLight.Messaging;
using KnifeStoreWpf.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace KnifeStoreWpf.BL
{
    internal class AuthLogic : IAuthLogic
    {
        private IMessenger messengerService;
        private IHostSettings hostSettings;
        public AuthLogic(IMessenger messengerService, IHostSettings hostSettings)
        {
            this.messengerService = messengerService;
            this.hostSettings = hostSettings;
        }

        public SimpUser Auth(User u)
        {
            try
            {
                string api = hostSettings.Address() + $"Auth/Login";
                WebClient wc = new WebClient();
                var json = JsonConvert.SerializeObject(new { validationName = u.ValidationName, password=u.Password.Password });
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var response = wc.UploadString(api, "PUT", json);
                TokenData tokendata = JsonConvert.DeserializeObject<TokenData>(response);

                api = hostSettings.Address() + $"Auth/GetUser/{u.ValidationName}";
                wc = new WebClient();
                wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {tokendata.Token}";
                response = wc.DownloadString(api);
                SimpUser simpUser = JsonConvert.DeserializeObject<SimpUser>(response);
                simpUser.Token = tokendata.Token;

                this.messengerService.Send($"Bejelentkeztél {simpUser.UserName} névvel.", "AuthResult");
                return simpUser;
            }
            catch (Exception ex)
            {
                this.messengerService.Send("BEJELENTKEZÉS SIKERTELEN", "AuthResult");
                return new SimpUser();
            }
        }
    }
    class TokenData
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
