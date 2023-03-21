using EasyTalk.Application.Attachments.Commands.CreateAttachment;
using EasyTalk.Application.Attachments.Queries.GetAttachment;
using EasyTalk.Application.Dialogs.Commands.CreateDialog;
using EasyTalk.Application.Dialogs.Commands.DeleteDialog;
using EasyTalk.Application.Dialogs.Queries.GetDialogDetails;
using EasyTalk.Application.Dialogs.Queries.GetDialogsList;
using EasyTalk.Application.Interests.Commands.CreateInterest;
using EasyTalk.Application.Interests.Commands.DeleteInterest;
using EasyTalk.Application.Interests.Commands.UpdateInterest;
using EasyTalk.Application.Interests.Queries.GetInterestsList;
using EasyTalk.Application.Languages.Commands.CreateLanguage;
using EasyTalk.Application.Languages.Commands.DeleteLanguage;
using EasyTalk.Application.Languages.Queries.GetLanguagesList;
using EasyTalk.Application.Messages.Commands.CreateMessage;
using EasyTalk.Application.Messages.Commands.UpdateMessage;
using EasyTalk.Application.Pictures.Commands.AddPicture;
using EasyTalk.Application.Pictures.Commands.DeletePicture;
using EasyTalk.Application.Pictures.Queries.GetPicture;
using EasyTalk.Application.Translator.Commands;
using EasyTalk.Application.Users.Commands.Auth;
using EasyTalk.Application.Users.Commands.DeleteUser;
using EasyTalk.Application.Users.Commands.Registration;
using EasyTalk.Application.Users.Commands.UpdateUser;
using EasyTalk.Application.Users.Queries.GetUserProfile;
using EasyTalk.Application.Users.Queries.GetUsersList;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTalk.Application
{
    public static class ValidationBehaviorDependencyInjection
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(RegistrationCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(DeleteUserCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(AuthCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(UpdateUserCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetUserProfileQueryValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetUsersListQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CreateAttachmentCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetAttachmentQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CreateDialogCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(DeleteDialogCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetDialogDetailsQueryValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetDialogsListQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CreateInterestCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(DeleteInterestCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(UpdateInteresCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(InterestsListValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CreateLanguageCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(DeleteLanguageCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetLanguagesListQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CreateMessageCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(UpdateMessageCommandValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(AddPictureCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(DeletePictureCommandValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(GetPictureQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(TranslateCommandValidator));

            return services;
        }
    }
}
