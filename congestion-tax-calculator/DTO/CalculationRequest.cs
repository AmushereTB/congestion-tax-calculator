using congestion_tax_calculator.Interfaces;

namespace congestion_tax_calculator.DTO;

public class CalculationRequest
{
    public string VehicleType { get; set; }

    public string[] Dates { get; set; }
}