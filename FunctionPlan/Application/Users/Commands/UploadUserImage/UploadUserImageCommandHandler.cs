using Application.Abstractions.Storage;
using Application.Exceptions;
using Domain.Common;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Users.Commands.UploadUserImage
{
    internal sealed class UploadUserImageCommandHandler : IRequestHandler<UploadUserImageCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;

        public UploadUserImageCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IBlobService blobService
            , IOptions<BlobSettings> blobOptions)
        {
            _userRepository = userRepository;
            _blobService = blobService;
            _unitOfWork = unitOfWork;
            _blobSettings = blobOptions.Value;

        }

        public async Task Handle(UploadUserImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null) 
            {
                throw new UserNotFoundException("User not found");
            }

            //Generate GUID for filename
            var fileId = Guid.NewGuid();


            //Check if user alerdy uploaded image
            if (!string.IsNullOrWhiteSpace(user.ProfilePictureUrl)) 
            {
                if (Guid.TryParse(user.ProfilePictureUrl, out var oldPictureId)) 
                {
                    await _blobService.DeleteFileAsync(
                        _blobSettings.AvatarsContainerName, 
                        oldPictureId, 
                        cancellationToken
                    );
                }
            }


            //Upload image
            await _blobService.UploadFileAsync(
                _blobSettings.AvatarsContainerName, 
                fileId, 
                request.UploadedImage.Stream, 
                request.UploadedImage.ContentType, 
                cancellationToken
            );


            //Update metadata
            user.SetUserImage(fileId.ToString());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
