using System;
using System.Collections.Generic;
using System.Linq;
using congestion_tax_calculator.Interfaces;
using congestion_tax_calculator.Models;

namespace congestion_tax_calculator.Services;

public class CongestionTaxCalculation : ICongestionTaxCalculation
{
    public List<Invoice> GetInvoice(IVehicle vehicle, DateTime[] dates)
    {
        var sortedDates = dates
            .OrderBy(dt => dt)
            .GroupBy(time => time.Date.DayOfYear)
            .ToList();
        var invoiceInfo = new List<Invoice>();
        sortedDates.ForEach(dateGroup =>
        {
            var tax = GetTax(vehicle, dateGroup.ToArray());

            invoiceInfo.Add(new Invoice
            {
                Date = dateGroup.First().Date.ToString("yyyy-MM-dd"),
                Tax = tax
            });
        });
        return invoiceInfo;
    }
    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
        var sortedTime = dates.OrderBy(time => time).ToArray();
        var intervalStart = sortedTime[0];
        var totalFee = 0;
        foreach (var date in sortedTime)
        {
            var nextFee = GetTollFee(date, vehicle);
            var tempFee = GetTollFee(intervalStart, vehicle);

            var diffInMillies = date - intervalStart;
            var minutes = (long)(diffInMillies.TotalMilliseconds / 1000 / 60);
            
            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                intervalStart = date;
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }
    
    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        var hour = date.Hour;
        var minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 30 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }
    
    private Boolean IsTollFreeDate(DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
        
        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}