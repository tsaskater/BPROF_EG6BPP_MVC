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
        const string url = "http://localhost:5000/";
        private IMessenger messengerService;

        public AuthLogic(IMessenger messengerService)
        {
            this.messengerService = messengerService;
        }

        public string Auth(User u)
        {
            try
            {
                string api = url + $"Auth/Login";
                WebClient wc = new WebClient();
                var json = JsonConvert.SerializeObject(u);
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var response = wc.UploadString(api, "PUT", json);
                TokenData tokendata = JsonConvert.DeserializeObject<TokenData>(response);
                this.messengerService.Send($"Bejelentkeztél {u.ValidationName} névvel.", "AuthResult");
                return tokendata.Token;
            }
            catch (Exception ex)
            {
                this.messengerService.Send("LOGIN FAILED", "AuthResult");
                return "";
            }
        }
    }
    class TokenData
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
