using POLET.Views;
using POLET.Classes;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Collections.ObjectModel;

namespace POLET
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DatabaseQueries _flightService;
		private ObservableCollection<Flights> Flights = new ObservableCollection<Flights> { };

		public MainWindow()
		{
			InitializeComponent();
			_flightService = new DatabaseQueries();
			Flights = _flightService.GetFlights();
			FlightsDataGrid.ItemsSource = Flights;
		}

		private void IncreaseButton_Click(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(NumericTextBox.Text, out int value) && value < 100)
			{
				NumericTextBox.Text = (value + 1).ToString();
			}
		}

		private void DecreaseButton_Click(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(NumericTextBox.Text, out int value) && value > 1)
			{
				NumericTextBox.Text = (value - 1).ToString();
			}
		}

		private void AdminLoginButton_Click(object sender, RoutedEventArgs e)
		{
			var authorizationWin = new AuthorizationWin();
			authorizationWin.ShowDialog();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			// Получение значений из текстовых полей
			string fromFlight = WhereFromBox.Text;
			string whereFlight = WhereToBox.Text;
			string dateFrom = DatePicker.SelectedDate.HasValue ? DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : null;
			string numberSeats = NumericTextBox.Text;

			// Проверка на заполненность обязательных полей
			if (string.IsNullOrWhiteSpace(fromFlight) || string.IsNullOrWhiteSpace(whereFlight) || string.IsNullOrWhiteSpace(dateFrom) || string.IsNullOrWhiteSpace(numberSeats))
			{
				// Составляем сообщение о недостающих данных
				string errorMessage = "Пожалуйста, введите:";
				if (string.IsNullOrWhiteSpace(fromFlight)) errorMessage += "\n - Откуда";
				if (string.IsNullOrWhiteSpace(whereFlight)) errorMessage += "\n - Куда";
				if (string.IsNullOrWhiteSpace(dateFrom)) errorMessage += "\n - Дату вылета";
				if (string.IsNullOrWhiteSpace(numberSeats)) errorMessage += "\n - Количество билетов";

				MessageBox.Show(errorMessage); // Отображение сообщения
				return;
			}

			// Поиск рейсов по введенным параметрам
			ObservableCollection<Flights> foundFlights = DatabaseQueries.FindFlightsBySubstring(fromFlight, whereFlight, dateFrom, numberSeats);

			if (foundFlights.Count == 0) // Если коллекция пуста
			{
				MessageBox.Show("По вашему запросу ничего не найдено."); // Отображение сообщения
			}
			else
			{
				// Установка найденных рейсов в качестве источника данных для DataGrid
				FlightsDataGrid.ItemsSource = foundFlights;
			}
		}

		private void ReservationButton_Click(object sender, RoutedEventArgs e)
		{
			// Получение значений из текстовых полей
			string name = NameBox.Text;
			string surname = SurnameBox.Text;
			string patronymic = PatronymicBox.Text;
			string email = null;

			if (MailBox.Text.Contains('@'))
			{
				email = MailBox.Text;
			}
			else
			{
				MessageBox.Show("Почта должна содержать собачку.");
				return;
			}
			// Проверка ввода данных
			if (string.IsNullOrWhiteSpace(name) && ContainsInvalidCharacters(name) || string.IsNullOrWhiteSpace(surname) && ContainsInvalidCharacters(surname) || !string.IsNullOrWhiteSpace(patronymic) && ContainsInvalidCharacters(patronymic))
			{
				MessageBox.Show("Пожалуйста, введите корректные данные. Имя, фамилия и отчество не должны содержать цифр и лишних символов.");
				return;
			}
			// Получение выбранного рейса из DataGrid
			if (FlightsDataGrid.SelectedItem is Flights selectedFlight)
			{
				// Проверка наличия свободных мест
				if (selectedFlight.numberseats <= 0)
				{
					MessageBox.Show("На выбранном рейсе нет свободных мест.");
					return;
				}

				// Создание объекта пассажира
				var passenger = new Passengers
				{
					namepassenger = name,
					surnamepassenger = surname,
					patronymicpassenger = patronymic,
					counttickets = 1, // Предполагаем, что бронируем 1 билет за раз
					idflights = selectedFlight.idflight,
					email = email
				};

				// Добавление пассажира в базу данных
				bool passengerAdded = DatabaseQueries.AddPassenger(passenger);

				if (passengerAdded)
				{
					
					// Обновление количества свободных мест в базе данных
					bool bookingSuccess = DatabaseQueries.BookFlight(selectedFlight.idflight);

					if (bookingSuccess)
					{
						selectedFlight.numberseats--; // Уменьшаем количество свободных мест локально
						FlightsDataGrid.Items.Refresh(); // Обновляем DataGrid

						MessageBox.Show("Бронирование успешно выполнено!");
					}
					else
					{
						MessageBox.Show("Произошла ошибка при бронировании. Попробуйте снова.");
					}
				}
				else
				{
					MessageBox.Show("Произошла ошибка при добавлении пассажира. Попробуйте снова.");
				}
			}
			else
			{
				MessageBox.Show("Пожалуйста, выберите рейс из списка.");
			}
		}
		private bool ContainsInvalidCharacters(string input)
		{
			foreach (char c in input)
			{
				if (!char.IsLetter(c) && c != '-')
				{
					return true;
				}
			}
			return false;
		}
	}
}