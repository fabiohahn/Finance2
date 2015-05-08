using App.Helpers;
using Finance;

namespace App.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionTypeDescription { get; set; }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            TransactionType = category.TransactionType;
            TransactionTypeDescription = "";
//             category.TransactionType.DescriptionAttr();
        }
    }
}