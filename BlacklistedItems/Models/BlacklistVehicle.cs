using System;
using System.Xml.Serialization;

namespace RestoreMonarchy.BlacklistedItems.Models
{
    public class BlacklistVehicle
    {
        public BlacklistVehicle() { }
        public BlacklistVehicle(ushort vehicleId, string name, bool canSpawn, bool canEnter)
        {
            VehicleId = vehicleId;
            Name = name;
            CanSpawn = canSpawn;
            CanEnter = canEnter;
        }

        [XmlAttribute]
        public ushort VehicleId { get; set; }
        [XmlAttribute]
        public Guid? GUID { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public bool CanSpawn { get; set; }
        [XmlAttribute]
        public bool CanEnter { get; set; }

        public bool ShouldSerializeVehicleId() => VehicleId != 0;
        public bool ShouldSerializeName() => !string.IsNullOrEmpty(Name);
        public bool ShouldSerializeGUID() => GUID.HasValue;
    }
}
