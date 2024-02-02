    public class User
    {
        public int Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }

        public List<VirtualQueue> VirtualQueue { get; set; } = new List<VirtualQueue>();
    }
