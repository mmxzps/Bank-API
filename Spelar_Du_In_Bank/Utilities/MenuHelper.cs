﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelar_Du_In_Bank.Utilities
{
    internal class MenuHelper
    {
        //private int SelectedIndex;  //Fields for storing data 
        //private string[] Options;
        //private string Prompt;

        //public MenuHelper(string propmt, string[] options)    //Konstruktor 
        //{
        //    Prompt = propmt;
        //    Options = options;
        //    SelectedIndex = 0;
        //}
        //public void DisplayOptions()    //Method
        //{
        //    Console.CursorVisible = false;
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
        //    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine(Prompt);
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
        //    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
        //    Console.ResetColor();
        //    Console.WriteLine("\nWelcome to the Spelar du in bank, what would you like to do\r\n(Use arrow keys to navigate and enter to select option)\n");

        //    for (int i = 0; i < Options.Length; i++)
        //    {
        //        string currentOption = Options[i];
        //        string prefix;


        //        if (i == SelectedIndex)
        //        {
        //            prefix = "";

        //            Console.ForegroundColor = ConsoleColor.Black;
        //            Console.BackgroundColor = ConsoleColor.Red;
        //        }
        //        else
        //        {
        //            prefix = " ";

        //            Console.ForegroundColor = ConsoleColor.Yellow;
        //            Console.BackgroundColor = ConsoleColor.Black;
        //        }

        //    }
        //    Console.ResetColor();

        //}
        //public int RunHorizontal()    //Run displays options, and registers what keys been pressed 3.
        //{
        //    ConsoleKey keyPressed;
        //    do
        //    {

        //        DisplayOptions();   //Calls meny that display options I/E displays main prompt and meny options. 

        //        ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Registers key info 
        //        keyPressed = keyInfo.Key;   //Update selectedIndex based on arrow keys
        //        if (keyPressed == ConsoleKey.LeftArrow)
        //        {
        //            SelectedIndex--;
        //            if (SelectedIndex == -1)
        //            {
        //                SelectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
        //            }
        //        }
        //        else if (keyPressed == ConsoleKey.RightArrow)
        //        {
        //            SelectedIndex++;
        //            if (SelectedIndex == Options.Length)
        //            {
        //                SelectedIndex = Options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
        //            }
        //        }
        //    }
        //    while (keyPressed != ConsoleKey.Enter); //While loop aslong keypress is not enter. 
        //    {

        //    }
        //    return SelectedIndex;
        //}
        //public int RunVertical()    //Run displays options, and registers what keys been pressed 3.
        //{
        //    ConsoleKey keyPressed;
        //    do
        //    {

        //        DisplayOptionsVertical();   //Calls meny that display options I/E displays main prompt and meny options. 

        //        ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Registers key info 
        //        keyPressed = keyInfo.Key;   //Update selectedIndex based on arrow keys
        //        if (keyPressed == ConsoleKey.UpArrow)
        //        {
        //            SelectedIndex--;
        //            if (SelectedIndex == -1)
        //            {
        //                SelectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
        //            }
        //        }
        //        else if (keyPressed == ConsoleKey.DownArrow)
        //        {
        //            SelectedIndex++;
        //            if (SelectedIndex == Options.Length)
        //            {
        //                SelectedIndex = Options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
        //            }
        //        }
        //    }
        //    while (keyPressed != ConsoleKey.Enter); //While loop aslong keypress is not enter. 
        //    {

        //    }
        //    return SelectedIndex;
        //}

        //public void DisplayOptionsVertical()
        //{
        //    Console.CursorVisible = false;
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine(Prompt);
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.ResetColor();
        //    Console.WriteLine("\nWelcome to the Spelar du in bank, what would you like to do\r\n(Use up and down arrows to navigate and enter to select option)\n");

        //    for (int i = 0; i < Options.Length; i++)
        //    {
        //        string currentOption = Options[i];
        //        string prefix;


        //        if (i == SelectedIndex)
        //        {
        //            prefix = "";

        //            Console.ForegroundColor = ConsoleColor.Black;
        //            Console.BackgroundColor = ConsoleColor.Red;
        //        }
        //        else
        //        {
        //            prefix = " ";

        //            Console.ForegroundColor = ConsoleColor.Yellow;
        //            Console.BackgroundColor = ConsoleColor.Black;
        //        }

        //        Console.WriteLine($"{prefix}{currentOption}");
        //    }
        //    Console.ResetColor();
        //}
        private int _currentAnimationFrame;
        public void ConsoleSpinner()
        {
            SpinnerAnimationFrames = new[]
                                     {
                                         '|',
                                         '/',
                                         '-',
                                         '\\'
                                     };
        }

        public static char[] SpinnerAnimationFrames { get; set; }

        public void UpdateProgress()
        {
            // Store the current position of the cursor
            var originalX = Console.CursorLeft;
            var originalY = Console.CursorTop;

            // Write the next frame (character) in the spinner animation
            Console.Write(SpinnerAnimationFrames[_currentAnimationFrame]);

            // Keep looping around all the animation frames
            _currentAnimationFrame++;
            if (_currentAnimationFrame == SpinnerAnimationFrames.Length)
            {
                _currentAnimationFrame = 0;
            }

            // Restore cursor to original position
            Console.SetCursorPosition(originalX, originalY);
        }
        public void LoadingScreen(CancellationToken cancellationToken)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Loading: ");
            ConsoleSpinner();
           
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Thread.Sleep(100);
                    UpdateProgress();
                    Console.ResetColor();
                    //break;
                }
        }
        public static int RunMeny(string[] options, bool alignment, bool vertical, int position1, int position2)
        {
            Console.CursorVisible = false;
            int selectedIndex = 0;
            //string[] options = { "Account information", "Main meny" };
            ConsoleKey keyPressed;
            do
            {   //Jag ändrade den så att den alltid ligger längst ner i fönstret. Du kan ändra tillbaka om du vill /Mojtaba
                if (alignment == true)
                {
                    Console.SetCursorPosition(1, Console.WindowHeight - 1);
                }
                else if (alignment == false)
                {
                    Console.SetCursorPosition(position1, position2);
                }
               
                for (int i = 0; i < options.Length; i++)
                {
                    string currentOption = options[i];
                    string prefix;

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
                    if (vertical == true)
                    {
                        Console.Write($"{prefix}{currentOption}");
                    }
                    else if (vertical == false)
                    {
                        Console.WriteLine($"{prefix}{currentOption}");
                    }
                }
                   
                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Registers key info 
                keyPressed = keyInfo.Key;   //Update selectedIndex based on arrow keys
                if (vertical== true)
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
            {

            }
            return selectedIndex;
        }


    }
}
