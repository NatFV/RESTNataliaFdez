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
	 * Método Button_Clicked, abre una nueva página para crear un nuevo músico
	 *
	 **/
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoMusico (null));
    }




    /**
   * Método Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
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
 * Método Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
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