/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System.ComponentModel.DataAnnotations;

namespace Phi.MobileWebApp.Validation
{
    /// <summary>
    /// Attribute for Length validation.
    /// </summary>
    public class ValidLengthAttribute : ValidationAttribute
    {
        public int Length { get; set; }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string val = (string)value;

                if (val.Length >Length)
                    return false;
            }

            return true;
        }
    }
}