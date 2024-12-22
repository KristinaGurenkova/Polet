using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MahApps.Metro.Controls;
using Npgsql;
using POLET.Classes;

namespace POLET.Views
{
    /// <summary>
    /// Логика взаимодействия для AddFlightWin.xaml
    /// </summary>
    public partial class AddFlightWin : Window
    {
		private DatabaseQueries _flightService;
		private ObservableCollection<Flights> Flights = new ObservableCollection<Flights> { };
		public AddFlightWin()
        {
            InitializeComponent();
        }

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DatabaseQueries.AddFlight(
					WhereFromBox.Text,
					WhereToBox.Text,
					DatePicker.SelectedDate.HasValue ? DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : null,
					TimeFromBox.Text,
					DatePicker2.SelectedDate.HasValue ? DatePicker2.SelectedDate.Value.ToString("yyyy-MM-dd") : null,
					TimeWhereBox.Text,
					CountSeats.Text,
					TransfersBox.Text,
					PriceBox.Text
				);
				MessageBox.Show("Рейс успешно добавлен!");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при добавлении рейса: {ex.Message}");
			}
		}
	}
}
