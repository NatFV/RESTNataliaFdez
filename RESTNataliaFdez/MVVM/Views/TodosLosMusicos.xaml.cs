using RESTNataliaFdez.MVVM.Models;
using RESTNataliaFdez.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace RESTNataliaFdez.MVVM.Views;

public partial class TodosLosMusicos : ContentPage
{
    public ObservableCollection<Musician> Musicians { get; set; } // Propiedad para almacenar la lista
    public TodosLosMusicos(ObservableCollection<Musician> Musicians)
	{
		InitializeComponent();
        BindingContext = App.SharedViewModel;
        App.SharedViewModel.Musicians = Musicians;

    }
   
}