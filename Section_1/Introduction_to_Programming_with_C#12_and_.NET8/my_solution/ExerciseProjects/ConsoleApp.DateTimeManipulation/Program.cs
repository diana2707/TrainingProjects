
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("---------- DateTime manipulations ----------");

// Empty DateTime
DateTime dateTime = new DateTime();

// Create a DateTime for a specific date
DateTime specificDate = new DateTime(2023, 10, 5, 14, 45, 25);
Console.WriteLine($"Specific Date: {specificDate}");

Console.WriteLine($"Year: {specificDate.Year}");
Console.WriteLine($"Month: {specificDate.Month}");
Console.WriteLine($"Day: {specificDate.Day}");
Console.WriteLine($"Time: {specificDate.TimeOfDay}");
Console.WriteLine($"Tick: {specificDate.Ticks}");
Console.WriteLine($"Kind: {specificDate.Kind}");

// Create a DateTime from current timestamp
DateTime currentDate = DateTime.Now;
Console.WriteLine($"Current Date: {currentDate}");

// Create a DateTime from a string
Console.WriteLine("Enter your DOB (MM/dd/yyyy): ");
string? input = Console.ReadLine();
var userDOB = DateTime.Parse(input);
Console.WriteLine($"Your DOB is: {userDOB}");

// Change format of DateTime
Console.WriteLine($"Formatted DOB: {userDOB:dd-MM-yyyy}");
Console.WriteLine($"Formatted DOB: {userDOB:dd, MM-yyyy}");

// Add additional time
Console.WriteLine($"One hour from now is: " + DateTime.Now.AddHours(1));

// Ticks from DateTime


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
var indiaDto = dto.ToOffset(indiaTz.GetUtcOffset(DateTime.UtcNow));
Console.WriteLine($"India DateTimeOffset: {indiaDto}");