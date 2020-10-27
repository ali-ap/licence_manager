using System.Threading.Tasks;
using AutoMapper;
using Licence.Service.Common.Common.Behavior;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;
using Licence.Service.Registration.Proxy;
using Licence.Service.Registration.Service.Protocol;

namespace Licence.Service.Registration.Service.Implementation
{
    public class RegistrationService : IRegistrationService
    {

        private readonly IMapper _mapper;
        private readonly SignatureGenerationProxy _proxy;

        public RegistrationService(IMapper mapper, SignatureGenerationProxy proxy)
        {
            _mapper = mapper;
            _proxy = proxy;
        }

        [BindingModelValidation]
        async Task<RegisterNewLicenceResponseModel> IRegistrationService.Register(RegisterNewLicenceBindingModel model)
        {
          
            return await _proxy.GenerateSignature(model);
        }
    }
}