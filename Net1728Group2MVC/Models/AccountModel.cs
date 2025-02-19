using DAL.Entities;

namespace Net1728Group2MVC.Models
{
    public class AccountModel
    {
        public short? AccountId { get; set; }

        public string? AccountName { get; set; }

        public string? AccountEmail { get; set; }

        public int? AccountRole { get; set; }

        public string? AccountPassword { get; set; }

    }
}
