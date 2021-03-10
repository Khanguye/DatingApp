using System;

namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateTime dob){

            var age = DateTime.Now.Year - dob.Year;
            if ( dob.AddYears(age) > DateTime.Now) return age-1;
            return age;
        }
    }
}