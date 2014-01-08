using System.Web.Security;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(object id, object name, object email, bool createPersistentCookie);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(object id, object name, object email, bool createPersistentCookie)
        {            
            FormsAuthentication.SetAuthCookie(string.Format("{0}|{1}|{2}", id, name, email), createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}