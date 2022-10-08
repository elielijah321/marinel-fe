using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Principal;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Microsoft.Graph;

namespace SchoolDraftWebsite.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly ILogger<SignOutModel> _logger;

        public SignOutModel(ILogger<SignOutModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("OpenIdConnect");

            return Redirect("/");
        }
    }
}
