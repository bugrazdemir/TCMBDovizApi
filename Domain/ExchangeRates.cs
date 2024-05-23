﻿using System.Xml.Serialization;
using System.Collections.Generic;


namespace Domain;
[XmlRoot("Tarih_Date")]
public class ExchangeRates
{
    [XmlElement("Currency")]
    public List<Currency> Currencies { get; set; }
}
