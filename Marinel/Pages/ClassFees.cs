using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using SchoolDraftWebsite.Helpers;


namespace SchoolDraftWebsite.Pages
{
    public class ClassFeesModel : PageModel
    {
        private readonly ILogger<ClassFeesModel> _logger;
        private readonly ISchoolRepository _schoolRepository;

        public List<int> Years { get; set; }
        public int CurrentYear { get; set; }

        public Dictionary<int, List<ClassFeeInfoItem>> ClassFeeTerms { get; set; }

        public ClassFeesModel(ILogger<ClassFeesModel> logger, ISchoolRepository schoolRepo)
        {
            _logger = logger;
            _schoolRepository = schoolRepo;

            Years = new List<int>();
            CurrentYear = DateTime.Now.Year;

            ClassFeeTerms = new Dictionary<int, List<ClassFeeInfoItem>>();
        }

        public void OnGet()
        {
            GetPageData();
        }

        public void OnPost()
        {
            var formId = Request.Form["form-id"].ToString();

            var classFeeDate = Request.Form[$"class-fee-date-{formId}"].ToString();
            var enrolment = Request.Form[$"enrolment-{formId}"].ToString();
            var numPaid = Request.Form[$"num-paid-{formId}"].ToString();
            var amountReceived = Request.Form[$"amount-received-{formId}"].ToString();

            var parsedDate = DateTime.Parse(classFeeDate);

            var classFeeInfoItemModel = new ClassFeeInfoItem()
            {
                Date = parsedDate,
                NumberEnrolled = Int32.Parse(enrolment),
                NumberPaid = Int32.Parse(numPaid),
                AmountReceived = long.Parse(amountReceived)
            };

            if (formId.Equals("new"))
            {
                _schoolRepository.AddClassFeeInfoItem(classFeeInfoItemModel);
            }
            else
            {
                classFeeInfoItemModel.Id = Int32.Parse(formId);
                _schoolRepository.UpdateClassFeeInfoItem(classFeeInfoItemModel);
            }

            GetPageData();
        }

        private void GetPageData()
        {
            var allClassInfoItems = _schoolRepository.GetAllClasseFeeInfoItems().ToList();

            allClassInfoItems = TermTimeHelper.CalculateTerm(allClassInfoItems);

            var groupedClassFeeItems = allClassInfoItems.GroupBy(t => t.Date.Year);

            foreach (var item in groupedClassFeeItems)
            {
                var year = item.Key;
                Years.Add(year);
                ClassFeeTerms.Add(year, item.Select(fi => fi).ToList());
            }
        }
    }
}
