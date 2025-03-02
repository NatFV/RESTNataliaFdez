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
        //Hemos de asignarle al músico seleccionado el músico del sharedviewmodel
        App.SharedViewModel.musicoSeleccionado = musicoSeleccionado;
        CargarTareaEnCampos(musicoSeleccionado);
       
    }
    /**
     * Método CargarTareaEnCampos
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
   * Método Entry_TextChanged, se ejcuta cada vez que un texto del Entry cambia
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
     * Método que se ejecuta cuando el usuario finaliza la edición de un Entry
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
     * Botón cancelar
     * Cuando se hace click en el botón cancelar, vuelve a la página inicial
     */
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}