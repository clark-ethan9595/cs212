/*
 * Project 1 - CS212
 * Ethan Clark
 * Professor Plantinga
 * Compute the floor(log(log(x))) of a number without using any special functions
 * Due Friday September 25
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class ProjectOne
    {
        //Main Function to ask the user to enter a value for N
        //Returns the Floored Log(Log(N))
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a number N here: ");

                //Read in the N value from the User
                double var1 = double.Parse(Console.ReadLine());

                //Compute the Log Log of the value entered by the user
                int var2 = ComputeLog(var1);
                int var3 = ComputeLog(var2);

                //Write out the answer to the Console for the user
                Console.WriteLine("The floor of log(log({0})) = {1}.", var1, var3);

                //Give the user an opportunity to quit
                Console.WriteLine("Enter 'q' to exit the program or any other key to continue.");
                Console.WriteLine('\n');

                //Determine if user wants to quit
                string user_input = Console.ReadLine();
                if (user_input == "q")
                {
                    break;
                }
            }
        }

        //Function to compute the log of a value
        static int ComputeLog(double value1)
        {
            //Counter to keep track how many times you can divide by 2
            int counter = 0;

            //As long as the value is greater than 1
            while (value1 > 1)
            {
                value1 /= 2;
                counter++;
            }

            //Return the value of the counter
            return counter;
        }
    }
}
