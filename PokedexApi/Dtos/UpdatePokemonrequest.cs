namespace PokedexApi.Dtos;
public class UpdatePokemonRequest
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Level { get; set; }

    public int Height { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}
