using Qurre.API.Addons;
using System.ComponentModel;
using System.Collections.Generic;
using Respawning;
using Qurre.API;
using Qurre.API.Objects;

namespace StarterUtils
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "StarterUtils";
        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;


        [Description("Allow SCP to talk to people?")]
        public bool SCPSpeakEnable { get; set; } = true;
        [Description("Allows you to set the time required for the arrival of the elevator? Default: 5 sec.")]
        public float ElevatorTime { get; set; } = 5f;
        [Description("Allow you to talk on the radio with infinite energy?")]
        public bool InfiniteRadioEnable { get; set; } = true;
        [Description("Allow people to have infinite ammo? 0 - disable, 1 - infinite gun, 2 - infinite ammo.")]
        public ushort InfiniteAmmoEnable { get; set; } = 2;
        [Description("Allow people to change size when changing roles?")]
        public bool SizeChangerEnable { get; set; } = true;
        [Description("Turn off gravity for objects?")]
        public bool OffGravityEnable { get; set; } = false;
        [Description("The size of objects after turning off gravity? Default: 1")]
        public float ItemScale { get; set; } = 1;
        [Description("A dictionary contain strings for changing the name of a player's team. +The text used in the intercom.")]
        public Dictionary<SpawnableTeamType, string> TeamMessage { get; set; } = new Dictionary<SpawnableTeamType, string>()
        {
            [SpawnableTeamType.ChaosInsurgency] = "<color=green>Chaos</color>",
            [SpawnableTeamType.ClassD] = "<color=orange>D Class</color>",
            [SpawnableTeamType.NineTailedFox] = "<color=#42AAFF>MTF</color>",
            [SpawnableTeamType.Scientist] = "<color=yellow>Scienst</color>",
            [SpawnableTeamType.SCP] = "<color=red>SCP</color>",
            [SpawnableTeamType.Tutorial] = "<color=green>Tutorial</color>"
        };
        [Description("Whether to include a text in the intercom that shows the time and the players.")]
        public bool IntercomTextEnable { get; set; } = true;
        [Description("You can change the server name string in the intercom.")]
        public string NameServer { get; set; } = "<color=yellow>My Server Name</color>";
        [Description("You can change the 'time round' string in the intercom.")]
        public string IntercomTimeText { get; set; } = "<color=yellow>Time round</color>";
        [Description("Launch an automatic warhead after a set time?")]
        public bool NukeTimeEnable { get; set; } = true;
        [Description("Set the time in minutes after which the warhead will launch.")]
        public int NukeTimeMinutes { get; set; } = 40;
        [Description("Warhead launch text.")]
        public string NukeTimeText { get; set; } = "<color=yellow>Launching</color> <color=red>Auto-Warhead</color>";
        [Description("Allow items and ragdolls to be cleaned after a while?")]
        public bool CleanUpEnable { get; set; } = true;
        [Description("Every N minutes the server will be cleared.")]
        public int CleanUpTimeMinutes { get; set; } = 10;
        [Description("Does the server need to clean items?")]
        public bool CleanUpItems { get; set; } = false;
        [Description("Does the server need to clean ragdolls?")]
        public bool CleanUpRagdolls { get; set; } = true;
        [Description("Does the server need to clean blood?")]
        public bool CleanUpBlood { get; set; } = true;
        [Description("Cleanup broadcast text.")]
        public string CleanUpText { get; set; } = "<color=yellow>The map has been cleared!</color>";
        [Description("Is it necessary to clean the mutes after the start and end of the round?")]
        public bool MuteClearEnable { get; set; } = true;
        [Description("Show a message when you looked at SCP-096?")]
        public bool Scp096Target { get; set; } = true;
        [Description("Text when you looked at SCP-096.")]
        public string Scp096Text { get; set; } = "<color=#8b00ff>You looked at a <color=red>SCP-096</color></color>";
        [Description("Allow items to be removed after they are thrown away from the player?")]
        public bool PickupCleanEnable { get; set; } = true;
        [Description("A list of items that will be removed after throwing them.")]
        public List<ItemType> Items { get; set; } = new List<ItemType>()
        {
            ItemType.Ammo12gauge,
            ItemType.Ammo44cal,
            ItemType.Ammo556x45,
            ItemType.Ammo762x39,
            ItemType.Ammo9x19,
            ItemType.Radio
        };
        [Description("Enable friendly fire at the end of the round?")]
        public bool FriendlyFireEnable { get; set; } = true;
        [Description("Enable friendly fire in round start?")]
        public bool FriendlyFireRoundStart { get; set; } = false;
        [Description("Is it necessary to detonate a warhead at the end of the round?")]
        public bool WarheadEndEnable { get; set; } = false;
        [Description("Should we not allow the handcuffed players to be killed?")]
        public bool HandcuffedGodEnable { get; set; } = true;
        [Description("Should we not allow the handcuffed players to be killed?")]
        public string HandcuffedAttackerText { get; set; } = "<color=red>You can't kill a handcuffed player</color>";
        

        [Description("Allow you to customize ammo for each class?")]
        public bool ClassAmmoEnable { get; set; } = true;
        [Description("True: Give all ammo to all classes when spawning?")]
        public bool ClassAmmoAllEnable { get; set; } = true;
        [Description("True: How many bullets to give when spawning?")]
        public ushort ClassAmmoAllCount { get; set; } = 100;
        [Description("False: You can customize RoleType, AmmoType and the amount of Ammo.")]
        public Dictionary<RoleType, Dictionary<AmmoType, ushort>> ClassAmmo { get; set; } = new Dictionary<RoleType, Dictionary<AmmoType, ushort>>()
        {
            [RoleType.ClassD] = new Dictionary<AmmoType, ushort>()
            {
                [AmmoType.Ammo12Gauge] = 50,
                [AmmoType.Ammo44Cal] = 50,
                [AmmoType.Ammo556] = 50,
                [AmmoType.Ammo762] = 50,
                [AmmoType.Ammo9] = 50
            }
        };
    }
}
