using System;
using System.Collections.Generic;

namespace Garage3.NewFolder
{

    public class UniqueRandomRegNumberGenerator
    {
        private Random _random = new Random();
        private HashSet<string> _uniqueNumbers = new HashSet<string>();
        private int _numberLength = 6; // Length of the registration number

        public string GenerateUniqueRegNumber()
        {
            while (true)
            {
                string number = GenerateRandomNumber(_numberLength);
                if (_uniqueNumbers.Add(number))
                {
                    return number;
                }
            }
        }

        private string GenerateRandomNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[_random.Next(chars.Length)];
            }

            return new string(buffer);
        }
    }

}
