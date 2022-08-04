using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDraftWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISchoolRepository _schoolRepository;

        public List<Student> Students { get; set; }
        public string Env { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ISchoolRepository schoolRepo)
        {
            _logger = logger;
            _schoolRepository = schoolRepo;
        }

        public void OnGet()
        {
            Students = _schoolRepository.GetAllStudents().ToList();
            Env = _schoolRepository.GetKey();
        }
    }
}
