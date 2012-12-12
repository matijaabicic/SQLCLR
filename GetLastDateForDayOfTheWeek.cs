using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

public partial class GetLastDateForDayOfTheWeek
{
    private static DateTime WorkingDate = new DateTime();
    private static DayOfWeek DayOfTheWeek = new DayOfWeek();

    [SqlFunction(Name = "GetLastDateForDayOfTheWeek", FillRowMethodName = "FillRow", TableDefinition = "ReturnDate datetime")]
    public static DateTime fnGetLastDateForDayOfTheWeek(string DayIdentifier, string startingdate = "")
    {
        //if starting date has not been passed or cannot be parsed, initialize it to today
        if (!DateTime.TryParse(startingdate, out WorkingDate))
            WorkingDate = DateTime.Today;
        
        //enumerize day of the week
        switch (DayIdentifier.ToUpper())
        {
            case "SUNDAY":
            case "SUN":
            case "S": DayOfTheWeek = DayOfWeek.Sunday; break;
            case "MONDAY":
            case "MON":
            case "M": DayOfTheWeek = DayOfWeek.Monday; break;
            case "TUESDAY":
            case "TUE":
            case "T": DayOfTheWeek = DayOfWeek.Tuesday; break;
            case "WEDNESDAY":
            case "WED":
            case "W": DayOfTheWeek = DayOfWeek.Wednesday; break;
            case "THURSDAY":
            case "THU":
            case "TH": DayOfTheWeek = DayOfWeek.Thursday; break;
            case "FRIDAY":
            case "FRI":
            case "F": DayOfTheWeek = DayOfWeek.Friday; break;
            case "SATURDAY":
            case "SAT":
            case "SA": DayOfTheWeek = DayOfWeek.Saturday; break;
            default: DayOfTheWeek = DayOfWeek.Sunday; break;
        }
        //if starting date is today, and day of the week identifier is today, return today's date
        if (!(WorkingDate == DateTime.Today && DayOfTheWeek == DateTime.Today.DayOfWeek))
        {
            DateTime tempdate = new DateTime();
            tempdate = DateTime.Today;
            //main logic branch
            do
            {
                //go back a day and compare to target DayOfTheWeek. return if we find date of the week
                tempdate = tempdate.AddDays(-1);
                if (tempdate.DayOfWeek == DayOfTheWeek)
                {
                    return tempdate;
                }
            } while (true);
        }
        return DateTime.Today;
    }
    public static void FillRow(object row, out SqlString str)
    {
        str = new SqlString((string)row);
    }
}
