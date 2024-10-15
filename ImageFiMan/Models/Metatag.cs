namespace ImageFiMan.Models
{
    public class Metatag(string name, string description)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
    }
}
