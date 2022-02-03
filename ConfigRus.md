``` yaml
# Название плагина
Name: StarterUtils
# Включить плагин?
IsEnable: true
# Позволять SCP разговаривать с людьми?
SCPSpeakEnable: true
# Позволяет установить время, необходимое для прибытия лифта? По умолчанию: 5 сек.
ElevatorTime: 5
# Позволять вам говорить по радио с бесконечной энергией?
InfiniteRadioEnable: true
# Позволить людям иметь бесконечные боеприпасы? 0 - откл, 1 - беск оружие, 2 - беск боеприпасы.
InfiniteAmmoEnable: 2
# Позволять людям изменять размер при смене ролей?
SizeChangerEnable: true
# Отключить гравитацию для предметов?
OffGravityEnable: true
# Размер предметов после откл гравитации? Значение по умолчанию: 1
ItemScale: 2
# Словарь содержит строки для изменения названия команды игрока. +Текст, используемый в интеркоме.
TeamMessage:
  ChaosInsurgency: <color=green>Хаосит</color>
  ClassD: <color=orange>Класс Д</color>
  NineTailedFox: <color=#42AAFF>МТФ</color>
  Scientist: <color=yellow>Ученый</color>
  SCP: <color=red>ДЦП</color>
  Tutorial: <color=green>Туториал</color>
# Следует ли включать текст в интерком, показывающий время и игроков.
IntercomTextEnable: true
# Вы можете изменить строку имени сервера в интеркоме.
NameServer: <color=yellow>Название сервера</color>
# Вы можете изменить строку "Время раунда" в интеркоме.
IntercomTimeText: <color=yellow>Время раунда</color>
# Запустить автоматическую боеголовку через установленное время?
NukeTimeEnable: true
# Установите время в минутах, после которого боеголовка запустится.
NukeTimeMinutes: 40
# Текст о запуске боеголовки.
NukeTimeText: <color=yellow>Запуск</color> <color=red>Авто-Боеголовки</color>
# Разрешить чистку предметов и трупов через некоторое время?
CleanUpEnable: true
# Каждые N минут сервер будет очищаться.
CleanUpTimeMinutes: 10
# Нужно ли серверу очищать предметы?
CleanUpItems: false
# Нужно ли серверу чистить трупы?
CleanUpRagdolls: true
# Нужно ли серверу очищать кровь при попадании или смерти?
CleanUpBlood: true
# Текст бродкаста
CleanUpText: <color=yellow>Карта была очищена!</color>
# Необходимо ли убирать муты после начала и окончания раунда?
MuteClearEnable: true
# Показывать сообщение, когда вы смотрели на SCP-096?
Scp096Target: true
# Сообщение, когда ты смотрел на SCP-096.
Scp096Text: <color=#8b00ff>Ты посмотрел на <color=red>SCP-096</color></color>
# Разрешить удаление предметов после того, как они будут выброшены игроком?
PickupCleanEnable: true
# Список предметов, которые будут удалены после их выброса.
Items:
- Ammo12gauge
- Ammo44cal
- Ammo556x45
- Ammo762x39
- Ammo9x19
- Radio
# Включить дружественный огонь в конце раунда?
FriendlyFireEnable: true
# Необходимо ли взрывать боеголовку в конце раунда?
WarheadEndEnable: false
# Позволяет настраивать боеприпасы для каждого класса?
ClassAmmoEnable: true
# True: Раздавать все боеприпасы всем классам при появлении?
ClassAmmoAllEnable: true
# True: Сколько патронов нужно дать при спавне?
ClassAmmoAllCount: 100
# False: Вы можете настроить тип роли, тип боеприпасов и количество боеприпасов.
ClassAmmo:
  ClassD:
    Ammo12Gauge: 50
    Ammo44Cal: 50
    Ammo556: 50
    Ammo762: 50
    Ammo9: 50
```
