using System.Web.Security;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(long id, string name, string email, int roleId, bool createPersistentCookie);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(long id, string name, string email, int roleId, bool createPersistentCookie)
        {            
            FormsAuthentication.SetAuthCookie(string.Format("{0}|{1}|{2}|{3}", id, name, email, roleId), createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}