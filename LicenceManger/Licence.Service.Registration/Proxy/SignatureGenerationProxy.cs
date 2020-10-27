using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Consul;
using IdentityModel.Client;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Licence.Service.Registration.Proxy
{
    public class SignatureGenerationProxy
    {
        private readonly AgentService _oAthAgent;
        private readonly AgentService _licenceGeneratorAgent;
        public readonly TokenResponse _token;

        public SignatureGenerationProxy()
        {
            (_oAthAgent, _licenceGeneratorAgent) =  GetAddressesFromGateway();
            _token =  GetAuthenticationToken();

        }

        private (AgentService identity, AgentService generator) GetAddressesFromGateway()
        {
            var consulClient = new ConsulClient(c => c.Address = new Uri(System.Environment.GetEnvironmentVariable("CONSUL_ADDRESS")));
            var services = consulClient.Agent.Services().Result.Response;
            var identity = services.First(x => x.Value.Service == "Identity");
            var generator = services.First(x => x.Value.Service == "Generator");
            return (identity.Value, generator.Value);
        }

        private TokenResponse GetAuthenticationToken()
        {
            var client = new HttpClient();
            var disco =  client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                //Address = Environment.GetEnvironmentVariable("Identity"),
                Address = $"http://{_oAthAgent.Address}:{_oAthAgent.Port}",
                Policy =
                {
                    RequireHttps = false,
                    ValidateIssuerName = false,
                    ValidateEndpoints = false,
                }
            }).Result;
            if (disco.IsError) throw new Exception(disco.Error);

            var tokenResponse =  client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            }).Result;

            if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);
            return tokenResponse;

        }

        public async Task<RegisterNewLicenceResponseModel> GenerateSignature(RegisterNewLicenceBindingModel model)
        {
            var client = new HttpClient();
            client.SetBearerToken(_token.AccessToken);
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"http://{_licenceGeneratorAgent.Address}:{_licenceGeneratorAgent.Port}/Signature", content);
            var result = JsonConvert.DeserializeObject<RegisterNewLicenceResponseModel>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
