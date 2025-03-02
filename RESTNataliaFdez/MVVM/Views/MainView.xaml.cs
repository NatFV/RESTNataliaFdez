using RESTNataliaFdez.MVVM.ViewModels;
using System.Diagnostics;
namespace RESTNataliaFdez.MVVM.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
		BindingContext = new DataViewModel();
	}

	/**
	 * M�todo Button_Clicked, abre una nueva p�gina para crear un nuevo m�sico
	 *
	 **/
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoMusico (null));
    }




    /**
   * M�todo Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
   * Muestra el texto actualizado
   */
    private void SelectedIdEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry? entry = sender as Entry;
        if (entry is not null)
        {
            Debug.WriteLine($"Entry_TextChanged: {entry.Text}");
        }
    }

    /**
 * M�todo Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
 * Muestra el texto actualizado
 */
    private  void SelectedIdEntry_Completed(object sender, EventArgs e)
    {
        Entry? entry = sender as Entry;
        if (entry is not null)
        {
            Debug.WriteLine($"Entry_Completed: {entry.Text}");
        }
    }

    
}