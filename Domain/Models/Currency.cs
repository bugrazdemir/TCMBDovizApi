using System.Xml.Serialization;
using System.Collections.Generic;
namespace Domain.Models;
public class Currency
{
    [XmlAttribute("Kod")]
    public string Code { get; set; }

    [XmlElement("CurrencyName")]
    public string CurrencyName { get; set; }

    [XmlElement("ForexBuying")]
    public string ForexBuying { get; set; }

    [XmlElement("ForexSelling")]
    public string ForexSelling { get; set; }

    [XmlElement("BanknoteBuying")]
    public string BanknoteBuying { get; set; }

    [XmlElement("BanknoteSelling")]
    public string BanknoteSelling { get; set; }

    public decimal GetForexBuying() => decimal.TryParse(ForexBuying, out var value) ? value : 0;

    public decimal GetForexSelling() => decimal.TryParse(ForexSelling, out var value) ? value : 0;

    public decimal GetBanknoteBuying() => decimal.TryParse(BanknoteBuying, out var value) ? value : 0;

    public decimal GetBanknoteSelling() => decimal.TryParse(BanknoteSelling, out var value) ? value : 0;
}