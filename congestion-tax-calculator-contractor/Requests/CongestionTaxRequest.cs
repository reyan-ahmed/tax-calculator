namespace congestion_tax_calculator_contractor.Requests;

public class CongestionTaxRequest
{
    public Guid VehicleTypeId { get; set; }
    public Guid CityId { get; set; }
    public DateTime[] Dates{ get; set; }
}
