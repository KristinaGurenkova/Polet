using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Npgsql;

namespace POLET.Classes
{
	internal class DatabaseQueries
	{
		private ObservableCollection<Flights> _flights = new ObservableCollection<Flights> { };

		static string connectionString = "Server=localhost;Port=5432;Database=Polet2; User Id = postgres; Password=1234;";
		public ObservableCollection<Flights> GetFlights()
		{
			NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
			npgsqlConnection.Open();
			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = npgsqlConnection;
			command.CommandType = CommandType.Text;
			command.CommandText = @"
			SELECT flights.*, transfers.namecity 
			FROM public.flights 
			LEFT JOIN transfers ON flights.idtransfer = transfers.idtransfer
			ORDER BY flights.idflight";
			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var flights = new Flights
					{
						idflight = reader.GetInt32(0),
						numberseats = reader.GetInt32(1),
						fromflight = reader.GetString(2),
						whereflight = reader.GetString(3),
						datefrom = reader.GetDateTime(4),
						timefrom = reader.GetTimeSpan(5),
						datewhere = reader.GetDateTime(6),
						timewhere = reader.GetTimeSpan(7),
						idtransfer = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
						price = reader.GetInt32(9),
						Transfers = reader.IsDBNull(10) ? null : new Transfers { namecity = reader.GetString(10)}
					};
					_flights.Add(flights); 
				}
			}
			npgsqlConnection.Close();
			return _flights;
		}

		public static ObservableCollection<Flights> FindFlightsBySubstring(string fromFlight, string whereFlight, string dateFrom, string numberSeats)
		{
				ObservableCollection<Flights> foundFlights = new ObservableCollection<Flights>();

				using (var npgsqlConnection = new NpgsqlConnection(connectionString))
				{
					npgsqlConnection.Open();

					var command = npgsqlConnection.CreateCommand();
					command.CommandText = @"SELECT flights.*, transfers.namecity 
					FROM public.flights
					LEFT JOIN transfers ON flights.idtransfer = transfers.idtransfer
					WHERE fromflight ILIKE @fromFlight 
					AND whereflight ILIKE @whereFlight 
					AND datefrom = @dateFrom::DATE 
					AND numberseats >= @numberSeats";

					command.Parameters.AddWithValue("@fromFlight", "%" + fromFlight + "%");
					command.Parameters.AddWithValue("@whereFlight", "%" + whereFlight + "%");
					command.Parameters.AddWithValue("@dateFrom", dateFrom);
					command.Parameters.AddWithValue("@numberSeats", int.Parse(numberSeats));

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var flight = new Flights
							{
								idflight = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
								numberseats = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
								fromflight = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
								whereflight = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
								datefrom = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
								timefrom = reader.IsDBNull(5) ? TimeSpan.Zero : reader.GetTimeSpan(5),
								datewhere = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
								timewhere = reader.IsDBNull(7) ? TimeSpan.Zero : reader.GetTimeSpan(7),
								idtransfer = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
								price = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
								Transfers = reader.IsDBNull(10) ? null : new Transfers { namecity = reader.GetString(10)}
							};
							foundFlights.Add(flight);
						}
					}
				}
				return foundFlights;
		}
		public static bool BookFlight(int flightId)
		{
			bool success = false;

			using (var npgsqlConnection = new NpgsqlConnection(connectionString))
			{
				npgsqlConnection.Open();

				var command = npgsqlConnection.CreateCommand();
				command.CommandText = "UPDATE public.flights SET numberSeats = numberSeats - 1 WHERE idFlight = @flightId AND numberSeats > 0";
				command.Parameters.AddWithValue("@flightId", flightId);

				try
				{
					int rowsAffected = command.ExecuteNonQuery();
					success = rowsAffected > 0;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
			return success;
		}
		public static bool AddPassenger(Passengers passenger)
		{
			bool success = false;
			using (var npgsqlConnection = new NpgsqlConnection(connectionString))
			{
				npgsqlConnection.Open();

				var command = npgsqlConnection.CreateCommand();
				command.CommandText = @"
				INSERT INTO public.passengers (namePassenger, surnamePassenger, patronymicPassenger, counttickets, idflights, email)
				VALUES (@name, @surname, @patronymic, @counttickets, @idflights, @email)
				RETURNING idPassenger";

				command.Parameters.AddWithValue("@name", passenger.namepassenger);
				command.Parameters.AddWithValue("@surname", passenger.surnamepassenger);
				command.Parameters.AddWithValue("@patronymic", string.IsNullOrEmpty(passenger.patronymicpassenger) ? DBNull.Value : (object)passenger.patronymicpassenger);
				command.Parameters.AddWithValue("@counttickets", passenger.counttickets);
				command.Parameters.AddWithValue("@idflights", passenger.idflights);
				command.Parameters.AddWithValue("@email", passenger.email);

				try
				{
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						passenger.idpassenger = reader.GetInt32(0);
						success = true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при добавлении пассажира: " + ex.Message);
				}
			}
			return success;
		}
		public static bool DeleteFlights(int idFlights)
		{
			bool success = false;

			using (var npgsqlConnection = new NpgsqlConnection(connectionString))
			{
				npgsqlConnection.Open();

				var command = npgsqlConnection.CreateCommand();
				command.CommandText = @"
				DELETE FROM public.passengers
				WHERE idflights = @idFlights";

				command.Parameters.AddWithValue("@idFlights", idFlights);

				try
				{
					var rowsAffectedPassengers = command.ExecuteNonQuery();

					command.CommandText = @"
					DELETE FROM public.flights
					WHERE idflight = @idFlights";

					var rowsAffectedFlights = command.ExecuteNonQuery();
					if (rowsAffectedFlights > 0)
					{
						success = true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при удалении рейса и пассажиров: " + ex.Message);
				}
			}
			return success;
		}
		public static void AddFlight(string fromFlight, string whereFlight, string dateFrom, string timeFrom, string dateWhere, string timeWhere, string numberSeats, string transfers, string price)
		{
			if (string.IsNullOrWhiteSpace(fromFlight) || string.IsNullOrWhiteSpace(whereFlight) ||
				string.IsNullOrWhiteSpace(dateFrom) || string.IsNullOrWhiteSpace(timeFrom) ||
				string.IsNullOrWhiteSpace(dateWhere) || string.IsNullOrWhiteSpace(timeWhere) ||
				string.IsNullOrWhiteSpace(numberSeats) || string.IsNullOrWhiteSpace(price))
			{
				throw new ArgumentException("Пожалуйста, заполните все обязательные поля.");
			}

			TimeSpan timeFromValue;
			TimeSpan timeWhereValue;
			int numberSeatsValue;
			int priceValue;
			int? transfersValue = null;

			if (!TimeSpan.TryParse(timeFrom, out timeFromValue))
			{
				throw new ArgumentException("Неверный формат времени вылета. Пожалуйста, используйте формат ЧЧ:ММ.");
			}

			if (!TimeSpan.TryParse(timeWhere, out timeWhereValue))
			{
				throw new ArgumentException("Неверный формат времени прибытия. Пожалуйста, используйте формат ЧЧ:ММ.");
			}

			if (!int.TryParse(numberSeats, out numberSeatsValue))
			{
				throw new ArgumentException("Неверный формат количества мест.");
			}

			if (!int.TryParse(price, out priceValue))
			{
				throw new ArgumentException("Неверный формат цены.");
			}

			using (var npgsqlConnection = new NpgsqlConnection(connectionString))
			{
				npgsqlConnection.Open();

				if (!string.IsNullOrWhiteSpace(transfers))
				{
					var checkTransferCommand = npgsqlConnection.CreateCommand();
					checkTransferCommand.CommandText = "SELECT idtransfer FROM public.transfers WHERE namecity = @nameCity";
					checkTransferCommand.Parameters.AddWithValue("@nameCity", transfers);

					using (var reader = checkTransferCommand.ExecuteReader())
					{
						if (reader.Read())
						{
							transfersValue = reader.GetInt32(0);
						}
					}

					if (transfersValue == null)
					{
						var addTransferCommand = npgsqlConnection.CreateCommand();
						addTransferCommand.CommandText = "INSERT INTO public.transfers (namecity) VALUES (@nameCity) RETURNING idtransfer";
						addTransferCommand.Parameters.AddWithValue("@nameCity", transfers);
						transfersValue = (int)addTransferCommand.ExecuteScalar();
					}
				}

				var addFlightCommand = npgsqlConnection.CreateCommand();
				addFlightCommand.CommandText = @"
				INSERT INTO public.flights (fromFlight, whereFlight, dateFrom, timeFrom, dateWhere, timeWhere, numberSeats, idTransfer, price)
				VALUES (@fromFlight, @whereFlight, @dateFrom::DATE, @timeFrom, @dateWhere::DATE, @timeWhere, @numberSeats, @idTransfer, @price)";

				addFlightCommand.Parameters.AddWithValue("@fromFlight", fromFlight);
				addFlightCommand.Parameters.AddWithValue("@whereFlight", whereFlight);
				addFlightCommand.Parameters.AddWithValue("@dateFrom", dateFrom);
				addFlightCommand.Parameters.AddWithValue("@timeFrom", timeFromValue);
				addFlightCommand.Parameters.AddWithValue("@dateWhere", dateWhere);
				addFlightCommand.Parameters.AddWithValue("@timeWhere", timeWhereValue);
				addFlightCommand.Parameters.AddWithValue("@numberSeats", numberSeatsValue);
				addFlightCommand.Parameters.AddWithValue("@idTransfer", (object)transfersValue ?? DBNull.Value);
				addFlightCommand.Parameters.AddWithValue("@price", priceValue);

				addFlightCommand.ExecuteNonQuery();
			}
		}
		public static void UpdateFlight(Flights flight)
		{
			if (flight == null) throw new ArgumentNullException(nameof(flight));
			if (string.IsNullOrWhiteSpace(flight.fromflight) || string.IsNullOrWhiteSpace(flight.whereflight) ||
				flight.datefrom == DateTime.MinValue || flight.timefrom == TimeSpan.Zero ||
				flight.datewhere == DateTime.MinValue || flight.timewhere == TimeSpan.Zero ||
				flight.numberseats <= 0 || flight.price <= 0)
			{
				throw new ArgumentException("Пожалуйста, заполните все обязательные поля.");
			}

			using (var npgsqlConnection = new NpgsqlConnection(connectionString))
			{
				npgsqlConnection.Open();

				int? transfersValue = null;
				if (flight.Transfers != null && !string.IsNullOrWhiteSpace(flight.Transfers.namecity))
				{
					var checkTransferCommand = npgsqlConnection.CreateCommand();
					checkTransferCommand.CommandText = "SELECT idtransfer FROM public.transfers WHERE namecity = @nameCity";
					checkTransferCommand.Parameters.AddWithValue("@nameCity", flight.Transfers.namecity);

					using (var reader = checkTransferCommand.ExecuteReader())
					{
						if (reader.Read())
						{
							transfersValue = reader.GetInt32(0);
						}
					}

					if (transfersValue == null)
					{
						var addTransferCommand = npgsqlConnection.CreateCommand();
						addTransferCommand.CommandText = "INSERT INTO public.transfers (namecity) VALUES (@nameCity) RETURNING idtransfer";
						addTransferCommand.Parameters.AddWithValue("@nameCity", flight.Transfers.namecity);
						transfersValue = (int)addTransferCommand.ExecuteScalar();
					}
				}

				var updateFlightCommand = npgsqlConnection.CreateCommand();
				updateFlightCommand.CommandText = @"
				UPDATE public.flights
				SET fromFlight = @fromFlight, whereFlight = @whereFlight, dateFrom = @dateFrom::DATE, timeFrom = @timeFrom, dateWhere = @dateWhere::DATE, timeWhere = @timeWhere, numberSeats = @numberSeats, idTransfer = @idTransfer, price = @price
				WHERE idFlight = @flightId";

				updateFlightCommand.Parameters.AddWithValue("@flightId", flight.idflight);
				updateFlightCommand.Parameters.AddWithValue("@fromFlight", flight.fromflight);
				updateFlightCommand.Parameters.AddWithValue("@whereFlight", flight.whereflight);
				updateFlightCommand.Parameters.AddWithValue("@dateFrom", flight.datefrom);
				updateFlightCommand.Parameters.AddWithValue("@timeFrom", flight.timefrom);
				updateFlightCommand.Parameters.AddWithValue("@dateWhere", flight.datewhere);
				updateFlightCommand.Parameters.AddWithValue("@timeWhere", flight.timewhere);
				updateFlightCommand.Parameters.AddWithValue("@numberSeats", flight.numberseats);
				updateFlightCommand.Parameters.AddWithValue("@idTransfer", (object)transfersValue ?? DBNull.Value);
				updateFlightCommand.Parameters.AddWithValue("@price", flight.price);

				updateFlightCommand.ExecuteNonQuery();
			}
		}
	}
}
