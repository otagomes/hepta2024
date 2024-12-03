using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel.Lib
{
    public class CodigoRandomico
    {
        public static string RandomString(int size = 6, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            var _random = new Random();
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static int RandomNumber(int min, int max)
        {
            var _random = new Random();
            return _random.Next(min, max);
        }

        public string RandomPassword()
        {
            var _random = new Random();
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        public string DiaSemana(int ano, int mes, int dia)
        {
            DateTime dt = new DateTime(ano, mes, dia);
            var culture = new System.Globalization.CultureInfo("pt-BR");
            var day = culture.DateTimeFormat.GetDayName(dt.DayOfWeek);
            return day;
        }
    }
}
