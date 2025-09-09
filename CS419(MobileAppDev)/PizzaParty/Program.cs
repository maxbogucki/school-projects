using System;

namespace PizzaParty
{
    internal class Program
    {
        // Declare constants
        const double SLICE_SIZE = 14.124;
        const int SLICES_PER_PERSON = 4;

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("*** Pizza Party ***");
            Console.WriteLine();

            int people = 0;
            while (true)
            {
                Console.Write("Enter the number of people attending the party: ");
                bool isValid = int.TryParse(Console.ReadLine(), out people);

                if (isValid && people >= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Enter a number greater than or equal to 3.\n");
                }
            }

            int diameter = 0;
            while (true)
            {
                Console.Write("Enter the diameter of the pizza in inches (14, 16, 18): ");
                bool isValid = int.TryParse(Console.ReadLine(), out diameter);

                if (isValid && (diameter == 14 || diameter == 16 || diameter == 18))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid pizza size.\n");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You should purchase {CalculatePizza(people, diameter)} pizza(s).");
        }

        static double CalculatePizza(int people, int diameter)
        {
            double radius = diameter / 2.0;
            double area = Math.PI * Math.Pow(radius, 2);
            double slices = area / SLICE_SIZE;
            double total = people * SLICES_PER_PERSON;
            double required = Math.Ceiling(total / slices);
            return required;
        }
    }
}
