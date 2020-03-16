using System;
using System.Text.RegularExpressions;
using System.Linq;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        Match matchedPhoneNumber = Regex.Match(Regex.Replace(phoneNumber, @"\D", ""), @"^1?([2-9]\d{2}[2-9]\d{6})$");
        return matchedPhoneNumber.Success ? matchedPhoneNumber.Groups[1].ToString() : throw new ArgumentException();
    }
}