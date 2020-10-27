using System.Threading.Tasks;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;

namespace Licence.Service.SignatureGenerator.Service.Protocol
{
    public interface ISignatureService
    {
        Task<RegisterNewLicenceResponseModel> Generate(RegisterNewLicenceBindingModel model);
    }
}

