using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddDaysConsole
{
    class AddDaysToDate
    {
        static void Main(string[] args)
        {
            try
            {
                MyDateTime mydate = new MyDateTime();
                mydate.AddDate(); //calling the AddDate function which is the main function to add entered value to the given date.
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
                throw e;
            }
        }
    }

    //Class MyDateTime is used to read the inputs and provide expected output.
    class MyDateTime
    {
        uint d1, m1, y1; //variable to store splitted value of date in day, month and year
        UInt64 date1; //variable to store entered date
        uint value; //variable to store entered integer value to add to the date


        /* Function name AddDate() 
         * Is used to read entered values, perform calculations and return final calculated date
         */
        public void AddDate()
        {
            try
            {
                date1 = ReadDate();
                value = ReadValue();
                CheckDate(date1); //Function called to validate the enetered date

                uint offset1 = OffsetDays(); //offset1 variable is used to store the total number of days passed from the entered date in the year
                uint remDays = IsLeapYear(y1) ? (366 - offset1) : (365 - offset1); //remDays stores the remaining no. of days in the year

                /* variable y2 is used to store final year after calculation
                 * variable offset2 is used to store the total no. of pass days in the year + value entered by user
                 */
                uint y2, offset2;
                if (value <= remDays)
                {
                    //in If then it mean year will be the same so the final year y2 = y1
                    y2 = y1;
                    offset2 = offset1 + value;
                }
                else
                {
                    //if else then it means year should get increased
                    value -= remDays;
                    y2 = y1 + 1;
                    uint y2days = IsLeapYear(y2) ? (uint)366 : (uint)365; // calculating no. of days in the year/leap year
                    while (value >= y2days)
                    {
                        //if the value entered is greater than the total number of days in year, then year should get increased more
                        value -= y2days;
                        y2++;
                        y2days = IsLeapYear(y2) ? (uint)366 : (uint)365; // finding total no. of days in the year/leap year
                    }
                    offset2 = value;
                }
                //calling method RevOffsetDays() which takes input of total offset with value entered and calculated final year.
                //WriteDate takes result of RevOffsetDays() the final calculated date in YYYYMMDD format
                WriteDate(RevOffsetDays(offset2, y2)); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /* Function name ReadDate() 
         * Is used to read entered date and throw exception in case entered date is invalid or in wrong format
         * It returns entered date in UInt64 datatype.
         */
        UInt64 ReadDate()
        {
            UInt64 date = 0;
            Console.WriteLine("Please enter date in YYYYMMDD format: ");
            try
            {
                date = Convert.ToUInt64(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Date entered is not valid.");
                throw e;
            }
            return date;
        }

        /* Function name WriteDate() 
         * It takes the final calculated date in UInt64 datatype and in YYYYMMDD format 
         */
        void WriteDate(UInt64 date)
        {
            Console.WriteLine("Your new date in YYYYMMDD format is: {0}", date);
        }


        /* Function name ReadValue() 
         * Is used to read entered value to add into date and throw exception in case entered value is invalid or in wrong format
         * It returns entered value in UInt datatype.
         */
        uint ReadValue()
        {
            uint value = 0;
            Console.WriteLine("Please enter an integer value to add: ");
            try
            {
                value = Convert.ToUInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid value entered.");
                throw e;
            }
            return value;
        }

        /* Function name IsLeapYear() 
         * This function is used to calculate if the entered year is leap year or not
         * It takes year as a input and return bool(T/F)
         * It returns entered value in UInt datatype.
         */
        bool IsLeapYear(uint year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                return true;
            else
                return false;
        }

        /* Function name CheckDate() 
         * This function is used to split the entered date into day, month and year
         * It takes UInt64 type parameter which is the entered date.        
         */
        void CheckDate(UInt64 date)
        {
            try
            {
                d1 = (uint)date % 100;
                m1 = (uint)date / 100 % 100;
                y1 = (uint)date / 10000;
                if ((d1 < 0 || d1 > 31) ||
                    (m1 < 0 || m1 > 12) ||
                    (y1 < 0 || y1 > 9999))
                {
                    throw new Exception("Invalid date value.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /* Function name OffsetDays() 
         * This function is used to calculate no. of days passed in the year from the date enetered         
         * It returns calculated value in UInt datatype
         */
        uint OffsetDays()
        {
            uint[] noOfDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30 };

            uint offset = d1;
            for (uint i = m1 - 1; i > 0; i--)
            {
                offset += noOfDays[i - 1];
            }

            if (IsLeapYear(y1) && m1 > 2)
                offset += 1;
            return offset;
        }


        /* Function name RevOffsetDays() 
         * This function performs calculation and generate updated value of the day and month.
         * It takes calculated no. of days passed and final calculated year as a parameter
         * In returns it calls Getdate() method and pass final calculated day, month and year.
         */
        UInt64 RevOffsetDays(uint offset, uint y)
        {
            uint[] month = new uint[] { 0, 31, 28, 31, 30, 31, 30,
                      31, 31, 30, 31, 30, 31 };

            if (IsLeapYear(y))
                month[2] = 29;

            uint i;
            for (i = 1; i <= 12; i++)
            {
                if (offset <= month[i])
                    break;
                offset = offset - month[i];
            }

            uint d = offset;
            uint m = i;
            return GetDate(d, m, y);
        }


        /* Function name GetDate() 
         * This function is used to calculate the final date in the format YYYYMMDD
         * It takes final calculated day, month and year as a input
         * It returns value in UInt64 datatype.
         */
        UInt64 GetDate(uint d, uint m, uint y)
        {
            return (y * 10000) + (m * 100) + d;
        }
    }
}

