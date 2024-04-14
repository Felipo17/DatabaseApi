using ExchangeRates;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace ExchangeRatesGUI
{
    public partial class Form1 : Form
    {
        private ExchangeRateContext _context;

        public Form1()
        {
            InitializeComponent();
            _context = new ExchangeRateContext();
            LoadTreeView();
            LoadDatesListBox();
            LoadExchangeRatesForToday();
        }

        private void LoadExchangeRatesForToday()
        {
            var today = DateTime.Today;
            var ratesForToday = _context.ExchangeRates.Where(er => er.Date == today).ToList();
            if (!ratesForToday.Any())
            {
                buttonDownloadApi.Enabled = true;
            }
            else
            {
                LoadTreeViewWithRates(ratesForToday);
            }
        }

        private void LoadTreeViewWithRates(List<ExchangeRate> rates)
        {
            treeView1.Nodes.Clear();
            foreach (var rate in rates)
            {
                TreeNode node = new TreeNode($"{rate.Date.ToShortDateString()}: 1 USD = {rate.Rate} {rate.Currency}");
                treeView1.Nodes.Add(node);
            }
        }

        private void LoadTreeView()
        {
            var currencies = _context.ExchangeRates.Select(er => er.Currency).Distinct().ToList();
            treeView1.Nodes.Clear();

            foreach (var currency in currencies)
            {
                TreeNode currencyNode = new TreeNode(currency);
                var dates = _context.ExchangeRates.Where(er => er.Currency == currency).Select(er => er.Date).Distinct().ToList();

                foreach (var date in dates)
                {
                    TreeNode dateNode = new TreeNode(date.ToShortDateString());
                    currencyNode.Nodes.Add(dateNode);
                }
                treeView1.Nodes.Add(currencyNode);
            }
        }

        private void LoadDatesListBox()
        {
            Dates.Items.Clear();
            var dates = _context.ExchangeRates.Select(er => er.Date).Distinct().ToList();

            foreach (var date in dates)
            {
                Dates.Items.Add(date.ToShortDateString());
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                var date = DateTime.Parse(e.Node.Text);
                var currency = e.Node.Parent.Text;
                var rate = _context.ExchangeRates.FirstOrDefault(er => er.Currency == currency && er.Date == date)?.Rate;
                MessageBox.Show($"On {date.ToShortDateString()}, the exchange rate for {currency} was {rate}");
            }
        }

        private void Dates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddExchangeRate_Click(object sender, EventArgs e)
        {
            string currency = textBoxCurrency.Text.Trim().ToUpper();
            if (decimal.TryParse(textBoxExchangeRate.Text, out decimal rate) && !string.IsNullOrEmpty(currency))
            {
                _context.ExchangeRates.Add(new ExchangeRate
                {
                    Currency = currency,
                    Rate = rate,
                    Date = DateTime.Today
                });
                _context.SaveChanges();
                MessageBox.Show($"Added new currency: {currency} with rate: {rate}");
                LoadTreeView();
            }
            else
            {
                MessageBox.Show("Invalid input for currency or exchange rate");
            }
        }

        private void buttonShowDate_Click(object sender, EventArgs e)
        {
            if (Dates.SelectedItems != null)
            {
                var date = DateTime.Parse(Dates.SelectedItem.ToString());
                var rates = _context.ExchangeRates.Where(er => er.Date == date).ToList();
                treeView1.Nodes.Clear();
                foreach (var rate in rates)
                {
                    TreeNode dateNode = new TreeNode($"{date.ToShortDateString()}: 1 USD = {rate.Rate} {rate.Currency}");
                    treeView1.Nodes.Add(dateNode);
                }
            }
        }

        private async void buttonDownloadApi_Click(object sender, EventArgs e)
        {
            buttonDownloadApi.Enabled = false;
            await DownloadTodaysRates();
            LoadExchangeRatesForToday();
            buttonDownloadApi.Enabled = true;
        }

        private async Task DownloadTodaysRates()
        {
            var today = DateTime.Today;
            var httpClient = new HttpClient();
            string apiUrl = $"https://openexchangerates.org/api/historical/{today:yyyy-MM-dd}.json?app_id=f14c4f08d316469ba966b39193165986&base=USD";
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
                        _context.ExchangeRates.Add(new ExchangeRate
                        {
                            Currency = rate.Key,
                            Rate = rate.Value,
                            Date = today
                        });
                    }
                    _context.SaveChanges();
                    MessageBox.Show("Today's rates have been downloaded and added to the database");
                }
                else
                {
                    MessageBox.Show("Failed to deserialize response or no exchange rates available for today");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while fetching data from API: {ex.Message}");
                buttonDownloadApi.Enabled = true;
            }
        }

        private void buttonDeleteCurrency_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 0)
            {
                string currency = treeView1.SelectedNode.Text;
                var ratesToDelete = _context.ExchangeRates.Where(er => er.Currency == currency).ToList();
                _context.ExchangeRates.RemoveRange(ratesToDelete);
                _context.SaveChanges();
                MessageBox.Show($"Currency {currency} has been deleted");
                LoadTreeView();
            }
            else
            {
                MessageBox.Show("Please select a currency to delete");
            }
        }
    }
}