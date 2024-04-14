using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ExchangeRatesGUI")]

namespace ExchangeRates
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DisplayTodaysExchangeRatesAsync();

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1: Display today's currency rates.");
                Console.WriteLine("2: Display currency rates from a specific date.");
                Console.WriteLine("3: Add a new currency with its exchange rate.");
                Console.WriteLine("4: Delete the selected currency");
                Console.WriteLine("5: Exit");
                Console.Write("Selection: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DisplayTodaysExchangeRatesAsync();
                        break;
                    case "2":
                        Console.Write("Enter the date (YYYY-MM-DD): ");
                        string dateString = Console.ReadLine();
                        if (DateTime.TryParse(dateString, out DateTime date))
                        {
                            DisplayExchangeRatesFromDate(date);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect date format:");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Specify the currency and rate (currency, rate): ");
                        string[] input = Console.ReadLine().Split(',');
                        Console.WriteLine($"Entered: '{input[0]}' for currency and '{input[1]}' for the exchange rate.");
                        if (input.Length == 2)
                        {
                            bool parseSuccess = decimal.TryParse(input[1].Trim(),
                                                                 NumberStyles.Any,
                                                                 CultureInfo.InvariantCulture,
                                                                 out decimal rate);
                            if (parseSuccess)
                            {
                                AddExchangeRate(input[0].Trim(), rate);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect input format");
                            }
                        }
                        break;
                    case "4":
                        Console.Write("Specify the currency to be deleted: ");
                        DeleteExchangeRate(Console.ReadLine().Trim());
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
        }

        public static async Task DisplayTodaysExchangeRatesAsync()
        {
            Console.WriteLine("\nExchange rates for today:");
            await GetExchangeRatesAsync(DateTime.Today);
        }

        public static async Task GetExchangeRatesAsync(DateTime date)
        {
            using var context = new ExchangeRateContext();
            var ratesForDate = context.ExchangeRates.Where(x => x.Date == date).ToList(); //pobranie wszystkich kursów dla daty z bazy

            if (ratesForDate.Any())//sprawdza czy istnieja jakiekolwiek kursy w bazie
            {
                foreach (var rate in ratesForDate)
                {
                    Console.WriteLine($"{date.ToShortDateString()}: 1 USD = {rate.Rate} {rate.Currency}");
                }
            }
            else//jak nie to pobiera
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://openexchangerates.org/api/historical/{date:yyyy-MM-dd}.json?app_id=f14c4f08d316469ba966b39193165986&base=USD";

                try
                {
                    string response = await httpClient.GetStringAsync(apiUrl);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString
                    };

                    var exchangeRateData = JsonSerializer.Deserialize<ExchangeRateResponse>(response, options);
                    if (exchangeRateData != null && exchangeRateData.Rates != null)
                    {
                        foreach (var rate in exchangeRateData.Rates)
                        {
                            Console.WriteLine($"{date.ToShortDateString()}: 1 USD = {rate.Value} {rate.Key}");
                            context.ExchangeRates.Add(new ExchangeRate
                            {
                                Currency = rate.Key,
                                Rate = rate.Value,
                                Date = date
                            });
                        }

                        context.SaveChanges();
                        Console.WriteLine("New exchange rates have been added to the database");
                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize response or no exchange rates for the given date");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving data from the API: {ex.Message}");
                }
            }
        }

        public static void DisplayExchangeRatesFromDate(DateTime date)
        {
            using var context = new ExchangeRateContext();
            var rates = context.ExchangeRates.Where(x => x.Date == date).ToList();

            if (rates.Any())
            {
                foreach (var rate in rates)
                {
                    Console.WriteLine($"{rate.Date.ToShortDateString()}: 1 USD = {rate.Rate} {rate.Currency}");
                }
            }
            else
            {
                Console.WriteLine($"No exchange rates for the date: {date.ToShortDateString()}");
            }
        }

        public static void AddExchangeRate(string currency, decimal rate)
        {
            using var context = new ExchangeRateContext();
            context.ExchangeRates.Add(new ExchangeRate
            {
                Currency = currency,
                Rate = rate,
                Date = DateTime.Today
            });
            context.SaveChanges();
            Console.WriteLine("Added new currency");
        }

        private static void DeleteExchangeRate(string currency)
        {
            using var context = new ExchangeRateContext();
            var rateToDelete = context.ExchangeRates.FirstOrDefault(r => r.Currency == currency && r.Date == DateTime.Today);
            if (rateToDelete != null)
            {
                context.ExchangeRates.Remove(rateToDelete);
                context.SaveChanges();
                Console.WriteLine("Deleted currency");
            }
            else
            {
                Console.WriteLine("Could't find a currency to delete");
            }
        }

        /*private static void ShowLatestExchangeRateDate()
        {
            using var context = new ExchangeRateContext();
            var latestDate = context.ExchangeRates.Max(r => r.Date);
            Console.WriteLine(latestDate.ToString("d"));
        }*/
    }
}