using congestion_tax_calculator_contractor.Enums;
using congestion_tax_calculator_contractor.Requests;
using congestion_tax_handler.Services;
using NUnit.Framework;

namespace congestion_tax_calculator_unit_test;

[TestFixture]
public class CongestionTaxCalculatorServiceTests
{
    [TestCase]
    public void GetZeroTaxForTollFreeVehicle_MotorBike()
    {
        //Arrange
        DateTime[] dates = new DateTime[10];
        dates[0] = DateTime.Now;

        var request = new CongestionTaxRequest()
        {
            CityId = Guid.Parse("ec518ee0-81ee-4728-89a0-d0c88d5cfd3b"),
            VehicleTypeId = Guid.Parse("90f20bee-4bb9-47fa-8e44-d2a95aa76f2f"),
            Dates = dates
        };


        CongestionTaxService congestionTaxService = new CongestionTaxService();
        var tax = congestionTaxService.GetTotalTax(request);

        //Assert
        Assert.That(tax, Is.EqualTo(0));
    }
}