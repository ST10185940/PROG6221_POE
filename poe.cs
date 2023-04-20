using System;

namespace PoePt1{

    public class RecipeRun{

        public static void main (string [] args){
            Recipe recipe = new Recipe();
            try {
                menu();
            } catch (IO.IOException e ){
                Console.WriteLine("Please enter an integer to proceed");
                menu();
            }
           
        }

    }
        
        private void menu(){
            Console.WriteLine("Sanele Cooks (Beta)");
                Console.WriteLine("Please select an option to proceed");
            int option = Convert.ToInt32(Console.ReadLine());
            do {
                    Console.WriteLine("1. Enter a recipe");
                     Console.WriteLine("2. Scale entered recipe ingredient quantities ");
                       Console.WriteLine("3. Reset to defualt");
                        Console.WriteLine("4. View recipe");
                         Console.WriteLine("5. Delete recipe");
                          Console.WriteLine("6. Exit");
         //switch case

            switch(option)
            {
                
                case 1:
                    recipe.entryPrimary();
                    recipe.entrySecondary();
                    recipe.Format();
                    break;                  
                case 2:
                    recipe.Scale();
                    recipe.Format();
                    break;                            
                case 3:
                    recipe.Reset();
                    break;
                case 4:
                    recipe.View();
                    break;        
                case 5:
                    recipe.Delete();
                    break;                          
                case 6: 
                    Console.WriteLine("Happy Coooking ,Goodbye");
                    Environment.Exit(0);
                    break;
                default:
                   Console.WriteLine("Invalid operation");
                   break;
                   }           
            } while (option <= 6 );
        }

    public class Recipe{
        private string name{get;set;}
        private string numIngredients{get;set;}
        private int steps{get;set;}
        private string [] ingredientName = new string[numIngredients];
        private double [] quantity = new double[numIngredients];
        private var quantityUnit = new string[numIngredients];
        private string [] stepInfo = new string[numIngredients];   
        private var defaultValues = new List<double>(qunatity);

        private static void entryPrimary(){
                try{
                Console.WriteLine("Enter the name of the recipe");
                name = Console.ReadLine();
                Console.Writeline($"Enter the number of ingredients for the {name}");
                numIngredeinients = Convert.ToInt32(Console.Readline());
                Console.Writeline($"How many steps are invovled in making the {name}");
                steps = Convert.ToInt32(Console.Readline());
                }catch (IO.IOException e){
                    Console.WriteLine("Come on don't be an idiot , enter the right values");
                    Entry();
                }catch(FormatException e){
                    Console.WriteLine("An error on our side occured , please try again");
                    Entry();
                }

             
           
            for( int s = 0 ; s < steps; s++){
                Console.WriteLine($"Describe step{s}");
                stepInfo[s] = Console.Readline();
                }

        }

        private static void entrySecondary(){

             for( int i = 0 ;  i < numIngredients ; i++){
                try {
                    do{
                        Console.WriteLine($"Enter the name of ingredient {i}");
                        ingredientName[i] = Console.Readline();
                        
                        Console.WriteLine($"How will the {ingredientName}s be measured , Example 'gram's,'teaspoons','tablespoons','milliliters' or 'cups'");
                        quantityUnit[i] = Console.Realine();
                        
                        Console.WriteLine($"Enter the quantity of {ingredientName[i]}s");
                        quantity[i] = Convert.ToInt32(Console.Readline());
                        if(ingredientName == null || quantity == null || quantityUnit == null || quantity < 0){
                            Console.WriteLine("Enter values to proceed");
                        }
                    }while(ingredinetName[i] == null && quantity[i] == null && quantity[i] > 0 && quantityUnit[i] == null );
                }catch(IO.IOException e){
                    Console.WriteLine("Come on don't be an idiot , enter the right values");
                    entrySecondary();
                }catch(IndexOutOfRangeException r){
                    Console.WriteLine("An error on our side occured , please try again");
                    menu();
                }  
            }
              Console.WriteLine($"Recipe for the{name} has been captured");
        }
        
        private static void Format(){
           // change the quantity amount quantity Unit  according  result of Scale method
           try{
                 for (int i = 0 ; i < quantity.Count ; i++){
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
                else if( qunatity[i] < 1 && quantityUnit[i].Contains("tablespoons")){
                    quantity[i] = quantity[i]*3;
                    quantityUnit[i] = "teaspoons";
                }

               //converts between tablespoons and cups
                else if (quantity[i] >=  16 && quantityUnit[i].Contains("tablespoons")){
                    quantity[i] = quantity[i]/16;
                    quantityUnit[i] = "cups";
                }
                else if(quanttiy[i] < 1 && quantityUnit[i].Contains("cups")){
                    quantity[i] = quantity[i]*16;
                    quantityUnit[i] = "tablespoons";
                }

                    //converts between cups and liters
                else if (quantity[i] >= 4.22 && quantityUnit[i].Contains("cups")){
                    quantity[i] = quantity[i]/4.22;
                    quantityUnit[i] = "liters";
                }
                else if(quanttiy[i] < 1 && quantityUnit[i].Contains("liters")){
                    quantity[i] = quantity[i]*4;
                    quantityUnit[i] = "cups";
                }
            } 
           }catch(IndexOutOfRangeException e){
                Console.WriteLine("An error occured, please make sure you have entered a recipe first");
                menu();
           }
           
        }

        private static void Delete(){
            //Ask user to confirm before deleting
            try{
                Console.WriteLine($"Are you sure you would like to delete the recipe for the {name}, type 'yes'  or 'no' to confirm");
                string check = Console.Readline();
            }catch(IO.IOException e){
                    Console.WriteLine("Enter 'yes' or 'no' to preceed");
                    Delete();
            }
            try{
                    if (check.Contains("yes")){
                    Array.Clear(ingredientName,0,ingredientName.Length);
                    Array.Clear(quantity,0,quantity.Length);
                    Array.Clear(quantityUnit,0,quantityUnit.Length);
                    Array.Clear(stepInfo,0,stepInfo.Length);
                    Console.WriteLine($"Recipe for the {name} has been deleted");}
                else if (check.Contains("no"))
                {
                    Console.WriteLine("What else would you like to do");
                    menu();
                }
            }catch(IndexOutOfRangeException e){
                Console.WriteLine("No recipe has been captured");
            }

        }
            

        private static void Scale(){ 
            try{
                do {
                    Console.WriteLine($"At what scale would you like to alter recipe quantities");
                    double scale = Convert.ToDouble(Console.Readline());
                    if (scale < 1 || scale = null)
                     Console.WriteLine("plase enter a number greater than 0");
                    }
                while( scale < 1 && scale = null );
            }catch(IO.IOException e){
                Console.WriteLine("please enter a numerical value");
                 Scale();
            }catch(FormatException e){
                Console.WriteLine("An error occured, please enter a numerical value");
                 Scale();
            }

            try{
              Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of{scale} ,type 'yes' or 'no' to confirm");
              string check = Console.Readline(); 
            }catch(IO.IOException e){
               Console.WriteLine("please enter 'yes' or 'no'"); 
                 Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of{scale} ,type 'yes' or 'no' to confirm");
                   check = Console.Readline(); 
            }

            if (check.Contains("yes")){
             quantity.ForEach(sc => sc*scale);
             Console.WriteLine($"Recipe has been scaled by a factor of{scale}");}
            else if (check.Contains("no")){
                try{
                Console.WriteLine("Press 0 to still make changes 1 return to the menu");
                    int go = Convert.ToInt32(COnsole.Readline()); }
                    catch(IO.IOException e){
                        Console.WriteLine("press 0 or 1 ");
                    }catch( FormatException fe){
                        Console.WriteLine("An error occured, please try again ");
                        menu();
                    }
                    finally{
                        if(go = 0)
                         Scale();
                        else if(go=1)
                         menu();
                    }
              }     
        }

        private static void Reset(){
            try{
               Console.WriteLine("Are your sure you want to reset ingredient quantity values");
               string check = Console.readline();
            if(check.Constains("yes"))
                for (int d = 0 ; d < quantity.Count ; d++){
                    quantity[d] = defaultValues[d];
                }
            Console.WriteLine($"quantity values for the {name}'s ingredients have been reset");

            }catch(IO.IOException e){
                Console.WriteLine( "Please enter 'yes' or 'no'");
                Reset();

            }catch(IndexOutOfRangeException r){
                Console.WriteLine("Values have alredy been reset or don't exist");
                menu();
            }
        }

        private static View(){
            Console.WriteLine($"Recipe:{name}");
            System.Console.WriteLine(" ");
            Console.WriteLine("Ingredients:");
            for (int v= 0 ; v < ingredientName.Count;v++){
                Console.WriteLine($"{quantity[v]} {quantityUnit[v]} of {ingredientName[v]}");
            }
            System.Console.WriteLine(" ");
            Console.WriteLine("Steps:");
            stepInfo.ForEach(st => Console.WriteLine(st));
        }
    }
}
