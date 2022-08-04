using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;

namespace SchoolDraftWebsite.Pages
{

    public class UniformPageData
    {
        public List<Class> Classes { get; set; }
        public List<InventoryItem> UniformTypes { get; set; }
        public List<Tuple<InventoryItem, List<UniformSale>>> UniformListSales { get; set; }

        public List<PandCUniformSale> PandCUniformSales { get; set; }

        public UniformPageData()
        {
            Classes = new List<Class>();
            UniformTypes = new List<InventoryItem>();
            UniformListSales = new List<Tuple<InventoryItem, List<UniformSale>>>();
            PandCUniformSales = new List<PandCUniformSale>();
        }

    }

    public class UniformPageModel : PageModel
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ILogger<UniformPageModel> _logger;

        public UniformPageData UniformPageData { get; set; }

        public UniformPageModel(ISchoolRepository schoolRepo, ILogger<UniformPageModel> logger)
        {
            _schoolRepository = schoolRepo;
            _logger = logger;

            UniformPageData = new UniformPageData();
        }

        public void OnGet()
        {
             GetPageData();
        }

        private void GetPageData()
        {
            UniformPageData.Classes = _schoolRepository.GetAllClasses().ToList();
            UniformPageData.UniformTypes = _schoolRepository.GetAllInventoryItems().Where(i => i.InventoryType.Name == "Uniform").ToList();
            
            var uniformSales = _schoolRepository.GetAllUniformSales();

            foreach (var uniformType in UniformPageData.UniformTypes)
            {
                var typeSold = uniformSales.Where(us => us.InventoryItemId == uniformType.Id).ToList();
                var typeAndSales = new Tuple<InventoryItem, List<UniformSale>>(uniformType, typeSold);

                UniformPageData.UniformListSales.Add(typeAndSales);
            }

            UniformPageData.PandCUniformSales = _schoolRepository.GetAllPandCUniformSales().ToList();
        }

        public JsonResult OnGetStudentsByClassId(int classId)
        {
            GetPageData();

            var students = UniformPageData.Classes.FirstOrDefault(c => c.Id == classId).Students.Select(s => {
                return new { Id = s.Id, Name = s.Name };
            });


            return new JsonResult(students);
        }

        public void OnPost()
        {
            var formType = Request.Form["form-type"].ToString();


            switch (formType)
            {
                case "new-uniform-sale":
                    AddNewUniformSale();
                    break;
                case "edit-uniform-sale":
                    UpdateUniformSale();
                    break;
                case "new-p-and-c-sale":
                    AddNewPandCSale();
                    break;
                case "edit-p-and-c-sale":
                    UpdatePandCSale();
                    break;


                default:
                    break;
            }



            GetPageData();
        }

        private void AddNewUniformSale()
        {
            var u_Date = Request.Form["new-uniform-sale-date"].ToString();
            var u_Type = Request.Form["new-uniform-type"].ToString();
            var u_Quantity = Request.Form["new-uniform-quantity"].ToString();
            var u_StudentId = Request.Form["new-uniform-student"].ToString();
            var u_Received = Request.Form["new-uniform-received-options"].ToString();
            var u_Paid = Request.Form["new-uniform-paid-options"].ToString();

            var parsedDate = DateTime.Parse(u_Date);
            var parsedInventoryItemId = int.Parse(u_Type);
            var parsedQuantity = int.Parse(u_Quantity);
            var parsedStudentId = int.Parse(u_StudentId);

            var uniformSale = new UniformSale();
            uniformSale.Date = parsedDate;
            uniformSale.StockDate = parsedDate;
            uniformSale.Quantity = parsedQuantity;
            uniformSale.InventoryItemId = parsedInventoryItemId;
            uniformSale.StudentId = parsedStudentId;
            uniformSale.PaidInFull = u_Paid == "y";
            uniformSale.Received = u_Received == "y";

            _schoolRepository.AddUniformSale(uniformSale);
        }

        private void UpdateUniformSale()
        {
            var formId = Request.Form["form-id"].ToString();

            var u_Date = Request.Form[$"edit-uniform-sale-date-{formId}"].ToString();
            var u_Type = Request.Form[$"edit-uniform-type-{formId}"].ToString();
            var u_Quantity = Request.Form[$"edit-uniform-quantity-{formId}"].ToString();
            var u_StudentId = Request.Form[$"edit-uniform-student-{formId}"].ToString();
            var u_UniformReceived = Request.Form[$"edit-uniform-received-options-{formId}"].ToString();
            var u_UniformPaid = Request.Form[$"edit-uniform-paid-options-{formId}"].ToString();

            var parsedId = int.Parse(formId);
            var parsedDate = DateTime.Parse(u_Date);
            var parsedInventoryItemId = int.Parse(u_Type);
            var parsedQuantity = int.Parse(u_Quantity);
            var parsedStudentId = int.Parse(u_StudentId);

            var uniformSale = new UniformSale();
            uniformSale.Id = parsedId;
            uniformSale.Date = parsedDate;
            uniformSale.StockDate = parsedDate;
            uniformSale.Quantity = parsedQuantity;
            uniformSale.InventoryItemId = parsedInventoryItemId;
            uniformSale.StudentId = parsedStudentId;
            uniformSale.PaidInFull = u_UniformPaid == "y";
            uniformSale.Received = u_UniformReceived == "y";

            _schoolRepository.UpdateUniformSale(uniformSale);
        }

        private void AddNewPandCSale()
        {
            var paid_Date = Request.Form["new-p-and-c-sale-date-paid"].ToString();
            var received_Date = Request.Form["new-p-and-c-sale-date-received"].ToString();
            var check_Quantity = Request.Form["new-p-and-c-check-yards-quantity"].ToString();
            var pink_Quantity = Request.Form["new-p-and-c-pink-yards-quantity"].ToString();
            var student_Id = Request.Form["new-p-and-c-student"].ToString();
            var u_Received = Request.Form["new-p-and-c-received-options"].ToString();
            var u_Paid = Request.Form["new-p-and-c-paid-options"].ToString();
            var seamstress_Paid = Request.Form["new-p-and-c-seamstress-paid-options"].ToString();

            var parsedPaidDate = DateTime.Parse(paid_Date);
            var parsedReceivedDate = DateTime.Parse(received_Date);
            var parsedCheckQuantity = int.Parse(check_Quantity);
            var parsedPinkQuantity = int.Parse(pink_Quantity);
            var parsedStudentId = int.Parse(student_Id);
            var paid = u_Paid == "y";
            var received = u_Received == "y";
            var seamstressPaid = seamstress_Paid == "y";

            var pAndCUniformSale = new PandCUniformSale();
            pAndCUniformSale.DatePaid = parsedPaidDate;
            pAndCUniformSale.ReceivedDate = parsedReceivedDate;
            pAndCUniformSale.CheckYardsQuantity = parsedCheckQuantity;
            pAndCUniformSale.PinkYardsQuantity = parsedPinkQuantity;
            pAndCUniformSale.StudentId = parsedStudentId;
            pAndCUniformSale.PaidInFull = paid;
            pAndCUniformSale.Received = received;
            pAndCUniformSale.SeamstressPaid = seamstressPaid;

            _schoolRepository.AddPandCUniformSale(pAndCUniformSale);
        }

        private void UpdatePandCSale()
        {
            var formId = Request.Form["form-id"].ToString();

            var paid_Date = Request.Form[$"edit-p-and-c-sale-date-paid-{formId}"].ToString();
            var received_Date = Request.Form[$"edit-p-and-c-sale-date-received-{formId}"].ToString();
            var check_Quantity = Request.Form[$"edit-p-and-c-check-yards-quantity-{formId}"].ToString();
            var pink_Quantity = Request.Form[$"edit-p-and-c-pink-yards-quantity-{formId}"].ToString();
            var student_Id = Request.Form[$"edit-p-and-c-student-{formId}"].ToString();
            var u_Received = Request.Form[$"edit-p-and-c-received-options-{formId}"].ToString();
            var u_Paid = Request.Form[$"edit-p-and-c-paid-options-{formId}"].ToString();
            var seamstress_Paid = Request.Form[$"edit-p-and-c-seamstress-paid-options-{formId}"].ToString();

            var parsedId = int.Parse(formId);
            var parsedPaidDate = DateTime.Parse(paid_Date);
            var parsedReceivedDate = DateTime.Parse(received_Date);
            var parsedCheckQuantity = int.Parse(check_Quantity);
            var parsedPinkQuantity = int.Parse(pink_Quantity);
            var parsedStudentId = int.Parse(student_Id);
            var paid = u_Paid == "y";
            var received = u_Received == "y";
            var seamstressPaid = seamstress_Paid == "y";

            var pAndCUniformSale = new PandCUniformSale();
            pAndCUniformSale.Id = parsedId;
            pAndCUniformSale.DatePaid = parsedPaidDate;
            pAndCUniformSale.ReceivedDate = parsedReceivedDate;
            pAndCUniformSale.CheckYardsQuantity = parsedCheckQuantity;
            pAndCUniformSale.PinkYardsQuantity = parsedPinkQuantity;
            pAndCUniformSale.StudentId = parsedStudentId;
            pAndCUniformSale.PaidInFull = paid;
            pAndCUniformSale.Received = received;
            pAndCUniformSale.SeamstressPaid = seamstressPaid;

            _schoolRepository.UpdatePandCUniformSale(pAndCUniformSale);
        }
    }
}
