using System.Net;
using System.Threading.Tasks;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;
using Licence.Service.Registration.Service.Protocol;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Licence.Service.Registration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenceController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<LicenceController> _logger;

        public LicenceController(ILogger<LicenceController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;

        }

        /// <summary>
        /// Register a new Licence
        /// </summary>
        /// <param name="requestModel">Register New Licence Binding Model </param>
        /// <returns>Register New Licence Response Model </returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(RegisterNewLicenceResponseModel))]
        public async Task<IActionResult> Post([FromBody] RegisterNewLicenceBindingModel requestModel)
        {
            return Ok(await _registrationService.Register(requestModel));
        }


    }
}
