namespace PNJSystem.Core.Items
{
    //pourquoi pas une abstract
    public class Item
    {
        public string Id { get; }
        public string Name { get; }

        public Item(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}