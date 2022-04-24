using congestion_tax_calculator.Interfaces;

namespace congestion_tax_calculator.Models.Vehicle;

public class Tractor : IVehicle
{
    public string GetVehicleType()
    {
        return "Tractor";
    }
}