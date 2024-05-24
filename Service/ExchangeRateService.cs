using Domain.Models;
using System.Xml.Serialization;
namespace Service;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;
    private const string TcmbUrl= "https://www.tcmb.gov.tr/kurlar/today.xml";

    public ExchangeRateService(HttpClient httpClient)
    {
       _httpClient = httpClient;
    }
    
    public async Task<(ExchangeRates exchangeRates,DateTime fetchTime)> GetExchangeRatesAsync()
    {
        var response =await _httpClient.GetAsync(TcmbUrl);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var fetchTime=DateTime.Now;
       
        var serializer= new XmlSerializer(typeof(ExchangeRates));
        var exchangeRates=(ExchangeRates)serializer.Deserialize(stream);

        return (exchangeRates, fetchTime);
    }

}
