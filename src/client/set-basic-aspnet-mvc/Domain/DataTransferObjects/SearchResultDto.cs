using set_basic_aspnet_mvc.Domain.DataTransferObjects;

namespace set_basic_aspnet_mvc.Domain.Entities
{
    public class SearchResultDto : BaseDto
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}