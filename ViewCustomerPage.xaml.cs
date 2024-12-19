using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace CPSY_Project;

public partial class ViewCustomerPage : ContentPage
{
    private string connectionString = "server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%";

    private ObservableCollection<string> clientList = new ObservableCollection<string>();

    public ViewCustomerPage()
    {
        InitializeComponent();
        CustomerListView.ItemsSource = clientList;
        LoadCustomers();
    }

    private async void LoadCustomers()
    {
        clientList.Clear();
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT Id, First_Name, Last_Name, Contact_phone FROM Customers";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string item = $"ID: {reader.GetInt32(0)}, First_Name:{reader.GetString(1)}, Last_Name: {reader.GetString(2)}, Contact_phone: {reader.GetString(3)}";
                        clientList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}