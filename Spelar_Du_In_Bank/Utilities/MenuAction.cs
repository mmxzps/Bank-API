﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spelar_Du_In_Bank.Utilities;
using Spelar_Du_In_Bank.Data;
using Spelar_Du_In_Bank.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Principal;
using ConsoleTables;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.Design;
using Microsoft.Extensions.Options;
using System.Drawing;

namespace Spelar_Du_In_Bank.Utilities
{
    internal class MenuAction
    {
        public static void Start()     //first method being called in program.cs 1
        {
            Console.Title = "Spelar du in?";
            MainMenu(); //It main purpoise is to change main title and call this method 
        }
        public static void MainMenu() //Main meny method, this is what will be shown when entering console starts
        {
            Console.Clear();
            string prompt =
@" .oooooo..o oooooooooo.   ooooo         oooooooooo.                        oooo                              
d8P'    `Y8 `888'   `Y8b  `888'         `888'   `Y8b                       `888                              
Y88bo.       888      888  888           888     888  .oooo.   ooo. .oo.    888  oooo   .ooooo.  ooo. .oo.   
 `""Y8888o.   888      888  888           888oooo888' `P  )88b  `888P""Y88b   888 .8P'   d88' `88b `888P""Y88b  
     `""Y88b  888      888  888  8888888  888    `88b  .oP""888   888   888   888888.    888ooo888  888   888  
oo     .d8P  888     d88'  888           888    .88P d8(  888   888   888   888 `88b.  888    .o  888   888  
8""""88888P'  o888bood8P'   o888o         o888bood8P'  `Y888""""8o o888o o888o o888o o888o `Y8bod8P' o888o o888o 
                                                        ";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            string[] options = { "Admin", "User", "About", "Exit" };   //Meny options
            Console.ForegroundColor = ConsoleColor.Black;
            //MenuHelper mainMeny = new MenuHelper(prompt, options);
            int selectedIndex = MenuHelper.RunMenu(options, false, true, 1, 13);     //Run method that registers arrowkeys and displays the options. 


            switch (selectedIndex)
            {
                case 0:
                    RunAdminChoice();
                    break;
                case 1:
                    RunUserChoice();
                    break;
                case 2:
                    DisplayAboutInfo();
                    break;
                case 3:
                    ExitProgram();
                    break;
            }
        }
        public static void ExitProgram() //Exit the game
        {
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void DisplayAboutInfo() //Displays about info 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This program was made by:");
            Console.WriteLine();
            Console.ResetColor();
            string[] jing = new string[] { "J", "i", "n", "g", " ", "Z", "h", "a", "n", "g" };
            foreach (string letter in jing)
            {
                Console.Write(letter);
                Thread.Sleep(40);
            }
            Console.WriteLine();
            string[] jonny = new string[] { "J", "o", "n", "n", "y", " ", "T", "o", "u", "m", "a" };
            foreach (string letter in jonny)
            {
                Console.Write(letter);
                Thread.Sleep(40);
            }
            Console.WriteLine();
            string[] max = new string[] { "M", "a", "x", " ", "S", "a", "m", "u", "e", "l", "s", "s", "o", "n" };
            foreach (string letter in max)
            {
                Console.Write(letter);
                Thread.Sleep(35);
            }
            Console.WriteLine();
            string[] moj = new string[] { "M", "o", "j", "t", "a", "b", "a", " ", "M", "o", "b", "a", "s", "h", "e", "r", "i" };
            foreach (string letter in moj)
            {
                Console.Write(letter);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            string[] sean = new string[] { "S", "e", "a", "n", " ", "O", "r", "t", "e", "g", "a", " ", "S", "c", "h", "e", "l", "i", "n" };
            foreach (string letter in sean)
            {
                Console.Write(letter);
                Thread.Sleep(20);

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Found a bug? Contact us on Slack. Your feedback helps us improve. Thanks!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to return to main menu");
            Console.ResetColor();
            Console.ReadKey(true);
            MainMenu();
        }
        public static void RunAdminChoice()
        {
            Console.Clear();
            string prompt = (" \t\t\t\t\t\tWelcome Admin");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            string[] options = { "Login", "Return" };
            int selectIndex = MenuHelper.RunMenu(options, false, true, 1, 6);

            switch (selectIndex)
            {
                case 0:
                    AdminLogin();
                    break;
                case 1:
                    MainMenu();
                    break;
            }

        }
        public static void RunUserChoice()
        {
            Console.Clear();
            string prompt = (" \t\t\t\t\t\tWelcome User");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            string[] options = { "Login", "Return" };
            int selectIndex = MenuHelper.RunMenu(options, false, true, 1, 6);

            switch (selectIndex)
            {
                case 0:
                    LoginMenu();
                    break;
                case 1:
                    MainMenu();
                    break;
            }
        }
        public static void AdminLogin()
        {
            Console.Clear();
            string prompt = (" \t\t\t\t\t\tWelcome Admin");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            //Made the cursor visible again
            Console.CursorVisible = true;
            Console.Write("Enter pin code:");
            string pin = Console.ReadLine();

            CancellationTokenSource cts = new CancellationTokenSource();    //Create a cancellationtoken source, witch is used to create a cancellationtoken
            Thread loadingThread = new Thread(() => MenuHelper.LoadingScreen(cts.Token));   //create a new thread and start it, use lamda expression to call on method.
            loadingThread.Start();  //Start thread

            if (pin == "1234") // If user chooses to login as an administrator, I thought it would be redundant to force the user to type in "admin" as username so I the only requirement I made is for the admin pin to be correct (and it is hard coded)
            {                  // If pin input is right the first time, the program goes to the admin menu
                cts.Cancel();
                loadingThread.Join();
                AdminActions.DoAdminTasks();
            }
            else if (pin != "1234") // If PIN input is wrong, the user then has 3 tries to login correctly.
            {
                ////////// LOGIN ATTEMPT COUNTDOWN METHOD ////////////

                cts.Cancel();
                loadingThread.Join();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid admin PIN code!");
                Console.ResetColor();
                int attempts;
                for (attempts = 3; attempts > 0; attempts--) // For loop that substracts attempts variable by 1 after every failed login attempts. -Sean 14/11/23
                {

                    cts.Cancel();
                    loadingThread.Join();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid PIN code.");
                    Console.WriteLine($"{attempts} attempts left");
                    Console.WriteLine("Would you like to try again?");
                    string[] options = { "Yes", "No" };
                    Console.ResetColor();
                    int selectIndex = MenuHelper.RunMenu(options, false, true, 1, 6);
                    switch (selectIndex) // Switch case. If user decides to try to login again, attempts variable decreases by 1
                    {
                        case 0:
                            Console.Clear();
                            Console.WriteLine();
                            Console.CursorVisible = true;
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.Write("Enter pin code:"); // User is asked to input right PIN code again. 
                            pin = Console.ReadLine();

                            if (pin == "1234")
                            {
                                Console.WriteLine("Correct admin PIN");
                                AdminActions.DoAdminTasks();
                            }
                            break;
                        case 1:
                            MainMenu();
                            break;
                    }
                }
                Console.WriteLine("You are out of tries");
                Console.ReadLine();
            }
        }
        public static void LoginMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To login press [any key] or press [escape key] to return");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Reads the key press and stores it to keyInfo, set to true so we dont want to show the keypress in console
            if (keyInfo.Key == ConsoleKey.Escape)   //if esc pressed return to menu 
            {
                Console.WriteLine("You pressed Escape key");
                Thread.Sleep(700);
                MainMenu();
            }
            Console.CursorVisible = true;
            Console.Write("Enter username:");
            string userName = Console.ReadLine();

            Console.Write("Enter pin code:");
            string pin = Console.ReadLine();

            CancellationTokenSource cts = new CancellationTokenSource();    //Create a cancellationtoken source, witch is used to create a cancellationtoken
            Thread loadingThread = new Thread(() => MenuHelper.LoadingScreen(cts.Token));   //create a new thread and start it, use lamda expression to call on method.
            loadingThread.Start();  //Start thread

            using (BankContext context = new BankContext())
            {
                //Looking into user-table to find both username and pin. if found we go to "UserMenu"
                User user = context.Users.SingleOrDefault(u => u.FirstName == userName && u.Pin == pin);

                if (user != null)
                {
                    cts.Cancel();
                    loadingThread.Join();
                    RunUserMenu(user);
                }

                else
                {
                    ////////// LOGIN ATTEMPT COUNTDOWN METHOD ////////////
                    cts.Cancel();
                    loadingThread.Join();
                    int attempts;
                    for (attempts = 3; attempts > 0; attempts--) // For loop that substracts attempts variable by 1 after every failed login attempts. -Sean 14/11/23
                    {
                        cts.Cancel();
                        loadingThread.Join();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid username or pin code.");
                        Console.WriteLine("If you are an administrator, please restart the program and attempt login again.");

                        Console.WriteLine($"{attempts} attempts left");

                        Console.WriteLine("Would you like to try again?");
                        string[] options = { "Yes", "no" };
                        Console.ResetColor();
                        int selectIndex = MenuHelper.RunMenu(options, false, true, 1, 6);

                        switch (selectIndex) // Switch case. If user decides to try to login again, attempts variable decreases by 1
                        {
                            case (0):
                                Console.Clear();
                                Console.WriteLine();
                                Console.CursorVisible = true;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("Enter username:");
                                userName = Console.ReadLine();
                                Console.Write("Enter pin code:");
                                pin = Console.ReadLine();


                                user = context.Users.SingleOrDefault(u => u.FirstName == userName && u.Pin == pin); // CHecking if current attempt at username and PIN works

                                if (user != null) // if above LINQ query returns the right account, the program runs the user menu.
                                {
                                    RunUserMenu(user);
                                }
                                break;
                            case (1):
                                MainMenu();
                                break;


                        }
                    }
                    if (attempts == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Maximum number of attempts reached. The program will now close.");
                        Console.ResetColor();
                        Environment.Exit(1);
                    }

                }
                Console.ResetColor();

            }
        }
        public static void RunUserMenu(User user)
        {
            using (BankContext context = new BankContext())
            {
                Console.Clear();
                string prompt = ($"\t\t\t\t\t\tWelcome back {user.FirstName}!~");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                string[] options = {
                "Accounts & Balance",
                "Internal transfer",
                "Withdrawal",
                "Deposit",
                "Open new account",
                "External transfer",
                "Logout" };
                int selectIndex = MenuHelper.RunMenu(options, false, false, 0, 7);

                switch (selectIndex)
                {
                    case 0:
                        UserActions.AccountInfo(context, user);
                        break;
                    case 1:
                        UserActions.OwnTransfer(context, user);
                        break;
                    case 2:
                        UserActions.WithdrawMoney(context, user);
                        break;
                    case 3:
                        UserActions.InsertMoney(context, user);
                        break;
                    case 4:
                        UserActions.CreateNewAccount(context, user);
                        break;
                    case 5:
                        UserActions.ExternalTransferMoney(context, user);
                        break;
                    case 6:
                        MainMenu();
                        break;
                }
            }
        }

    }
}