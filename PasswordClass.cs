using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp21
{
    public class PasswordGenerator
    {
        bool lowerCase { get; }
        bool upperCase { get; }
        bool specialSign { get; }
        bool numbers { get; }
        bool underCore { get; }
        byte lenPassword { get; }

        public PasswordGenerator (bool lowerCase, bool upperCase, bool specialSign, bool numbers, bool underCore, byte lenPassword)
        {
            this.lowerCase = lowerCase;
            this.upperCase = upperCase;
            this.specialSign = specialSign;
            this.numbers = numbers;
            this.underCore = underCore;
            this.lenPassword = lenPassword;
        }

        public string Generate()
        {
            string characters = "";
            string lowerCh = "abcdefghijklmnopqrstuwxyz";
            string upperCh = "ABCDEFGHIJKLMNOPQRSTUWXYZ";
            string specialCh = "!@#$%^&*()";
            string numberCh = "0123456789";
            string underCh = "_";

            char[] password = new char[lenPassword];
            Random random = new Random();


            if (lowerCase)
            {
                characters += lowerCh;
                password[0] = lowerCh[random.Next(lowerCh.Length)];
            }
            if (upperCase)
            {
                characters += upperCh;
                password[1] = upperCh[random.Next(upperCh.Length)];
            }
            if (specialSign)
            {
                characters += specialCh;
                password[2] = specialCh[random.Next(specialCh.Length)];
            }
            if (numbers)
            {
                characters += numberCh;
                password[3] = numberCh[random.Next(numberCh.Length)];
            }
            if (underCore)
            {
                characters += underCh;
                password[4] = Convert.ToChar(underCh);
            }

            if (lowerCase == false && upperCase==false && specialSign==false && numbers == false && underCore == false)
            {
                characters = lowerCh + upperCh + specialCh + numberCh + underCh;

            }


            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == 0)
                {
                    password[i] = characters[random.Next(characters.Length)];
                }
            }

            password = password.OrderBy(_ => random.Next()).ToArray();

            return new string (password);

           
        }

        public string PasswordStrenght()
        {
            int strenghtValue = 0;
            if (lowerCase) strenghtValue++;
            if (upperCase) strenghtValue++;
            if (specialSign) strenghtValue++;
            if (numbers) strenghtValue++;
            if (underCore) strenghtValue++;

            if (lowerCase == false && upperCase == false && specialSign == false && numbers == false && underCore == false)
            {
                strenghtValue += 5;
            }



            if (lowerCase == false && upperCase == false && specialSign == false && numbers == false && underCore == true)
            {
                
            }
            else if (lenPassword < 8)
            {
                strenghtValue += 3;
            }
            else if (lenPassword < 12)
            {
                strenghtValue += 6;
            }
            else  
            {
                strenghtValue += 9;
            }
            

            return Convert.ToString(strenghtValue);
        }

    }
}
