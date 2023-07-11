using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
        private readonly DataContext context;
        private readonly IPhotoAccessor photoAccesor;
        private readonly IUserAccessor userAccesor;
            public Handler(DataContext context, IPhotoAccessor photoAccesor, IUserAccessor userAccesor)
            {
            this.userAccesor = userAccesor;
            this.photoAccesor = photoAccesor;
            this.context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await this.context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.UserName == this.userAccesor.GetUsername());

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if(photo == null) return null;

                if(photo.IsMain) return Result<Unit>.Failure("You can't delete your photo");

                var result = await this.photoAccesor.DeletePhoto(photo.Id);

                if(result == null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");

                user.Photos.Remove(photo);

                var success = await this.context.SaveChangesAsync() > 0 ;

                if(success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem deleting photo from API");
            }
        }
    }
}