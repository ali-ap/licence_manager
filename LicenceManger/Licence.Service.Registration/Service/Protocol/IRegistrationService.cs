using System.Threading.Tasks;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;

namespace Licence.Service.Registration.Service.Protocol
{
    public interface IRegistrationService
    {
        Task<RegisterNewLicenceResponseModel> Register(RegisterNewLicenceBindingModel model);
    }
}

