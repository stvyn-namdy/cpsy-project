using Microsoft.Maui.Controls;

namespace CPSY_Project;

public partial class MainPage : ContentPage

{
	public MainPage()

	{
		InitializeComponent();
	}

	private async void OnManageEquipmentClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManageEquipmentPage());
	}

	private async void OnManageClientsClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManageClientsPage());
	}

	private async void OnViewEquipmentClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ViewEquipmentPage());
	}

	private async void OnViewCustomerClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ViewCustomerPage());
	}

	private async void OnProcessRentalClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ProcessRentalPage());
	}
}