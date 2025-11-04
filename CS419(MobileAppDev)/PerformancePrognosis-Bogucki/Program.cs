using System;

class Program
{
    static void Main(string[] args)
    {
        int counter = 0;
        string input;
        char predictedGrade;

        Console.WriteLine("*************************************************************");
        Console.WriteLine("Performance Prognosis Inventory for Analytical Chemistry");
        Console.WriteLine("*************************************************************\n");

        Console.WriteLine("The inventory below lists behaviors that you should exhibit to excel in Analytical\r\nChemistry. Please enter true or false for each of the following statements describing the\r\nway you will study in this class.\r\n");

        Console.Write("1. I will always read the lecture material before I go to lecture: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("2. I will go over my lecture notes as soon as possible after lecture to rework them and mark problem areas: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("3. I will learn the relevant concepts from General Chemistry so that I have the background necessary to understand the material in Analytical Chemistry: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("4. I will try to work on the homework problems without looking at the example problems or my notes from class: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("5. I will go to office hours or tutoring regularly to discuss problems on the homework: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("6. I will rework all of the homework problems before the test or quiz: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("7. I will spend some time studying analytical chemistry at least five days per week (outside of class time): ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("8. I will “teach” concepts to friends, myself in the mirror, stuffed animals, imaginary students, etc.: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("9. I will make flashcards and use mnemonics for myself to help remember facts and equations: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("10. I will make diagrams or draw mental pictures of the concepts, experimental procedures, and instruments discussed in class: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("11. I will actively participate in my study group where we will discuss homework problems and quiz ourselves on the material: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("12. I will rework all of the quiz and test items I have missed before the next class session: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        Console.Write("13. I know that I can make an A in this class, and will put forth the effort to do so: ");
        input = Console.ReadLine();
        if (input.ToLower() == "true") counter++;
        Console.WriteLine();

        if (counter >= 10 && counter <= 13)
        {
            predictedGrade = 'A';
        }
        else if (counter >= 6)
        {
            predictedGrade = 'B';
        }
        else if (counter >= 4)
        {
            predictedGrade = 'C';
        }
        else if (counter >= 2)
        {
            predictedGrade = 'D';
        }
        else
        {
            predictedGrade = 'F';
        }

        Console.WriteLine("Based on your study habits, the predicted grade for your performance this semester in Analytical Chemistry will be: " + predictedGrade);

    }
}