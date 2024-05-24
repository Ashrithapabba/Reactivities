namespace Infrastructure.Photos
{
    public interface CloudinarySettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set;}
        public string ApiSecret { get; set; }
    }
}