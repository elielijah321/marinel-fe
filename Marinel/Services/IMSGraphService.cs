using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Marinel.Services
{
    public interface IMSGraphService
    {
        Task<List<User>> GetAllUsers();
        Task<User> AddUser(User newUser);
        Task<User> UpdateUser(string id, User user);
        Task DeleteUser(string id);
    }
}

