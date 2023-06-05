using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace PoePt1
{

    public class RecipeRun
    { //class to run program
        List<Recipe> recipes = new List<Recipe>();
         
        public static void Main(string[] args)
        {
            RecipecalorieChecker calorieChecker = new RecipecalorieChecker();
            calorieChecker.CalorieNotification += HandleCalorieNotification;

            //main method to run program
            Console.WriteLine("Sanele Cooks (Beta v1.0)");

            Recipe recipe = new Recipe();  //create instance of Recipe class
            
            string recipeName = recipe.name;
            double calories = recipe.totalcal;
            calorieChecker.CheckCalories(recipeName, calories);
            try
            {
                recipe.menu();  //call menu method
                
            }
            catch (IOException)
            {
                Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }
        }

        static void HandleCalorieNotification( string recipename, double calories)
        {          
           Console.WriteLine($"The recipe {recipename} has {calories} calories , which exceeds 300");
        }
        
        public void mainMenu()
        {
           do{
            Console.WriteLine("Welcome to Sanele Cooks");
            Console.WriteLine("1.View all available recipes ");
            Console.WriteLine("2.Exit");
            int pick = Convert.ToInt32(Console.ReadLine().Trim());
                switch (pick)
                {
                    case 1:
                        displayNames();
                        break;
                    case 2:
                        Console.WriteLine("Happy Coooking ,Goodbye");
                        Environment.Exit(0);
                        break;
                    default: //returns user to menu if invalid option is selected
                        Console.WriteLine("Invalid operation");
                        break;
                }
            } while (pick <= 2);
        }
        public void displayNames()
        {
            Console.WriteLine("Available recipes:");
            Console.WriteLine("  ");
            // shows list of recipe names in alphabetical order

            List<string> recipeNames = new List<string>();
            recipes.ForEach(recipe => recipeNames.Add(recipe.name));
            recipes.ForEach(recipe => Console.WriteLine(recipe.name));
            Console.WriteLine("Which recipe would you like to view ? , enter it's name");
            string search = Console.ReadLine().Trim();
             foreach (var rec in recipes)
            {
                if (search.Contains(rec.name))
                {
                    rec.View();
                }
            }
        }
    }

    public delegate void CalorieNotificationDelegate(string recipename, double calories);
    public class RecipecalorieChecker
    {
        public event CalorieNotificationDelegate CalorieNotification;
     
        public void CheckCalories(string recipename, double calories)
        {
            if (calories > 300)
            {
                CalorieNotification?.Invoke(recipename, calories);
            }
        }
    }


    public class Recipe
    { //class to store recipe information

        //class variables
        public string? name { get; set; }
        private static int numIngredients { get; set; }
        private int steps { get; set; }
        private List<string> ingredientName = new List<string>();
        private static List<double> quantity = new List<double>();
        private List<string> quantityUnit = new List<string>();
        private List<string> stepInfo = new List<string>();
        public  List<double> calories = new List<double>();
        List<double> defaultValues = new List<double>(); //list to store default values of quantity
        List<double> defaultCal = new List<double>();  //list to store default calories entered 
        public double totalcal;


        private void entryPrimary()
        { //method to capture recipe name , number of ingredients and steps
            try
            {
                while (string.IsNullOrWhiteSpace(name) && (numIngredients < 0) && (steps < 0))
                {
                    //try catch block to handle exceptions
                    Console.WriteLine("Enter the name of the recipe");
                    name = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(name)) { throw new IOException(); }   //check if user has entered values 
                    Console.WriteLine($"Enter the number of ingredients for the {name}");
                    numIngredients = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"How many steps are invovled in making the {name}");
                    steps = Convert.ToInt32(Console.ReadLine());

                    for (int s = 1; s <= steps; s++)
                    {
                        Console.WriteLine($"Describe step {s}");
                        string aStep = Console.ReadLine().Trim();
                        if (!string.IsNullOrWhiteSpace(aStep))
                            stepInfo[s] = aStep;
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("* No magic steps here chief !, now go ahead and enter an actual step ");
                            Console.ResetColor();
                            entryPrimary();
                        }
                    }
                }

            }
            catch (IOException)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Come on don't be an idiot , enter the right values");
                Console.ResetColor();
                entryPrimary();  //call method again if exception is caught
            }
            catch (FormatException)
            {
                Console.WriteLine("An error on our side occured , please try again");
                entryPrimary(); //call method again if exception is caught
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("An error on our side occured , please try again");
                entryPrimary(); //call method again if exception is caught
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("An error on our side occured , please try again");
                entryPrimary(); //call method again if exception is caught
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An error on our side occured , please try again");
                menu(); //return user to menu if exception is caught
            }

            entrySecondary(); //call method to capture ingredient information
        }
        private void entrySecondary()
        {  //method to capture ingredient information
            List<string> Measurements = new List<string>() { "gram's", "kg", "teaspoons", "tablespoons", "cups", "liters", "N.A" };
            for (int i = 0; i < numIngredients; i++)
            {
                try
                {
                    while (string.IsNullOrWhiteSpace(ingredientName[i]) || quantity[i] <= 0 || string.IsNullOrWhiteSpace(quantityUnit[i]))
                    { //loop to capture ingredient information
                        Console.WriteLine($"What's the name of ingredient {i}?");
                        string aIngredient = Console.ReadLine().Trim();
                        if (!string.IsNullOrWhiteSpace(aIngredient)) { ingredientName[i] = aIngredient; }
                        else throw new IOException(); //check if user has entered values

                        Console.WriteLine($"How many calories does the {calories[i]} have ?");
                        int aCal = Convert.ToInt32(Console.ReadLine().Trim());
                        if (aCal >= 0) { calories[i] = aCal; }

                        Console.WriteLine($"How many {ingredientName[i]}s will we need?");
                        quantity[i] = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine($"How will we measure the {ingredientName}(s), Example 'gram's','kg', 'teaspoons','tablespoons', 'cups' or 'liters'.Emter 'N.A', if unnecessary ");
                        string aUnit = Console.ReadLine().ToLower().Trim();
                        if (!string.IsNullOrWhiteSpace(aUnit) && Measurements.Any(aUnit.Contains))
                            quantityUnit[i] = aUnit;

                        if (string.IsNullOrWhiteSpace(ingredientName[i]) || quantity[i] <= 0 || string.IsNullOrWhiteSpace(quantityUnit[i]))
                        { //check if user has entered values
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("*Please enter values to proceed");
                            Console.ResetColor();
                            entrySecondary();
                        }
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Let's be smart here!, now go ahead and enter the right values, PLEASE!!!");
                    entrySecondary(); //recursive call to method if exception is caught
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();  // returns user to menu if data entry is hindered 
                }
                catch (FormatException)
                {
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();
                }
            }
            Console.WriteLine($" A recipe for the {name} has been created. As Malema once said ''We are cruising nicely '' "); //confirmation message
            addcalories();  
        }

        private void Format()
        {
            //method to change the quantity amount quantity Unit according result of Scale method
            try
            {
                for (int i = 0; i < numIngredients; i++)
                {
                    //converts between grams and kilograms
                    if (quantity[i] >= 1000 && quantityUnit[i].Contains("grams"))
                    {
                        quantity[i] = quantity[i] / 1000;
                        quantityUnit[i] = "kg";
                    }
                    else if (quantity[i] < 1 && quantityUnit[i].Contains("kg"))
                    {
                        quantity[i] = quantity[i] * 1000;
                        quantityUnit[i] = "grams";
                    }

                    //converts between teaspoons and tablespoons
                    else if (quantity[i] >= 3 && quantityUnit[i].Contains("teaspoons"))
                    {
                        quantity[i] = quantity[i] / 3;
                        quantityUnit[i] = "tablespoons";
                    }
                    else if (quantity[i] < 1 && quantityUnit[i].Contains("tablespoons"))
                    {
                        quantity[i] = quantity[i] * 3;
                        quantityUnit[i] = "teaspoons";
                    }

                    //converts between tablespoons and cups
                    else if (quantity[i] >= 16 && quantityUnit[i].Contains("tablespoons"))
                    {
                        quantity[i] = quantity[i] / 16;
                        quantityUnit[i] = "cups";
                    }
                    else if (quantity[i] < 1 && quantityUnit[i].Contains("cups"))
                    {
                        quantity[i] = quantity[i] * 16;
                        quantityUnit[i] = "tablespoons";
                    }

                    //converts between cups and liters
                    else if (quantity[i] >= 4.22 && quantityUnit[i].Contains("cups"))
                    {
                        quantity[i] = quantity[i] / 4.22;
                        quantityUnit[i] = "liters";
                    }
                    else if (quantity[i] < 1 && quantityUnit[i].Contains("liters"))
                    {
                        quantity[i] = quantity[i] * 4;
                        quantityUnit[i] = "cups";
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An error occured, please make sure you have entered a recipe first");
                Format(); //recursive call to method if exception is caught
                menu(); //return user to menu if exception is caught
            }
           
        }

        private void addcalories()
        {
            calories.ForEach(c => totalcal += c);
        }


        private void Delete()
        {  //method to delete recipe 
            string? check = "no";
            //Ask user to confirm before deleting
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Are you sure you would like to delete the recipe for the {name}, type 'yes'  or 'no' to confirm");
                Console.ResetColor();
                check = Console.ReadLine().ToLower().Trim();

            }
            catch (IOException)
            { //catch exception if user enters wrong values
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Enter 'yes' or 'no' to preceed");
                Console.ResetColor();
                Delete();
            }
            catch (ArgumentOutOfRangeException)
            {  //if user enters invalid input
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Enter 'yes' or 'no' to preceed");
                Console.ResetColor();
                Delete();
            }
            try
            {
                if (check.Contains("yes"))
                {  //if user confirms delete , clear all arrays and return to menu
                    ingredientName.Clear();
                    quantity.Clear();
                    quantityUnit.Clear();
                    stepInfo.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Recipe for the {name} has been deleted"); //confirmation message
                    Console.ResetColor();
                    menu();
                }
                else if (check.Contains("no")) //if user does not confirm delete , return to menu
                {
                    Console.WriteLine("What else would you like to do");
                    menu();
                }
            }
            catch (IndexOutOfRangeException)
            { //if user tries to delete a recipe that has not been captured
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*No recipe has been captured or recipe has alredy been deleted");
                Console.ResetColor();
                Delete();  // recursion
            }
        }

        private void Scale()
        { //method to scale recipe
            double scale = 0; //variable to store scale factor
            int back;
            string? check = null;

            try
            {
                while ((scale <= 0) || string.IsNullOrWhiteSpace(check)) //loop until user enters a valid scale factor and confirmation
                {  //loop to ensure user enters a valid scale factor
                    Console.WriteLine($"At what scale would you like to alter recipe quantities");
                    scale = Convert.ToDouble(Console.ReadLine().Trim());

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of {scale} ,type 'yes' or 'no' to confirm");
                    Console.ResetColor();

                    check = Console.ReadLine().ToLower().Trim();
                    if ((scale <= 0) || string.IsNullOrWhiteSpace(check))
                    { //if user enters a scale factor less than 0 or enters no , return to menu
                        Console.WriteLine("please enter a number greater than 0 or type yes or no where required");
                    }
                }

                if (check.Contains("yes"))
                {  //if user confirms scaling
                    for (int i = 0; i < numIngredients; i++)
                    { //loop to mulitply each quantity by the scale factor
                        quantity[i] *= scale;
                        calories[i] *= scale;
                    }
                    Console.WriteLine($"Recipe has been scaled by a factor of {scale}");
                }  //shows confirmation that recipe has been scaled
                else if (check.Contains("no"))
                {
                    Console.WriteLine("Press 0 to still make changes 1 return to the menu");
                    back = Convert.ToInt32(Console.ReadKey());
                    if (back.Equals(0))
                        Scale();
                    else if (back.Equals(1))
                        menu();
                }
            }
            catch (IOException)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*please enter a numerical value or enter 'yes' or 'no' where required");
                Console.ResetColor();
                Scale();  //recursion to allow user to enter a valid scale factor again
            }
            catch (FormatException)
            {  //if user enters invalid input
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*An error occured, please enter a numerical value");
                Console.ResetColor();
                Scale();
            }
            catch (IndexOutOfRangeException)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*An error occured, please enter a numerical value");
                Console.ResetColor();
                Scale();
            }
        }

        private void Reset()
        { //method to reset recipe values
            string? check = null; // check value with default set to null to ensure user does not reset values by mistake
            try
            {
                while (check == null)
                {
                    Console.WriteLine("Are your sure you want to reset the ingredients' quantity values");
                    check = Console.ReadLine().ToLower().Trim();
                    if (check.Contains("yes"))
                    { //if user confirms reset assigns values from defaultValues array to quantity array
                        for (int d = 0; d < numIngredients; d++)
                        {
                            quantity[d] = defaultValues[d];
                        }
                        Console.WriteLine($"quantity values for the {name}'s ingredients have been reset");
                    } //show confirmation that values have been reset
                    else if (check.Contains("no")) //if user does not confirm reset returns to menu
                        Console.WriteLine("Values have not been reset, what else would you like to do");
                    menu();
                }
            }
            catch (IOException)
            { //catches exception if user enters wrong input and ensure user is able to try again
                Console.WriteLine("Please enter 'yes' or 'no'");
                Reset();

            }
            catch (IndexOutOfRangeException)
            { //catches exception if array indexing error occurs ensures user is returned to menu
                Console.WriteLine("Values have alredy been reset or don't exist");
                menu();
            }
        }

        public void View()
        {  //method to view recipe
            Console.WriteLine(" ");
            Console.WriteLine("Your are going to be preparing the following recipe ");
            Console.WriteLine(" ");
            Console.WriteLine($"Recipe:{name}");
            System.Console.WriteLine(" ");
            System.Console.WriteLine("**************************************************");
            Console.WriteLine("Ingredients:");
            for (int v = 0; v < numIngredients; v++)
            {  //loop to display ingredients by quantity , unit and item
                Console.WriteLine($"{quantity[v]} {quantityUnit[v]} of {ingredientName[v]}");
            }
            System.Console.WriteLine(" ");
            Console.WriteLine($"Total calories of the {name} : {totalcal}");
            System.Console.WriteLine("**************************************************");
            Console.WriteLine("Steps:");
            int num = 1;
            foreach (var st in stepInfo) //loop to display numbered steps
            {
                Console.WriteLine($"{num},{st}");
                Console.WriteLine(" ");
                num++;
            }
            Console.WriteLine("Enjoy!");
            menu(); //returns to menu after recipe is displayed 
        }


        public void menu()
        {    //method to display menu
            int option;
            Console.WriteLine(" ");
            Console.WriteLine("Main Menu:");
            Console.WriteLine(" ");
            Console.WriteLine("please select on option to proceed");

            do
            {   //numeric menu as entry point for program
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("1. Create a recipe");
                Console.WriteLine("2. Scale a recpie ");
                Console.WriteLine("3. Reset recipe quantities");
                Console.WriteLine("4. View entered recipe");
                Console.WriteLine("5. Delete recipe");
                Console.WriteLine("6. Exit");
                Console.ResetColor();
                option = Convert.ToInt32(Console.ReadLine());
                switch (option) //switch statement to allow user to select option
                {

                    case 1:   //calls methods to enter recipe and format quantities accordingly 
                        entryPrimary();
                        defaultValues.AddRange(quantity); //adds values from quantity array to defaultValues array
                        defaultCal.AddRange(calories); //adds values from calories array to defaultCal array    
                        Format();  //calls method to format quantity units of measurements                          
                        break;
                    case 2: //calls method to scale recipe
                        Scale();
                        Format();
                        break;
                    case 3:  //calls method to reset recipe
                        Reset();
                        Format();
                        break;
                    case 4:  // calls method to view recipe
                        View();
                        break;
                    case 5: //calls method to delete recipe
                        Delete();
                        break;
                    case 6: //exits program
                        Console.WriteLine("Happy Coooking ,Goodbye");
                        Environment.Exit(0);
                        break;
                    default: //returns user to menu if invalid option is selected
                        Console.WriteLine("Invalid operation");
                        break;
                }
            } while (option <= 6); //loop to ensure user is returned to menu after each operation
        }
    }
}
