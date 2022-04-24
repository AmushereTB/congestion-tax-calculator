using System;
using System.Collections.Generic;
using congestion_tax_calculator.Interfaces;
using congestion_tax_calculator.Models;

namespace congestion_tax_calculator.Services;

public interface ICongestionTaxCalculation
{
    public int GetTax(IVehicle vehicle, DateTime[] dates);

    public List<Invoice> GetInvoice(IVehicle vehicle, DateTime[] dates);
}