using PokemonExplorer.PokeAPI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonExplorer
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };

        private ObservableCollection<Pokemon> PokemonValue;
        public ObservableCollection<Pokemon> Pokemon
        {
            get => PokemonValue;
            set
            {
                PokemonValue = value;
                OnPropertyChanged(nameof(Pokemon));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            _ = LoadPokemonListAsync();
        }

        public async Task LoadPokemonListAsync()
        {
            // Use this to load all pokemon (WARNING: can cause long loading times)
            //var response = await client.GetAsync("pokemon/?offset=0&limit=2000");
            var response = await client.GetAsync("pokemon");

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var pokemons = JsonSerializer.Deserialize<Pokemons>(body);

            Pokemon = pokemons.Results;
        }

        public async Task LoadPokemonDetailsAsync(Pokemon pokemon)
        {
            var response = await client.GetAsync($"pokemon/{pokemon.Name}");

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var pokemonWithDetails = JsonSerializer.Deserialize<Pokemon>(body);

            var index = Pokemon.IndexOf(Pokemon.FirstOrDefault(p => p.Name == pokemon.Name));
            Pokemon[index] = pokemonWithDetails;
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var pokemon = e.Item as Pokemon;
            if (pokemon.Loaded is false)
            {
                await LoadPokemonDetailsAsync(pokemon);
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new PokemonDetails(e.Item as Pokemon));
        }
    }
}
