namespace set_basic_aspnet_mvc.Domain.DataTransferObjects
{
    public class SearchResultDto : BaseDto
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}