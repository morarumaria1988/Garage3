using Garage3.Models.Entities;
using Garage3.Models.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Services
{
    public class GetVehicleTypeService : IGetVehicleTypeService
    {
        private readonly GarageMVCContext context;

        public GetVehicleTypeService(GarageMVCContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetVehicleTypes()
        {
            return await context.VTypes.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Name
            }).ToListAsync();
        }
    }
}
