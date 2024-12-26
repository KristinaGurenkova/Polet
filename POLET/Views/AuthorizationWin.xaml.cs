using System;
using System.Collections.Generic;
using System.Data;
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
using Npgsql;

namespace POLET.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWin.xaml
    /// </summary>
    public partial class AuthorizationWin : Window
    {
		static string connectionString = "Server=localhost;Port=5432;Database=Polet2; User Id = postgres; Password=1234;";
		public AuthorizationWin()
        {
            InitializeComponent();
        }
		private void logIn_Click(object sender, RoutedEventArgs e)
		{
			NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
			npgsqlConnection.Open();
			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = npgsqlConnection;
			command.CommandType = CommandType.Text;
			command.CommandText = @"
			SELECT users.*, roles.namerole 
			FROM users 
			INNER JOIN roles ON users.idrole = roles.idrole
			WHERE login = @username AND pass = @password";

			// Добавление параметров в команду SQL
			command.Parameters.AddWithValue("@username", loginBox.Text);
			command.Parameters.AddWithValue("@password", passBox.Password);

				// Выполнение команды и обработка результатов
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read()) // Если пользователь найден
					{
						// Получение роли пользователя
						var role = reader.GetString(4);

						// Открытие соответствующего окна в зависимости от роли пользовател
						switch (role)
						{
							case "Администратор":
								// Открываем окно для админа
								AdminWin adminWin = new AdminWin();
								adminWin.Show();
								Close();
							break;
						}
					}
					else // Если пользователь не найден
					{
						// Вывод сообщения об ошибке
						MessageBox.Show("Неверный логин или пароль");
					}
				}
			}
		}
    }