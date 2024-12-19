using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace CPSY_Project;

public partial class ManageEquipmentPage : ContentPage
{
    private string connectionString = "server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%";

    public ManageEquipmentPage()
    {
        InitializeComponent();
    }

    private async void OnAddEquipmentClicked(object sender, EventArgs e)
    {
        string name = EquipmentNameEntry.Text;
        string category = EquipmentCategory.Text;

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(category))
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Equipment (Name, Category) VALUES (@name, @category)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@category", category);
                        await command.ExecuteNonQueryAsync();
                        await DisplayAlert("Success", "Equipment added successfully.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }

    private async void OnDeleteEquipmentClicked(object sender, EventArgs e)
    {
        int.TryParse(EquipmentIdEntry.Text, out int equipmentId);

        if (equipmentId > 0)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM Equipment WHERE Id = @id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", equipmentId);
                        int rows = await command.ExecuteNonQueryAsync();

                        if (rows > 0)
                         await DisplayAlert("Success", "Equipment deleted successfully", "OK");
                         else
                         await DisplayAlert("Error", "Equipment not found with that ID", "OK");
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
            await DisplayAlert("Error", "Please enter a valid Equipment ID.", "OK");
        }
    }
}