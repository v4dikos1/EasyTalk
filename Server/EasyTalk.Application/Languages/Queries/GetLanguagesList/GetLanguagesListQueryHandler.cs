using AutoMapper;
using EasyTalk.Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class GetLanguagesListQueryHandler : IRequestHandler<GetLanguagesListQuery, LanguageListVm>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageRepository _languageRepository;
        private readonly IValidator<GetLanguagesListQuery> _validator;

        public GetLanguagesListQueryHandler(IMapper mapper, ILanguageRepository languageRepository,
            IValidator<GetLanguagesListQuery> validator)
        {
            _mapper = mapper;
            _languageRepository = languageRepository;
            _validator = validator;
        }

        public async Task<LanguageListVm> Handle(GetLanguagesListQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var languages = await _languageRepository.GetLanguages(request.Offset, request.Limit, cancellationToken);

            return new LanguageListVm { Languages = _mapper.Map<List<LanguageLookupDto>>(languages) };
        }
    }
}
