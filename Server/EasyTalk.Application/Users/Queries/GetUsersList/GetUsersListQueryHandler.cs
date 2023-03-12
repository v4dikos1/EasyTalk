using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListVm>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UsersListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var interestsFilter = request.InterestsFilter;
            var targetLanguagesFilter = request.TargetLanguagesFilter;

            var users = await _dbContext.Users
                .Include("Interests")
                .Include("NativeLanguage")
                .Include("TargetLanguages")
                .ProjectTo<UserProfileVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (request.NativeLanguagesFilter != null)
            {
                users = users.Where(u => request.NativeLanguagesFilter.Contains(u.NativeLanguage.Id)).ToList();
            }

            if (request.InterestsFilter != null)
            {
                users = users.Where(u => request.InterestsFilter.All(i => u.Interests.ConvertAll(x => x.Id).Contains(i)))
                    .ToList();
            }

            if (request.TargetLanguagesFilter != null)
            {
                users = users
                    .Where(u => request.TargetLanguagesFilter
                        .All(i => u.TargetLanguages
                            .ConvertAll(x => x.Id)
                            .Contains(i)))
                    .ToList();
            }

            users = users
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();

            return new UsersListVm { Users = users };
        }
    }
}
