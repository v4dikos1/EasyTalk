using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Interests.Commands.CreateInterest;

namespace EasyTalk.WebApi.Models
{
    public class CreateInterestDto : IMapWith<CreateInterestCommand>
    {
        public string Name { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateInterestDto, CreateInterestCommand>();
        }
    }
}
