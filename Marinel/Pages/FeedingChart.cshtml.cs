using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;
using SchoolDraftWebsite.Data.Enums;
using SchoolDraftWebsite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDraftWebsite.Pages
{
    public class FeedingChartPageModel : PageModel
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ILogger<FeedingChartPageModel> _logger;

        public List<int> Years { get; set; }
        public int CurrentYear { get; set; }

        public Dictionary<int, List<FeedingInfoItem>> FeedingTerms { get; set; }

        public FeedingChartPageModel(ISchoolRepository schoolRepo, ILogger<FeedingChartPageModel> logger)
        {
            _schoolRepository = schoolRepo;
            _logger = logger;

            Years = new List<int>();
            CurrentYear = DateTime.Now.Year;

            FeedingTerms = new Dictionary<int, List<FeedingInfoItem>>();
        }

        public void OnGet()
        {
            GetPageData();
        }

        public void OnPost()
        {
            var formId = Request.Form["form-id"].ToString();

            var feedingDate = Request.Form[$"feeding-date-{formId}"].ToString();
            var totalCollected = Request.Form[$"total-collected-{formId}"].ToString();
            var numOfStudents = Request.Form[$"num-of-students-{formId}"].ToString();

            var parsedDate = DateTime.Parse(feedingDate);
            var parsedTotalotalCollected = long.Parse(totalCollected);
            var parsedFeedingExpenses = Request.Form.Keys.Where(k => k.Contains($"hidden-feeding-expense-{formId}")).Select(key => {

               var expenseValues = Request.Form[key].ToString().Split(",");
               var expenseAmount = long.Parse(expenseValues[0]);
               var expenseReason = expenseValues[1];

                var feedingExpense = new FeedingExpense();
                feedingExpense.ExpenseAmount = expenseAmount;
                feedingExpense.ExpenseReason = expenseReason;

                return feedingExpense;

            }).ToList();

            var totalExpenseAmount = parsedFeedingExpenses.Select(r =>
            {
                return r.ExpenseAmount;

            }).Sum();
            var revenue = parsedTotalotalCollected - totalExpenseAmount;

            var feedingInfoItemModel = new FeedingInfoItem()
            {
                Date = parsedDate,
                TotalCollected = parsedTotalotalCollected,
                NumberOfStudents = Int32.Parse(numOfStudents),
                Revenue = revenue,
                FeedingExpenses = parsedFeedingExpenses
            };


            if (formId.Equals("new"))
            {
                _schoolRepository.AddFeedingInfoItem(feedingInfoItemModel);
            }
            else
            {
                feedingInfoItemModel.Id = Int32.Parse(formId);
                _schoolRepository.UpdateFeedingInfoItem(feedingInfoItemModel);
            }

            GetPageData();
        }

        private void GetPageData()
        {
            var allFeedingInfoItems = _schoolRepository.GetAllFeedingItems().ToList();
            allFeedingInfoItems = TermTimeHelper.CalculateTerm(allFeedingInfoItems);

            var groupedFeedingItems = allFeedingInfoItems.GroupBy(t => t.Date.Year);

            foreach (var item in groupedFeedingItems)
            {
                var year = item.Key;
                Years.Add(year);
                FeedingTerms.Add(year, item.Select(fi => fi).ToList());
            }
        }
    }
}
