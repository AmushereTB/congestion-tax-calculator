using congestion_tax_calculator.Interfaces;

namespace congestion_tax_calculator.Models.Vehicle;

public class Motorcycle : IVehicle
{
    public string GetVehicleType()
    {
        return "Motorcycle";
    }
}