namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface IFormsAuthenticationService
    {
        void SignIn(long id, string name, string email, int roleId, bool createPersistentCookie);

        void SignOut();
    }
}