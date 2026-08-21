namespace BattleSim
{
    // CHARACTER
    class Character (String PlayerName)
    {
        // CHARACTER STATS
        public String CharName = PlayerName;
        public float CharHealth = 100;


        // APPLIES DAMAGE (METHOD)
        public void TakeDamage(float Damage)
        {
            var newhealth = CharHealth - Damage;
            CharHealth = newhealth;
        }

        // CHECK LIFE STATUS (METHOD)
        public bool IsAlive()
        {   
            return this.CharHealth > 0;

        }
    }

    public class PrintUtility
    {
        public static void Border()
        {
            System.Console.WriteLine("====================================================================================");
        }
    }

    // ITEM
    class Item (int itemID, String itemName, float damage, float accuracy, float parry)
    {
        public String ItemName = itemName;
        public int ItemID = itemID;
        public float Damage = damage;
        public float Accuracy = accuracy;
        public float Parry = parry;

        // Print Stats Method
        public void PrintStats()
        {
            System.Console.WriteLine("[ " + ItemID + " " + ItemName + " ]" + " - [" + "POWER:" + Damage + " ]" + " - [" + "ACCURACY:" + Accuracy + "% ]" + " - [" + "PARRY:" + Parry + "% ]");
        }
    }

    class BattleEngine()
    {
        // Generate Items (ITEMS JUST NEED TO GET ADDED HERE)
        // (ItemID, ItemName, DMG, ACCURACY%, PARRY%)
        List<Item> Items = new List<Item>
        {
        new (itemID: 1, itemName:"Sword", damage:20f, accuracy:95f, parry:20f),
        new (itemID: 2, itemName:"Katana", damage:30f, accuracy:75f, parry:15f),
        new (itemID: 3, itemName:"Claymore", damage:40f, accuracy:50f, parry:5f)
        };
       

        public void ShowItems ()
        {
            System.Console.WriteLine("[ Input Item #ID To Use Them ]");
            foreach (Item BattleItem in Items)
            {
                BattleItem.PrintStats();
            }
        }

        public bool IsValidItem (int playerMove)
        {
            foreach (Item BattleItem in Items)
            {
                if (BattleItem.ItemID == playerMove)
                {
                    return true;
                }
            }
            return false;
        }

        public void CombatCalc (PlayerTurn Player1turn, PlayerTurn Player2turn)
        {
            // WIP
        }
        
    }

    class PlayerTurn (int weaponID, String playerName, float playerHP)
    {
        public int weaponID = weaponID;
        public String PlayerName = playerName;
        public float PlayerHP = playerHP;

    }

    // GAME
    class RunningGame
    {
        static public String  Player1Name;
        static public String Player2Name;
        // START
        static void Main(string[] args)
        {
            // Opener
            System.Console.WriteLine(" ");
            System.Console.WriteLine("[WELCOME TO BATTLE GAME]");
            System.Console.WriteLine(" ");
            System.Console.WriteLine(" Player 1 Type Your Name : ");
            Player1Name = Console.ReadLine();
            var Player1 = new Character(Player1Name);
            System.Console.WriteLine(" Player 2 Type Your Name : ");
            Player2Name = Console.ReadLine();
            var Player2 = new Character(Player2Name);
            System.Console.WriteLine(" ");
            System.Console.WriteLine("[BATLLE START!!!]");
            System.Console.WriteLine(" ");
            // Initialize Components Needed For The Game Loop
            var BattleEngine = new BattleEngine();

            // GAME LOOP
            while (Player1.IsAlive() && Player2.IsAlive())
            {
                // Turncount
                int TurnCount = 1;

                // Player 1
                System.Console.WriteLine("[ " + Player1.CharName +" ]" + "CHOOSE WEAPON TO USE FOR TURN: " + TurnCount);
                BattleEngine.ShowItems();

                PrintUtility.Border();
                int Player1Move = 0;
                bool Player1MoveIsValid = false;
                while (Player1MoveIsValid == false)
                {
                    if (int.TryParse(Console.ReadLine(), out Player1Move) && BattleEngine.IsValidItem(Player1Move))
                    {
                        Player1MoveIsValid = true;
                    }
                    else
                    {
                        System.Console.WriteLine("[!] - [ PLEASE ENTER A VALID ITEM# ]");
                    }
                }
                PrintUtility.Border();

                PlayerTurn Player1Turn = new PlayerTurn(weaponID: Player1Move, playerName:Player1.CharName,playerHP:Player1.CharHealth);

                // Player 2
                System.Console.WriteLine("[ " + Player2.CharName +" ]" + "CHOOSE WEAPON TO USE FOR TURN: " + TurnCount);
                BattleEngine.ShowItems();

                PrintUtility.Border();
                int Player2Move = 0;
                bool Player2MoveIsValid = false;
                while (Player2MoveIsValid == false)
                {
                    if (int.TryParse(Console.ReadLine(), out Player2Move) && BattleEngine.IsValidItem(Player1Move))
                    {
                        Player2MoveIsValid = true;
                    }
                    else
                    {
                        System.Console.WriteLine("[!] - [ PLEASE ENTER A VALID ITEM# ]");
                    }
                }
                PrintUtility.Border();

                PlayerTurn Player2Turn = new PlayerTurn(weaponID: Player2Move, playerName:Player2.CharName,playerHP:Player2.CharHealth);

                // Proceed To Combat
                BattleEngine.CombatCalc(Player1Turn, Player2Turn);
                
            }
        }
    }
}

