using FluentValidation;
using System.Reflection.PortableExecutable;

namespace Application.Users.Commands.UploadUserImage
{
    public sealed class UploadUserImageValidator: AbstractValidator<UploadUserImageCommand>
    {
        //Max image size is 5MB
        private const int MaxFileSize = 5 * 1024 * 1024;
        private readonly string[] _allowedExtensions = {".jpg", ".jpeg", ".png"};
        private readonly string[] _allowedContentTypes = { "image/jpeg", "image/png" };

        public UploadUserImageValidator() 
        {
            RuleFor(x => x.UserId)
                 .GreaterThanOrEqualTo(0)
                 .WithMessage("Incorrect user id");

            RuleFor(x => x.UploadedImage)
                .NotNull()
                .WithMessage("File is required");

            //If privided image is not null
            When(x => x.UploadedImage != null, () => 
            {
                //Check file size
                RuleFor(x => x.UploadedImage.Stream.Length)
                    .LessThanOrEqualTo(MaxFileSize)
                    .WithMessage("Incorrect file size");

                //Check content type
                RuleFor(x => x.UploadedImage.ContentType)
                    .Must(IsValidContentType)
                    .WithMessage("Incorrect file type");

                //Check file extension
                RuleFor(x => x.UploadedImage.Filename)
                .Must(IsValidImageFormat)
                .WithMessage("Incorrect image format");


                //Check magic numbers to make sure the file is actually an image
                RuleFor(x => x.UploadedImage.Stream)
                .Must(IsValidImage)
                .WithMessage("Incorrect image file");
            });
        }



        //Check content type
        private bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return false;
            }

            return _allowedContentTypes.Contains(contentType.ToLowerInvariant());
        }


        //Check image extension
        private bool IsValidImageFormat(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }
            var ext = Path.GetExtension(filename);

            if(string.IsNullOrWhiteSpace(ext))
            {
                return false;
            }

            return _allowedExtensions.Contains(ext.ToLowerInvariant());
        }


        //Check file bytes (image magic numbers)
        private bool IsValidImage(Stream stream)
        {
            if (stream == null || !stream.CanRead || stream.Length < 4)
            {
                return false;
            }

            var originalPosition = stream.Position;
            stream.Position = 0;

            using var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true);

            //Read first 4 bytes
            var headerBytes = reader.ReadBytes(4);
            stream.Position = originalPosition;


            //Validate .jpg
            if (headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF)
            {
                return true;
            }

            //Validate .png
            if (headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47)
            {
                return true;
            }

            return false;
        }
    }
}
