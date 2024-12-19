using Microsoft.Maui.Controls;
using RentalManagementApp;

namespace CPSY_Project;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage());
	}
}
