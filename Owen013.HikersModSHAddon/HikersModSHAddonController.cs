using OWML.ModHelper;

namespace HikersModSHAddon
{
    public class HikersModSHAddonController : ModBehaviour
    {
        public void Start()
        {
            ModHelper.Console.WriteLine("Hiker's Mod Compatibility with Smol Hatchling is OBSOLETE! Please uninstall!", OWML.Common.MessageType.Error);
        }
    }
}