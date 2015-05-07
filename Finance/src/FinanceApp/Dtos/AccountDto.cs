using Finance;

namespace App.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AccountDto(){}

        public AccountDto(Account account)
        {
            Id = account.Id;
            Name = account.Name;
        }
    }
}