
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

//Console.WriteLine("---------- DateTime manipulations ----------");

//// Empty DateTime
//DateTime dateTime = new DateTime();

//// Create a DateTime for a specific date
//DateTime specificDate = new DateTime(2023, 10, 5, 14, 45, 25);
//Console.WriteLine($"Specific Date: {specificDate}");

//Console.WriteLine($"Year: {specificDate.Year}");
//Console.WriteLine($"Month: {specificDate.Month}");
//Console.WriteLine($"Day: {specificDate.Day}");
//Console.WriteLine($"Time: {specificDate.TimeOfDay}");
//Console.WriteLine($"Tick: {specificDate.Ticks}");
//Console.WriteLine($"Kind: {specificDate.Kind}");

//// Create a DateTime from current timestamp
//DateTime currentDate = DateTime.Now;
//Console.WriteLine($"Current Date: {currentDate}");

//// Create a DateTime from a string
//Console.WriteLine("Enter your DOB (MM/dd/yyyy): ");
//string? input = Console.ReadLine();
//var userDOB = DateTime.Parse(input);
//Console.WriteLine($"Your DOB is: {userDOB}");

//// Change format of DateTime
//Console.WriteLine($"Formatted DOB: {userDOB:dd-MM-yyyy}");
//Console.WriteLine($"Formatted DOB: {userDOB:dd, MM-yyyy}");

//// Add additional time
//Console.WriteLine($"One hour from now is: " + DateTime.Now.AddHours(1));

//// Ticks from DateTime


Console.WriteLine("---------- DateTime Offset ----------");

// UTC = coordinated universal time
var utcDateTime = DateTime.UtcNow;
Console.WriteLine($"Now DateTime: {DateTime.Now}");
Console.WriteLine($"UTC DateTime: {utcDateTime}");
Console.WriteLine($"Local DateTime: {utcDateTime.ToLocalTime()}");

// Time zones
var tz = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
Console.WriteLine($"Local Time Zone: {tz}");

var dto = new DateTimeOffset(DateTime.Now, tz);
Console.WriteLine($"DateTimeOffset: {dto}");
Console.WriteLine($"DateTimeOffset UtcDateTime: {dto.UtcDateTime}");

var indiaTz = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
var indiaDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, indiaTz);

var indiaDto = dto.ToOffset(indiaTz.GetUtcOffset(DateTime.UtcNow));
Console.WriteLine($"India DateTimeOffset: {indiaDto}");


// DateOnly
var dateOnly = new DateOnly(1999, 12, 3);
var nextDay = dateOnly.AddDays(1);
var previousDay = dateOnly.AddDays(-1);
var decadeLater = dateOnly.AddYears(10);
var lastMonth = dateOnly.AddMonths(-1);

Console.WriteLine($"DateOnly: {dateOnly}");
Console.WriteLine($"Next Day: {nextDay}");
Console.WriteLine($"Previous Day: {previousDay}");
Console.WriteLine($"Decade Later: {decadeLater}");
Console.WriteLine($"Last Month: {lastMonth}");

var dateOnlyFromDateTime = DateOnly.FromDateTime(DateTime.Now);
Console.WriteLine($"DateOnly from DateTime: {dateOnlyFromDateTime}");

Console.WriteLine("What is your DOB (dd/MM/yyyy)");
string? dobInput = Console.ReadLine();

var userDob = DateOnly.ParseExact(dobInput, "dd/MM/yyyy",CultureInfo.InvariantCulture);
Console.WriteLine($"Your DOB is: {userDob}");

// TimeOnly
var timeOnly = new TimeOnly(14, 30, 15);
var timeFromDateTime = TimeOnly.FromDateTime(DateTime.Now);

Console.WriteLine($"TimeOnly: {timeOnly}");
Console.WriteLine($"TimeOnly from DateTime: {timeFromDateTime: hh:mm tt}");

// Date comparisons
var date1 = new DateOnly(2023, 10, 5);
var date2 = new DateOnly(2023, 10, 10);

Console.WriteLine($"{date1} is earlier than {date2}: {date1 < date2}");
Console.WriteLine($"{date1} is later than {date2}: {date1 > date2}");
Console.WriteLine($"{date1} is equal to {date2}: {date1 == date2}");