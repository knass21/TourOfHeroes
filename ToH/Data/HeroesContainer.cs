namespace ToH.Data;

public class HeroesContainer : IDatabase
{
    private readonly List<Hero> _heroes;

    public HeroesContainer()
    {
        _heroes = new List<Hero>()
        {
            new() { Id = 11, Name = "Dr. Nice" },
            new() { Id = 12, Name = "Narco" },
            new() { Id = 13, Name = "Bombasto" },
            new() { Id = 14, Name = "Celeritas" },
            new() { Id = 15, Name = "Magneta" },
            new() { Id = 16, Name = "RubberMan" },
            new() { Id = 17, Name = "Dynama" },
            new() { Id = 18, Name = "Dr. IQ" },
            new() { Id = 19, Name = "Magma" },
            new() { Id = 20, Name = "Tornado" },
        };
    }
    public IReadOnlyList<Hero> GetAllHeroes()
    {
        return _heroes;
    }
}