using System.Security.Cryptography;
using System.Text;

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
        private const string _specialCharacthers = "@!./?<>;[]\\(){}";
        private const int minLength = 10;
        private const int maxLength = 32;
        public RNGPassword(bool upperCharB, bool lowerCharB, bool numbersB, bool specialB)
        {
            this.upperCharB = upperCharB;
            this.lowerCharB = lowerCharB;
            this.numbersB = numbersB;
            this.specialB = specialB;
        }

        public string? generatePassword(int length)
        {
            if(length <= 0)
            {
                return null;
            }
            string availableChars = string.Empty;
            if (upperCharB)
            {
                availableChars = availableChars + _UpperCaseLetters;
            }
            if (lowerCharB)
            {
                availableChars= availableChars + _LowerCaseLetters;
            }
            if (numbersB)
            {
                availableChars= availableChars + _Numbers;
            }
            if (specialB)
            {
                availableChars = availableChars + _specialCharacthers;
            }
            if (availableChars.Length <= 0)
            {
                return null;
            }

            StringBuilder str = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                str.Append(availableChars[RandomNumberGenerator.GetInt32(availableChars.Length)]);
            }
            return str.ToString();
        }
        public static int getMin()
        {
            return minLength;
        }
        public static int getMax()
        {
            return maxLength;
        }
        public static bool validPassword(string password)
        {
            if (!(password.Length >= minLength && password.Length <= maxLength))
            {
                return false;
            }
            bool[] reqCheck = new bool[4];
            foreach(char i in password)
            {
                if(_UpperCaseLetters.Contains(i))
                {
                    reqCheck[0] = true;
                }
                if(_LowerCaseLetters.Contains(i)) 
                {
                    reqCheck[1] = true;
                }
                if (_Numbers.Contains(i)) 
                {
                    reqCheck[2] = true;
                }
                if (_specialCharacthers.Contains(i))
                {
                    reqCheck[3] = true;
                }
            }
            return reqCheck.All(result => true);
        }
    }
}
