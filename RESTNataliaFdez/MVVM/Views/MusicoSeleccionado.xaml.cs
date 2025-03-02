namespace RESTNataliaFdez.MVVM.Views;
using RESTNataliaFdez.MVVM.ViewModels;
using RESTNataliaFdez.MVVM.Models;
using System.Diagnostics;

public partial class MusicoSeleccionado : ContentPage
{
	public MusicoSeleccionado(Musician musicoSeleccionado)
	{
        InitializeComponent();
        BindingContext = App.SharedViewModel;
        //Hemos de asignarle al m�sico seleccionado el m�sico del sharedviewmodel
        App.SharedViewModel.musicoSeleccionado = musicoSeleccionado;
        CargarTareaEnCampos(musicoSeleccionado);
       
    }
    /**
     * M�todo CargarTareaEnCampos
     * Se encarga de guardar los datos rellenados por el usuario como propiedades
     * de la clase Tarea
     */
    private void CargarTareaEnCampos(Musician musicoSeleccionado)
    {
        NameEntry.Text = musicoSeleccionado.Name;
        LocationEntry.Text = musicoSeleccionado.Location;
        GenreEntry.Text = musicoSeleccionado.MusicGenre;
        SongNameEntry.Text = musicoSeleccionado.SongName;
        IdEntry.Text = musicoSeleccionado.Id.ToString();
    }


    /**
   * M�todo Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
   * Muestra el texto actualizado
   */
    private void NombreEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry? entry = sender as Entry;
        if (entry is not null)
        {
            Debug.WriteLine($"Entry_TextChanged: {entry.Text}");
        }

    }
    /**
     * M�todo que se ejecuta cuando el usuario finaliza la edici�n de un Entry
     */
    private void NombreEntry_Completed(object sender, EventArgs e)
    {
        Entry? entry = sender as Entry;
        if (entry is not null)
        {
            Debug.WriteLine($"Entry_Completed: {entry.Text}");
        }
    }

    /**
     * Bot�n cancelar
     * Cuando se hace click en el bot�n cancelar, vuelve a la p�gina inicial
     */
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}