using FluentValidation;
using Licence.Service.Common.Model.Request;

namespace Licence.Service.Common.Validation
{
    public class RegisterNewLicenceBindingModelValidator : AbstractValidator<RegisterNewLicenceBindingModel>
    {
        public RegisterNewLicenceBindingModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.ContactPerson).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.LicenceKey).NotNull().NotEmpty();
        }
    }

}
