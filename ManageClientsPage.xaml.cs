using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace CPSY_Project;

public partial class ManageClientsPage : ContentPage
{
    private string connectionString = "server=localhost;Database=RentalManagement;Uid=root;Pwd=Gadgethaven16%";

    public ManageClientsPage()
    {
        InitializeComponent();
    }

        private async void OnAddCientClicked(object sender, EventArgs e)
    {
        string name = CustomerNameEntry.Text;
        string contact = CustomerContactEntry.Text;

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(contact))
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Clients (Name, Contact) VALUES (@name, @contact)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@contact", contact);
                        await command.ExecuteNonQueryAsync();
                        await DisplayAlert("Success", "Client added successfully.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }

     private async void OnDeleteClientClicked(object sender, EventArgs e)
    {
        int.TryParse(CustomerIdEntry.Text, out int clientId);

        if (clientId > 0)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM Equipment WHERE Id = @id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientId);
                        int rows = await command.ExecuteNonQueryAsync();

                        if (rows > 0)
                         await DisplayAlert("Success", "Client deleted successfully", "OK");
                         else
                         await DisplayAlert("Error", "Client not found with that ID", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}