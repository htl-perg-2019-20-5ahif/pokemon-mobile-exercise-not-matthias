using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace PokemonExplorer.PokeAPI
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weight")]
        public float Weight { get; set; }

        [JsonPropertyName("abilities")]
        public IEnumerable<AbilitySlot> AbilitySlots { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }

        [JsonIgnore]
        public bool Loaded { get; set; }
    }

    public class Pokemons
    {
        [JsonPropertyName("results")]
        public ObservableCollection<Pokemon> Results { get; set; }
    }

    public class AbilitySlot
    {
        [JsonPropertyName("ability")]
        public Ability Ability { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string Front { get; set; }

        [JsonPropertyName("back_default")]
        public string Back { get; set; }
    }
}
