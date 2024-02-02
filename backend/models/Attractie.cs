public class Attractie
{
    public int AttractieId { get; set; }
    public string Naam { get; set; }
    public int Capaciteit { get; set; }
    public int Duur { get; set; }
    public string Beschrijving { get; set; }
    public string AfbeeldingUrl { get; set; }

    public TimeSpan Duration { get; set; }
    public List<VirtualQueue> VirtualQueue { get; set; } = new List<VirtualQueue>();


}
