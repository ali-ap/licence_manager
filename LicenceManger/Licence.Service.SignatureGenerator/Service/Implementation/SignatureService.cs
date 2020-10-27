using System;
using System.Threading.Tasks;
using AutoMapper;
using Licence.Service.Common.Common.Behavior;
using Licence.Service.Common.Common.Enum;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;
using Licence.Service.SignatureGenerator.Service.Protocol;
using Microsoft.Extensions.Caching.Memory;

namespace Licence.Service.SignatureGenerator.Service.Implementation
{
    public class SignatureService : ISignatureService
    {

        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public SignatureService(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        [BindingModelValidation]
        public Task<RegisterNewLicenceResponseModel> Generate(RegisterNewLicenceBindingModel model)
        {
            var domain = _mapper.Map<Common.Domain.Licence>(model);
            if (_cache.TryGetValue((domain.Email, domain.Key), out Common.Domain.Licence item))
                return Task.FromResult(new RegisterNewLicenceResponseModel()
                { Licence = "", Message = "Already Registered", IsSuccessful = false });

            _cache.Set((domain.Email, domain.Key), domain);
            return Task.FromResult(new RegisterNewLicenceResponseModel()
            { Licence = domain.Value, Message = "Licence Successfully Generated", IsSuccessful = true });
        }
    }
}