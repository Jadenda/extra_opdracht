public class VirtualQueue
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AttractionId { get; set; }
    public DateTime EntryTime { get; set; }
    public bool IsPresent { get; set; }

    public User User { get; set; }
    public Attractie Attractie { get; set; }

}