namespace Application.Exceptions
{
    internal sealed class ImageNotFound: AppException
    {
        public ImageNotFound(string message) : base(message, 404)
        {
        }
    }
}
