using System;

namespace PoePt1{

    public class RecipeRun{ //class to run program

        public void main (string [] args){ 
            //main method to run program
            Console.WriteLine("Sanele Cooks (Beta)");
           
            Recipe recipe = new Recipe();  //create instance of Recipe class
            try {  
                recipe.menu();  //call menu method
            } catch (IOException){
                Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }  catch(FormatException){
               Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }
        }

    }  
       

    public class Recipe{ //class to store recipe information
        
        //class variables
        private string name;
        public string Name{
            get{return name;}
            set{
                if (string.IsNullOrEmpty(value)){
                    throw new ArgumentException("Name cannot be null or empty");
                }
                name=value;
            }
        }
        private  static int numIngredients{get;set;}
        private int steps{get;set;}
        private string [] ingredientName = new string[numIngredients];
        private  static double [] quantity = new double[numIngredients];
        private string [] quantityUnit = new string[numIngredients];
        private string [] stepInfo = new string[numIngredients];   
        List<double> defaultValues = new List<double>(); //list to store default values of quantity
        

        private void entryPrimary(){ //method to capture recipe name , number of ingredients and steps
            try{  //try catch block to handle exceptions
                Console.WriteLine("Enter the name of the recipe");
                name = Console.ReadLine(); 
                Console.WriteLine($"Enter the number of ingredients for the {name}");
                numIngredients = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"How many steps are invovled in making the {name}");
                steps = Convert.ToInt32(Console.ReadLine());
            }catch (IOException){ 
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Come on don't be an idiot , enter the right values");
                Console.ResetColor();
                entryPrimary();  //call method again if exception is caught
            }catch(FormatException){
                Console.WriteLine("An error on our side occured , please try again");
                entryPrimary(); //call method again if exception is caught
            }
            
            for( int s = 0 ; s < steps; s++){  //loop to capture step information
            do {
                Console.WriteLine($"Describe step{s}");
                stepInfo[s] = Console.ReadLine();
                if (stepInfo[s].Equals(null))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*Enter values to proceed");
                    Console.ResetColor();
                }
            }while(stepInfo[s].Equals(null));
          }
        }
        private void entrySecondary(){  //method to capture ingredient information

             for( int i = 0 ;  i < numIngredients ; i++){ 
                try {
                    do{ //loop to capture ingredient information
                        Console.WriteLine($"Enter the name of ingredient {i}");
                        ingredientName[i] = Console.ReadLine();
                        
                        Console.WriteLine($"Enter the quantity of {ingredientName[i]}s");
                        quantity[i] = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine($"How will the {ingredientName}s be measured , Example 'gram's','kg', 'teaspoons','tablespoons', 'cups' or 'liters'");
                        quantityUnit[i] = Console.ReadLine().ToLower();
                          
                        if(ingredientName[i] == null || quantity[i] <= 0 || quantityUnit[i] == null ){ //check if user has entered values
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("*Enter values to proceed");
                            Console.ResetColor();
                        }
                    }while(ingredientName[i] == null || quantity[i] <=0 || quantityUnit[i] == null ); 
                }catch(IOException){
                    Console.WriteLine("Let's be smart here!, now go ahead and enter the right values, PLEASE!!!");
                    entrySecondary(); //recursive call to method if exception is caught
                }catch(IndexOutOfRangeException){
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();  // returns user to menu if data entry is hindered 
                }catch(FormatException){
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();
                }
            }
            Console.WriteLine($"Recipe for the{name} has been captured"); //confirmation message
        }
        
        private void Format(){  
           //method to change the quantity amount quantity Unit according result of Scale method
           try{
                for (int i = 0 ; i < numIngredients ; i++){
                //converts between grams and kilograms
                if (quantity[i] >= 1000  &&quantityUnit[i].Contains("grams") ){
                    quantity[i] = quantity[i]/1000;
                    quantityUnit[i] = "kg";
                }
                else if(quantity[i] < 1 && quantityUnit[i].Contains("kg")){
                    quantity[i] = quantity[i]*1000;
                    quantityUnit[i] = "grams";
                }

                //converts between teaspoons and tablespoons
                else if (quantity[i] >= 3 && quantityUnit[i].Contains("teaspoons")){
                    quantity[i] = quantity[i]/3;
                    quantityUnit[i] = "tablespoon(s)";
                }
                else if( quantity[i] < 1 && quantityUnit[i].Contains("tablespoons")){
                    quantity[i] = quantity[i]*3;
                    quantityUnit[i] = "teaspoons";
                }

               //converts between tablespoons and cups
                else if (quantity[i] >=  16 && quantityUnit[i].Contains("tablespoons")){
                    quantity[i] = quantity[i]/16;
                    quantityUnit[i] = "cups";
                }
                else if(quantity[i] < 1 && quantityUnit[i].Contains("cups")){
                    quantity[i] = quantity[i]*16;
                    quantityUnit[i] = "tablespoons";
                }

                 //converts between cups and liters
                else if (quantity[i] >= 4.22 && quantityUnit[i].Contains("cups")){
                    quantity[i] = quantity[i]/4.22;
                    quantityUnit[i] = "liters";
                }
                else if(quantity[i] < 1 && quantityUnit[i].Contains("liters")){
                    quantity[i] = quantity[i]*4;
                    quantityUnit[i] = "cups";
                }
            } 
           }catch(IndexOutOfRangeException){ 
                Console.WriteLine("An error occured, please make sure you have entered a recipe first");
                menu();
           }
        }

        private void Delete(){  //method to delete recipe 
        string check = "no";
            //Ask user to confirm before deleting
            try{
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Are you sure you would like to delete the recipe for the {name}, type 'yes'  or 'no' to confirm");
                Console.ResetColor();
                check = Console.ReadLine().ToLower();
            }catch(IOException ){ //catch exception if user enters wrong values
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*Enter 'yes' or 'no' to preceed");
                    Console.ResetColor();
                    Delete();
            }catch(ArgumentOutOfRangeException){  //if user enters invalid input
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Enter 'yes' or 'no' to preceed");
                Console.ResetColor();
                Delete();
            }
            try{
                    if (check.Contains("yes")){  //if user confirms delete , clear all arrays and return to menu
                    Array.Clear(ingredientName,0,ingredientName.Length);
                    Array.Clear(quantity,0,quantity.Length);
                    Array.Clear(quantityUnit,0,quantityUnit.Length);
                    Array.Clear(stepInfo,0,stepInfo.Length);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Recipe for the {name} has been deleted"); //confirmation message
                    Console.ResetColor();
                    menu();}
                else if (check.Contains("no")) //if user does not confirm delete , return to menu
                {
                    Console.WriteLine("What else would you like to do");
                    menu();
                }
            }catch(IndexOutOfRangeException){ //if user tries to delete a recipe that has not been captured
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*No recipe has been captured or recipe has alredy been deleted");
                Console.ResetColor();
                Delete();  // recursion
            }
        }

        private void Scale(){ //method to scale recipe
         double scale; //variable to store scale factor
         int back;      
         string check; 
            try{ 
                do{  //loop to ensure user enters a valid scale factor
                    Console.WriteLine($"At what scale would you like to alter recipe quantities");
                    scale = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of {scale} ,type 'yes' or 'no' to confirm");
                    Console.ResetColor();
                    check = Console.ReadLine().ToLower();
                    if ((scale <= 0) || check.Equals(null)){ //if user enters a scale factor less than 0 or enters no , return to menu
                    Console.WriteLine("please enter a number greater than 0 or type yes or no where required");
                    }
                }while((scale <= 0) || check.Equals(null)); //loop until user enters a valid scale factor and confirmation

                if (check.Contains("yes")){  //if user confirms scaling
                    for (int i = 0 ; i < numIngredients ; i++){ //loop to mulitply each quantity by the scale factor
                        quantity[i] = quantity[i]*scale; 
                    } 
                Console.WriteLine($"Recipe has been scaled by a factor of{scale}");}  //show confirmation that recipe has been scaled
                else if (check.Contains("no")){
                    try{ 
                        Console.WriteLine("Press 0 to still make changes 1 return to the menu");
                        back = Convert.ToInt32(Console.ReadKey());  
                        if(back.Equals(0))
                        Scale();
                        else if( back.Equals(1))
                            menu();
                        }catch(IOException){
                            Console.WriteLine("press 0 or 1 ");
                        }catch( FormatException){
                            Console.WriteLine("An error occured, please try again ");
                            menu();
                        }
                }     
            }catch(IOException){
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*please enter a numerical value or enter 'yes' or 'no' where required");
                Console.ResetColor();
                 Scale();  //recursion to allow user to enter a valid scale factor again
            }catch(FormatException){  //if user enters invalid input
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*An error occured, please enter a numerical value");
                Console.ResetColor();
                 Scale(); 
            }     
        }

        private void Reset(){ //method to reset recipe values
        string check = "no"; // check value with default set to "no" to ensure user does not reset values by mistake
            try{
                do{
                Console.WriteLine("Are your sure you want to reset ingredient quantity values");
                check = Console.ReadLine().ToLower();  
            if(check.Contains("yes")){ //if user confirms reset assigns values from defaultValues array to quantity array
                for (int d = 0 ; d < numIngredients ; d++){
                    quantity[d] = defaultValues[d];
                }
                Console.WriteLine($"quantity values for the {name}'s ingredients have been reset");} //show confirmation that values have been reset
                else if (check.Contains("no")) //if user does not confirm reset returns to menu
                 Console.WriteLine("Values have not been reset, what else would you like to do");
                    menu();
            }while(check != null);
            }catch(IOException){ //catches exception if user enters wrong input and ensure user is able to try again
                Console.WriteLine( "Please enter 'yes' or 'no'");
                Reset();

            }catch(IndexOutOfRangeException){ //catches exception if array indexing error occurs ensures user is returned to menu
                Console.WriteLine("Values have alredy been reset or don't exist");
                menu();  
            }
        }

        private  void View(){  //method to view recipe
            Console.WriteLine(" ");
            Console.WriteLine("Your are going to be preparing the following recipe ");
            Console.WriteLine(" ");
            Console.WriteLine($"Recipe:{name}");
            System.Console.WriteLine(" ");
            System.Console.WriteLine("**************************************************");
            Console.WriteLine("Ingredients:");
            for (int v= 0 ; v < numIngredients;v++){  //loop to display ingredients by quantity , unit and item
                Console.WriteLine($"{quantity[v]} {quantityUnit[v]} of {ingredientName[v]}");
            }
            System.Console.WriteLine(" ");
            System.Console.WriteLine("**************************************************");
            Console.WriteLine("Steps:");  
            int num = 1;
            foreach (var st in stepInfo) //loop to display numbered steps
            {
                Console.WriteLine($"{num}. {st}");
                Console.WriteLine(" ");
                num++;
            }
         Console.WriteLine("Enjoy!");
            menu(); //returns to menu after recipe is displayed 
        }

         public void menu(){    //method to display menu
            Console.WriteLine(" ");
            Console.WriteLine("Main Menu:");
            Console.WriteLine(" ");
            Console.WriteLine("please select on option to proceed");
            int option = Convert.ToInt32(Console.ReadLine());
            do {   //numeric menu as entry point for program
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("1. Enter a recipe");
                Console.WriteLine("2. Scale entered recipe ingredient quantities ");
                Console.WriteLine("3. Reset to defualt");
                Console.WriteLine("4. View recipe");
                Console.WriteLine("5. Delete recipe");
                Console.WriteLine("6. Exit");
                Console.ResetColor();
                
            switch(option) //switch statement to allow user to select option
            {
                
                case 1:   //calls methods to enter recipe and format quantities accordingly 
                    entryPrimary();
                    entrySecondary();
                    defaultValues.AddRange(quantity); //adds values from quantity array to defaultValues array
                    Format();  //calls method to format quantity units of measurements
                    break;                  
                case 2: //calls method to scale recipe
                    Scale();
                    Format();
                    break;                            
                case 3:  //calls method to reset recipe
                    Reset(); 
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
