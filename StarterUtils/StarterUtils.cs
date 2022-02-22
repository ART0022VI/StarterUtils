using System;
using System.Collections.Generic;
using System.Linq;
using Qurre;
using Qurre.API;
using Qurre.Events;
using Qurre.API.Events;

using Round = Qurre.Events.Round;
using Player = Qurre.Events.Player;
using Map = Qurre.Events.Map;

namespace StarterUtils
{
    public class StarterUtils : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "StarterUtils";
        public override Version Version => new Version(1, 2, 0);
        public override int Priority => int.MaxValue;
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public static Config CustomConfig { get; private set; }
        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Round.Start += EventHandler.RoundStarted;
            Round.Waiting += EventHandler.RoundWaiting;
            Round.End += EventHandler.RoundEnded;

            if (CustomConfig.FriendlyFireEnable) Player.DamageProcess += EventHandler.OnDamageProcessing;

            if (CustomConfig.HandcuffedGodEnable) Player.Damage += EventHandler.OnDamaging;

            if (CustomConfig.CleanUpBlood) Map.NewBlood += EventHandler.BloodSpawn;

            if (CustomConfig.ClassAmmoEnable) Player.Spawn += EventHandler.OnSpawning;

            if (CustomConfig.SizeChangerEnable) Player.RoleChange += EventHandler.OnRoleChanging;

            if (CustomConfig.PickupCleanEnable) Map.CreatePickup += EventHandler.OnCreatePickup;

            if (CustomConfig.PickupCleanEnable) Player.DropAmmo += EventHandler.OnDropAmmo;

            if (CustomConfig.Scp096Target) Scp096.AddTarget += EventHandler.Scp096TargetAdded;

            if (CustomConfig.OffGravityEnable) Player.DroppingItem += EventHandler.OnDropItem;

            if (CustomConfig.InfiniteAmmoEnable > 0) Player.Shooting += EventHandler.OnShooting;

            if (CustomConfig.InfiniteRadioEnable) Player.RadioUsing += EventHandler.OnRadioUsing;

            if (CustomConfig.SCPSpeakEnable)
            {
                Voice.PressPrimaryChat += EventHandler.PressedQ;
                Voice.PressAltChat += EventHandler.PressedV;
            }
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Round.Start -= EventHandler.RoundStarted;
            Round.Waiting -= EventHandler.RoundWaiting;
            Round.End -= EventHandler.RoundEnded;

            if (CustomConfig.FriendlyFireEnable) Player.DamageProcess -= EventHandler.OnDamageProcessing;

            if (CustomConfig.HandcuffedGodEnable) Player.Damage -= EventHandler.OnDamaging;

            if (CustomConfig.CleanUpBlood) Map.NewBlood -= EventHandler.BloodSpawn;

            if (CustomConfig.ClassAmmoEnable) Player.Spawn -= EventHandler.OnSpawning;

            if (CustomConfig.SizeChangerEnable) Player.RoleChange -= EventHandler.OnRoleChanging;

            if (CustomConfig.PickupCleanEnable) Map.CreatePickup -= EventHandler.OnCreatePickup;

            if (CustomConfig.PickupCleanEnable) Player.DropAmmo -= EventHandler.OnDropAmmo;

            if (CustomConfig.Scp096Target) Scp096.AddTarget -= EventHandler.Scp096TargetAdded;

            if (CustomConfig.OffGravityEnable) Player.DroppingItem -= EventHandler.OnDropItem;

            if (CustomConfig.InfiniteAmmoEnable > 0) Player.Shooting -= EventHandler.OnShooting;

            if (CustomConfig.InfiniteRadioEnable) Player.RadioUsing -= EventHandler.OnRadioUsing;

            if (CustomConfig.SCPSpeakEnable)
            {
                Voice.PressPrimaryChat -= EventHandler.PressedQ;
                Voice.PressAltChat -= EventHandler.PressedV;
            }
        }
    }
}
