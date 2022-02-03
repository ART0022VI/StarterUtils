``` yaml
# Plugin Name
Name: StarterUtils
# Enable the plugin?
IsEnable: true
# Allow SCP to talk to people?
SCPSpeakEnable: true
# Allows you to set the time required for the arrival of the elevator? Default: 5 sec.
ElevatorTime: 5
# Allow you to talk on the radio with infinite energy?
InfiniteRadioEnable: true
# Allow people to have infinite ammo? 0 - disable, 1 - infinite gun, 2 - infinite ammo.
InfiniteAmmoEnable: 2
# Allow people to change size when changing roles?
SizeChangerEnable: true
# Turn off gravity for objects?
OffGravityEnable: true
# The size of objects after turning off gravity? Default: 1
ItemScale: 2
# A dictionary contain strings for changing the name of a player's team. +The text used in the intercom.
TeamMessage:
  ChaosInsurgency: <color=green>Chaos</color>
  ClassD: <color=orange>D Class</color>
  NineTailedFox: <color=#42AAFF>MTF</color>
  Scientist: <color=yellow>Scienst</color>
  SCP: <color=red>SCP</color>
  Tutorial: <color=green>Tutorial</color>
# Whether to include a text in the intercom that shows the time and the players.
IntercomTextEnable: true
# You can change the server name string in the intercom.
NameServer: <color=yellow>My Server Name</color>
# You can change the 'time round' string in the intercom.
IntercomTimeText: <color=yellow>Time round</color>
# Launch an automatic warhead after a set time?
NukeTimeEnable: true
# Set the time in minutes after which the warhead will launch.
NukeTimeMinutes: 40
# Warhead launch text.
NukeTimeText: <color=yellow>Launching</color> <color=red>Auto-Warhead</color>
# Allow items and ragdolls to be cleaned after a while?
CleanUpEnable: true
# Every N minutes the server will be cleared.
CleanUpTimeMinutes: 10
# Does the server need to clean items?
CleanUpItems: false
# Does the server need to clean ragdolls?
CleanUpRagdolls: true
# Does the server need to clean blood?
CleanUpBlood: true
# Cleanup broadcast text.
CleanUpText: <color=yellow>The map has been cleared!</color>
# Is it necessary to clean the mutes after the start and end of the round?
MuteClearEnable: true
# Show a message when you looked at SCP-096?
Scp096Target: true
# Text when you looked at SCP-096.
Scp096Text: <color=#8b00ff>You looked at a <color=red>SCP-096</color></color>
# Allow items to be removed after they are thrown away from the player?
PickupCleanEnable: true
# A list of items that will be removed after throwing them.
Items:
- Ammo12gauge
- Ammo44cal
- Ammo556x45
- Ammo762x39
- Ammo9x19
- Radio
# Enable friendly fire at the end of the round?
FriendlyFireEnable: true
# Is it necessary to detonate a warhead at the end of the round?
WarheadEndEnable: false
# Allow you to customize ammo for each class?
ClassAmmoEnable: true
# True: Give all ammo to all classes when spawning?
ClassAmmoAllEnable: true
# True: How many bullets to give when spawning?
ClassAmmoAllCount: 100
# False: You can customize RoleType, AmmoType and the amount of Ammo.
ClassAmmo:
  ClassD:
    Ammo12Gauge: 50
    Ammo44Cal: 50
    Ammo556: 50
    Ammo762: 50
    Ammo9: 50
```
