namespace RESTNataliaFdez.MVVM.Views;
using RESTNataliaFdez.MVVM.ViewModels;
using RESTNataliaFdez.MVVM.Models;
using System.Diagnostics;
using System.Xml.Linq;

public partial class NuevoMusico : ContentPage
{
    /**
    * Constructor NuevoMusico
    * Inicializa la p�gina con un objeto Musico denominado MusicoSeleccionado.
    * Establece el contexto de enlace Binding Context y el SharedViewModel
    * Si el m�sico no es nulo implementa el m�todo cargar m�sico en campos
    * 
    */
    public NuevoMusico(Musician musician)
	{
		InitializeComponent();
        BindingContext = App.SharedViewModel;
        LimpiarCampos();
    }
    /**
     * M�todo CargarTareaEnCampos
     * Se encarga de guardar los datos rellenados por el usuario como propiedades
     * de la clase Tarea
     */
    private void CargarTareaEnCampos(Musician musico)
    {
        NameEntry.Text = musico.Name;
        LocationEntry.Text = musico.Location;
        GenreEntry.Text = musico.MusicGenre;
        SongNameEntry.Text = musico.SongName;
        IdEntry.Text = musico.Id.ToString();
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
     * Cuando se selecciona el bot�n cancelar, vuelve a la p�gina principal
     */
    private async void Button_Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PopAsync();
    }

    private void LimpiarCampos()
    {
        NameEntry.Text = null;
        LocationEntry.Text = null;
        GenreEntry.Text = null;
        SongNameEntry.Text = null;
        IdEntry.Text = null; // Asegurarse de que no haya m�sico seleccionada
    }


}