using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolDraftWebsite.Data;
using SchoolDraftWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDraftWebsite.Pages
{
    public class SettingsPageModel : PageModel
    {
        private readonly ILogger<SettingsPageModel> _logger;
        private readonly ISchoolRepository _schoolRepository;

        public List<Class> Classes { get; set; }
        public List<Student> Students { get; set; }
        public List<InventoryType> InventoryTypes { get; set; }
        public List<InventoryItem> InventoryItems { get; set; }

        public SettingsPageModel(ISchoolRepository schoolRepo, ILogger<SettingsPageModel> logger)
        {
            _schoolRepository = schoolRepo;
            _logger = logger;
        }

        public void OnGet()
        {
            GetPageData();
        }

        public void OnPost()
        {
            var formType = Request.Form["form-type"].ToString();
            GetPageData();

            switch (formType)
            {
                case "student":
                    AddStudent();
                    break;

                case "class":
                    AddClass();
                    break;

                case "edit-class":
                    UpdateClassItem();
                    break;

                case "inventory-type":
                    AddInventoryType();
                    break;

                case "inventory-item":
                    AddInventory();
                    break;
                case "inventory-item-update":
                    UpdateInventoryItem();
                    break;

                default:
                    break;
            }

            
            GetPageData();
        }

        private void AddStudent()
        {
            var s_Name = Request.Form["studentName"].ToString();
            var s_Class = Request.Form["studentClass"].ToString();

            var studentClass = Classes.FirstOrDefault(c => c.Name == s_Class);

            var student = new Student();
            student.Name = s_Name;
            student.Class = studentClass;

            _schoolRepository.AddStudent(student);
        }

        private void AddClass()
        {
            var c_Name = Request.Form["className"].ToString();
            var pAndCPrice = Request.Form["pAndCPrice"].ToString();

            var parsedPandCPrice = long.Parse(pAndCPrice);

            var _class = new Class();
            _class.Name = c_Name;
            _class.PinkAndCheckUniformPrice = parsedPandCPrice;

            _schoolRepository.AddClass(_class);
        }


        private void AddInventoryType()
        {
            var it_Name = Request.Form["new-inventory-type-input"].ToString();

            var _invType = new InventoryType();
            _invType.Name = it_Name;

            _schoolRepository.AddInventoryType(_invType);
        }

        private void AddInventory()
        {
            var i_Name = Request.Form["item-name-input"].ToString();
            var i_type = Request.Form["item-type-input"].ToString();

            var i_quantity = Request.Form["item-quantity-input"].ToString();
            var i_cost = Request.Form["item-unit-cost-input"].ToString();
            var i_selling_price = Request.Form["item-selling-price-input"].ToString();

            var parsedQuantity = int.Parse(i_quantity);
            var parsedCost = long.Parse(i_cost);
            var parsedSellingPrice = long.Parse(i_selling_price);

            var inventoryType = InventoryTypes.FirstOrDefault(i => i.Name == i_type);

            var inventoryItem = new InventoryItem();
            inventoryItem.Name = i_Name;
            inventoryItem.InventoryType = inventoryType;
            inventoryItem.Quantity = parsedQuantity;
            inventoryItem.CostPerUnit = parsedCost;
            inventoryItem.SellingPrice = parsedSellingPrice;

            _schoolRepository.AddInventoryItem(inventoryItem);
        }

        private void UpdateInventoryItem()
        {

            var formIDString = Request.Form[$"form-id"].ToString();


            var i_Name = Request.Form[$"item-name-input-{formIDString}"].ToString();
            var i_type = Request.Form[$"item-type-input-{formIDString}"].ToString();

            var i_quantity = Request.Form[$"item-quantity-input-{formIDString}"].ToString();
            var i_cost = Request.Form[$"item-unit-cost-input-{formIDString}"].ToString();
            var i_selling_price = Request.Form[$"item-selling-price-input-{formIDString}"].ToString();

            var parsedItemId = int.Parse(formIDString);
            var parsedQuantity = int.Parse(i_quantity);
            var parsedCost = long.Parse(i_cost);
            var parsedSellingPrice = long.Parse(i_selling_price);

            var inventoryType = InventoryTypes.FirstOrDefault(i => i.Name == i_type);

            var inventoryItem = new InventoryItem();
            inventoryItem.Id = parsedItemId;
            inventoryItem.Name = i_Name;
            inventoryItem.InventoryType = inventoryType;
            inventoryItem.Quantity = parsedQuantity;
            inventoryItem.CostPerUnit = parsedCost;
            inventoryItem.SellingPrice = parsedSellingPrice;

            _schoolRepository.UpdateInventoryItem(inventoryItem);

        }


        private void UpdateClassItem()
        {
            var formIDString = Request.Form[$"form-id"].ToString();

            var c_Name = Request.Form[$"className-{formIDString}"].ToString();
            var pAndCPrice = Request.Form[$"pAndCPrice-{formIDString}"].ToString();

            int parsedClassId = int.Parse(formIDString);
            var parsedPandCPrice = long.Parse(pAndCPrice);

            var classItem = new Class();
            classItem.Id = parsedClassId;
            classItem.Name = c_Name;
            classItem.PinkAndCheckUniformPrice = parsedPandCPrice;

            _schoolRepository.UpdateClassItem(classItem);
        }

        private void GetPageData()
        {
            Classes = _schoolRepository.GetAllClasses().ToList();
            Students = _schoolRepository.GetAllStudents().ToList();
            InventoryTypes = _schoolRepository.GetAllInventoryTypes().ToList();
            InventoryItems = _schoolRepository.GetAllInventoryItems().ToList();
        }
    }
}
