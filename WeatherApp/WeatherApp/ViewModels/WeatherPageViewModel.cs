using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
	public class WeatherPageViewModel: INotifyPropertyChanged
	{
		private WeatherData data;

		#region PropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
		}
		#endregion


		public WeatherData Data
		{
			get => data;
			set
			{
				data = value;
				OnPropertyChanged();
			}
		}
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
