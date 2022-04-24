using congestion_tax_calculator.Interfaces;

namespace congestion_tax_calculator.Models;

public class Car : IVehicle
{
    public string GetVehicleType()
    {
        return "Car";
    }
}