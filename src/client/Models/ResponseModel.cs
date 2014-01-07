namespace set_basic_aspnet_mvc.Models
{
    public class ResponseModel : BaseModel
    {
        public bool IsOk { get; set; }
        public object Result { get; set; }
    }
}