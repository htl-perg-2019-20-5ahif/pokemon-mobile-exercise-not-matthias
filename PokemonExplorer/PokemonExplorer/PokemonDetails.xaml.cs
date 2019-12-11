
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokemonExplorer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetails : ContentPage
    {
        public PokemonDetails(PokeAPI.Pokemon pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }
    }
}