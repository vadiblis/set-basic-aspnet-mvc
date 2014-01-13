using set_basic_aspnet_mvc.Controllers;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class HomeControllerBuilder
    {
        internal HomeController Build()
        {
            return new HomeController();
        }
    }
}