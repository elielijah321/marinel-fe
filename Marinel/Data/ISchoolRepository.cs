using SchoolDraftWebsite.Data.Entities;
using System.Collections.Generic;

namespace SchoolDraftWebsite.Data
{
    public interface ISchoolRepository
    {
        string GetKey();
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Class> GetAllClasses();
        IEnumerable<FeedingInfoItem> GetAllFeedingItems();
        IEnumerable<ClassFeeInfoItem> GetAllClasseFeeInfoItems();
        IEnumerable<InventoryType> GetAllInventoryTypes();
        IEnumerable<InventoryItem> GetAllInventoryItems();
        IEnumerable<BookSale> GetAllBookSales();
        IEnumerable<UniformSale> GetAllUniformSales();
        IEnumerable<PandCUniformSale> GetAllPandCUniformSales();

        void AddStudent(Student student);
        void AddClass(Class newClass);
        void AddFeedingInfoItem(FeedingInfoItem newFeedingInfoItem);
        void AddInventoryType(InventoryType newInventoryType);
        void AddInventoryItem(InventoryItem newInventoryItem);
        void AddClassFeeInfoItem(ClassFeeInfoItem newClassFeeInfoItem);
        void AddBookSale(BookSale newBookSale);
        void AddUniformSale(UniformSale newUniformSale);
        void AddPandCUniformSale(PandCUniformSale newPandCUniformSale);

        void UpdateFeedingInfoItem(FeedingInfoItem feedingInfoItem);
        void UpdateClassFeeInfoItem(ClassFeeInfoItem classFeeInfoItem);
        void UpdateInventoryItem(InventoryItem inventoryItem);
        void UpdateClassItem(Class classItem);
        void UpdateBookSale(BookSale bookSale);
        void UpdateUniformSale(UniformSale uniformSale);
        void UpdatePandCUniformSale(PandCUniformSale pAndCUniformSale);
    }
}