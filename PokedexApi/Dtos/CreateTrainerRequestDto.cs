namespace PokedexApi.Dtos;

public class CreateTrainerRequestDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    public IList<MedalDto> Medals { get; set; }
}