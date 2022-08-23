using Qurre;
using Qurre.API;
using Qurre.API.Events;
using Qurre.API.Objects;
using Qurre.API.Controllers;
using Qurre.API.Controllers.Items;
using UnityEngine;
using Mirror;
using InventorySystem.Items.Firearms;
using Respawning.NamingRules;
using Respawning;
using MEC;
using System.Collections.Generic;

using Map = Qurre.API.Map;
using Random = UnityEngine.Random;
using Firearm = InventorySystem.Items.Firearms.Firearm;

namespace StarterUtils
{
    public class EventHandler
    {
        public static void RoundWaiting()
        {
            Map.ElevatorsMovingSpeed = StarterUtils.CustomConfig.ElevatorTime;

            Server.FriendlyFire = StarterUtils.CustomConfig.FriendlyFireRoundStart;

            var TeamMessage = StarterUtils.CustomConfig.TeamMessage;

            foreach (var team in TeamMessage)
            {
                RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit
                {
                    UnitName = team.Value,
                    SpawnableTeam = (byte)team.Key
                });
            }
        }
        public static void RoundStarted()
        {
            Timing.RunCoroutine(TimingRoundStarted(), "Intercom");
            Timing.RunCoroutine(TimingCleanup(), "Cleanup");

            if (StarterUtils.CustomConfig.MuteClearEnable)
                foreach (Player player in Player.List)
                {
                    player.Muted = false;
                }
            if (StarterUtils.CustomConfig.CleanVaseDope) DeleteScp244();
        }
        public static IEnumerator<float> TimingCleanup()
        {
            var CustomConfig = StarterUtils.CustomConfig;
            while (!Round.Ended && CustomConfig.CleanUpEnable)
            {
                yield return Timing.WaitForSeconds(60f * CustomConfig.CleanUpTimeMinutes);

                Log.Info("Cleanup all");

                if (CustomConfig.CleanUpItems)
                {
                    foreach (Pickup pickup in Map.Pickups)
                    {
                        pickup.Base.DestroySelf();
                    }
                }
                if (CustomConfig.CleanUpRagdolls)
                {
                    foreach (Ragdoll ragdoll in Object.FindObjectsOfType<Ragdoll>())
                    {
                        Object.Destroy(ragdoll.gameObject);
                    }
                }
            }
            yield break;
        }
        public static void RoundEnded(RoundEndEvent ev)
        {
            if (StarterUtils.CustomConfig.MuteClearEnable)
                foreach (Player player in Player.List)
                {
                    player.Muted = false;
                }

            if (StarterUtils.CustomConfig.WarheadEndEnable)
            {
                Alpha.Start();
                Alpha.TimeToDetonation = 5;
            }
        }
        public static void OnCreatePickup(CreatePickupEvent ev)
        {
            var Items = StarterUtils.CustomConfig.Items;
            foreach (ItemType types in Items)
                if (ev.Info.ItemId == types) ev.Allowed = false;
        }
        public static void Scp096TargetAdded(AddTargetEvent ev)
        {
            var Text = StarterUtils.CustomConfig.Scp096Text;
            ev.Target.ShowHint(Text, 5);
        }
        public static IEnumerator<float> TimingRoundStarted()
        {
            var CustomConfig = StarterUtils.CustomConfig;
            bool AlphaActivated = true;
            while (!Round.Ended)
            {
                if (CustomConfig.NukeTimeEnable && AlphaActivated && CustomConfig.NukeTimeMinutes <= Round.ElapsedTime.Minutes)
                {
                    Map.Broadcast(CustomConfig.NukeTimeText, 10);
                    Log.Info("Alpha-Warhead launch!");
                    AlphaActivated = false;
                    Alpha.Locked = true;
                    Alpha.Enabled = true;
                    Alpha.Start();
                }
                if (StarterUtils.CustomConfig.IntercomTextEnable)
                {
                    int DClassCount = 0;
                    int ScientistCount = 0;
                    int MtfCount = 0;
                    int ChaosCount = 0;
                    int TutCount = 0;
                    int SCPCount = 0;
                    foreach (Player player in Player.List)
                    {
                        if (player.Team == Team.CDP) DClassCount++;
                        if (player.Team == Team.RSC) ScientistCount++;
                        if (player.Team == Team.MTF) MtfCount++;
                        if (player.Team == Team.CHI) ChaosCount++;
                        if (player.Team == Team.TUT) TutCount++;
                        if (player.Team == Team.SCP) SCPCount++;
                    }
                    ReferenceHub.HostHub.GetComponent<Intercom>().CustomContent = $"<size=20>{CustomConfig.NameServer}</size>" +
                        $"\n <size=15>{CustomConfig.TeamMessage[SpawnableTeamType.ClassD]}: {DClassCount}" +
                        $"\n {CustomConfig.TeamMessage[SpawnableTeamType.Scientist]}: {ScientistCount}" +
                        $"\n {CustomConfig.TeamMessage[SpawnableTeamType.NineTailedFox]}: {MtfCount}" +
                        $"\n {CustomConfig.TeamMessage[SpawnableTeamType.ChaosInsurgency]}: {ChaosCount}" +
                        $"\n {CustomConfig.TeamMessage[SpawnableTeamType.Tutorial]}: {TutCount}" +
                        $"\n {CustomConfig.TeamMessage[SpawnableTeamType.SCP]}: {SCPCount}" +
                        $"\n <color=cyan>{CustomConfig.IntercomTimeText}: {Round.ElapsedTime.Minutes}:{Round.ElapsedTime.Seconds} </color></size>";
                }
                if (Round.ElapsedTime.Minutes > 30f)
                {
                    AfterMidGame();
                }
                yield return Timing.WaitForSeconds(1f);
            }
            yield break;
        }
        public static void OnDropItem(DroppingItemEvent ev)
        {
            var ItemScale = StarterUtils.CustomConfig.ItemScale;

            Pickup pickup = Pickup.Get(ev.Item.Base.PickupDropModel);
            NetworkServer.UnSpawn(pickup.Base.gameObject);
            pickup.Base.GetComponent<Rigidbody>().useGravity = false;
            pickup.Base.transform.localScale = new Vector3(ItemScale, ItemScale, ItemScale);
            NetworkServer.Spawn(pickup.Base.gameObject);
        }
        public static void OnShooting(ShootingEvent ev)
        {
            var InfiniteAmmoEnable = StarterUtils.CustomConfig.InfiniteAmmoEnable;

            if (InfiniteAmmoEnable == 1)
            {
                var arm = ev.Shooter.ItemInHand.Base as Firearm;
                arm.Status = new FirearmStatus(255, arm.Status.Flags, arm.Status.Attachments);
            }
            else if (InfiniteAmmoEnable == 2)
            {
                var item = ev.Shooter.ItemTypeInHand;
                if (item == ItemType.GunCOM15 || item == ItemType.GunCOM18 || item == ItemType.GunCrossvec || item == ItemType.GunFSP9) ev.Shooter.Ammo9++;
                else if (item == ItemType.GunE11SR) ev.Shooter.Ammo556++;
                else if (item == ItemType.GunRevolver) ev.Shooter.Ammo44Cal++;
                else if (item == ItemType.GunShotgun) ev.Shooter.Ammo12Gauge = (ushort)(ev.Shooter.Ammo12Gauge + 2);
                else if (item == ItemType.GunAK || item == ItemType.GunLogicer) ev.Shooter.Ammo762++;
            }
        }
        public static void OnDropAmmo(DropAmmoEvent ev) => ev.Allowed = false;
        public static void OnRoleChanging(RoleChangeEvent ev)
        {
            float scale = Random.Range(0.85f, 1.15f);
            ev.Player.Scale = new Vector3(scale, scale, scale);
        }
        public static void OnSpawning(SpawnEvent ev)
        {
            var CustomConfig = StarterUtils.CustomConfig;
            var ClassAmmo = StarterUtils.CustomConfig.ClassAmmo;

            if (ClassAmmo.ContainsKey(ev.Player.Role) && !CustomConfig.ClassAmmoAllEnable)
            {
                ev.Player.Ammo12Gauge = ClassAmmo[ev.Player.Role][AmmoType.Ammo12Gauge];
                ev.Player.Ammo44Cal = ClassAmmo[ev.Player.Role][AmmoType.Ammo44Cal];
                ev.Player.Ammo556 = ClassAmmo[ev.Player.Role][AmmoType.Ammo556];
                ev.Player.Ammo762 = ClassAmmo[ev.Player.Role][AmmoType.Ammo762];
                ev.Player.Ammo9 = ClassAmmo[ev.Player.Role][AmmoType.Ammo9];
            }
            else
            {
                ev.Player.Ammo12Gauge = CustomConfig.ClassAmmoAllCount;
                ev.Player.Ammo44Cal = CustomConfig.ClassAmmoAllCount;
                ev.Player.Ammo556 = CustomConfig.ClassAmmoAllCount;
                ev.Player.Ammo762 = CustomConfig.ClassAmmoAllCount;
                ev.Player.Ammo9 = CustomConfig.ClassAmmoAllCount;
            }
        }
        public static void OnDamaging(DamageEvent ev)
        {
            if (StarterUtils.CustomConfig.HandcuffedGodEnable)
            {
                if (ev.Target.Cuffed && ev.Attacker.Team != Team.SCP)
                {
                    if (ev.Target.Cuffed && ev.DamageType == DamageTypes.Falldown)
                    {
                        ev.Target.Kill(null);
                    }
                    else
                    {
                        var text = StarterUtils.CustomConfig.HandcuffedAttackerText;
                        ev.Allowed = false;
                        ev.Attacker.ShowHint(text, 2.5f);
                    }
                }
            }
            if (StarterUtils.CustomConfig.FasterZombieEnable)
            {
                if (ev.Attacker.Role == RoleType.Scp049 && ev.Attacker != ev.Target)
                {
                    ev.Target.DropItems();
                    ev.Allowed = false;
                    BlockAndChangeRolePlayer(ev.Target, RoleType.Scp0492);
                    if (StarterUtils.CustomConfig.SCPHealthEnable)
                    {
                        StarterUtils.CustomConfig.ScpClassHealth.TryGetValue(ev.Attacker.Role, out int hp);
                        GetHealthForKill(ev.Attacker, hp);
                    }
                }
            }
        }
        public static void OnDamageProcessing(DamageProcessEvent ev)
        {
            if (Round.Ended)
            {
                ev.FriendlyFire = false;
                ev.Amount = 30;
                ev.Allowed = true;
            }
            if (Server.FriendlyFire == false && ev.Attacker == ev.Target)
            {
                if (ev.DamageType == DamageTypes.Explosion)
                {
                    ev.Amount = 0;
                    ev.Allowed = false;
                }
            }
        }
        public static void AfterMidGame()
        {
            foreach (Pickup pickup in Map.Pickups)
            {
                pickup.Base.DestroySelf();
            }
            foreach (Ragdoll ragdoll in Object.FindObjectsOfType<Ragdoll>())
            {
                Object.Destroy(ragdoll.gameObject);
            }
        }
        public static void DeleteScp244()
        {
            foreach (Pickup pickup in Map.Pickups)
            {
                if (pickup.Type == ItemType.SCP244a || pickup.Type == ItemType.SCP244b)
                {
                    RandomItem(pickup);
                    pickup.Base.DestroySelf();
                }
                if (pickup.Type == ItemType.SCP1853)
                {
                    RandomItem(pickup);
                    pickup.Base.DestroySelf();
                }
            }
        }
        public static void PlayerDead(DeadEvent ev)
        {
            if (ev.Killer.Team == Team.SCP && ev.Killer != ev.Target)
            {
                StarterUtils.CustomConfig.ScpClassHealth.TryGetValue(ev.Killer.Role, out int hp);
                GetHealthForKill(ev.Killer, hp);
            }
        }
        public static void GetHealthForKill(Player player, int hp)
        {
            player.Hp += hp;
            player.ShowHint(StarterUtils.CustomConfig.SCPHealthText, 2);
        }
        public static void RandomItem(Pickup pickup)
        {
            var value = Random.Range(0, 47);
            switch (value)
            {
                // Keycards
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    {
                        var item = new Item((ItemType)value).Spawn(pickup.Position, Quaternion.Euler(pickup.Rotation.x + 90f, pickup.Rotation.y + 0f, pickup.Rotation.z + 90f));
                        item.Scale = new Vector3(2f, 10f, 2f);
                    }
                    break;
                // Weapons
                case 16:
                case 20:
                case 23:
                case 24:
                case 39:
                case 40:
                case 41:
                case 47: // cleared
                    {
                        new Item((ItemType)value).Spawn(pickup.Position, pickup.Rotation).Scale = new Vector3(0.5f, 0.5f, 0.2f);
                    }
                    break;
                // Ammo
                case 19:
                case 22:
                case 27:
                case 28:
                case 29:
                    {
                        new Item((ItemType)value).Spawn(pickup.Position, pickup.Rotation).Scale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                    break;
                // Armor
                case 36:
                case 37:
                case 38:
                    {
                        new Item((ItemType)value).Spawn(pickup.Position, pickup.Rotation).Scale = new Vector3(0.7f, 0.5f, 0.5f);
                    }
                    break;
                // Shit-Update
                case 12:
                case 44:
                case 45:
                case 46:
                    {
                        // coin
                        new Item((ItemType)35).Spawn(pickup.Position, pickup.Rotation).Scale = new Vector3(2f, 2f, 2f);
                    }
                    break;
                default:
                    {
                        new Item((ItemType)value).Spawn(pickup.Position, pickup.Rotation);
                    }
                    break;
            }
        }
        public static void PlacingBulletHoles(PlaceBulletHoleEvent ev)
        {
            if (Round.ElapsedTime.Minutes > 30f) ev.Allowed = false;
        }
        public static void BloodSpawn(NewBloodEvent ev)
        {
            if (Round.ElapsedTime.Minutes > 30f) ev.Allowed = false;
        }
        public static void PressedQ(PressPrimaryChatEvent ev)
        {
            ev.Player.Dissonance.MimicAs939 = false;
        }
        public static void PressedV(PressAltChatEvent ev)
        {
            ev.Player.Dissonance.MimicAs939 = true;
        }
        public static void OnRadioUsing(RadioUsingEvent ev)
        {
            ev.Battery = 100;
        }
        public static void BlockAndChangeRolePlayer(Player player, RoleType role)
        {
            player.BlockSpawnTeleport = true;
            player.Role = role;
        }
    }
}
