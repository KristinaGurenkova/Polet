using POLET.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POLET.Views
{
	/// <summary>
	/// Логика взаимодействия для EditFlightWin.xaml
	/// </summary>
	public partial class EditFlightWin : Window
	{
		private Flights _flight;
		public EditFlightWin(Flights flight)
		{
			InitializeComponent();
			_flight = flight;

			WhereFromBox.Text = _flight.fromflight;
			WhereToBox.Text = _flight.whereflight;

			// Разделение даты и времени
			DateTime dateFrom = _flight.datefrom.Date;
			DateTime dateWhere = _flight.datewhere.Date;

			// Принудительное обновление интерфейса для DatePicker
			DatePicker.Dispatcher.Invoke(() =>
			{
				DatePicker.SelectedDate = dateFrom != DateTime.MinValue ? (DateTime?)dateFrom : null;
			}, System.Windows.Threading.DispatcherPriority.Background);

			DatePicker2.Dispatcher.Invoke(() =>
			{
				DatePicker2.SelectedDate = dateWhere != DateTime.MinValue ? (DateTime?)dateWhere : null;
			}, System.Windows.Threading.DispatcherPriority.Background);

			TimeFromBox.Text = _flight.timefrom.ToString(@"hh\:mm");
			TimeWhereBox.Text = _flight.timewhere.ToString(@"hh\:mm");
			CountSeats.Text = _flight.numberseats.ToString();
			TransfersBox.Text = _flight.Transfers?.namecity;
			PriceBox.Text = _flight.price.ToString();
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				_flight.fromflight = WhereFromBox.Text;
				_flight.whereflight = WhereToBox.Text;
				_flight.datefrom = DatePicker.SelectedDate ?? DateTime.MinValue;
				_flight.timefrom = TimeSpan.Parse(TimeFromBox.Text);
				_flight.datewhere = DatePicker2.SelectedDate ?? DateTime.MinValue;
				_flight.timewhere = TimeSpan.Parse(TimeWhereBox.Text);
				_flight.numberseats = int.Parse(CountSeats.Text);
				_flight.Transfers = !string.IsNullOrWhiteSpace(TransfersBox.Text) ? new Transfers { namecity = TransfersBox.Text } : null;
				_flight.price = int.Parse(PriceBox.Text);

				DatabaseQueries.UpdateFlight(_flight);
				MessageBox.Show("Рейс успешно обновлен!");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при обновлении рейса: {ex.Message}");
			}
		}
    }
}