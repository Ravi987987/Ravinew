﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemTextJsonSamples
{
    public class Callbacks
    {
        public static void Run()
        {
            string jsonString;
            var wf = WeatherForecastFactories.CreateWeatherForecast();
            wf.DisplayPropertyValues();

            // <SnippetSerialize>
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new WeatherForecastCallbacksConverter());
            serializeOptions.WriteIndented = true;
            jsonString = JsonSerializer.Serialize(wf, serializeOptions);
            // </SnippetSerialize>

            Console.WriteLine($"JSON output:\n{jsonString}\n");
            //jsonString = @"{""Date"": null,""TemperatureCelsius"": 25,""Summary"":""Hot""}";

            // <SnippetDeserialize>
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new WeatherForecastCallbacksConverter());
            wf = JsonSerializer.Deserialize<WeatherForecast>(jsonString, deserializeOptions);
            // <SnippetDeserialize>

            wf.DisplayPropertyValues();
        }
    }
   
}
