namespace set_basic_aspnet_mvc.Models
{
    public abstract class BaseModel
    {
        public string Msg { get; set; }

        public abstract bool IsValid();
    }
}