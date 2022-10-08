using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Marinel.Services
{
    public class MSGraphService : IMSGraphService
    {
        private GraphServiceClient _graphServiceClient { get; set; }
        private const string PrincipalUserId = "7c90a817-c8bc-48ad-9696-1606f587c8ec";
        private const string Domain = "@marineldev.onmicrosoft.com";

        public IConfiguration _configuration { get; }


        public MSGraphService(IConfiguration configuration)
        {

            _configuration = configuration;

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = Environment.GetEnvironmentVariable("TenantId") ?? _configuration["TenantId"];

            // Values from app registration
            var clientId = Environment.GetEnvironmentVariable("ClientId") ?? _configuration["ClientId"];
            var clientSecret = Environment.GetEnvironmentVariable("B2CClientSecret") ?? _configuration["B2CClientSecret"];

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            _graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _graphServiceClient.Users
               .Request()
               .GetAsync();

            return users.Where(u => u.Id != PrincipalUserId).Select(u =>
            {
                var username = u.UserPrincipalName.Split('@')[0];
                u.UserPrincipalName = username;

                return u;
            })
            .ToList();
        }

        public async Task<User> AddUser(User newUser)
        {
            newUser.MailNickname = newUser.UserPrincipalName;
            newUser.AccountEnabled = true;
            newUser.UserPrincipalName = newUser.UserPrincipalName + Domain;

            var createdUser = await _graphServiceClient.Users
                .Request()
                .AddAsync(newUser);

            return createdUser;
        }

        public async Task<User> UpdateUser(string id, User user)
        {
            user.UserPrincipalName = user.UserPrincipalName + Domain;

            var updatedUser = await _graphServiceClient.Users[id]
                .Request()
                .UpdateAsync(user);

            return updatedUser;
        }

        public async Task DeleteUser(string id)
        {
            await _graphServiceClient.Users[id]
                .Request()
                .DeleteAsync();
        }
    }
}

