using ConsoleApplication1.Model;
using System;
using System.Linq;

namespace ConsoleApplication1.Utils
{
    public class Converter
    {

        ///// <summary>
        ///// Converts the user input from either integer or string to meaningful command. Outputs operation enum.
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="c"></param>
        ///// <returns>Returns a boolean value that points whether the user input successfuly converted to a command or not.</returns>
        //public static Boolean TryConvert(String value, out Operation? c)
        //{
        //    c = null;
        //    try
        //    {
        //        if (Int32.TryParse(value, out Int32 intValue))
        //        {
        //            c = (Operation)intValue - 1;
        //            return true;
        //        }
        //        else if (Enum.TryParse<Operation>(Converter.FirstCharToUpper(value), out Operation temp))
        //        {
        //            c = temp;
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Converts the user input from either integer or string to meaningful DeliveryMethod. Outputs DeliveryMethod enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="c"></param>
        /// <returns>Returns a boolean value that points whether the user input successfuly converted to a DeliveryMethod or not.</returns>
        public static bool TryConvert(string value, out DeliveryMethod? deliveryMethod)
        {
            deliveryMethod = null;
            try
            {
                if (Int32.TryParse(value, out Int32 intValue))
                {
                    deliveryMethod = (DeliveryMethod)intValue - 1;
                    return true;
                }
                else if (Enum.TryParse<DeliveryMethod>(Converter.FirstCharToUpper(value), out DeliveryMethod temp))
                {
                    deliveryMethod = temp;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Fixes string to be upper case on first character and lower case for the remaining.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns the fixed string</returns>

        private static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }


    }
}

