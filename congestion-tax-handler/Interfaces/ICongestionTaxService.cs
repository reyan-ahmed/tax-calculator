using congestion_tax_calculator_contractor.Requests;

namespace congestion_tax_handler.Interfaces;

public interface ICongestionTaxService
{
    public int GetTotalTax(CongestionTaxRequest congestionTaxRequest);
}
