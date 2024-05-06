using System.ComponentModel.DataAnnotations;

namespace Garage3.Validation
{
    public class CheckPersonalNumber: ValidationAttribute
    {

        public override bool IsValid(object? value)
        {

            if (value is string input) { 
                
                // 198010201330
               
                if(input.ToCharArray().Count() != 12) return false;

                foreach(char c in input.ToCharArray())
                {
                    string t = c.ToString();

                    if (!int.TryParse(t, out int _)) {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
