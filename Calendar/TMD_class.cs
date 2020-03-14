using System;
using System.Collections.Generic;

[Serializable]
public class TMD_class
{
    public List<WeatherForecast> WeatherForecasts { get; set; }

    public class Location
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Data
    {
        public int cond { get; set; }
        public double rain { get; set; }
        public double tc_max { get; set; }
        public double tc_min { get; set; }
        public double rh { get; set; }
        public double ws10m { get; set; }
    }

    public class Forecast
    {
        public DateTime time { get; set; }
        public Data data { get; set; }
    }

    public class WeatherForecast
    {
        public Location location { get; set; }
        public List<Forecast> forecasts { get; set; }
    }

}
