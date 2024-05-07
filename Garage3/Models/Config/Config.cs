using Garage3.Models.Entities;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Garage3.Models.Config
{
    public class Config
    {

        public static GarageInfo GarageInfo { get; set; } 

        public Config() {


            using (StreamReader file = File.OpenText(@"GarageConfig.json"))
            {
                string content = file.ReadToEnd();

                Config.GarageInfo = JsonConvert.DeserializeObject<GarageInfo>(content);
                
            }
        }
    }
}
