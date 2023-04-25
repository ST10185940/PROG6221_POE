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
        private string  name{get;set;}
        private  static int numIngredients{get;set;}
        private int steps{get;set;}
        private string [] ingredientName = new string[numIngredients];
        private  static double [] quantity = new double[numIngredients];
        private string [] quantityUnit = new string[numIngredients];
        private string [] stepInfo = new string[numIngredients];   
        private  var defaultValues = new List<double>(quantity); //list to store default values of quantity

        private void entryPrimary(){ //method to capture recipe name , number of ingredients and steps
                try{  //try catch block to handle exceptions
                Console.WriteLine("Enter the name of the recipe");
                name = Console.ReadLine(); 
                Console.WriteLine($"Enter the number of ingredients for the {name}");
                numIngredients = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"How many steps are invovled in making the {name}");
                steps = Convert.ToInt32(Console.ReadLine());
                }catch (IOException){ 
                    Console.WriteLine("Come on don't be an idiot , enter the right values");
                    entryPrimary();  //call method again if exception is caught
                }catch(FormatException){
                    Console.WriteLine("An error on our side occured , please try again");
                    entryPrimary(); //
                }
            
            for( int s = 0 ; s < steps; s++){  //loop to capture step information
            do {
                Console.WriteLine($"Describe step{s}");
                stepInfo[s] = Console.ReadLine();
                }while(stepInfo[s] != null);
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
                        quantityUnit[i] = Console.ReadLine();
                          
                        if(ingredientName[i] == null || quantity[i] <= 0 || quantityUnit[i] == null ){ //check if user has entered values
                            Console.WriteLine("Enter values to proceed");
                        }
                    }while(ingredientName[i] == null && quantity[i] <=0 && quantityUnit[i] == null ); 
                }catch(IOException){
                    Console.WriteLine("Come on don't be an idiot , enter the right values");
                    entrySecondary();
                }catch(IndexOutOfRangeException){
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();
                } catch(FormatException){
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
                Console.WriteLine($"Are you sure you would like to delete the recipe for the {name}, type 'yes'  or 'no' to confirm");
                  check = Console.ReadLine();
            }catch(IOException ){
                    Console.WriteLine("Enter 'yes' or 'no' to preceed");
                    Delete();
            }catch(ArgumentOutOfRangeException){
                Console.WriteLine("Enter 'yes' or 'no' to preceed");
                Delete();
            }
            try{
                    if (check.Contains("yes")){ //if user confirms delete , clear all arrays and return to menu
                    Array.Clear(ingredientName,0,ingredientName.Length);
                    Array.Clear(quantity,0,quantity.Length);
                    Array.Clear(quantityUnit,0,quantityUnit.Length);
                    Array.Clear(stepInfo,0,stepInfo.Length);
                    Console.WriteLine($"Recipe for the {name} has been deleted"); //confirmation message
                    menu();}
                else if (check.Contains("no"))
                {
                    Console.WriteLine("What else would you like to do");
                    menu();
                }
            }catch(IndexOutOfRangeException){
                Console.WriteLine("No recipe has been captured or recipe has alredy been deleted");
            }

        }
            

        private void Scale(){ //method to scale recipe
         double scale; //variable to store scale factor
         int back;      
         string check; 
            try{
                do {  //loop to ensure user enters a valid scale factor
                    Console.WriteLine($"At what scale would you like to alter recipe quantities");
                     scale = Convert.ToDouble(Console.ReadLine());
                    if ((scale <= 0))
                     Console.WriteLine("plase enter a number greater than 0");
                    }
                while( (scale <= 0));
            }catch(IOException){
                Console.WriteLine("please enter a numerical value");
                 Scale();
            }catch(FormatException){
                Console.WriteLine("An error occured, please enter a numerical value");
                 Scale();
            }
            
            try{ //ask user to confirm before scaling
              Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of {scale} ,type 'yes' or 'no' to confirm");
               check = Console.ReadLine(); 
            }catch(IOException){  //catches exception if user enters wrong input
               Console.WriteLine("please enter 'yes' or 'no'"); 
                 Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of {scale} ,type 'yes' or 'no' to confirm");
                   check = Console.ReadLine(); 
            }

            if (check.Contains("yes")){  //if user confirms scaling
            //loop to mulitply each quantity by the scale factor
                for (int i = 0 ; i < numIngredients ; i++){
                    quantity[i] = quantity[i]*scale;
                }
             Console.WriteLine($"Recipe has been scaled by a factor of{scale}");}  //show confirmation that recipe has been scaled
            else if (check.Contains("no")){
                try{
                Console.WriteLine("Press 0 to still make changes 1 return to the menu");
                     back = Convert.ToInt32(Console.ReadKey()); }
                    catch(IOException){
                        Console.WriteLine("press 0 or 1 ");
                    }catch( FormatException){
                        Console.WriteLine("An error occured, please try again ");
                        menu();
                    }
                    finally{  //finally block to ensure that the user is returned to the menu
                        if( back == 0)
                         Scale();
                        else if( back == 1)
                         menu();
                    }
              }     
        }

        private void Reset(){  //method to reset recipe values
            try{
               Console.WriteLine("Are your sure you want to reset ingredient quantity values");
               string check = Console.ReadLine(); 
            if(check.Contains("yes")) //if user confirms reset
                for (int d = 0 ; d < numIngredients ; d++){
                    quantity[d] = defaultValues[d];
                }
            Console.WriteLine($"quantity values for the {name}'s ingredients have been reset"); //show confirmation that values have been reset

            }catch(IOException){ //catches exception if user enters wrong input
                Console.WriteLine( "Please enter 'yes' or 'no'");
                Reset();

            }catch(IndexOutOfRangeException){  //catches exception if user enters wrong input and ensure user is returned to menu
                Console.WriteLine("Values have alredy been reset or don't exist");
                menu();  
            }
        }

        private  void View(){  //method to view recipe
            Console.WriteLine($"Recipe:{name}");
            System.Console.WriteLine(" ");
            Console.WriteLine("Ingredients:");
            for (int v= 0 ; v < numIngredients;v++){  //loop to display ingredients by quantity , unit and item
                Console.WriteLine($"{quantity[v]} {quantityUnit[v]} of {ingredientName[v]}");
            }
            System.Console.WriteLine(" ");
            Console.WriteLine("Steps:");  
            int num = 1;
            foreach (var st in stepInfo) //loop to display numbered steps
            {
                Console.WriteLine($"{num}. {st}");
                num++;
            }
          
        }


         public void menu(){    //method to display menu
            Console.WriteLine(" ");
            Console.WriteLine("Main Menu:");
            Console.WriteLine(" ");
            Console.WriteLine("please select on option to proceed");
            int option = Convert.ToInt32(Console.ReadLine());
            do {
                Console.WriteLine("1. Enter a recipe");
                    Console.WriteLine("2. Scale entered recipe ingredient quantities ");
                    Console.WriteLine("3. Reset to defualt");
                    Console.WriteLine("4. View recipe");
                        Console.WriteLine("5. Delete recipe");
                        Console.WriteLine("6. Exit");
         
            switch(option) //switch statement to allow user to select option
            {
                
                case 1:   //calls methods to enter recipe and format quantities accordingly 
                    entryPrimary();
                    entrySecondary();
                    Format();
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
            } while (option <= 6 ); //loop to ensure user is returned to menu after each operation
        }
    }
}
