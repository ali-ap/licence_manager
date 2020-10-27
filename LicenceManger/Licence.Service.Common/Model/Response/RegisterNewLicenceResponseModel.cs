using Licence.Service.Common.Common.Enum;

namespace Licence.Service.Common.Model.Response
{
    public class RegisterNewLicenceResponseModel
    {
        public string Licence { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
