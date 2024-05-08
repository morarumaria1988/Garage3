using Garage3.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class VehicleStatisticsViewModel
    {
        //● Hur många fordon finns av varje typ?
        public List<VehicleTypeStatisticsViewModel> VehicleTypes { get; set; }
        //● Hur många hjul finns det totalt i garaget just nu?
        public int TotalAmountOfWheels { get; set; }
        //● Hur mycket har fordonen som står i garaget just nu genererat i    intäkt
        public int TotalGeneratedIncome { get; set; }
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
