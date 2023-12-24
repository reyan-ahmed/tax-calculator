namespace congestion_tax_handler.Models;

public class Car : IVehicle
{
    public string GetVehicleType()
    {
        return "Car";
    }
}
