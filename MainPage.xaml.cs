using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;

namespace RentalManagementApp
{
	public partial class MainPage : ContentPage
	{
		private const string ConnectionString = "Server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%;";

		public ObservableCollection<string> EquipmentList { get; set; } = new();
		public ObservableCollection<string> ClientList { get; set; } = new();

		public MainPage()
		{
			InitializeComponent();
			LoadEquipment();
			LoadClient();
		}

		private void LoadEquipment()
		{
			try
			{
				EquipmentList.Clear();
				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("Select name FROM Equipment", connection);
				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
					EquipmentList.Add(reader.GetString(0));
				}

				EquipmentListView.ItemsSource = EquipmentList;
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Faailed to load equipment: {ex.Message}", "OK");
			}
		}

		private void LoadClient()
		{

			try
			{
				ClientList.Clear();
				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("SELECT CONCAT(first_name, ' ', last_name) FROM Customers", connection);
				using var reader = command.ExecuteReader();
				while (reader.Read())
				{
					ClientList.Add(reader.GetString(0));
				}

				ClientListView.ItemsSource = ClientList;
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Failed to load clients: {ex.Message}", "OK");
			}

		}

		private void AddEquipment(object sender, EventArgs e)
		{

			try
			{
				string name = EquipmentNameEntry.Text;
				string description = EquipmentDescriptionEntry.Text;
				decimal dailyRate = decimal.Parse(EquipmentRateEntry.Text);

				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("INSERT INTO Equipment (name, description, daily_rate) VALUES (@name, @description, @rate)", connection);
				command.Parameters.AddWithValue("@name", name);
				command.Parameters.AddWithValue("@description", description);
				command.Parameters.AddWithValue("@rate", dailyRate);

				LoadEquipment();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Failed to add equipment: {ex.Message}", "OK");
			}
		}

		private void DeleteEquipment(object sender, EventArgs e)
		{

			try
			{
				string name = EquipmentNameEntry.Text;

				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("DELETE FROM Equipment WHERE name = @name", connection);
				command.Parameters.AddWithValue("@name", name);
				command.ExecuteNonQuery();

				LoadEquipment();
			}
			catch (System.Exception)
			{
				DisplayAlert("Error", $"Failed to delete equipment: {ex.Message}", "OK");
			}
			
		}

		private void AddClient(object sender, EventArgs e)
		{

			try
			{
				string firstName = ClientFirstNameEntry.Text;
				string lastName = ClientLastNameEntry.Text;
				string phone = ClientPhoneEntry.Text;
				string email = ClientEmailEntry.Text;

				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("INSERT INTO Customers (first_name, last_name, contact_phone, email) VALUES (@firstName, @lastName, @phone, @eMail)", connection);
				command.Parameters.AddWithValue("@firstName", firstName);
				command.Parameters.AddWithValue("@lastName", lastName);
				command.Parameters.AddWithValue("@phone", phone);
				command.Parameters.AddWithValue("@eMail", email);

				LoadClient();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Failed to add client: {ex.Message}", "OK");
			}
			
		}

		private void RentEquipment(object sender, EventArgs e)
		{
			try
			{
				int customerId = int.Parse(ClientIdEntry.Text);
				int equipmentId = int.Parse(EquipmentIdEntry.Text);
				DateTime rentalDate = RentalDatePicker.Date;
				DateTime returnDate = RentalDatePicker.Date;
				decimal cost = decimal.Parse(RentalCostEntry.Text);

				using var connection = new MySqlConnection(ConnectionString);
				connection.Open();

				var command = new MySqlCommand("INSERT INTO Rentals (customer_id, equipment_id, rental_date, return_date, cost) VALUES (@customerId, @equipmentId, @rentalDate, @returnDate, @cost)", connection);
				command.Parameters.AddWithValue("@customerId", customerId);
				command.Parameters.AddWithValue("@equipmentId", equipmentId);
				command.Parameters.AddWithValue("@rentalDate", rentalDate);
				command.Parameters.AddWithValue("@returnDate", returnDate);
				command.Parameters.AddWithValue("@cost", cost);
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Failed to process rental: {ex.Message}", "OK");
			}
		}
	}
}