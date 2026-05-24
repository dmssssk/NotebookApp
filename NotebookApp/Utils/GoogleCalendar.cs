namespace NotebookApp.Utils;
using System.Text.RegularExpressions;
using System.Globalization;
using NotebookApp.Models;    
    
public static class GoogleCalendar
{
    public static List<DateTime> CheckDate(string text)
    {
        Regex regex = new Regex(@"\d{2}\.\d{2}\.\d{4}");
        
        MatchCollection matches = regex.Matches(text);
        List<DateTime> dates = new List<DateTime>();

        foreach (Match match in matches)
        {
            string dateStr = match.Value;

            if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) && parsedDate > DateTime.Now)
            {
                dates.Add(parsedDate);
            }
        }

        return dates;
    }
    
    public static string LinkToGoogleCalendar(Note note_, DateTime date)
    {
        string title = note_.Title;
        string description = note_.Content;
        string location = "Онлайн";
        string timeZone = "Europe/Moscow"; // Ваш часовой пояс

        string formattedDate = date.ToString("yyyyMMdd");
        string formattedDateEnd = date.AddDays(1).ToString("yyyyMMdd");

        var encodedTitle = Uri.EscapeDataString(title);
        var encodedDescription = Uri.EscapeDataString(description);
        var encodedLocation = Uri.EscapeDataString(location);

        string googleCalendarUrl = $"https://calendar.google.com/calendar/u/0/r/eventedit" +
                                   $"?text={encodedTitle}" +
                                   $"&dates={formattedDate}/{formattedDateEnd}" +
                                   $"&ctz={timeZone}" +
                                   $"&details={encodedDescription}" +
                                   $"&location={encodedLocation}";

        return googleCalendarUrl;
    }
    
    
    
}