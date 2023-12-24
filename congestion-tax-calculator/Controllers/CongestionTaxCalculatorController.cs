using congestion_tax_calculator_contractor.Requests;
using Microsoft.AspNetCore.Mvc;

namespace congestion_tax_calculator.Controllers;

[ApiController]
[Route("[controller]")]
public class CongestionTaxCalculatorController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICongestionTaxService _congestionTaxService;

    public CongestionTaxCalculatorController(ILogger<CongestionTaxCalculatorController> logger, ICongestionTaxService congestionTaxService)
    {
        _logger= logger;
        _congestionTaxService=congestionTaxService;
    }

    [HttpGet]
    public int GetTotalTax([FromBody]CongestionTaxRequest congestionTaxRequest)
    {
        try
        {
            return _congestionTaxService.GetTotalTax(congestionTaxRequest);
        }
        catch (Exception ex)
        { 
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
} 