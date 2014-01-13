using NUnit.Framework;
using set_basic_aspnet_mvc.Configurations;

namespace set_basic_aspnet_mvc.test.Controllers
{
    public class BaseTests
    {
        [SetUp]
        public void TestSetups()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}