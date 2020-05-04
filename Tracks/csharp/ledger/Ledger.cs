using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LedgerEntry
{
    public LedgerEntry(DateTime date, string description, decimal change)
    {
        Date = date;
        Description = description;
        Change = change;
    }

    public DateTime Date { get; }
    public string Description { get; }
    public decimal Change { get; }
}

public class Culture
{
    public readonly int NegativePattern;
    public readonly string ShortDatePattern;
    public readonly string DateSeparator;
    public readonly string Header;

    public Culture(int negativePattern, string datePattern, string dateSeparator, string header)
    {
        NegativePattern = negativePattern;
        ShortDatePattern = datePattern;
        DateSeparator = dateSeparator;
        Header = header;
    }
}

public static class Ledger
{
    private const string SEPARATOR = " | ";

    private static readonly Dictionary<string, Culture> supportedCultures = new Dictionary<string, Culture>
    {
        {  "nl-NL", new Culture(12, "dd/MM/yyyy", "-", "Datum      | Omschrijving              | Verandering  ") },
        { "en-US", new Culture(0, "MM/dd/yyyy", "/", "Date       | Description               | Change       ") }
    };

    private enum Currency
    {
        USD = '$',
        EUR = '€',
    }

    public static LedgerEntry CreateEntry(string date, string locale, int change) =>
        new LedgerEntry(DateTime.Parse(date, CultureInfo.InvariantCulture), locale, change / 100.0m);

    private static CultureInfo CreateCulture(string cur, string locale)
    {
        if (!Enum.GetNames(typeof(Currency)).Contains(cur))
        {
            throw new ArgumentException("Invalid currency");
        }

        if (!supportedCultures.ContainsKey(locale))
        {
            throw new ArgumentException("Invalid culture locale");
        }

        var culture = new CultureInfo(locale);
        Culture currentCulture = supportedCultures[locale];
        culture.NumberFormat.CurrencySymbol = ((char)(Currency)Enum.Parse(typeof(Currency), cur)).ToString();
        culture.NumberFormat.CurrencyNegativePattern = currentCulture.NegativePattern;
        culture.DateTimeFormat.ShortDatePattern = currentCulture.ShortDatePattern;
        culture.DateTimeFormat.DateSeparator = currentCulture.DateSeparator;
        return culture;
    }

    private static string PrintHead(string locale) => 
        supportedCultures.TryGetValue(locale, out Culture head) ? head.Header : throw new ArgumentException("Invalid culture locale.");

    private static string Date(IFormatProvider culture, DateTime date) => date.ToString("d", culture);

    private static string Description(string desc) => string.Format("{0,-25}", desc.Length > 25 ? desc.Substring(0, 22) + "..." : desc);

    private static string Change(IFormatProvider culture, decimal change) =>
        string.Format("{0,13}", $"{change.ToString("C", culture)}{(change < 0.0m ? "" : " ")}");

    private static string PrintEntry(IFormatProvider culture, LedgerEntry entry)
    {
        string date = Date(culture, entry.Date);
        string description = Description(entry.Description);
        string change = Change(culture, entry.Change);

        return $"{date}{SEPARATOR}{description}{SEPARATOR}{change}";
    }

    private static IEnumerable<LedgerEntry> SortByChangeAndText(LedgerEntry[] entries) => 
        entries.OrderBy(x => x.Change).ThenBy(x => x.Date + "@" + x.Description + "@" + x.Change);

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        var culture = CreateCulture(currency, locale);

        return SortByChangeAndText(entries).Aggregate(PrintHead(locale), (result, entry) => result + $"\n{PrintEntry(culture, entry)}");
    }
}
