using POLET.Classes;
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
using System.Xml.Linq;

namespace POLET.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
		private DatabaseQueries _flightService;
		private ObservableCollection<Flights> Flights = new ObservableCollection<Flights> { };
		
		public AdminWin()
        {
            InitializeComponent();
            Load();
		}
        public void Load() 
        {
			_flightService = new DatabaseQueries();
			Flights = _flightService.GetFlights();
			FlightsDataGrid.ItemsSource = Flights;
		}
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
        }
		public void DeleteMethod()
		{
			if (FlightsDataGrid.SelectedItem is Flights selectedFlight)
			{

				bool updateSucces = DatabaseQueries.DeleteFlights(selectedFlight.idflight);

				if (updateSucces)
				{
					MessageBox.Show("Рейс удален");
					Load();
				}
				else
				{
					MessageBox.Show("Произошла ошибка при удален. Попробуйте снова.");
				}
			}
		}

		private void AddMenu_Click(object sender, RoutedEventArgs e)
		{
			AddFlightWin addFlightWin = new AddFlightWin();
			addFlightWin.ShowDialog();
			Load();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			DeleteMethod();
		}

		private void AddButon_Click(object sender, RoutedEventArgs e)
		{
			AddFlightWin addFlightWin = new AddFlightWin();
			addFlightWin.ShowDialog();
			Load();
		}


		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			DeleteMethod();
		}
		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (FlightsDataGrid.SelectedItem != null)
			{
				Flights selectedFlight = (Flights)FlightsDataGrid.SelectedItem;
				EditFlightWin editFlightWin = new EditFlightWin(selectedFlight);
				editFlightWin.ShowDialog();
				Load();
			}
			else
			{
				MessageBox.Show("Пожалуйста, выберите рейс для редактирования.");
			}
		}
	}
}
