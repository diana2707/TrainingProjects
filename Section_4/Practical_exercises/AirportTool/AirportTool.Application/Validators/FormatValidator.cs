using AirportTool.Application.Exceptions;
using System.Text.RegularExpressions;

namespace AirportTool.Application.Validators
{
    public static class FormatValidator
    {
        public static void ValidateConfirmationCode(string confirmationCode)
        {
            if (string.IsNullOrWhiteSpace(confirmationCode))
            {
                throw new DomainValidationException(
                    "Confirmation code is required.");
            }

            if (!Regex.IsMatch(confirmationCode, "^[A-Z0-9]{6,8}$"))
            {
                throw new DomainValidationException(
                    "Confirmation code must be 6–8 characters long and contain only uppercase letters and digits.");
            }
        }

        public static void ValidateAirportIataCode(string iataCode)
        {
            if (string.IsNullOrWhiteSpace(iataCode))
            {
                throw new DomainValidationException(
                    "Airport IATA code is required.");
            }

            if (!Regex.IsMatch(iataCode, "^[A-Z]{3}$"))
            {
                throw new DomainValidationException(
                    "Airport IATA code must consist of exactly 3 uppercase letters.");
            }
        }

        public static void ValidateFlightNumber(string flightNumber)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                throw new DomainValidationException(
                    "Flight number is required.");
            }

            if (!Regex.IsMatch(flightNumber, "^[A-Z0-9]{2}[0-9]{1,4}$"))
            {
                throw new DomainValidationException(
                    "Flight number must consist of a 2-character airline code followed by 1–4 digits (e.g. LH123).");
            }
        }
    }
}
