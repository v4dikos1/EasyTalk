using AutoMapper;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using MediatR;

namespace EasyTalk.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListVm>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUsersListQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UsersListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var interestsFilter = request.InterestsFilter;
            var targetLanguagesFilter = request.TargetLanguagesFilter;

            var users = await _userRepository.GetUsers(request.Limit, request.Offset,
                request.InterestsFilter, request.NativeLanguagesFilter, request.TargetLanguagesFilter, cancellationToken);

            return new UsersListVm { Users = _mapper.Map<List<UserProfileVm>>(users) };
        }
    }
}
