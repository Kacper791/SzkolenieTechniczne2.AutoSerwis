using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Dtos;

namespace AutoSerwis.Mvc.UI.Models
{
    public class ClientDetailsViewModel
    {
        public ClientDetailsDto Client { get; set; } = default!;
        public RegisterRepairOrderViewModel RepairOrderModel { get; set; } = new();
    }
}