using System.Xml.Serialization;
using System.Collections.Generic;


namespace Domain.Models;
[XmlRoot("Tarih_Date")]
public class ExchangeRates
{
    [XmlElement("Currency")]
    public List<Currency> Currencies { get; set; }
}
