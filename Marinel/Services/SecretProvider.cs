using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;

namespace SchoolDraftWebsite.Services
{
    public class SecretProvider : ISecretProvider
    {
        private readonly IConfiguration _config;
        private readonly SecretClient _secretClient;

        public SecretProvider(IConfiguration config)
        {
            _config = config;

            string kvUrl = _config.GetValue<string>("KeyVaultConfig:KVUrl");
            string tenantId = _config.GetValue<string>("KeyVaultConfig:TenantId");
            string clientId = _config.GetValue<string>("KeyVaultConfig:ClientId");
            string clientSecret = _config.GetValue<string>("KeyVaultConfig:ClientSecret");

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            _secretClient = new SecretClient(new Uri(kvUrl), credential);
        }

        public string GetSecret(string key)
        {
            return _secretClient.GetSecretAsync(key).Result.Value.Value;
        }
    }
}
