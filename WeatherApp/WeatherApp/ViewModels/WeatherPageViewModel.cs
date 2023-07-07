using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
	public class WeatherPageViewModel
	{
        public WeatherData Data { get; set; }
        public ICommand SearchCommand { get; set; }

        public WeatherPageViewModel()
        {
            SearchCommand = new Command(async (searchTerm) =>
            {
                await GetData("https://api.weatherbit.io/v2.0/");
            });
		}

		private async Task GetData(string url)
		{
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws and exception if there is an error

            var jsonResult = await response.Content.ReadAsStringAsync();

			// We need a WeatherData Object, from jsonResult
			var result = JsonConvert.DeserializeObject<WeatherData>(jsonResult);
            Data = result;
		}
	}
}
