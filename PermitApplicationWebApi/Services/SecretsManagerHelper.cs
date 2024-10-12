using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace PermitApplicationWebApi.Services
{
    public class SecretsManagerHelper
    {
        private IAmazonSecretsManager _secretsManager;

        public SecretsManagerHelper()
        {
            string region = "ap-southeast-1";
            _secretsManager = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
        }

        public async Task<Dictionary<string, string>> GetSecretAsync()
        {
            string secretName = "dev/cpas/rdsConnection";

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await _secretsManager.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw e;
            }

            if (response.SecretString != null)
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString)!;
            }

            return new Dictionary<string, string>();
        }
    }
}
