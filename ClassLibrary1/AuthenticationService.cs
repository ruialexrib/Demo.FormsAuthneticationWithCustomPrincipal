using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ClassLibrary1
{
    public class AuthenticationService
    {
        public HttpCookie Authenticate(User user)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //FormsAuthentication.SetAuthCookie(user.Username, false);

            var principal = new CustomPrincipal(user.Username);
            principal.Id = 1;
            principal.FullName = user.Username;
            principal.Password = user.Password;
            string userData = serializer.Serialize(principal);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                false,
                userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            return faCookie;

        }


    }
}
