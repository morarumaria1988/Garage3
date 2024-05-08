using Garage3.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage3.Services
{
    public interface IGetVehicleTypeService
    {
        Task<IEnumerable<SelectListItem>> GetVehicleTypes();
    }
}