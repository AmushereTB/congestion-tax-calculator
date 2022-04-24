using System;
using System.Linq;
using congestion_tax_calculator.Models;
using congestion_tax_calculator.Services;
using Xunit;
using FluentAssertions;

namespace congestion_tax_calculator.test;

public class CongestionTaxCalculatorTest
{
    private readonly ICongestionTaxCalculation _calculation;
    
    public CongestionTaxCalculatorTest()
    {
        _calculation = new CongestionTaxCalculation();
    }
    
    [Fact]
    public void GetTax_Should_Right_Value()
    {
        var dateArray = new string[] {
            "2013-09-23 15:01:00",
            "2013-09-23 07:21:21"
        };
        var vehicle = new Car();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetTax(vehicle, dates);
        result.Should().Be(31);
    }
    
    [Fact]
    public void TollFree_Vehicle_GetTax_Should_Return_0()
    {
        var dateArray = new string[] {
            "2013-09-23 15:01:00",
            "2013-09-23 07:21:21"
        };
        var vehicle = new Diplomat();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetTax(vehicle, dates);
        result.Should().Be(0);
    }
    
    [Fact]
    public void TollFree_Vehicle_GetInvoice_Should_Return_0()
    {
        var dateArray = new string[] {
            "2013-09-23 15:01:00",
            "2013-09-23 07:21:21",
            "2013-05-07 17:05:21",
            "2013-09-21 07:25:21"
        };
        var vehicle = new Diplomat();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetInvoice(vehicle, dates);
        result[2].Tax.Should().Be(0);
        result[0].Tax.Should().Be(0);
    }
    
    [Fact]
    public void GetTax_Should_Return_Max_Charge()
    {
        var dateArray = new string[] {
            "2013-02-05 06:20:27",
            "2013-02-05 06:27:00",
            "2013-02-05 14:35:00",
            "2013-02-05 15:29:00",
            "2013-02-05 15:47:00",
            "2013-02-05 16:01:00",
            "2013-02-05 16:48:00",
            "2013-02-05 17:49:00",
            "2013-02-05 18:29:00"
        };
        var vehicle = new Car();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetTax(vehicle, dates);
        result.Should().Be(60);
    }
    
    [Fact]
    public void Diff_Day_GetInvoice_Should_Return_Right_Value()
    {
        var dateArray = new string[] {
            "2013-09-23 15:01:00",
            "2013-09-23 07:21:21",
            "2013-05-07 17:05:21",
            "2013-09-21 07:25:21"
        };
        var vehicle = new Car();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetInvoice(vehicle, dates);
        result[2].Tax.Should().Be(31);
        result[0].Tax.Should().Be(13);
        result[1].Tax.Should().Be(0);
    }


    [Fact]
    public void Real_Case_Testing()
    {
        var dateArray = new string[] {
            "2013-01-14 21:00:00",
            "2013-01-15 21:00:00",
            "2013-02-07 06:23:27",
            "2013-02-07 15:27:00",
            "2013-02-08 06:27:00",
            "2013-02-08 06:20:27",
            "2013-02-08 14:35:00",
            "2013-02-08 15:29:00",
            "2013-02-08 15:47:00",
            "2013-02-08 16:01:00",
            "2013-02-08 16:48:00",
            "2013-02-08 17:49:00",
            "2013-02-08 18:29:00",
            "2013-02-08 18:35:00",
            "2013-03-26 14:25:00",
            "2013-03-28 14:07:27"
        };
        var vehicle = new Car();
        var dates = dateArray.Select(dt => DateTime.Parse(dt)).ToArray();
        var result = _calculation.GetInvoice(vehicle, dates);
        result[0].Tax.Should().Be(0);
        result[2].Tax.Should().Be(21);
        result[3].Tax.Should().Be(60);
    }
}