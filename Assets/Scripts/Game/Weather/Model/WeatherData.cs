using System;

[Serializable]
public class WeatherData
{
    public string Icon { get; set; }      
    public int TemperatureF { get; set; } 
    public string StartTime { get; set; }      
    public string EndTime { get; set; }    
}


[Serializable]
public class WeatherForecastResponse
{
    public Properties properties;
}

[Serializable]
public class Properties
{
    public ForecastPeriod[] periods;
}

[Serializable]
public class ForecastPeriod
{
    public int temperature;
    public string icon;
    public string startTime;
    public string endTime;
}