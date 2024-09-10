using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3._1
{
    public class Helper
    {
        public static bool Check<T>(T obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                foreach (var error in results)
                {
                    Helper.WriteErrorMessage(error.ErrorMessage);
                }
                return false;
            }
            return true;
        }

        public static void WriteSuccessfulMessage(string message) 
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        public static void WriteErrorMessage(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error!{message}");
            Console.ForegroundColor = color;
        }

        private const string _subMessage = "Enter";

        public static int GetInt(string value)
        {
            int result = 0;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!int.TryParse(Console.ReadLine(), out result))//пока не ввется цыфра
            {
                Console.WriteLine("{0} {1}: ", _subMessage, value);
            }
            return result;
        }

        public static string GetString(string value)
        {
            string result = String.Empty;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (String.IsNullOrWhiteSpace( result = Console.ReadLine()))//нужен хоть один символ введенный в консоли
            {
                Console.WriteLine("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
    
    }
}
