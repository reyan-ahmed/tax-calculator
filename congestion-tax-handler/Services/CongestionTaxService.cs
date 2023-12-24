namespace congestion_tax_handler.Services;

public class CongestionTaxService : ICongestionTaxService
{
    public int GetTotalTax(CongestionTaxRequest congestionTaxRequest)
    {
        if (IsTollFreeVehicle(VehicleType(congestionTaxRequest.VehicleTypeId)))
            return 0;

        var dateTimeAndTax = new List<CongestionDateTax>();

        foreach (var date in congestionTaxRequest.Dates.Distinct().OrderBy(x => x.Date))
            dateTimeAndTax.Add(new CongestionDateTax
            {
                Date = date,
                Tax = GetTollFee(date)
            });

        if (Cities.Gothenburg == GetCity(congestionTaxRequest.CityId))
        {
            return ApplySingleChargeRule(dateTimeAndTax).Sum(x => x.Tax);
        }
        else
        {
            return dateTimeAndTax.Sum(x => x.Tax);
        }

    }

    private List<CongestionDateTax> ApplySingleChargeRule(List<CongestionDateTax> dateTimeAndTax)
    {
        for (int i = 0; i < dateTimeAndTax.Count(); i++)
        {
            for (int j = 0; j < dateTimeAndTax.Count(); j++)
            {
                if (i != j)
                {
                    if (dateTimeAndTax[i].Date.Date == dateTimeAndTax[j].Date.Date)
                    {
                        int totalMinutes = (int)(dateTimeAndTax[j].Date - dateTimeAndTax[i].Date).TotalMinutes;
                        if (totalMinutes < 60 && totalMinutes > 0)
                        {
                            if (dateTimeAndTax[i].Tax > dateTimeAndTax[j].Tax)
                            {
                                dateTimeAndTax.RemoveAt(j);
                                ApplySingleChargeRule(dateTimeAndTax);
                            }
                            else
                            {
                                dateTimeAndTax.RemoveAt(i);
                                ApplySingleChargeRule(dateTimeAndTax);
                            }
                        }
                    }
                }

            }
        }
        return dateTimeAndTax;
    }
    private static Cities GetCity(Guid cityId) => cityId.ToString().ToLower() switch
    {
        "ec518ee0-81ee-4728-89a0-d0c88d5cfd3b" => Cities.Gothenburg,
        "0a4fbeff-11bb-416f-a7ea-03d1ed282728" => Cities.Stockholm,
        _ => throw new ArgumentOutOfRangeException(nameof(cityId), $"Not expected city value: {cityId}"),
    };


    private static IVehicle VehicleType(Guid vehicleId) => vehicleId.ToString().ToLower() switch
    {
        "3878df9a-c2ef-41ff-b62b-2bfcfcf6e5dc" => new Car(),
        "90f20bee-4bb9-47fa-8e44-d2a95aa76f2f" => new Motorbike(),
        "d08de386-9211-42c8-b57e-c5b52d9c9efb" => new Tractor(),
        _ => throw new ArgumentOutOfRangeException(nameof(vehicleId), $"Not expected vehicle value: {vehicleId}"),
    };


    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

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

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    private int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

}
