using System.Xml.Serialization;

namespace BlacklistedItems.Models
{
    public class BlacklistItem
    {
        [XmlAttribute]
        public ushort ItemId { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public bool CanCraft { get; set; }
        [XmlAttribute]
        public bool CanSpawn { get; set; }
        [XmlAttribute]
        public bool CanTake { get; set; }
        [XmlAttribute]
        public bool CanStore { get; set; }

        public bool ShouldSerializeName() => !string.IsNullOrEmpty(Name);

        public BlacklistItem() { }
        public BlacklistItem(ushort itemId, string name, bool canCraft = false, bool canSpawn = false, bool canTake = false, bool canStore = false)
        {
            ItemId = itemId;
            Name = name;
            CanCraft = canCraft;
            CanSpawn = canSpawn;
            CanTake = canTake;
            CanStore = canStore;
        }
    }
}
