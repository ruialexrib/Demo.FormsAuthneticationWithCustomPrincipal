using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class CustomPrincipal :  ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }


        public CustomPrincipal(string name)
        {
            this.Identity = new GenericIdentity(name);
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
