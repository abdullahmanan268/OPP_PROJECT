namespace LostAndFoundCSharp.Models
{
    
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }   // lost / found / claimed

        
        public override string ToString()
        {
            return Name + " | " + Description + " | " + Location + " | " + Date + " | " + Status;
        }
    }
}
