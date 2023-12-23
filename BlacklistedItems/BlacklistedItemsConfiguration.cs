using BlacklistedItems.Models;
using Rocket.API;
using System.Xml.Serialization;

namespace RestoreMonarchy.BlacklistedItems
{
    public class BlacklistedItemsConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public string BypassPermission { get; set; }
        [XmlArrayItem("Item")]
        public BlacklistItem[] BlacklistItems { get; set; }
        

        public void LoadDefaults()
        {
            MessageColor = "yellow";
            BypassPermission = "blacklist.bypass";
            BlacklistItems =
            [
                new BlacklistItem(366),
                new BlacklistItem(367),
                new BlacklistItem(368),
                new BlacklistItem(37),
                new BlacklistItem(39),
                new BlacklistItem(41)
            ];
        }
    }
}