using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;

namespace SchoolDraftWebsite.Pages
{
    public class BookChartPageModel : PageModel
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ILogger<BookChartPageModel> _logger;
        public List<Tuple<InventoryItem, List<BookSale>>> BookListSales { get; set; }

        public BookChartPageModel(ISchoolRepository schoolRepo, ILogger<BookChartPageModel> logger)
        {
            _schoolRepository = schoolRepo;
            _logger = logger;

            BookListSales = new List<Tuple<InventoryItem, List<BookSale>>>();
        }

        public void OnGet()
        {
            GetPageData();
        }

        public void OnPost()
        {
             var formType = Request.Form["form-type"].ToString();


            switch (formType)
            {
                case "new-book-sale":
                    AddNewBookSale();
                    break;
                case "edit-book-sale":
                    UpdateBookSale();
                    break;


                default:
                    break;
            }



            GetPageData();
        }

        private void GetPageData()
        {
            var bookTypes = _schoolRepository.GetAllInventoryItems().Where(i => i.InventoryType.Name == "Book").ToList();
            var bookSales = _schoolRepository.GetAllBookSales();

            foreach (var bookType in bookTypes)
            {
                var typeSold = bookSales.Where(bs => bs.InventoryItemId == bookType.Id).ToList();
                var typeAndSales = new Tuple<InventoryItem, List<BookSale>>(bookType, typeSold);

                BookListSales.Add(typeAndSales);
            }

        }


        private void AddNewBookSale()
        {
            var b_Date = Request.Form["new-book-sale-date"].ToString();
            var b_Type = Request.Form["new-book-sale-type"].ToString();
            var b_Sold = Request.Form["new-book-sale-number-sold"].ToString();

            var parsedDate = DateTime.Parse(b_Date);
            var parsedInventoryItemId = int.Parse(b_Type);
            var parsedNoOfBooks = int.Parse(b_Sold);

            var bookSale = new BookSale();
            bookSale.Date = parsedDate;
            bookSale.InventoryItemId = parsedInventoryItemId;
            bookSale.NoOfBooksSold = parsedNoOfBooks;

            _schoolRepository.AddBookSale(bookSale);
        }


        private void UpdateBookSale()
        {
            var formId = Request.Form["form-id"].ToString();

            var b_Date = Request.Form[$"edit-book-sale-date-{formId}"].ToString();
            var b_Type = Request.Form[$"edit-book-sale-type-{formId}"].ToString();
            var b_Sold = Request.Form[$"edit-book-sale-number-sold-{formId}"].ToString();


            var pasedId = int.Parse(formId);
            var parsedDate = DateTime.Parse(b_Date);
            var parsedInventoryItemId = int.Parse(b_Type);
            var parsedNoOfBooks = int.Parse(b_Sold);

            var bookSale = new BookSale();
            bookSale.Id = pasedId;
            bookSale.Date = parsedDate;
            bookSale.InventoryItemId = parsedInventoryItemId;
            bookSale.NoOfBooksSold = parsedNoOfBooks;

            _schoolRepository.UpdateBookSale(bookSale);
        }
    }
}
