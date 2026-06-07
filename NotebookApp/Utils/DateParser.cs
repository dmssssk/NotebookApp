namespace NotebookApp.Utils;
using System.Text.RegularExpressions;

public class DateParser
{
    private static readonly Regex RegexNumericDate = new(
        @"(?<!\d)(?<day>0?[1-9]|[12]\d|3[01])(?<sep>[./])(?<month>0?[1-9]|1[0-2])(?:\k<sep>(?<year>\d{4}|\d{2})(?!\d)|(?!\k<sep>\d))", 
        RegexOptions.Compiled);

    private static readonly Regex RegexTextDate = new(
        @"(?<!\d)(?<day>0?[1-9]|[12]\d|3[01])\s+(?<month>янв(?:ар[ья])?|фев(?:рал[ья])?|мар(?:та?)?|апр(?:ел[ья])?|ма[йя]|июн[ья]?|июл[ья]?|авг(?:уста?)?|сен(?:тябр[ья])?|окт(?:ябр[ья])?|ноя(?:бр[ья]?)?|дек(?:абр[ья])?)\b(?:\s+(?<year>\d{4})(?!\d))?", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    

    private readonly Dictionary<string, string> _monthNumbers = new()
        {
            { "янв", "01" },
            { "фев", "02" },
            { "мар", "03" },
            { "апр", "04" },
            { "май", "05" },
            { "июн", "06" },
            { "июл", "07" },
            { "авг", "08" },
            { "сен", "09" },
            { "окт", "10" },
            { "нояб", "11" },
            { "дек", "12" }
        };


    public List<DateTime> ParseDates(string text)
    {
        List<DateTime> dates = new();

        foreach (Match match in RegexNumericDate.Matches(text))
        {
            string strDate;

            if (match.Groups["year"].Success)
            {
                strDate = $"{match.Groups["day"].Value}.{match.Groups["month"].Value}.{match.Groups["year"].Value}";
                if(DateTime.TryParse(strDate, out DateTime date) && date >  DateTime.Today) 
                    dates.Add(date);
            }

            else
            {
                strDate = $"{match.Groups["day"].Value}.{match.Groups["month"].Value}.{DateTime.Today.Year}";
                if(DateTime.TryParse(strDate, out DateTime date) &&  date > DateTime.Today)
                    dates.Add(date);
                else
                    dates.Add(date.AddYears(1));
            }
        }


        foreach (Match match in RegexTextDate.Matches(text))
        {
            string strDate;

            if (match.Groups["year"].Success)
            {
                strDate = $"{match.Groups["day"].Value}.{_monthNumbers[match.Groups["month"].Value.Length>3?match.Groups["month"].Value[..3]:match.Groups["month"].Value]}.{match.Groups["year"].Value}";
                if(DateTime.TryParse(strDate, out DateTime date) && date >  DateTime.Today) 
                    dates.Add(date);
            }

            else
            {
                strDate = $"{match.Groups["day"].Value}.{_monthNumbers[match.Groups["month"].Value.Length>3?match.Groups["month"].Value[..3]:match.Groups["month"].Value]}.{DateTime.Today.Year}";
                if(DateTime.TryParse(strDate, out DateTime date) &&  date > DateTime.Today)
                    dates.Add(date);
                else
                    dates.Add(date.AddYears(1));
            }
        }
        

        return dates;
    }



}