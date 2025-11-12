using CounterAW4E.ViewModels;

namespace CounterAW4E.Views;

public partial class CountersPage : ContentPage
{
	public CountersPage()
	{
        InitializeComponent();

        BindingContext = new CountersViewModel();
    }
}