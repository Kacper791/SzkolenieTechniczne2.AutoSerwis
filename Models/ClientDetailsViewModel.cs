using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Dtos;

namespace AutoSerwis.Mvc.UI.Models
{
    public class CreateClientViewModel
    {
        [Required(ErrorMessage = "Nazwa klienta jest wymagana.")]
        [StringLength(256, MinimumLength = 1)]
        [Display(Name = "Nazwa klienta")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rok rejestracji jest wymagany.")]
        [Range(1888, 2100, ErrorMessage = "Rok musi być między 1888 a 2100.")]
        [Display(Name = "Rok rejestracji")]
        public int RegistrationYear { get; set; }

        [Required(ErrorMessage = "Czas trwania usługi jest wymagany.")]
        [Range(1, 600, ErrorMessage = "Czas musi być między 1 a 600 minut.")]
        [Display(Name = "Czas trwania usługi (min)")]
        public int ServiceDuration { get; set; }

        [Required(ErrorMessage = "Wybierz kategorię.")]
        [Display(Name = "Kategoria klienta")]
        public long ClientCategoryId { get; set; }

        public List<ClientCategoryDto> Categories { get; set; } = new();
    }
}