using System.Security.Cryptography;

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
        Random Roll = new Random();
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

        public void CombatCalc (PlayerTurn player1turn, PlayerTurn player2turn)
        {
            // Player 1 Variables
            Character Player1 = player1turn.PlayerChar;
            Item Player1Item = null;

            // Player 1 Combat Flags
            bool Player1Hit = false;
            bool Player1Parry = false;

            // player 2 Variables
            Character Player2 = player2turn.PlayerChar;
            Item Player2Item = null;

            // Player 2 Combat Flags
            bool Player2Hit = false;
            bool Player2Parry = false;

            //  Make Rolls
            float AccRoll = Roll.Next(1,100);
            float ParryRoll = Roll.Next(1,100);

            // FIND and DEFINE
            foreach (Item BattleItem in Items)
            {
                if (BattleItem.ItemID == player1turn.WeaponID)
                {
                    Player1Item = BattleItem;
                    System.Console.WriteLine("[ " + Player1.CharName + " ]" + " USED " + Player1Item.ItemName);
                }

                if (BattleItem.ItemID == player2turn.WeaponID)
                {
                    Player2Item = BattleItem;
                    System.Console.WriteLine("[ " + Player2.CharName + " ]" + " USED " + Player2Item.ItemName);
                }
            }

            // OUTCOME PRINTING & CALCs

            // PHASE 1 Checks

            if (Player1Item.Accuracy >= AccRoll)
            {
                Player1Hit = true;

            }
            if (Player2Item.Accuracy >= AccRoll)
            {
                Player2Hit = true;
            }

            // PHASE 2 Checks

            if (Player1Hit == false)
            {
                System.Console.WriteLine("[ " + Player1.CharName + " ]" + " - ATTACK MISSED! ");
            }
            else
            {
                if (Player2Item.Parry >= ParryRoll)
                {
                    Player2Parry = true;
                    System.Console.WriteLine("[ " + Player2.CharName + " ]" + " - ATTACK PARRIED ! ");
                }
            }

            if (Player2Hit == false)
            {
                System.Console.WriteLine("[ " + Player2.CharName + " ]" + " - ATTACK MISSED! ");
            }
            else
            {
                if (Player1Item.Parry >= ParryRoll)
                {
                    Player1Parry = true;
                    System.Console.WriteLine("[ " + Player1.CharName + " ]" + " - ATTACK PARRIED ! ");
                }
            }

            // PHASE 3 

            if (Player1Hit == true && Player2Parry == false)
            {
                System.Console.WriteLine("[ " + Player1.CharName + " ]" + " - DEALT " + Player1Item.Damage + " DMG");
                Player2.TakeDamage(Player1Item.Damage);
            }

            if (Player2Hit == true && Player1Parry == false)
            {
                System.Console.WriteLine("[ " + Player2.CharName + " ]" + " - DEALT " + Player2Item.Damage + " DMG");
                Player1.TakeDamage(Player2Item.Damage);
            }
        }
        
    }

    class PlayerTurn (int weaponID, Character playerChar)
    {
        public int WeaponID = weaponID;
        public Character PlayerChar = playerChar;
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

            // Turncount
            int TurnCount = 1;

            // GAME LOOP
            while (Player1.IsAlive() && Player2.IsAlive())
            {
                PrintUtility.Border();
                System.Console.WriteLine("[ TURN : " + TurnCount + " ]");
                System.Console.WriteLine("[ " + Player1Name +  " | HP : " + Player1.CharHealth + " ]");
                System.Console.WriteLine("[ " + Player2Name +  " | HP : " + Player2.CharHealth + " ]");

                // PLAYER 1
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

                PlayerTurn Player1Turn = new PlayerTurn(weaponID: Player1Move, playerChar: Player1);

                // PLAYER 2
                System.Console.WriteLine("[ " + Player2.CharName +" ]" + "CHOOSE WEAPON TO USE FOR TURN: " + TurnCount);
                BattleEngine.ShowItems();

                PrintUtility.Border();
                int Player2Move = 0;
                bool Player2MoveIsValid = false;
                while (Player2MoveIsValid == false)
                {
                    if (int.TryParse(Console.ReadLine(), out Player2Move) && BattleEngine.IsValidItem(Player2Move))
                    {
                        Player2MoveIsValid = true;
                    }
                    else
                    {
                        System.Console.WriteLine("[!] - [ PLEASE ENTER A VALID ITEM # ]");
                    }
                }
                PrintUtility.Border();

                PlayerTurn Player2Turn = new PlayerTurn(weaponID: Player2Move, playerChar: Player2);

                // Proceed To Combat
                BattleEngine.CombatCalc(Player1Turn, Player2Turn);

                // Before Next LOOP
                System.Console.WriteLine("[ TURN : " + TurnCount + " ]" + " - HAS ENDED");
                TurnCount++;

            }
            
            // Winner Announcement
            if (!Player1.IsAlive() && !Player2.IsAlive())
            {
                System.Console.WriteLine("[ BATTLE IS A DRAW BOTH PLAYERS HAVE BEEN DEFEATED ]");
            }
            else if (Player1.IsAlive() && !Player2.IsAlive())
            {
                System.Console.WriteLine("[ " + Player1.CharName + " HAS WON!!! ]");
            }
            else if (Player2.IsAlive() && !Player1.IsAlive())
            {
                System.Console.WriteLine("[ " + Player2.CharName + " HAS WON!!! ]");
            }

        }
    }
}