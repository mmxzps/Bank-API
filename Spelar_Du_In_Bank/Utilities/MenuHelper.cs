using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelar_Du_In_Bank.Utilities
{
    internal class MenuHelper
    {
        public static void LoadingScreen(CancellationToken cancellationToken)
        {
            Console.CursorVisible = false;  //hide cursor
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Loading: ");

            char[] spinnerAnimationFrames = { '|', '/', '-', '\\' };
            int currentAnimationFrame = 0;

            while (!cancellationToken.IsCancellationRequested)  //Continue the animation untill a cancellation is requested, thsi comes from cancellationtoken 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(100);

                var originalX = Console.CursorLeft; //sets the cursor possition 
                var originalY = Console.CursorTop;

                Console.Write(spinnerAnimationFrames[currentAnimationFrame]);
                currentAnimationFrame++;    //Move to next frame

                if(currentAnimationFrame == spinnerAnimationFrames.Length)  //resets frames to create a loop
                {
                    currentAnimationFrame= 0;
                }
                Console.SetCursorPosition(originalX, originalY);    //restores current cursor possition to overwrite the previous frame
                
                Console.ResetColor();
            }
        }

        //from this method(RunMenu) we print out the menu AND get the number that can be used in our switch-case.
        public static int RunMenu(string[] options, bool alignment, bool vertical, int position1, int position2)
        {
            Console.CursorVisible = false;
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {  //where the menu will be printed.
                if (alignment == true)
                {
                    //this makes sure that the print of menu always will be at the
                    //bottom, regardless of the hight of information above.
                    Console.SetCursorPosition(1, Console.WindowHeight - 1);
                }
                else if (alignment == false)
                {
                    Console.SetCursorPosition(position1, position2);
                }
                //prints out the menu items one by one
                for (int i = 0; i < options.Length; i++)
                {
                    string currentOption = options[i];
                    string prefix;
                    //if itemsIndex in the menu = i then the color will be changed for it.
                    if (i == selectedIndex)
                    {
                        prefix = " ";

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        prefix = " ";

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    if (vertical)
                    {
                        Console.Write($"{prefix}{currentOption}");
                    }
                    else
                    {
                        Console.WriteLine($"{prefix}{currentOption}");
                    }
                }

                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Registers pressed key inside "keyInfo"
                keyPressed = keyInfo.Key;   //Update selectedIndex based on arrow keys .Key returns the pressed key.
                if (vertical == true)
                {

                    if (keyPressed == ConsoleKey.LeftArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                        {
                            selectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
                        }
                    }
                    else if (keyPressed == ConsoleKey.RightArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                        {
                            selectedIndex = options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
                        }
                    }
                }
                else if (vertical == false)
                {
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                        {
                            selectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
                        }
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                        {
                            selectedIndex = options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
                        }
                    }
                }

            }
            while (keyPressed != ConsoleKey.Enter); //While loop aslong keypress is not enter. 

            //when user press enter we return the selectedIndex so we can use it in our
            //e.g. switch-case whenever this method is used.
            return selectedIndex;
        }


    }
}
