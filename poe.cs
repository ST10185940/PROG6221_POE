using System;

namespace PoePt1{

    public class RecipeRun{

        public void main (string [] args){
            Console.WriteLine("Sanele Cooks (Beta)");
            Recipe recipe = new Recipe();
            try {
                recipe.menu();
            } catch (IOException){
                Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }  catch(FormatException){
               Console.WriteLine("Please enter an integer to proceed");
                recipe.menu();
            }
        }

    }  
       

    public class Recipe{
        private string  name{get;set;}
        private  static int numIngredients{get;set;}
        private int steps{get;set;}
        private string [] ingredientName = new string[numIngredients];
        private  static double [] quantity = new double[numIngredients];
        private string [] quantityUnit = new string[numIngredients];
        private string [] stepInfo = new string[numIngredients];   
        private  var defaultValues = new List<double>(quantity);

        private void entryPrimary(){
                try{
                Console.WriteLine("Enter the name of the recipe");
                name = Console.ReadLine();
                Console.WriteLine($"Enter the number of ingredients for the {name}");
                numIngredients = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"How many steps are invovled in making the {name}");
                steps = Convert.ToInt32(Console.ReadLine());
                }catch (IOException e){
                    Console.WriteLine("Come on don't be an idiot , enter the right values");
                    entryPrimary();
                }catch(FormatException e){
                    Console.WriteLine("An error on our side occured , please try again");
                    entryPrimary();
                }

            for( int s = 0 ; s < steps; s++){
                Console.WriteLine($"Describe step{s}");
                stepInfo[s] = Console.ReadLine();
                }
        }

        private void entrySecondary(){

             for( int i = 0 ;  i < numIngredients ; i++){
                try {
                    do{
                        Console.WriteLine($"Enter the name of ingredient {i}");
                        ingredientName[i] = Console.ReadLine();
                        
                        Console.WriteLine($"How will the {ingredientName}s be measured , Example 'gram's,'teaspoons','tablespoons','milliliters' or 'cups'");
                        quantityUnit[i] = Console.ReadLine();
                        
                        Console.WriteLine($"Enter the quantity of {ingredientName[i]}s");
                        quantity[i] = Convert.ToInt32(Console.ReadLine());
                        if(ingredientName[i] == null || quantity[i] == null && quantityUnit[i] == null || quantity[i] < 0){
                            Console.WriteLine("Enter values to proceed");
                        }
                    }while(ingredientName[i] == null && quantity[i] == null && quantity[i] > 0 && quantityUnit[i] == null );
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
              Console.WriteLine($"Recipe for the{name} has been captured");
        }
        
        private void Format(){
           // change the quantity amount quantity Unit  according  result of Scale method
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
           }catch(IndexOutOfRangeException e){
                Console.WriteLine("An error occured, please make sure you have entered a recipe first");
                menu();
           }
           
        }

        private void Delete(){
            //Ask user to confirm before deleting
         
            try{
                Console.WriteLine($"Are you sure you would like to delete the recipe for the {name}, type 'yes'  or 'no' to confirm");
                string  check = Console.ReadLine();
            }catch(IOException ){
                    Console.WriteLine("Enter 'yes' or 'no' to preceed");
                    Delete();
            }catch(ArgumentOutOfRangeException){
                Console.WriteLine("Enter 'yes' or 'no' to preceed");
                Delete();
            }
            try{
                    if (check.Contains("yes")){
                    Array.Clear(ingredientName,0,ingredientName.Length);
                    Array.Clear(quantity,0,quantity.Length);
                    Array.Clear(quantityUnit,0,quantityUnit.Length);
                    Array.Clear(stepInfo,0,stepInfo.Length);
                    Console.WriteLine($"Recipe for the {name} has been deleted");
                    menu();}
                else if (check.Contains("no"))
                {
                    Console.WriteLine("What else would you like to do");
                    menu();
                }
            }catch(IndexOutOfRangeException e){
                Console.WriteLine("No recipe has been captured or recipe has alredy been deleted");
            }

        }
            

        private void Scale(){ 
            try{
                do {
                    Console.WriteLine($"At what scale would you like to alter recipe quantities");
                    double scale = Convert.ToDouble(Console.ReadLine());
                    if (scale < 1 && scale = null)
                     Console.WriteLine("plase enter a number greater than 0");
                    }
                while( scale < 1 && scale = null );
            }catch(IOException){
                Console.WriteLine("please enter a numerical value");
                 Scale();
            }catch(FormatException){
                Console.WriteLine("An error occured, please enter a numerical value");
                 Scale();
            }

            try{
              Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of{scale} ,type 'yes' or 'no' to confirm");
              string check = Console.ReadLine(); 
            }catch(IOException){
               Console.WriteLine("please enter 'yes' or 'no'"); 
                 Console.WriteLine($"Are you sure you would like to scale the recipe ingredients by a factor of{scale} ,type 'yes' or 'no' to confirm");
                   check = Console.ReadLine(); 
            }

            if (check.Contains("yes")){
             quantity.ForEach(sc => sc*scale);
             Console.WriteLine($"Recipe has been scaled by a factor of{scale}");}
            else if (check.Contains("no")){
                try{
                Console.WriteLine("Press 0 to still make changes 1 return to the menu");
                    int go = Convert.ToInt32(Console.ReadKey()); }
                    catch(IOException e){
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

        private void Reset(){
            try{
               Console.WriteLine("Are your sure you want to reset ingredient quantity values");
               string check = Console.ReadLine();
            if(check.Contains("yes"))
                for (int d = 0 ; d < numIngredients ; d++){
                    quantity[d] = defaultValues[d];
                }
            Console.WriteLine($"quantity values for the {name}'s ingredients have been reset");

            }catch(IOException){
                Console.WriteLine( "Please enter 'yes' or 'no'");
                Reset();

            }catch(IndexOutOfRangeException){
                Console.WriteLine("Values have alredy been reset or don't exist");
                menu();
            }
        }

        private  void View(){
            Console.WriteLine($"Recipe:{name}");
            System.Console.WriteLine(" ");
            Console.WriteLine("Ingredients:");
            for (int v= 0 ; v < numIngredients;v++){
                Console.WriteLine($"{quantity[v]} {quantityUnit[v]} of {ingredientName[v]}");
            }
            System.Console.WriteLine(" ");
            Console.WriteLine("Steps:");
            stepInfo.ForEach(st => Console.WriteLine(st));
        }


         public void menu(){   
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
         //switch case
            switch(option)
            {
                
                case 1:
                    entryPrimary();
                    entrySecondary();
                    Format();
                    break;                  
                case 2:
                    Scale();
                    Format();
                    break;                            
                case 3:
                    Reset();
                    break;
                case 4:
                    View();
                    break;        
                case 5:
                    Delete();
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
    }
}
