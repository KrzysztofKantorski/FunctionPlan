namespace Application.Abstractions.Storage
{
    public class BlobSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string AvatarsContainerName { get; set; } = string.Empty;
    }
}
