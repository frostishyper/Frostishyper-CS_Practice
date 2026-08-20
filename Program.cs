using System;

namespace  TestBed
{
    public class Character
    {
        public string name = "Player";
        public float health = 100f;
        public float damage = 10f;

        public void TakeDamage()
        {   
            var newhealth = health - damage;
            health = newhealth;
            this.DamageReport(health, damage);
        }

        public bool IsAlive()
        {
            return this.health > 0;
        }

        public void DamageReport(float health, float damage)
        {
            System.Console.Write(name + " Has Taken " + damage + " Damage ");
            var status = this.IsAlive();
            if (status == false)
            {
                System.Console.Write("And Has Died");
            }
            else
            {
                System.Console.Write("And Is Still Alive With " + health + "HP Remaining");
            }
        }

        static void Main (string[] args)
        { 
            var Char = new Character();
            System.Console.WriteLine("Gretings " + Char.name);
            Char.TakeDamage();
            
        }
    }
}

