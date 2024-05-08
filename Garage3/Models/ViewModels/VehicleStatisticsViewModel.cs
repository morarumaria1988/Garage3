using Garage3.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class VehicleStatisticsViewModel
    {
        public List<VehicleTypeStatisticsViewModel> VehicleTypes { get; set; }
        [Display(Name="Total Amount of Wheels")]
        public int TotalAmountOfWheels { get; set; }
        [DisplayFormat(DataFormatString="{0:C}")]
        [Display(Name="Total Generated Income")]
        public double TotalGeneratedIncome { get; set; }
        //● Annan intressant statistik ni kan komma på.
    }

    public class VehicleTypeStatisticsViewModel
    {
        [Display(Name="Total Amount")]
        public int TotalAmount { get; set; }
        [Display(Name="Vehicle Type")]
        public string VehicleTypeName { get; set; }

    }
}
