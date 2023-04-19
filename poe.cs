using System;

namespace PoePt1.cs{
        public static void main (string [] args){
            recipe recipe = new recipe();
            try {
                menu();
            } catch (IO.IOException e ){
                Console.WriteLine("Please enter an integer to proceed");
            }
           
        }

        private void menu(){
            Console.WriteLine("Sanele Cooks (Beta)");
                Console.WriteLine("Please select an option to proceed");
            int option = Console.ReadLine();
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
                    recipe.Entry();
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
                    Console.WriteLine("Goodbye");
                    break;
                default:
                   Console.WriteLine("Invalid operation");
                   break;
                   }           
            } while (option <= 5 );
        }

    public class recipe{
        private string name{get;set;}
        private string numIngredients{get;set;}
        private string [] ingredientName = new string[numIngredients];
        private double [] quantity = new double[numIngredients];
        private var quantityUnit = new string[numIngredients];
        private int steps{get;set;}
        private string [] stepInfo = new string[numIngredients];   
        private var defaultValues = new List<double>(qunatity);

        private static void Entry(){
            name =  Console.Readline("Enter the name of the recipe");
            numIngredeinients = Console.Readline($"Enter the number of ingredients for the {name}");
            stepInfo = console.ReadLine($"How many steps are invovled in making the {name}");

            for( int i = 0 ;  i < ingredients.Count ; i++){
                ingredientName[i] = Console.Readline($"Enter the name of ingredient {i}");
                quantity[i] = Console.Readline($"Enter the quantity of {ingredientName[i]}");
                quantityUnit[i] = Console.Realine($"enter the unit of measurment for the {quantity[i]} {ingredientName}s");
            }

            steps = Console.readLines($"How many steps are involved in making the {name}");
            for( int s = 0 ; s < steps; s++){
                stepInfo[s] = Console.Readline($"Describe step{s}");
                }

            System.Console.WriteLine($"Recipe for the{name} has been captured");

        }
        
        private static void Formatt(){
           // change the quantity amount quantity Unit  according  result of Scale method
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
        }

        private static void Delete(){
            Array.Clear(ingredientName,0,ingredientName.Length);
            Array.Clear(quantity,0,quantity.Length);
            Array.Clear(quantityUnit,0,quantityUnit.Length);
            Array.Clear(stepInfo,0,stepInfo.Length);
            Console.WriteLine($"Recipe for the {name} has been deleted");
        }

        private static void Scale(){ 
        double scale = Console.Readline($"At what scale would you like to alter recipe quantities");
        string check = Console.Readline($"Are you sure you would like to scale the recipe ingredients by a factor of{scale} ,type yes to confirm");
        if (check.Contains("yes"))
        quantity.ForEach(sc => sc*scale);
        else
         Scale();
        Console.WriteLine($"Recipe has been scaled by a factor of{scale}");
       } 

        private static void Reset(){
                for (int d = 0 ; d < quantity.Count ; d++){
                    quantity[d] = defaultValues[d];
                }
            Console.WriteLine($"quantity values for the {name}'s ingredients have been reset");
        }

        private static View(){
            Console.WriteLine($"Recipe:{name}");
            Console.WriteLine("Ingredients:");
            for (int v= 0 ; v < ingredientName.Count;v++){
                Console.WriteLine(ingredient[v] + quantityUnit[v]);
            }
            Console.WriteLine("Steps:");
            stepInfo.ForEach(st => Console.WriteLine(st));
        }
    }
}
