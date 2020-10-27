using System.Net;
using System.Threading.Tasks;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;
using Licence.Service.SignatureGenerator.Service.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Licence.Service.SignatureGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureService _signatureService;
        private readonly ILogger<SignatureController> _logger;

        public SignatureController(ILogger<SignatureController> logger,ISignatureService signatureService)
        {
            _logger = logger;
            _signatureService = signatureService;

        }

        /// <summary>
        /// Register a new Licence
        /// </summary>
        /// <param name="requestModel">New Payment Detail </param>
        /// <returns>Create Payment Response </returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(RegisterNewLicenceResponseModel))]
        public async Task<IActionResult> Post(RegisterNewLicenceBindingModel requestModel)
        {
            return Ok(await _signatureService.Generate(requestModel));
        }


    }
}
