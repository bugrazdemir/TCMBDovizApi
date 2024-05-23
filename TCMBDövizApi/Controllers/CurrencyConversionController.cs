using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Service;

[ApiController]
[Route("api/[controller]")]
public class CurrencyConversionController : ControllerBase
{
    private readonly ExchangeRateService _exchangeRateService;

    public CurrencyConversionController(ExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet("{fromCode}/{toCode}/{amount}")]
    public async Task<IActionResult> Convert(string fromCode, string toCode, decimal amount)
    {
        var (exchangeRates, fetchTime) = await _exchangeRateService.GetExchangeRatesAsync();

        var fromCurrency = exchangeRates.Currencies
            .FirstOrDefault(c => c.Code.Equals(fromCode, StringComparison.OrdinalIgnoreCase));

        var toCurrency = exchangeRates.Currencies
            .FirstOrDefault(c => c.Code.Equals(toCode, StringComparison.OrdinalIgnoreCase));

        if (fromCurrency == null)
        {
            return NotFound(new { Message = $"'{fromCode}' tipinde istenen döviz birimi bulunamadı." });
        }

        if (toCurrency == null)
        {
            return NotFound(new { Message = $"'{toCode}' tipinde istenen döviz birimi bulunamadı." });
        }

        // Assuming we are using ForexBuying rates for conversion
        decimal convertedAmount = amount * fromCurrency.GetForexBuying() / toCurrency.GetForexBuying();

        return Ok(new
        {
            FromCurrency = fromCurrency.Code,
            ToCurrency = toCurrency.Code,
            OriginalAmount = amount,
            ConvertedAmount = convertedAmount,
            FetchTime = fetchTime
        });
    }
}
