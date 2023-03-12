using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileVm>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserProfileVm> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include("Interests")
                .Include("NativeLanguage")
                .Include("TargetLanguages")
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var userVm = _mapper.Map<UserProfileVm>(user);

            return userVm;
        }
    }
}
