using congestion_tax_calculator.Interfaces;

namespace congestion_tax_calculator.Models;

public class Motorbike : IVehicle
{
    public string GetVehicleType()
    {
        return "Motorbike";
    }
}