using System;

namespace task04
{

        public interface ISpaceship
        {
            void MoveForward();
            void Rotate(int angle);
            void Fire();
            int Speed { get; }
            int FirePower { get; }
        }

    public class Cruiser : ISpaceship
    {
       public int FirePower { get; } = 100;
       public  int Speed { get; } = 50;


        public void MoveForward()
        {
            Console.WriteLine("Крейсер летит со скоростбю:");
            Console.Write(Speed);   
        }

        public void Rotate(int angle)
        {
            Console.WriteLine($"Крейсер повернул на{angle} градусов:");
        }

        public void Fire()
        {
            Console.WriteLine($"Крейсер выпустил ракету мощностю{FirePower}");
        }
    }

    public class Fighter : ISpaceship
    {
        public int FirePower { get; } = 15;
        public int Speed { get; } = 100;


        public void MoveForward()
        {
            Console.WriteLine("Истребитель летит со скоростбю:");
            Console.Write(Speed);
        }

        public void Rotate(int angle)
        {
            Console.WriteLine($"Истребитель повернул на{angle} градусов:");
        }

        public void Fire()
        {
            Console.WriteLine($"Истребитель выпустил ракету мощностью{FirePower}");
        }
    }
}
