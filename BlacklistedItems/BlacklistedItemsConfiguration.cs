using BlacklistedItems.Models;
using Rocket.API;
using System.Xml.Serialization;

namespace RestoreMonarchy.BlacklistedItems
{
    public class BlacklistedItemsConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public bool EnableBypassPermission { get; set; }
        public string BypassPermission { get; set; }
        [XmlArrayItem("Item")]
        public BlacklistItem[] BlacklistItems { get; set; }        

        public void LoadDefaults()
        {
            MessageColor = "yellow";
            EnableBypassPermission = true;
            BypassPermission = "blacklist.bypass";
            BlacklistItems =
            [
                new BlacklistItem(1441, "Shadowstalker Mk. II"),
                new BlacklistItem(1442, "Shadowstalker Mk. II Scope"),
                new BlacklistItem(1443, "Shadowstalker Mk. II Drum"),
                new BlacklistItem(1444, "Shadowstalker Mk. II Magazine"),
                new BlacklistItem(1464, "Blindfold", false, true, true, true)
            ];
        }
    }
}