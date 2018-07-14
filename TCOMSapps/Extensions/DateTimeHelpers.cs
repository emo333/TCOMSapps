using System;
using System.Globalization;

namespace TCOMSapps.Extensions
{
	public static class DateTimeHelpers
	{
		// Gets the Calendar instance associated with a CultureInfo.
		private static readonly CultureInfo MyCi = new CultureInfo("en-US");
		private static readonly Calendar MyCal = MyCi.Calendar;
		// Gets the DTFI properties required by GetWeekOfYear.
		private static readonly CalendarWeekRule MyCwr = MyCi.DateTimeFormat.CalendarWeekRule;
		private static readonly DayOfWeek MyFirstDow = MyCi.DateTimeFormat.FirstDayOfWeek;

		public static int GetWeekOfYearByDate(DateTime dt)
		{
			return MyCal.GetWeekOfYear(dt, MyCwr, MyFirstDow);
		}


		public static DateTime GetFirstDayOfWeek(int year, int weekNumber,
				CultureInfo culture)
		{
			Calendar calendar = culture.Calendar;
			DateTime firstOfYear = new DateTime(year, 1, 1, calendar);
			DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber - 1);
			DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

			while (targetDay.DayOfWeek != firstDayOfWeek)
			{
				targetDay = targetDay.AddDays(-1);
			}

			return targetDay;
		}
	}
}
