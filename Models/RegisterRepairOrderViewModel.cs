using System;
using System.ComponentModel.DataAnnotations;

namespace AutoSerwis.Mvc.UI.Models
{
    public class RegisterRepairOrderViewModel
    {
        public long ClientId { get; set; }

        [Required(ErrorMessage = "Data zlecenia naprawy jest wymagana.")]
        [Display(Name = "Data i godzina zlecenia")]
        public DateTime RepairOrderDate { get; set; } = DateTime.Now.AddHours(1);
    }
}