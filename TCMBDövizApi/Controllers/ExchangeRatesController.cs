using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Service;


namespace TCMBDövizApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangeRatesController : ControllerBase
{
    private readonly ExchangeRateService _exchangeRateService;

    public ExchangeRatesController(ExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var(exchangeRates, fetchTime)=await _exchangeRateService.GetExchangeRatesAsync();
        var result = exchangeRates.Currencies.Select(currency => new
        {
            currency.Code,
            currency.CurrencyName,
            ForexBuying = currency.GetForexBuying(),
            ForexSelling = currency.GetForexSelling(),
            BanknoteBuying = currency.GetBanknoteBuying(),
            BanknoteSelling = currency.GetBanknoteSelling(),
            fetchTime = fetchTime,
        });
        return Ok(result);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var (exchangeRates,fetchTime)=await _exchangeRateService.GetExchangeRatesAsync();

        var currency = exchangeRates.Currencies
            .Where(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
            .Select(currency => new
            {
                currency.Code,
                currency.CurrencyName,
                ForexBuying=currency.GetForexBuying(),
                ForexSelling=currency.GetForexSelling(),
                BanknoteBuying=currency.GetBanknoteBuying(),
                BanknoteSelling=currency.GetBanknoteSelling(),
                fetchTime = fetchTime,
            })
            .FirstOrDefault();
        if(currency == null)
        {
            return NotFound(new { Message = $"'{code}' tipinde istenen döviz birimi bulunamadı. " });
        }
        return Ok(currency);
    }
}
