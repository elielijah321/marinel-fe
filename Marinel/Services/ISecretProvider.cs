using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDraftWebsite.Services
{
    public interface ISecretProvider
    {
        string GetSecret(string key);
    }
}
