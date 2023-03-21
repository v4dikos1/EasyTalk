using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileVm>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserProfileQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserProfileVm> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var userVm = _mapper.Map<UserProfileVm>(user);

            return userVm;
        }
    }
}
