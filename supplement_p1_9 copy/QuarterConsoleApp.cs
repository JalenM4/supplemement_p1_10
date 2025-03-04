using System;
using System.Collections;
using System.Collections.Generic;
using supplement_p1_9;

namespace supplement_p1_9
{
    /// <summary>
    /// Is a console interface for interacting with quarter class and a
    /// floating point sequence. Allows users to add quartersand handles potential InvalidSequenceException
    /// </summary>
    public class Program
    {
        private static int _sequenceIndex = 0;
        /// <summary>
        /// Main entry point of the console application  handles user interaction
        /// to add quarter objects and displays them, catching and reporting any
        /// InvalidSequenceExceptions that happen
        /// </summary>
        /// <param name="args"> Command Line arguments </param>
        static void Main(string[] args)
        {
            List<Class1.Quarter> quarters = new List<Class1.Quarter>();
            Class1.FloatingPointSequence sequence = new Class1.FloatingPointSequence();

            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add a Quarter");
                Console.WriteLine("2. Quit");
                Console.Write("Enter your choice (1 or 2): ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    try{
                        double newNumber = GetNextNumber(sequence);
                        Class1.Quarter newQuarter = new Class1.Quarter(newNumber);
                        quarters.Add(newQuarter);

                        Console.WriteLine("\nCurrent Quarters:");
                        DisplayQuarters(quarters);

                        Console.WriteLine("\nQuarter added successfully");
                    }
                    catch (Class1.InvalidSequenceException ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Application will now close.");
                        break;
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"\nError: Argument out of range: {ex.Message}");
                        Console.WriteLine("Application will now close.");
                        break;
                    }

                }
                else if (choice == "2")
                {
                    Console.WriteLine("Exiting Apllication.");
                    break;
                }
                else{
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }
            }
        }
        /// <summary>
        /// Retrieves the next floating point number from the given sequence
        /// </summary>
        /// <param name="sequence" the FloatingPointSequence to get the number from></param>
        /// <returns> The next double from the sequence</returns>
        public static double GetNextNumber(Class1.FloatingPointSequence sequence)
        {
             if (_sequenceIndex < sequence.Count())
        {
            List<double> values = (List<double>)sequence.Values;
            double number = values[_sequenceIndex];
            _sequenceIndex++;
            return number;
        }
        else
        {
            return 0.0; // Or handle the end of the sequence differently
        }
        }
        
        /// <summary>
        /// Displays the current list of Quarters to the console grouped by their
        /// quarter interval
        /// </summary>
        /// <param name="quarters">The list of Quarter objects to display</param>
        public static void DisplayQuarters(List<Class1.Quarter> quarters)
        {
            if (quarters.Count == 0)
            {
                Console.WriteLine("No quarters added yet.");
                return;
            }

            List<List<Class1.Quarter>> groupedQuarters = GroupQuarters(quarters);

            for (int i = 0; i < groupedQuarters.Count; i++)
            {
                Console.Write($"Quarter {i}: ");
                foreach (var quarter in groupedQuarters[i])
                {
                    Console.Write($"{quarter.Value} ");
                }
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Groups of list of quarter objects into sub-list based on their quarter intervals
        /// </summary>
        /// <param name="quarters">The list of quarter objects to group</param>
        /// <returns>a list of lists where each inner list contains quarters of the same interval</returns>
        public static List<List<Class1.Quarter>> GroupQuarters(List<Class1.Quarter> quarters)
        {
            List<List<Class1.Quarter>> groupedQuarters = new List<List<Class1.Quarter>>();
            groupedQuarters.Add(new List<Class1.Quarter>());
            groupedQuarters.Add(new List<Class1.Quarter>());
            groupedQuarters.Add(new List<Class1.Quarter>());
            groupedQuarters.Add(new List<Class1.Quarter>());

        foreach (var quarter in quarters)
        {
            if (Math.Round(quarter.Value, 2) < 0 || Math.Round(quarter.Value, 2) > 1)
            {
                throw new ArgumentOutOfRangeException("quarter.Value", "Quarter value must be between 0 and 1, inclusive.");
            }
        }
            
            foreach (var quarter in quarters)
            {
                int groupIndex = -1; // Initialize with an invalid index

                if (Math.Round(quarter.Value, 2) >= 0 && Math.Round(quarter.Value, 2) < 0.25)
                {
                    groupIndex = 0;
                    Console.WriteLine($"Assigned to group 0");
                }

                if (Math.Round(quarter.Value) >= 0 && Math.Round(quarter.Value, 2) < 0.25)
                    groupIndex = 0;
                else if (Math.Round(quarter.Value) >= 0.25 && Math.Round(quarter.Value, 2) < 0.5)
                    groupIndex = 1;
                else if (Math.Round(quarter.Value) >= 0.5 && Math.Round(quarter.Value, 2) < 0.75)
                    groupIndex = 2;
                else if (Math.Round(quarter.Value) >= 0.75 && Math.Round(quarter.Value, 2) <= 1)
                    groupIndex = 3;

                if (groupIndex >= 0 && groupIndex < groupedQuarters.Count) // Check index validity
                {
                    groupedQuarters[groupIndex].Add(quarter);
                }
                else
                {
                    Console.WriteLine($"Throwing exception for quarter.Value: {quarter.Value}");
                    throw new ArgumentOutOfRangeException("quarter.Value", "Quarter value is not within any valid group range."); 
                }
         }

            return groupedQuarters;
        }
       
        
       
    }
}