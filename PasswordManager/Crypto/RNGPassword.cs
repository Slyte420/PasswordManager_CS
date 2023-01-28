using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Crypto
{
    internal class RNGPassword
    {
        private bool upperCharB;
        private bool lowerCharB;
        private bool numbersB;
        private bool specialB;
        private const string _LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string _UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _Numbers = "0123456789";
        private const string specialCharacthers = "@!./?<>;[]\\(){}";
        RNGPassword(bool upperCharB, bool lowerCharB, bool numbersB, bool specialB)
        {
            this.upperCharB = upperCharB;
            this.lowerCharB = lowerCharB;
            this.numbersB = numbersB;
            this.specialB = specialB;
        }

        public string? generatePassword(int length)
        {
            string availableChars = string.Empty;
            if (upperCharB) {
                availableChars.Concat(_UpperCaseLetters);
            }
            if(lowerCharB) {
                availableChars.Concat(_LowerCaseLetters);
            }
            if(numbersB)
            {
                availableChars.Concat(_Numbers);
            }
            if(specialB) { 
                availableChars.Concat(specialCharacthers);
             }
            if(availableChars.Length <= 0)
            {
                return null;
            }
            
            StringBuilder str= new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                str.Append(availableChars[RandomNumberGenerator.GetInt32(availableChars.Length)]);
            }
            return str.ToString();
        }
        public bool validPassword(string password)
        {
            //TODO
            return true;
        }
    }
}
