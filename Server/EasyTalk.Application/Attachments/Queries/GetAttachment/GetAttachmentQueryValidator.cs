﻿using FluentValidation;

namespace EasyTalk.Application.Attachments.Queries.GetAttachment
{
    public class GetAttachmentQueryValidator : AbstractValidator<GetAttachmentQuery>
    {
        public GetAttachmentQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().WithMessage("Code field is required!");
        }
    }
}
