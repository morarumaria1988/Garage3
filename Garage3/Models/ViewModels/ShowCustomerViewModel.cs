using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class ShowCustomerViewModel
    {
        [Display(Name = "Personal number")]
        public string PersonalNumber { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Vehicles owned")]
        public int Vcount { get; set; }
    }
    
}
