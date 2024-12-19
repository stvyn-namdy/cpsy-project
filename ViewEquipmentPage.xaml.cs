using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace CPSY_Project;

public partial class ViewEquipmentPage : ContentPage
{
    private string connectionString = "server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%";

    private ObservableCollection<string> equipmentList = new ObservableCollection<string>();

    public ViewEquipmentPage()
    {
        InitializeComponent();
        EquipmentListView.ItemsSource = equipmentList;
        LoadEquipment();
    }

    private async void LoadEquipment()
    {
        equipmentList.Clear();
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Name, Category FROM Equipment";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string item = $"ID: {reader.GetInt32(0)}, Name:{reader.GetString(1)}, Category: {reader.GetString(2)}";
                        equipmentList.Add(item);
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