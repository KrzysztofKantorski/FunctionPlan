using Application.Exceptions;
using Domain.Common;
using Domain.Meetings;
using MediatR;

namespace Application.Meetings.Commands.ChangeCoordinates
{
    internal sealed class ChangeCoordinatesCommandHandler: IRequestHandler<ChangeCoordinatesCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeCoordinatesCommandHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangeCoordinatesCommand request, CancellationToken cancellationToken)
        {
            //What we need
            //Check if meeting exists
            //Check if user is Organizer
            //Create coordinates object (validated in constructor)
            //Call method, save changes
            
            var meetingToUpdate = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

            if (meetingToUpdate == null) 
            {
                throw new MeetingNotFoundException("Meeting does not exist");
            }

            if(meetingToUpdate.OrganizerId != request.OrganizerId)
            {
                throw new UserWithoutPermisionException("You dont have permision to update meeting");
            }

            var coordinates = new Coordinates(
                request.Latitude,
                request.Longitude
            );

            meetingToUpdate.ChangeLocation( coordinates );

            _meetingRepository.Update(meetingToUpdate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
