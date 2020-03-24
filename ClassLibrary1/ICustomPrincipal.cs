using System.Security.Principal;

namespace ClassLibrary1
{
    public interface ICustomPrincipal: IPrincipal
    {
        string FullName { get; set; }
        int Id { get; set; }
        IIdentity Identity { get; }
        string Password { get; set; }

        bool IsInRole(string role);
    }
}