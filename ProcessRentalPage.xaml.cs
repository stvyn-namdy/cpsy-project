using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace CPSY_Project;

public partial class ProcessRentalPage : ContentPage
{
    private string connectionString = "server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%";

    public ProcessRentalPage()
    {
        InitializeComponent();
    }

    private async void OnRentEquipmentClicked(object sender, EventArgs e)
    {
        int.TryParse(EquipmentIdEntry.Text, out int equipmentId);
        int.TryParse(ClientIdEntry.Text, out int clientId);

        if (equipmentId > 0 && clientId > 0)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Rentals (EquipmentId, ClientId, RentalDate) VALUES (@equipmentId, @clientId, NOW())";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@equipmentId", equipmentId);
                        command.Parameters.AddWithValue("@clientId", clientId);
                        await command.ExecuteNonQueryAsync();
                        await DisplayAlert("Success", "Equipment rented successfully", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid IDs", "OK");
        }
    }
}