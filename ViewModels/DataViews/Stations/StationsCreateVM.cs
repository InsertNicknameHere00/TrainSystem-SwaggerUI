using System.ComponentModel.DataAnnotations;

namespace TrainSystem.ViewModels.DataViews.Stations
{
    public class StationsCreateVM
    {
        [Required(ErrorMessage = "This field is Required!")]
        public string Location { get; set; }
    }
}
