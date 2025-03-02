

using PropertyChanged;
using RESTNataliaFdez.MVVM.Models;
using RESTNataliaFdez.MVVM.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using System.Xml.Schema;


namespace RESTNataliaFdez.MVVM.ViewModels
{
    /**Añadimos la interfaz AddINotifyPropertyChangedInterface que 
    * notifica de los cambios de los valores de las propiedades a la UI automáticamente
    * */

    [AddINotifyPropertyChangedInterface]
    public class DataViewModel
    {
        #region PUBLIC FIELDS
        public ObservableCollection<Musician> Musicians { get; set; } = new ObservableCollection<Musician>();

        public Musician musicoSeleccionado { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
        public string MusicGenre { get; set; }
        public string SongName { get; set; }
        public string Id { get; set; }

        //Datos del ID y del mensaje de error por si no se encuentra al músico
        public string IdToSearch { get; set; }
        public string ErrorMessage { get; set; }

        //Comandos para interactuar con la UI
        public ICommand AddMusicianCommand { get; }
        public ICommand DeleteMusicianCommand { get; }
        public ICommand GetMusicianCommand { get; }
        public ICommand UpdateMusicianCommand { get; }
        public ICommand GetAllMusiciansCommand { get; }
        #endregion

        #region PRIVATE FIELDS

        //Creación del cliente para hacer peticiones Http a una API de manera que se pueda manipular información
        private HttpClient _httpClient = new HttpClient();

        //Configuración de las opciones de serialización JSON sin tener en cuenta mayúsculas ni minúsculas
        private JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web);

        //Definimos la URL base de la API
        private const String _url = "https://67a0abf75bcfff4fabe02c92.mockapi.io/api/v1/musicians";
        #endregion

        #region CONSTRUCTOR DATAVIEWMODEL
        public DataViewModel()
        {
            _httpClient = new HttpClient();  // Inicializa el HttpClient
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };  

            // Inicializar comandos
            AddMusicianCommand = new Command(async () => await AddMusician());
            DeleteMusicianCommand = new Command(async () => await DeleteMusician());
            GetMusicianCommand = new Command(async () => await GetMusician());
            UpdateMusicianCommand = new Command(async () => await UpdateMusician());
            GetAllMusiciansCommand = new Command(async () => await GetAllMusicians());
        }
        #endregion

        #region MÉTODOS
        /**
         * Método para agregar músicos a la base de datos
         */
        public async Task AddMusician()
        {

            var musician = new Musician
            {
                Name = this.Name,
                Location = this.Location,
                MusicGenre = this.MusicGenre,
                SongName = this.SongName,
                Id = this.Id  // MockAPI asignará un ID automáticamente
            };

            //Construcción de la url de la API
            var url = _url;

            //Serialización y conversión de los strings en formato json para añadirlo a la base de datos
            var json = JsonSerializer.Serialize(musician, _jsonOptions);

            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            //Petición POST a la API usando httpClient
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                //Actualización de la lista local "Musicians"
                Musicians.Add(musician); 

            }
            else
            {
                Debug.WriteLine($"Error al agregar músico: {response.StatusCode}");
            }
            // Limpiar los campos después de agregar músico
            LimpiarCampos();
        }


        /**
         * Método GetMusician
         * A partir de un id dado por el usuario en el Main View, obtiene el músico de la lista con ese id
         * **/
        public async Task GetMusician()
        {
            //Si no es un id válido 
            if (string.IsNullOrWhiteSpace(IdToSearch) || !long.TryParse(IdToSearch, out long musicianId))
            {
                Debug.WriteLine("Por favor, ingresa un ID válido.");
                musicoSeleccionado = null;
                return;
            }
            //Construcción de la url de la API con el ID ingresado para obtener el músico
            var url = $"{_url}/{musicianId}";

            try
            {
                //Petición GET a la API usando httpClient
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    //Conversión de JSON en un objeto Musician
                    musicoSeleccionado = await response.Content.ReadFromJsonAsync<Musician>();
                    ErrorMessage = string.Empty;

                    // Navegación a la página de edición con los datos del músico
                    if (musicoSeleccionado != null)
                    {
                        
                        await Application.Current.MainPage.Navigation.PushAsync(new MusicoSeleccionado(musicoSeleccionado));
                    }
                }
                else
                {
                    ErrorMessage= ($"No se encontró al músico con ID {musicianId}.");
                    musicoSeleccionado = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar los datos: {ex.Message}");
                musicoSeleccionado = null;
            }
        }
        /**
        * Método DeleteMusician, para eliminar un músico de la base de datos 
        * A través de un id seleccionado previamente en la Main View, elimina al músico de la base de datos.
        */
        public async Task DeleteMusician()
        {

            if (musicoSeleccionado == null)
            {
                Debug.WriteLine("No hay músico seleccionado para eliminar.");
                return;
            }
            //Construción la url de la API con el ID ingresado
            var url = $"{_url}/{musicoSeleccionado.Id}";
            //Petición DELETE a la API usando httpClient para eliminar al músico de la base de datos
            var response = await _httpClient.DeleteAsync(url);


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Músico con ID {musicoSeleccionado.Id} eliminado correctamente.");

                // Se hace una búsqueda en la lista local de músicos y se elimina si existe
                var musicianToRemove = Musicians.FirstOrDefault(m => m.Id == musicoSeleccionado.Id);
                if (musicianToRemove != null)
                {
                    Musicians.Remove(musicianToRemove);
                }

                // Limpia los campos después de eliminar el músico
                LimpiarCampos();
            }
            else
            {
                Debug.WriteLine($"Error al eliminar músico: {response.StatusCode}");
            }
        }

        /**
         * Método UpdateMusician.
         * Actualiza el músico en la lista 
         * El músico ha sido previamente seleccionado por su ID en el MainView
         * */
        public async Task UpdateMusician()
        {

            if (musicoSeleccionado is not null)
            {
                musicoSeleccionado.Name = Name;
                musicoSeleccionado.Location = Location;
                musicoSeleccionado.MusicGenre = MusicGenre;
                musicoSeleccionado.SongName = SongName;

                //Construcción la url de la API con el ID ingresado
                var url = $"{_url}/{musicoSeleccionado.Id}";

               //Conversión del objeto músicoSeleccionado en JSON para poder enviarlo a la API
                var json = JsonSerializer.Serialize<Musician>(musicoSeleccionado, _jsonOptions);
                //Creación del contenido JSON para la petición
                StringContent content = new StringContent(
                 json,
                 Encoding.UTF8,
                 "application/json"
             );
                //Petición PUT a la API y verificación de éxito de la petición
                var response = await _httpClient.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Músico actualizado correctamente en la base de datos.");

                    // Actualización del músico en la lista local usando el Id
                    var index = Musicians.IndexOf(musicoSeleccionado);
                    if (index != -1)
                    {
                        Musicians[index] = musicoSeleccionado;  // Reemplazar el músico actualizado en la lista
                    }
                    // Limpiar los campos después de actualizar el músico
                    LimpiarCampos();

                }
                else
                {
                    Debug.WriteLine($"Error al actualizar músico: {response.StatusCode}");
                }
            }

        }

        /**
         * Método GetAllMusicians
         * Obtiene la lista completa de músicos
         * **/
        public async Task GetAllMusicians()
        {
            //Consulta a un servicio web en la URL _url/musicians para obtener información
            //sobre músicos y almacenacenamiento de  la respuesta en la variable response.
            var url = _url;
            var response = await _httpClient.GetAsync(url);

            //Si la respuesta es exitosa, procede a la deserialización
            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())

                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Musician>>(stream, _jsonOptions);
                    if (data != null)
                    {
                        foreach (var musician in data)
                        {
                            Debug.WriteLine($"Músico recibido: {musician.Name}, {musician.Location}, {musician.MusicGenre}, {musician.SongName}, {musician.Id}");
                        }
                        //En la lista local, hace una limpieza de lo que haya antes de agregar la lista
                        Musicians.Clear();
                        //Creación de una observable collection que contiene la lista de la API
                        Musicians = new ObservableCollection<Musician>(data);
                        //La lista se muestra en la página "Todos los músicos"
                        await Application.Current.MainPage.Navigation.PushAsync(new TodosLosMusicos(Musicians));
                    }

                }

            }
        }
        private void LimpiarCampos()
        {
            Name = string.Empty;
            Location = string.Empty;
            MusicGenre = string.Empty;
            SongName = string.Empty;
            Id = string.Empty;
            musicoSeleccionado = null; // Asegurarse de que no haya músico seleccionada
        }
        #endregion
    }
}


        

        













