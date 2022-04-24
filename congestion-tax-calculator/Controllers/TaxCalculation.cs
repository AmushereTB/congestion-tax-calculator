using congestion_tax_calculator.DTO;
using congestion_tax_calculator.Interfaces;
using congestion_tax_calculator.Models;
using congestion_tax_calculator.Models.Vehicle;
using congestion_tax_calculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace congestion_tax_calculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxCalculation : ControllerBase
{
    private readonly ICongestionTaxCalculation _calculation;

    public TaxCalculation(ICongestionTaxCalculation taxCalculation)
    {
        _calculation = taxCalculation;
    }
    
    [HttpPost]
    public ActionResult<List<Invoice>> CalculateToll(CalculationRequest request)
    {
        IVehicle vehicle = null;
        var sortedDates = request.Dates.Select(dt => DateTime.Parse(dt)).ToArray();
        switch (request.VehicleType)
        {
            case "Car": vehicle = new Car(); break;
            case "Motorbike": vehicle = new Motorbike(); break;
            case "Diplomat": vehicle = new Diplomat(); break;
            case "Emergency": vehicle = new Emergency(); break;
            case "Foreign": vehicle = new Foreign(); break;
            case "Military": vehicle = new Military(); break;
            case "Motorcycle": vehicle = new Motorcycle(); break;
            case "Tractor": vehicle = new Tractor(); break;
        }
        
        if (vehicle == null) return BadRequest();
      
        return _calculation.GetInvoice(vehicle, sortedDates);
    }
}