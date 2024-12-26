using Npgsql;
using POLET.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace POLET
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			CreateTable();
		}
		private void CreateTable()
		{
			string connectionString = "Server=localhost;Port=5432;Database=Polet2; User Id = postgres; Password=1234;";
			try
			{
				NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
				npgsqlConnection.Open();
				NpgsqlCommand command = new NpgsqlCommand();
				command.Connection = npgsqlConnection;
				command.CommandType = CommandType.Text;
				command.CommandText = @"CREATE TABLE IF NOT EXISTS roles (
					idrole SERIAL PRIMARY KEY,
					namerole VARCHAR(50)
					);";
				command.ExecuteNonQuery();
				command.CommandText = @"CREATE TABLE IF NOT EXISTS transfers (
				   idtransfer SERIAL PRIMARY KEY,
				   nameсity VARCHAR(50)
				   );";
				command.ExecuteNonQuery();
				command.CommandText = @"CREATE TABLE IF NOT EXISTS users (
				   iduser SERIAL PRIMARY KEY,
				   login VARCHAR(50),
				   pass VARCHAR(100),
				   idrole INTEGER REFERENCES roles(idrole)
			       );";
				command.ExecuteNonQuery();
				command.CommandText = @"CREATE TABLE IF NOT EXISTS flights (
				   idflight SERIAL PRIMARY KEY,
				   numberseats INTEGER,
				   fromflight VARCHAR(50),
				   whereflight VARCHAR(50),
				   datefrom DATE,
				   timefrom TIME,
				   datewhere DATE,
				   timewhere TIME,
				   idtransfer INTEGER REFERENCES transfers(idtransfer),
				   price INTEGER
				   );";
				command.ExecuteNonQuery();
				command.CommandText = @"CREATE TABLE IF NOT EXISTS passengers (
				   idpassenger SERIAL PRIMARY KEY,
				   namepassenger VARCHAR(50),
				   surnamepassenger VARCHAR(50),
				   patronymicpassenger VARCHAR(50),
				   counttickets INTEGER,
			       idflight INTEGER REFERENCES flights(idflight),
				   email VARCHAR(150) 
			       );";
				command.ExecuteNonQuery();

				command.CommandText = @"INSERT INTO public.roles (namerole) VALUES ('Администратор') ON CONFLICT (namerole) DO NOTHING";
				command.ExecuteNonQuery();
				command.CommandText = @"INSERT INTO public.users (login, pass, idrole) VALUES ('Admin',123,1) ON CONFLICT (login) DO NOTHING"; 
				command.ExecuteNonQuery();
			}
			catch 
			{}
		}
	}
}
