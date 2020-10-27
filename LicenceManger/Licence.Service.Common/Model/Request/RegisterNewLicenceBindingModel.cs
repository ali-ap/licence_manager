using Licence.Service.Common.Common.Behavior;
using Licence.Service.Common.Validation;

namespace Licence.Service.Common.Model.Request
{
    public class RegisterNewLicenceBindingModel : BindingModel<RegisterNewLicenceBindingModel, RegisterNewLicenceBindingModelValidator>
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string LicenceKey { get; set; }



    }
}
