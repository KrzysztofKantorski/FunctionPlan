using Application.Common.Dto;
using FluentValidation;

namespace Application.Common.Validators
{
    public sealed class ImageFileDtoValidator: AbstractValidator<FileDto>
    {
        private const int MaxFileSize = 5 * 1024 * 1024;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedContentTypes = { "image/jpeg", "image/png" };

        public ImageFileDtoValidator()
        {
            RuleFor(x => x.Stream.Length)
                .LessThanOrEqualTo(MaxFileSize)
                .WithMessage("Incorrect file size.");

            RuleFor(x => x.ContentType)
                .Must(IsValidContentType)
                .WithMessage("Incorrect file extension. Allowed: JPG, PNG");

            RuleFor(x => x.FileName)
                .Must(IsValidImageFormat)
                .WithMessage("Incorrect file. Allowed: .jpg, .jpeg lub .png.");

            RuleFor(x => x.Stream)
                .Must(IsValidImage)
                .WithMessage("Incorrect file.");
        }

        private bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType)) return false;
            return _allowedContentTypes.Contains(contentType.ToLowerInvariant());
        }

        private bool IsValidImageFormat(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) return false;
            var ext = Path.GetExtension(filename);
            if (string.IsNullOrWhiteSpace(ext)) return false;
            return _allowedExtensions.Contains(ext.ToLowerInvariant());
        }

        private bool IsValidImage(Stream stream)
        {
            if (stream == null || !stream.CanRead || stream.Length < 4) return false;

            var originalPosition = stream.Position;
            stream.Position = 0;

            using var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true);
            var headerBytes = reader.ReadBytes(4);
            stream.Position = originalPosition;

            if (headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF) return true;
            if (headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47) return true;

            return false;
        }
    }
}
