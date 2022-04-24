<font size="16">congestion-tax-calculator</font>
<hr style="border:1px solid gray"/>

####API URL: https://localhost:7266/api/TaxCalculation

####API Method: POST

####API Content Type: application/json

#####Input Example:
```
{
  "vehicleType": "Car",
  "dates": [
      "2013-09-23 15:01:00",
      "2013-09-23 07:21:21",
      "2013-05-07 17:05:21",
      "2013-09-21 07:25:21"
  ]
}
```
#####Output:
```
[
    {
        "date": "2013-05-07",
        "tax": 13
    },
    {
        "date": "2013-09-21",
        "tax": 0
    },
    {
        "date": "2013-09-23",
        "tax": 31
    }
]
```
<font size="6">Build and run</font>
####Environment: _Dotnet Core 6.0_
####Restore: 
``dotnet restore``
####Build:
``dotnet build``
####Run:
`` dotnet run --project .\congestion-tax-calculator\``

<font size="6">Unit Tests</font>
![](https://github.com/AmushereTB/congestion-tax-calculator/blob/main/Img/Capture1.PNG)

<font size="6">Postman img</font>
![](https://github.com/AmushereTB/congestion-tax-calculator/blob/main/Img/Capture2.PNG)
