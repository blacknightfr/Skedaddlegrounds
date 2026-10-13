using System.Drawing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Channels;

const int SLEEP_1_SEC = 1000;
const int SLEEP_2_SEC = 2000;
const int SLEEP_5_SEC = 5000;

//Base moves:
Attack punch = new()
{
    name = "Punch",
    minDamage = 10,
    maxDamage = 25,
    accuracy = 100,
    speed = 0,
    avoid = false,
};

Attack heavyPunch = new()
{
    name = "Heavy Punch",
    minDamage = 20,
    maxDamage = 50,
    accuracy = 85,
    speed = 0,
    avoid = false,
};

Attack jab = new()
{
    name = "Jab",
    minDamage = 5,
    maxDamage = 15,
    accuracy = 100,
    speed = 30,
    avoid = false,
};

Attack tackle = new()
{
    name = "Tackle",
    minDamage = 45,
    maxDamage = 55,
    accuracy = 75,
    speed = 15,
    avoid = false,
};


Attack chop = new()
{
    name = "Chop",
    minDamage = 25,
    maxDamage = 45,
    accuracy = 90,
    speed = 0,
    avoid = false,
};

Attack slash = new()
{
    name = "Slash",
    minDamage = 20,
    maxDamage = 30,
    accuracy = 100,
    speed = 0,
    avoid = false,
};

Attack criticalSlash = new()
{
    name = "Critical Slash",
    minDamage = 40,
    maxDamage = 70,
    accuracy = 75,
    speed = -10,
    avoid = false,
};

Attack maul = new()
{
    name = "Maul",
    minDamage = 65,
    maxDamage = 100,
    accuracy = 45,
    speed = -20,
    avoid = false,
};

Attack daggerThrow = new()
{
    name = "Dagger Throw",
    minDamage = 15,
    maxDamage = 60,
    accuracy = 70,
    speed = 15,
    avoid = false,
};

//Enemy exclusive:
Attack kick = new()
{
    name = "Kick",
    minDamage = 20,
    maxDamage = 30,
    accuracy = 95,
    speed = 10,
    avoid = false,
};

Attack dodge = new()
{
    name = "Dodge",
    minDamage = 0,
    maxDamage = 0,
    accuracy = 85,
    speed = 10000,
    avoid = true,
};


Attack parry = new()
{
    name = "Parry",
    minDamage = 0,
    maxDamage = 0,
    accuracy = 100,
    speed = 10000,
    avoid = true,
};


Attack check = new()
{
    name = "Check",
    minDamage = 0,
    maxDamage = 0,
    accuracy = 100,
    speed = 1000000,
    avoid = false,
};

Attack defend = new()
{
    name = "Defend",
    minDamage = 0,
    maxDamage = 0,
    accuracy = 100,
    speed = 1000000,
    avoid = false,
};

List<Item> weapons = new()
{
    new Item("Iron Sword", 15, 30, 0, 0, "Weapon", slash, criticalSlash), //name, price, damage, speed, health, item slot, attack1, attack2
    new Item("Iron Axe", 15, 50, -5, 0, "Weapon", chop, null),
    new Item("Iron Dagger", 10, 15, 0, 0, "Weapon", daggerThrow, null),
    new Item("Iron Greathammer", 35, 75, -20, 0, "Weapon", maul, null),
};

List<Item> equipment = new()
{
    new Item("Iron Shield", 20, 0, 0, 40, "Extra", parry, null),
    new Item("Iron Helmet", 10, 0, -10, 70, "Head", null, null),
    new Item("Iron Chestplate", 35, 0, -30, 150, "Chest", null, null),
    new Item("Iron Leggings", 25, 0, -15, 90, "Legs", null, null),
    new Item("Iron Greaves", 10, 0, -5, 60, "Feet", null, null),
};

Fighter warrior = new()
{
    baseHP = Random.Shared.Next(150, 1000),
    baseDMG = Random.Shared.Next(50, 200),
    baseSPD = Random.Shared.Next(25, 200),

    Attack1 = punch,
    Attack2 = heavyPunch,
    Attack3 = tackle,
    Attack4 = jab,

    ItemWeapon = null,
    ItemHead = null,
    ItemChest = null,
    ItemLegs = null,
    ItemFeet = null,
    ItemExtra = null,
};

Fighter enemy = new()
{
    baseHP = 0,
    baseDMG = 0,
    baseSPD = 0,

    Attack1 = punch,
    Attack2 = heavyPunch,
    Attack3 = dodge,
    Attack4 = kick,

    ItemWeapon = null,
    ItemHead = null,
    ItemChest = null,
    ItemLegs = null,
    ItemFeet = null,
    ItemExtra = null,
};

List<Attack> enemyMoves = new()
{
    enemy.Attack1,
    enemy.Attack2,
    enemy.Attack3,
    enemy.Attack4,
    defend
};

List<String> enemyNames = new()
{
    "Noui",
    "Abdi",
    "Hugo",
    "Michael",
    "Mikael",
    "Påsner",
    "Odysseus",
    "Chad",
    "Walter",
    "God",
    "Jesus-Christ",
    "Pope",
    "Jonathan",
    "Alexander",
    "Jeffery",
    "Epstein",
    "Joakim",
    "Adolf",
    "Sean",
    "Combs",
    "Ernstberger",
    "Kanye",
    "Vera",
    "Niklas",
    "Mio",
    "Nader",
    "Slaimon",
    "Eminem",
    "Boss",
    "Bergström",
    "Gabe",
    "Newell",
    "Gabriel",
    "Oliver",
    "King",
    "Boss",
    "Alvin",
    "Osama",
    "Bin-Laden",
    "Nader",
    "Fraud",
    "Cohen",
    "Ben",
    "Netanyahu",
    "Benjamin",
};

static bool yesno(string question)
{
    while (true)
    {
        Console.Write($"{question} [Y/N]: ");
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        string? answer = Console.ReadLine()?.ToLower();

        if (answer == "y")
            return true;

        if (answer == "n")
            return false;

        Console.WriteLine("Please answer with Y or N!");
        Thread.Sleep(SLEEP_2_SEC);
        Console.Clear();
    }
}

string? fighterName = null;
int dollars = 50;
int bet = 0;
int addBet;
int match = 0;

Location? location = null;
ShopLocation? shopLocation = null;
ArenaSection? arenaSection = null;


Console.WriteLine("Welcome to this world, I am Professor Tree.");
Thread.Sleep(SLEEP_1_SEC);
Console.WriteLine("[Prof. Tree]: \nThis world is inhabited by FIGHTERS, they will fight for you to their very end.");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("FIGHTER betting is a very popular sport practiced in the arena, many earn a living just from betting alone!");
Thread.Sleep(SLEEP_5_SEC);
Console.WriteLine("Myself... I study FIGHTERS as a profession.");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("I will grant you one FIGHTER before you go out into the world.");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("You may name it yourself!");
Thread.Sleep(SLEEP_1_SEC);
while (true)
{
    while (Console.KeyAvailable)
    {
        Console.ReadKey(true);
    }
    Console.WriteLine("What will you name your FIGHTER?");
    fighterName = Console.ReadLine();

    Console.Clear();
    if (yesno($"[Prof. Tree]: \nAre you sure you want to name your FIGHTER {fighterName}?"))
    {
        Console.WriteLine("(Press any key to continue)");
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        Console.ReadKey();

        break;
    }
    Console.Clear();
}

Console.Clear();
Console.WriteLine("[Prof. Tree]: \nNow I recommend you to visit the shop nearby, you may buy items there!");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("Items that you buy will increase your FIGHTER's stats by a sizeable amount, it will cost, however!");
Thread.Sleep(SLEEP_5_SEC);
Console.WriteLine("You may earn money as you win your battles!");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("But for now, i will give you $50.");
Thread.Sleep(SLEEP_2_SEC);
Console.WriteLine("Your very own FIGHTER legend is about to unfold! A world of dreams and adventures with FIGHTERS awaits! Let's go!");
Thread.Sleep(SLEEP_5_SEC);

Console.WriteLine("(Press any key to continue)");
while (Console.KeyAvailable)
{
    Console.ReadKey(true);
}
Console.ReadKey();

location = Location.streets;
while(location != null)
{
    List<Attack> moves = new()
    {
        warrior.Attack1,
        warrior.Attack2,
        warrior.Attack3,
        warrior.Attack4,
        check,
        defend
    };

    while (location == Location.streets)
    {
        Console.Clear();
        Console.WriteLine("(You are in the streets, what do you do?)");
        Console.WriteLine("[GO TO SHOP] \n[GO TO ARENA] \n[INSPECT FIGHTER]");
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        string? action = Console.ReadLine()?.ToLower();
        switch (action)
        {
            case "go to shop" or "[go to shop]" or "shop" or "to shop" or "go shop":
                location = Location.shop;
                break;
            case "go to arena" or "[go to arena]" or "arena" or "to arena" or "go arena":
                location = Location.arena;
                break;
            case "inspect fighter" or "[inspect fighter]" or "fighter" or "inspect":
                location = Location.inspectFighter;
                break;
            default:
                continue;
        }
    }

    while (location == Location.shop)
    {
        Console.Clear();
        Console.WriteLine("(You enter the shop..)");
        Thread.Sleep(SLEEP_1_SEC);
        Console.WriteLine("[Shopkeeper]: \nShields? Swords? ARMOR? You want it? It's yours, my friend, as long as you have enough cash.");
        Console.WriteLine("[WEAPONS] \n[EQUIPMENT] \n[GO BACK]");
        while (true)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            string? aisle = Console.ReadLine()?.ToLower();
            switch (aisle)
            {
                case "weapons":
                    shopLocation = ShopLocation.weaponAisle;
                    Console.WriteLine("[Shopkeeper]: \nWeapons? I've got these in store for you:");
                    Thread.Sleep(SLEEP_2_SEC);
                    break;
                case "equipment":
                    shopLocation = ShopLocation.equipmentAisle;
                    Console.WriteLine("[Shopkeeper]: \nEquipment? I've got these in store for you:");
                    Thread.Sleep(SLEEP_2_SEC);
                    break;
                case "go back" or "[go back]" or "back" or "go" or "leave":
                    location = Location.streets;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"[Shopkeeper]: \nWhat's a {aisle}?");
                    Console.WriteLine("[WEAPONS] \n[EQUIPMENT] \n[GO BACK]");
                    continue;
            }
            break;
        }
        
        while (shopLocation == ShopLocation.weaponAisle)
        {
            Console.Clear();
            Console.WriteLine("[Shopkeeper]: \nWeapons? I've got these in store for you:");
            purchaseWeapons:
            Console.WriteLine($"(You currently have ${dollars}.)");
            foreach (Item item in weapons)
            {
                string line = $"[{item.Name} | ${item.Price} | {item.Damage}DMG {item.Speed}SPD]";
                if (item.Attack1 != null)
                {
                    line += $" +[{item.Attack1.name}]";
                }
                if (item.Attack2 != null)
                {
                    line += $" +[{item.Attack2.name}]";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("[GO BACK]");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            string? input = Console.ReadLine()?.ToLower();
            if(input == "go back" || input == "back" || input == "[go back]" || input == "go")
            {
                shopLocation = null;
                break;
            }

            bool foundItem = false;
            foreach (Item item in weapons)
            {
                if (item.Name.ToLower() == input)
                {
                    foundItem = true;

                    if (dollars >= item.Price)
                    {
                        while (true)
                        {
                            Console.Clear();
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey(true);
                            }
                            if (yesno($"(Purchase {item.Name}?)"))
                            {
                                dollars -= item.Price;
                                warrior.ItemWeapon = item;
                                Console.WriteLine($"Equipped [{item.Name}]!");
                                Thread.Sleep(SLEEP_2_SEC);
                                Console.Clear();
                                if (item.Attack1 != null)
                                {
                                    Console.Clear();
                                    if (yesno($"(Do you want to equip {item.Attack1.name}?)"))
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"(Which attack will you replace?)");
                                            Console.WriteLine($"[SLOT 1: {warrior.Attack1?.name}]");
                                            Console.WriteLine($"[SLOT 2: {warrior.Attack2?.name}]");
                                            Console.WriteLine($"[SLOT 3: {warrior.Attack3?.name}]");
                                            Console.WriteLine($"[SLOT 4: {warrior.Attack4?.name}]");
                                            while (Console.KeyAvailable)
                                            {
                                                Console.ReadKey(true);
                                            }
                                            input = Console.ReadLine()?.ToLower();
                                            switch (input)
                                            {
                                                case "slot 1" or "1" or "[slot 1]":
                                                    warrior.Attack1 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 1");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 2" or "2" or "[slot 2]":
                                                    warrior.Attack2 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 2");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 3" or "3" or "[slot 3]":
                                                    warrior.Attack3 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 3");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 4" or "4" or "[slot 4]":
                                                    warrior.Attack4 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 4");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                default:
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (item.Attack2 != null)
                                {
                                    Console.Clear();
                                    if (yesno($"(Do you want to equip {item.Attack2.name}?)"))
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"(Which attack will you replace?)");
                                            Console.WriteLine($"[SLOT 1: {warrior.Attack1?.name}]");
                                            Console.WriteLine($"[SLOT 2: {warrior.Attack2?.name}]");
                                            Console.WriteLine($"[SLOT 3: {warrior.Attack3?.name}]");
                                            Console.WriteLine($"[SLOT 4: {warrior.Attack4?.name}]");
                                            while (Console.KeyAvailable)
                                            {
                                                Console.ReadKey(true);
                                            }
                                            input = Console.ReadLine()?.ToLower();
                                            switch (input)
                                            {
                                                case "slot 1" or "1" or "[slot 1]":
                                                    warrior.Attack1 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 1");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 2" or "2" or "[slot 2]":
                                                    warrior.Attack2 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 2");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 3" or "3" or "[slot 3]":
                                                    warrior.Attack3 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 3");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 4" or "4" or "[slot 4]":
                                                    warrior.Attack4 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 4");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                default:
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    Console.Clear();
                                    goto purchaseWeapons;
                                }
                                goto purchaseWeapons;
                            }
                            Console.Clear();
                            goto purchaseWeapons;
                        }
                    }
                    else
                    {
                        Console.WriteLine("(Maybe you should come back when you're a little, mmm, richer!)");
                        Thread.Sleep(SLEEP_2_SEC);
                    }
                    break;
                }
            }
            if (!foundItem)
            {
                Console.WriteLine($"{input}? I have no such thing.");
                Thread.Sleep(SLEEP_2_SEC);
            }
        }

        while (shopLocation == ShopLocation.equipmentAisle)
        {
            Console.Clear();
            Console.WriteLine("[Shopkeeper]: \nEquipment? I've got these in store for you:");
            purchaseEquipment:
            Console.WriteLine($"(You currently have ${dollars}.)");
            foreach (Item item in equipment)
            {
                string line = $"[{item.Name} | ${item.Price} | SLOT \"{item.Slot}\" | {item.Health}HP {item.Speed}SPD]";
                if (item.Attack1 != null)
                {
                    line += $" +[{item.Attack1.name}]";
                }
                if (item.Attack2 != null)
                {
                    line += $" +[{item.Attack2.name}]";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("[GO BACK]");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            string? input = Console.ReadLine()?.ToLower();
            if(input == "go back" || input == "back" || input == "[go back]" || input == "go")
            {
                shopLocation = null;
                break;
            }

            bool foundItem = false;
            foreach (Item item in equipment)
            {
                if (item.Name.ToLower() == input)
                {
                    foundItem = true;

                    if (dollars >= item.Price)
                    {
                        while (true)
                        {
                            Console.Clear();
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey(true);
                            }
                            if (yesno($"(Purchase {item.Name}?)"))
                            {
                                dollars -= item.Price;
                                switch (item.Slot)
                                {
                                    case "Head":
                                        warrior.ItemHead = item;
                                        Console.WriteLine($"Equipped [{item.Name}]!");
                                        Thread.Sleep(SLEEP_2_SEC);
                                        Console.Clear();
                                        break;
                                    case "Chest":
                                        warrior.ItemChest = item;
                                        Console.WriteLine($"Equipped [{item.Name}]!");
                                        Thread.Sleep(SLEEP_2_SEC);
                                        Console.Clear();
                                        break;
                                    case "Legs":
                                        warrior.ItemLegs = item;
                                        Console.WriteLine($"Equipped [{item.Name}]!");
                                        Thread.Sleep(SLEEP_2_SEC);
                                        Console.Clear();
                                        break;
                                    case "Feet":
                                        warrior.ItemFeet = item;
                                        Console.WriteLine($"Equipped [{item.Name}]!");
                                        Thread.Sleep(SLEEP_2_SEC);
                                        Console.Clear();
                                        break;
                                    case "Extra":
                                        warrior.ItemExtra = item;
                                        Console.WriteLine($"Equipped [{item.Name}]!");
                                        Thread.Sleep(SLEEP_2_SEC);
                                        Console.Clear();
                                        break;
                                    default:
                                        Console.WriteLine("ERROR, item isn't correct");
                                        break;
                                }
                                if (item.Attack1 != null)
                                {
                                    Console.Clear();
                                    if (yesno($"(Do you want to equip {item.Attack1.name}?)"))
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"(Which attack will you replace?)");
                                            Console.WriteLine($"[SLOT 1: {warrior.Attack1?.name}]");
                                            Console.WriteLine($"[SLOT 2: {warrior.Attack2?.name}]");
                                            Console.WriteLine($"[SLOT 3: {warrior.Attack3?.name}]");
                                            Console.WriteLine($"[SLOT 4: {warrior.Attack4?.name}]");
                                            while (Console.KeyAvailable)
                                            {
                                                Console.ReadKey(true);
                                            }
                                            input = Console.ReadLine()?.ToLower();
                                            switch (input)
                                            {
                                                case "slot 1" or "1" or "[slot 1]":
                                                    warrior.Attack1 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 1");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 2" or "2" or "[slot 2]":
                                                    warrior.Attack2 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 2");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 3" or "3" or "[slot 3]":
                                                    warrior.Attack3 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 3");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 4" or "4" or "[slot 4]":
                                                    warrior.Attack4 = item.Attack1;
                                                    Console.WriteLine($"Equipped [{item.Attack1.name}] to Slot 4");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                default:
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (item.Attack2 != null)
                                {
                                    Console.Clear();
                                    if (yesno($"(Do you want to equip {item.Attack2.name}?)"))
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"(Which attack will you replace?)");
                                            Console.WriteLine($"[SLOT 1: {warrior.Attack1?.name}]");
                                            Console.WriteLine($"[SLOT 2: {warrior.Attack2?.name}]");
                                            Console.WriteLine($"[SLOT 3: {warrior.Attack3?.name}]");
                                            Console.WriteLine($"[SLOT 4: {warrior.Attack4?.name}]");
                                            while (Console.KeyAvailable)
                                            {
                                                Console.ReadKey(true);
                                            }
                                            input = Console.ReadLine()?.ToLower();
                                            switch (input)
                                            {
                                                case "slot 1" or "1" or "[slot 1]":
                                                    warrior.Attack1 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 1");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 2" or "2" or "[slot 2]":
                                                    warrior.Attack2 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 2");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 3" or "3" or "[slot 3]":
                                                    warrior.Attack3 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 3");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                case "slot 4" or "4" or "[slot 4]":
                                                    warrior.Attack4 = item.Attack2;
                                                    Console.WriteLine($"Equipped [{item.Attack2.name}] to Slot 4");
                                                    Thread.Sleep(SLEEP_1_SEC);
                                                    Console.Clear();
                                                    break;
                                                default:
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    Console.Clear();
                                    goto purchaseEquipment;
                                }
                                goto purchaseEquipment;
                            }
                            Console.Clear();
                            goto purchaseEquipment;
                        }
                    }
                    else
                    {
                        Console.WriteLine("(Maybe you should come back when you're a little, mmm, richer!)");
                        Thread.Sleep(SLEEP_2_SEC);
                    }
                    break;
                }
            }
            if (!foundItem)
            {
                Console.WriteLine($"{input}? I have no such thing.");
                Thread.Sleep(SLEEP_2_SEC);
            }
        }
    }

    while (location == Location.arena)
    {
        Console.Clear();
        Console.WriteLine("(You walk into the arena's main entrance...)");
        Thread.Sleep(SLEEP_1_SEC);
        Console.WriteLine("(You see..)");;
        Thread.Sleep(SLEEP_1_SEC);

        while (true)
        {
            Console.WriteLine("[BETTING STAND]");
            Console.WriteLine("[BATTLE SIGNUP]");
            Console.WriteLine("[GO BACK]");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            string? section = Console.ReadLine()?.ToLower();
            switch (section)
            {
                case "battle" or "battle signup" or "signup" or "[battle signup]" or "fight":
                    arenaSection = ArenaSection.fightingSection;
                    break;
                case "betting" or "betting stand" or "stand" or "[betting stand]" or "bet":
                    arenaSection = ArenaSection.bettingSection;
                    break;
                case "go back" or "[go back]" or "back" or "go" or "leave":
                    location = Location.streets;
                    break;
                default:
                    Console.Clear();
                    continue;
            }
            break;
        }
        while (arenaSection == ArenaSection.fightingSection)
        {
            if(match == 0)
            {
                Console.Clear();
                Console.WriteLine("(You walk up to the arena sign-up...)");
                Thread.Sleep(SLEEP_1_SEC);
                Console.WriteLine("[Arena Clerk]: \nWelcome, is this your first time in the arena?");
                Thread.Sleep(SLEEP_2_SEC);
                if (!yesno($"Do you want to register your fighter with us?"))
                {
                    Console.WriteLine("Then you may leave, there are others waiting behind you.");
                    Thread.Sleep(SLEEP_2_SEC);
                    arenaSection = null;
                    break;
                }
                Thread.Sleep(SLEEP_1_SEC);
                Console.WriteLine($"So your fighter's name is {fighterName}...");
                Thread.Sleep(SLEEP_2_SEC);
                Console.WriteLine($"Your fighter's power level is {warrior.totalST}...");
                Thread.Sleep(SLEEP_2_SEC);
                if (bet <1)
                {
                    Console.WriteLine("I see you haven't made any bets yet.");
                    Thread.Sleep(SLEEP_1_SEC);
                    Console.WriteLine("you should visit the betting stand if you wish to do so.");
                    Thread.Sleep(SLEEP_2_SEC);
                }
                Console.WriteLine("That should be all, we hope you enjoy your time here.");
                Thread.Sleep(SLEEP_2_SEC);
                match++;
            }
            Console.Clear();
            if(match == 2)
                Console.WriteLine($"[Arena Clerk]: \nYou have played in {match - 1} match so far.");
            else
                Console.WriteLine($"[Arena Clerk]: \nYou have played in {match - 1} matches so far.");
            Thread.Sleep(SLEEP_1_SEC);
            if (!yesno($"Do you wish to sign up for a match?"))
            {
                Console.WriteLine("Then you may leave.");
                Thread.Sleep(SLEEP_1_SEC);
                arenaSection = null;
                break;
            }
            Console.WriteLine("Splendid.");
            Thread.Sleep(SLEEP_1_SEC);
            if(bet <= 0)
            {
                if (!yesno($"I see you haven't made any bets, are you sure you want to continue?"))
                {
                    arenaSection = ArenaSection.bettingSection;
                    break;
                }
            }
            Console.WriteLine("||-----BATTLE-STARTING-----||");
            Thread.Sleep(SLEEP_2_SEC);
            arenaSection = null;
            location = Location.fightScene;
            break;

        }
        while (arenaSection == ArenaSection.bettingSection)
        {
            Console.Clear();
            Console.WriteLine("(You walk up to the betting stand...)");
            Thread.Sleep(SLEEP_1_SEC);
            Console.WriteLine($"(You have ${dollars}) (You have bet ${bet})");
            Thread.Sleep(SLEEP_1_SEC);
            Console.WriteLine("[The Guy]: \nHey, ya here to EARN? How much ya got?");
            Thread.Sleep(SLEEP_1_SEC);
            if (!yesno($"(Do you want to bet on your fighter?)"))
            {
                arenaSection = null;
                break;
            }
            Console.Clear();
            Console.WriteLine($"[THE GUY]: \nHow much ya wanna add to yer bet? Ya'll earn about 1.5x the amount you put in!");
            Console.WriteLine($"(You have ${dollars}) (You have bet ${bet})");
            while (!int.TryParse(Console.ReadLine(), out addBet) || addBet == 0)
            {
                Console.WriteLine("Eh? Put some proper cash on the table.");
            }
            if(addBet > dollars)
            {
                Console.WriteLine("(You do not have enough)");
                Thread.Sleep(SLEEP_1_SEC);
                arenaSection = null;
                break;
            }
            bet += addBet;
            dollars -= addBet;
            Console.WriteLine($"(You added ${addBet} to your bet!)");
            addBet = 0;
            Thread.Sleep(SLEEP_1_SEC);
            Console.WriteLine($"Your bet is now ${bet}!");
            Thread.Sleep(SLEEP_1_SEC);
            arenaSection = null;
        }
    }

    while (location == Location.inspectFighter)
    {
        Console.Clear();
        Console.WriteLine("Fighter's Stats:\n");

        Console.WriteLine($"Name: [{fighterName}]");
        Console.WriteLine($"Power Level: [{warrior.totalST}]");
        Console.WriteLine($"Attack Damage: [{warrior.totalDMG}]");
        Console.WriteLine($"Health: [{warrior.totalHP}]");
        Console.WriteLine($"Speed: [{warrior.totalSPD}]");

        Console.WriteLine("\nFighter's Equipment:\n");

        Console.WriteLine($"Weapon: [{warrior.ItemWeapon?.Name?.ToUpper()}]");
        Console.WriteLine($"Head: [{warrior.ItemHead?.Name?.ToUpper()}]");
        Console.WriteLine($"Chest: [{warrior.ItemChest?.Name?.ToUpper()}]");
        Console.WriteLine($"Legs: [{warrior.ItemLegs?.Name?.ToUpper()}]");
        Console.WriteLine($"Feet: [{warrior.ItemFeet?.Name?.ToUpper()}]");
        Console.WriteLine($"Extra: [{warrior.ItemExtra?.Name?.ToUpper()}]");

        Console.WriteLine("\nFighter's Skills:\n");

        Console.WriteLine($"Attack 1: [{warrior.Attack1?.name?.ToUpper()}]");
        Console.WriteLine($"Attack 2: [{warrior.Attack2?.name?.ToUpper()}]");
        Console.WriteLine($"Attack 3: [{warrior.Attack3?.name?.ToUpper()}]");
        Console.WriteLine($"Attack 4: [{warrior.Attack4?.name?.ToUpper()}]");
        Console.WriteLine($"Attack Extra: [DEFEND]");
        Console.WriteLine($"Attack Extra: [CHECK]");

        Console.WriteLine("(Enter name of attack for info)");

        Console.WriteLine("\n[STOP INSPECTING]");

        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        string? inspect = Console.ReadLine()?.ToLower();
        foreach (Attack move in moves)
        {
            if (inspect == move.name?.ToLower())
            {
                Console.Clear();
                if (move == check)
                {
                    Console.WriteLine("Checks the opponent's stats");
                    Console.WriteLine("Priority +2");
                    Thread.Sleep(SLEEP_1_SEC);
                    Console.WriteLine("(Press any key to continue)");
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                    Console.ReadKey();
                }
                else if (move == defend)
                {
                    Console.WriteLine("Halves incoming damage");
                    Console.WriteLine("Priority +2");
                    Thread.Sleep(SLEEP_1_SEC);
                    Console.WriteLine("(Press any key to continue)");
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                    Console.ReadKey();
                }
                else if (
                move.name?.ToLower() == warrior.Attack1?.name?.ToLower() || 
                move.name?.ToLower() == warrior.Attack2?.name?.ToLower() || 
                move.name?.ToLower() == warrior.Attack3?.name?.ToLower() || 
                move.name?.ToLower() == warrior.Attack4?.name?.ToLower())
                {
                    if (move.avoid)
                    {
                        Console.WriteLine("Evades enemy attacks");
                        Console.WriteLine("Priority +1");
                        Console.WriteLine($"Accuracy: [{move.accuracy}]%");
                        Thread.Sleep(SLEEP_1_SEC);
                        Console.WriteLine("(Press any key to continue)");
                        while (Console.KeyAvailable)
                        {
                            Console.ReadKey(true);
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Name: [{move.name}]");
                        Console.WriteLine($"Damage: [{move.minDamage}]-[{move.maxDamage}]");
                        Console.WriteLine($"Accuracy: [{move.accuracy}]%");
                        Console.WriteLine($"Speed: [{move.speed}]");
                        Thread.Sleep(SLEEP_1_SEC);
                        Console.WriteLine("(Press any key to continue)");
                        while (Console.KeyAvailable)
                        {
                            Console.ReadKey(true);
                        }
                        Console.ReadKey();
                    }
                }
            }
            else if (
                inspect == "go back" || 
                inspect == "stop" || 
                inspect == "stop inspecting" || 
                inspect == "[stop inspecting]" || 
                inspect == "back" )
            {
                Console.WriteLine("(Press any key to continue)");
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                Console.ReadKey();
                location = Location.streets;
                break;
            }
        }   
    }

    while (location == Location.fightScene)
    {

        enemy.baseHP = Random.Shared.Next(150, 1200);
        enemy.baseDMG = Random.Shared.Next(50, 300);
        enemy.baseSPD = Random.Shared.Next(25, 300);

        int fighter1HP = warrior.totalHP;
        int fighter2HP = enemy.totalHP + 1;
        int turn = 1;

        string? inputMove = null;

        string enemyName = enemyNames[Random.Shared.Next(enemyNames.Count)] + "-" + enemyNames[Random.Shared.Next(enemyNames.Count)];
        
        while (fighter1HP > 0 && fighter2HP > 0)
        {
            Attack? enemyMove = null;
            Attack? selectedMove = null;

            Console.Clear();
            Console.WriteLine("                 (-~-}-_-_-_-BATTLE-INTERFACE-_-_-_-{-~-)");
            Console.WriteLine($"[TURN: {turn}]");
            Console.WriteLine($"  |>--{fighterName?.ToUpper()}--{fighter1HP}HP--(´-`)--<|     <->     |>--{enemyName.ToUpper()}--{fighter2HP}HP--(`-´)--<|");
            Console.Write($"  [-~-{warrior.Attack1?.name?.ToUpper()}-~-]");   
            Console.WriteLine($"  [-~-{warrior.Attack2?.name?.ToUpper()}-~-]");
            Console.Write($"  [-~-{warrior.Attack3?.name?.ToUpper()}-~-]");
            Console.WriteLine($"  [-~-{warrior.Attack4?.name?.ToUpper()}-~-]");
            Console.WriteLine("  [-~-{CHECK-ENEMY}-~-]  [-~-{DEFEND}-~-]");

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            
            inputMove = Console.ReadLine()?.ToLower();
            Console.WriteLine("");

            foreach (Attack move in moves)
            {
                if (inputMove == move.name?.ToLower())
                {
                    if (move == check)
                    {
                        Console.Clear();
                        Console.WriteLine($"{enemyName} STATS:");
                        Console.WriteLine($"[PWR-{enemy.totalST}]");
                        Console.WriteLine($"[HP-{enemy.totalHP}]");
                        Console.WriteLine($"[SPD-{enemy.totalSPD}]");
                        Console.WriteLine($"[DMG-{enemy.totalDMG}]");
                        if (enemy.totalST > warrior.totalST && fighter2HP > fighter1HP)
                        {
                            Console.WriteLine("Your demise.");
                        }
                        else if (enemy.totalST < warrior.totalST)
                        {
                            Console.WriteLine("A weakling.");
                        }
                        else if (enemy.totalST == warrior.totalST)
                        {
                            Console.WriteLine("Your equal!");
                        }
                        else if (fighter2HP < 100)
                        {
                            Console.WriteLine("Frail and losing..");
                        }
                        else if (fighter2HP > 650)
                        {
                            Console.WriteLine("Standing strong!");
                        }
                        else
                        {
                            Console.WriteLine("In your way...");
                        }
                        Console.WriteLine("\n(Press any key to continue)");
                        while (Console.KeyAvailable)
                        {
                            Console.ReadKey(true);
                        }
                        Console.ReadKey();
                    }
                    else if (move == defend)
                    {
                        selectedMove = defend;
                    }
                    else if (
                    move.name?.ToLower() == warrior.Attack1?.name?.ToLower() || 
                    move.name?.ToLower() == warrior.Attack2?.name?.ToLower() || 
                    move.name?.ToLower() == warrior.Attack3?.name?.ToLower() || 
                    move.name?.ToLower() == warrior.Attack4?.name?.ToLower())
                    {
                        selectedMove = move;
                    }
                }
            }
            if (selectedMove == null)
                continue;
            else
            {
                enemyMove = enemyMoves[Random.Shared.Next(enemyMoves.Count)];
            }

            int accuracy1 = Random.Shared.Next(1, 100);
            int accuracy2 = Random.Shared.Next(1, 100);

            int fighter1SPD = warrior.totalSPD + selectedMove.speed;
            int fighter2SPD = enemy.totalSPD + enemyMove.speed;

            int receivedDMG = 0;
            int sentDMG = 0;

            bool fighter1Miss = false;
            bool fighter2Miss = false;

            
            receivedDMG = enemy.totalDMG + Random.Shared.Next(enemyMove.minDamage, enemyMove.maxDamage);
            sentDMG = warrior.totalDMG + Random.Shared.Next(selectedMove.minDamage, selectedMove.maxDamage);

            if (selectedMove == defend)
            {
                receivedDMG /= 2;
                sentDMG = 0;
            }
            if (enemyMove == defend)
            {
                sentDMG /= 2;
                receivedDMG = 0;
            }

            if (selectedMove.avoid == true)
            {
                sentDMG = 0;
                receivedDMG = 0;
            }
            if (enemyMove.avoid == true)
            {
                sentDMG = 0;
                receivedDMG = 0;
            }
            if (accuracy1 < selectedMove.accuracy)
                fighter2HP -= sentDMG;
            else fighter1Miss = true;
            if (accuracy2 < enemyMove.accuracy)
                fighter1HP -= receivedDMG;
            else fighter2Miss = true;

            int firstMove = Math.Max(fighter1SPD, fighter2SPD);
            int secMove = Math.Min(fighter1SPD, fighter2SPD);

            bool fighter1Moved = false;
            bool fighter2Moved = false;

            if (fighter1SPD == fighter2SPD)
            {
                Console.WriteLine("Fighter speeds are equal, it's a clash!");
                Thread.Sleep(SLEEP_1_SEC);
                Console.WriteLine("Neither fighter gets hit!");
            }
            if (selectedMove == defend && enemyMove == defend)
            {
                Console.WriteLine("Both fighters defend!");
                Thread.Sleep(SLEEP_1_SEC);
                Console.WriteLine("Neither fighter gets hit!");
                goto endTurn;
            }

            if (firstMove == fighter1SPD)
            {
                fighter1Moved = true;
                Console.WriteLine($"{fighterName} is preparing to {selectedMove.name}!");
                if (!fighter1Miss && !enemyMove.avoid)
                    if (selectedMove.avoid == true)
                    {
                        Console.WriteLine($"{fighterName} will avoid the next attack!");
                    }
                    else if (selectedMove == defend)
                    {
                        Console.WriteLine($"{fighterName} takes half damage from the next attack!");
                    }
                    else
                    {
                        Console.WriteLine($"{fighterName} deals {sentDMG} DMG to {enemyName}");
                    }
                else if (fighter1Miss)
                    Console.WriteLine($"{fighterName} missed!");
                else if (enemyMove.avoid)
                    Console.WriteLine($"{enemyName} avoided it!");
            }
            else if (firstMove == fighter2SPD)
            {
                fighter2Moved = true;
                Console.WriteLine($"{enemyName} is preparing to {enemyMove.name}!");
                if (!fighter2Miss && !selectedMove.avoid)
                    if (enemyMove.avoid == true)
                    {
                        Console.WriteLine($"{enemyName} will avoid the next attack!");
                    }
                    else if (enemyMove == defend)
                    {
                        Console.WriteLine($"{enemyName} takes half damage from the next attack!");
                    }
                    else
                    {
                        Console.WriteLine($"{enemyName} deals {receivedDMG} DMG to {fighterName}");
                    }
                else if (fighter2Miss)
                    Console.WriteLine($"{enemyName} missed!");
                else if (selectedMove.avoid)
                    Console.WriteLine($"{fighterName} avoided it!");
            }

            if (fighter2Moved && fighter1HP <= 0 || fighter1Moved && fighter2HP <= 0)
                goto matchEnd;

            if (secMove == fighter1SPD)
            {
                Console.WriteLine($"{fighterName} is preparing to {selectedMove.name}!");
                if (!fighter1Miss && !enemyMove.avoid)
                    if (selectedMove.avoid == true)
                        Console.WriteLine($"{fighterName} failed!");
                    else if (selectedMove == defend)
                    {
                        Console.WriteLine($"{fighterName} takes half damage from the next attack!");
                    }
                    else
                    {
                        Console.WriteLine($"{fighterName} deals {sentDMG} DMG to {enemyName}");
                    }
                else if (fighter1Miss)
                    Console.WriteLine($"{fighterName} missed!");
                else if (enemyMove.avoid)
                    Console.WriteLine($"{enemyName} avoided it!");
            }
            
            else if (secMove == fighter2SPD)
            {
                Console.WriteLine($"{enemyName} is preparing to {enemyMove.name}!");
                if (!fighter2Miss && !selectedMove.avoid)
                    if (enemyMove.avoid == true)
                        Console.WriteLine($"{enemyName} will avoid the next attack!");
                    else if (enemyMove == defend)
                    {
                        Console.WriteLine($"{enemyName} takes half damage from the next attack!");
                    }
                    else
                    {
                        Console.WriteLine($"{enemyName} deals {receivedDMG} DMG to {fighterName}");
                    }
                else if (fighter2Miss)
                    Console.WriteLine($"{enemyName} missed!");
                else if (selectedMove.avoid)
                    Console.WriteLine($"{fighterName} avoided it!");
            }
            endTurn:
            
            Console.WriteLine("(Press any key to continue)");

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            Console.ReadKey();
            turn++;
        }

        matchEnd:
        if (fighter1HP <= 0)
        {
            Console.WriteLine("\nYou lost!");
            Console.WriteLine($"{enemyName} is victorious!");
            Console.WriteLine($"(Lost ${bet} from bet)");
            bet = 0;
        }
        else if (fighter2HP <= 0)
        {
            int dollarLoot = Random.Shared.Next(1, 15);
            float betGain = bet * 1.5f;
            Console.WriteLine("\nYou won!");
            Console.WriteLine($"{fighterName} is victorious!");
            Console.WriteLine($"Won $[{betGain}] (bet) + $[{dollarLoot}]");
            dollars += (int)betGain + dollarLoot;
            bet = 0;
            dollarLoot = 0;
            betGain = 0;

        }
        else if (fighter1HP <= 0 && fighter2HP <= 0)
        {
            Console.WriteLine("\nDraw!");
            Console.WriteLine("You lose nothing, you earn nothing");
        }
        Thread.Sleep(SLEEP_2_SEC);
        Console.WriteLine("(Press any key to continue)");
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        Console.ReadKey();
        match++;
        location = Location.arena;
    }
}

class Fighter
{
    public int baseHP;
    public int totalHP => itemHP + baseHP;
    public int baseDMG;
    public int totalDMG => itemDMG + baseDMG;
    public int baseSPD;
    public int totalSPD => itemSPD + baseSPD;
    public int itemST => itemHP + itemDMG + itemSPD;
    public int baseST => baseHP + baseDMG + baseSPD;
    public int totalST => itemST + baseST;

    public required Attack Attack1;
    public required Attack Attack2;
    public required Attack Attack3;
    public required Attack Attack4;

    public Item? ItemWeapon;
    public Item? ItemHead;
    public Item? ItemChest;
    public Item? ItemLegs;
    public Item? ItemFeet;
    public Item? ItemExtra;

    public int itemHP =>
        (ItemWeapon?.Health ?? 0) +
        (ItemHead?.Health ?? 0) +
        (ItemChest?.Health ?? 0) +
        (ItemLegs?.Health ?? 0) +
        (ItemFeet?.Health ?? 0) +
        (ItemExtra?.Health ?? 0);

    public int itemDMG =>
        (ItemWeapon?.Damage ?? 0) +
        (ItemHead?.Damage ?? 0) +
        (ItemChest?.Damage ?? 0) +
        (ItemLegs?.Damage ?? 0) +
        (ItemFeet?.Damage ?? 0) +
        (ItemExtra?.Damage ?? 0);

    public int itemSPD =>
        (ItemWeapon?.Speed ?? 0) +
        (ItemHead?.Speed ?? 0) +
        (ItemChest?.Speed ?? 0) +
        (ItemLegs?.Speed ?? 0) +
        (ItemFeet?.Speed ?? 0) +
        (ItemExtra?.Speed ?? 0);
}

class Attack
{
    public required string name;
    public int minDamage;
    public int maxDamage;
    public int accuracy;
    public int speed;
    public bool avoid;
}

class Item
{
    public string Name;
    public int Price;
    public int Damage;
    public int Speed;
    public int Health;
    public string Slot;
    public Attack? Attack1;
    public Attack? Attack2;

    public Item(string name, int price, int damage, int speed, int health, string slot, Attack? attack1, Attack? attack2)
    {
        Name = name;
        Price = price;
        Damage = damage;
        Speed = speed;
        Health = health;
        Slot = slot;
        Attack1 = attack1;
        Attack2 = attack2;
    }
}

enum ShopLocation
{
    weaponAisle,
    equipmentAisle
}

enum ArenaSection
{
    bettingSection,
    fightingSection,
}

enum Location
{
    streets,
    shop,
    arena,
    inspectFighter,
    fightScene,
}