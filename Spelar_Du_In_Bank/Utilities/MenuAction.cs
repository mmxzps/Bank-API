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

namespace Spelar_Du_In_Bank.Utilities
{
    internal class MenuAction
    {
        public void Start()     //first method being called in program.cs 1
        {
            Console.Title = "Spelar du in?";
            RunMainMenu(); //It main purpoise is to change main title and call this method 
        }
        public void RunMainMenu() //Main meny method, this is what will be shown when entering console starts
        {
            string prompt =
@"   _____ ____  ____     ____              __            
  / ___// __ \/  _/    / __ )____ _____  / /_____  ____ 
  \__ \/ / / // /_____/ __  / __ `/ __ \/ //_/ _ \/ __ \
 ___/ / /_/ // /_____/ /_/ / /_/ / / / / ,< /  __/ / / /
/____/_____/___/    /_____/\__,_/_/ /_/_/|_|\___/_/ /_/ 
                                                        ";


            string[] options = { "Admin", "User", "About", "Exit" };   //Meny options
            Console.ForegroundColor = ConsoleColor.Black;
            MenuHelper mainMeny = new MenuHelper(prompt, options);
            int selectedIndex = mainMeny.Run();     //Run method that registers arrowkeys and displays the options. 


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


        public void ExitProgram() //Exit the game
        {
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public void DisplayAboutInfo() //Displays about info 
        {
            Console.Clear();
            Console.WriteLine("Made by:\nJonny Touma\nSean Ortega Schelin\nJing Zhang\nMohtaba Mobasheri\nMax Samuelsson");
            Console.WriteLine("Press any key to return to main meny");
            Console.ReadKey(true);
            RunMainMenu();
        }
        public void RunAdminChoice()
        {
            string prompt = (" Welcome Admin");
            string[] options = { "Login", "Main Menu" };
            MenuHelper loginMeny = new MenuHelper(prompt, options);
            int selectIndex = loginMeny.Run();

            switch (selectIndex)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
            //ExitProgram(); //maybe add return to main 
        }
        public void RunUserChoice()   //OBS!!!! method with switch, might not be used 
        {
            string prompt = (" Welcome User");
            string[] options = { "Login", "Main Menu" };
            MenuHelper userLogin = new MenuHelper(prompt, options);
            int selectIndex = userLogin.Run();

            switch (selectIndex)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    RunMainMenu();
                    break;

            }
        }
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Bank!");
            Console.WriteLine("Please login");

            Console.Write("Enter username:");
            string userName = Console.ReadLine();

            Console.Write("Enter pin code:");
            string pin = Console.ReadLine();

            if (userName == "admin")
            {
                if (pin != "1234")
                {
                    Console.WriteLine("Wrong password!");
                    return;
                }
                AdminActions.DoAdminTasks();
                
                return;
            }
            else
            {
                using (BankContext context = new BankContext())
                {
                    //Looking into user-table to find both username and pin. if found we go to "UserMenu"
                    User user = context.Users.SingleOrDefault(u => u.FirstName == userName && u.Pin == pin);

                    if (user != null)
                    {
                        MenuAction action = new MenuAction();
                        action.RunUserMenu(user);
                        
                    }
                    else
                    {
                        int attempts;
                        for (attempts = 3; attempts > 0; attempts--) // For loop that substracts attempts variable by 1 after every failed login attempts. -Sean 14/11/23
                        {
                            Console.WriteLine("Invalid username or pin code.");
                            // Asking the user what to do next if log in failed. - Max
                            Console.WriteLine("Would you like to try again? [1]: Yes\t [2]: No");
                            Console.WriteLine($"{attempts} attempts left");
                            string tryagainInput = Console.ReadLine();
                            switch (tryagainInput)
                            {
                                case "1":
                                    
                                    Console.Write("Enter username:");
                                    userName = Console.ReadLine();

            }

        }
        public void RunUserMenu(User user)   //OBS!!!! method with switch, might not be used 
        {
            using (BankContext context = new BankContext())
            {
                string prompt = ($"Welcome back {user.FirstName}!~");
                string[] options = { "Accounts & Balance",
                "Account transfer",
                "Whitdrawal",
                "Insert money",
                "Open new account",
                "Logout" };
                MenuHelper userLogin = new MenuHelper(prompt, options);
                int selectIndex = userLogin.RunVertical();

                switch (selectIndex)
                {
                    case 0:
                        AccountInfo(context, user);
                        break;
                    case 1:
                        OwnTransfer(context, user);
                        break;
                    case 2:
                        WithdrawMoney(context, user);
                        break;
                    case 3:
                        InsertMoney(context, user);
                        break;
                    case 4:
                        CreateNewAccount(context, user);
                        break;
                    case 5:
                        RunMainMenu();
                        break;
                   

                }
            }
               
        }

        public static void UserMenu(User user)  //This method need a seperate login to make it work 
        {
            Console.Clear();
            //when user found. welcome user -> UserMenu-Method.
            Console.WriteLine("_________________________");
            Console.WriteLine($"~Welcome back {user.FirstName}!~");
            Console.WriteLine("-------------------------");
            using (BankContext context = new BankContext())
            {
                //Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Choose one of the options:");
                Console.ResetColor();
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Sätt in pengar");
                Console.WriteLine("5. Öppna nytt konto");
                Console.WriteLine("6. Logga ut");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //Se saldo method.
                        AccountInfo(context, user);
                        break;
                    case "2":
                        //Överföring method.
                        OwnTransfer(user);
                        break;
                    case "3":
                        //Se Withdraw method.
                        WithdrawMoney(context, user);
                        break;
                    case "4":
                        //Se Deposit method.
                        InsertMoney(context, user);
                        break;
                    case "5":
                        //calling createNewAcc method.
                        CreateNewAccount(context, user);
                        break;
                    case "6":
                        //Logout method.
                        
                        MainMenu();
                        break;
                }
            }

        }
        public static void CreateNewAccount(BankContext context, User user)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                //Listing the existing accounts.
                Console.WriteLine($"{user.FirstName}s current accounts");
                Console.WriteLine("");
                var accounts = context.Users
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.Accounts)
                    .SingleOrDefault()
                    .Accounts
                    .ToList();

                for (int i = 0; i < accounts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{accounts[i].Name} Balance:{accounts[i].Balance:C2}");
                    Console.WriteLine("_____________________________________");
                    Console.ResetColor();
                }

                //Asking if user wants to creat a new account
                Console.WriteLine("_____________________________________");
                Console.WriteLine("[C] to create new Account");
                Console.WriteLine("[M] to go back to main menu");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "c":
                        Console.Write("Enter account name:");
                        string accName = Console.ReadLine();

                        //creating new acc with 0 balance.
                        Account newAcc = new Account()
                        {
                            Name = accName,
                            Balance = 0,
                            UserId = user.Id
                        };
                        context.Accounts.Add(newAcc);
                        context.SaveChanges();
                        break;

                    //returning back to "mainMenu"
                    case "m":
                        UserMenu(user);
                        break;
                }
            }
        }
        public static void InsertMoney(BankContext context, User user) // Mojtaba
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                //Listing the existing accounts.
                Console.WriteLine($"{user.FirstName}s current accounts");
                Console.WriteLine("");
                var accounts = context.Users
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.Accounts)
                    .SingleOrDefault()
                    .Accounts
                    .ToList();

                for (int i = 0; i < accounts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{accounts[i].Name} Balance:{accounts[i].Balance:C2}");
                    Console.WriteLine("_____________________________________");
                    Console.ResetColor();
                }

                //Asking if user wants to creat a new account
                Console.WriteLine("_____________________________________");
                Console.WriteLine("[D] to deposit money into your account");
                Console.WriteLine("[M] to go back to main menu");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "d":
                        Console.Write("Which account do you want to deposit money into?:");
                        string accName = Console.ReadLine();

                        var account = context.Accounts
                       .Where(a => a.Name == accName && a.UserId == user.Id)
                       .SingleOrDefault();

                        if (account != null)
                        {
                            Console.WriteLine("How much do you want to deposit?");
                            decimal deposit = decimal.Parse(Console.ReadLine());

                            account.Balance = account.Balance + deposit;
                            context.SaveChanges();
                            Console.WriteLine($"{deposit} added to {account.Name}");
                            Console.WriteLine($"Balance: {account.Balance}");
                            context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("This account doesnt exist!");
                        }
                        break;

                    //returning back to "mainMenu"
                    case "m":
                        UserMenu(user);
                        break;
                }
            }
        }
        public static void WithdrawMoney(BankContext context, User user) //- Sean. 
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                //Listing the existing accounts.
                Console.WriteLine($"{user.FirstName}s current accounts");
                Console.WriteLine("");
                var accounts = context.Users
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.Accounts)
                    .SingleOrDefault()
                    .Accounts
                    .ToList();
                //Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < accounts.Count; i++)
                {
                    
                    Console.WriteLine($"{i + 1}.{accounts[i].Name} Balance:{accounts[i].Balance:C2}");
                    Console.WriteLine("_____________________________________");
                    
                }
                Console.ResetColor();

                Console.WriteLine("Enter account name you wish to withdraw from or inpupt [M] to return to main menu:");              

                string input = Console.ReadLine(); //Input name of account to withdraw from
               
                var account = context.Accounts
                    .Where(a => a.Name.ToLower() == input.ToLower() && a.UserId == user.Id)
                    .SingleOrDefault();

                if (account == null) // if statement if searched account doesnt exist.
                {
                    Console.Clear();
                    Console.WriteLine("Account does not exist");
                    Console.WriteLine("Input any key to continue:");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Enter amount to withdraw or [M] to return to main menu:");               

                input = Console.ReadLine();

                if (input.ToLower() == "m")
                {
                    UserMenu(user);
                }

                while (withdrawal <= 0) // Decimal check here. 
                {
                    Console.WriteLine("Invalid input:");
                    Console.WriteLine("Enter amount to withdraw:");
                    withdrawal = Convert.ToDecimal(Console.ReadLine());
                }

                if (account.Balance <= 0)
                {
                    Console.WriteLine("Withdrawal failed. Insufficient funds in account:");
                    Console.WriteLine("Input any key to continue");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Please enter PIN to continue:");

                string pin = Console.ReadLine();

                while (pin != user.Pin)
                {
                    Console.WriteLine("Invalid pin code! Please try again or [M] to return to main menu:");                 
                    if (input.ToLower() == "m")
                    {
                        UserMenu(user);
                    }
                    pin = Console.ReadLine();
                }

                if (pin == user.Pin)
                {
                    Console.WriteLine("Correct PIN code. Withdrawal authorized.");
                }
              
                account.Balance -= withdrawal;
                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Withdrew {withdrawal:C2} from {account.Name}");
                Console.WriteLine($"Current balance on {account.Name}: {account.Balance:C2}");
                Console.ResetColor();
                Console.WriteLine("Input any key to continue:");
                Console.ReadKey();
                UserMenu(user);
            }

        }
        public static void OwnTransfer(User user) // Jing. Add code to check if valid Account ID is entered.
        {
            using (BankContext context = new BankContext())
            {
                Console.Clear();
                Console.WriteLine($"{user.FirstName}'s accounts:");
                
                var accounts = context.Accounts
                    .Where(a => a.UserId == user.Id)
                    .ToList();

                foreach (var account in accounts)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Account Id\tAccount Name\tAvailable balance");
                    Console.ResetColor();
                    Console.WriteLine($"{account.Id}\t\t{account.Name}\t\t{account.Balance}");
                }

                Console.WriteLine("Transfer from account (please enter the Account Id): ");
                int fromAccountId = int.Parse(Console.ReadLine());   //vertify if the account id exist
                Console.WriteLine("Transfer to account (please enter Account Id): ");
                int toAccountId = int.Parse(Console.ReadLine());  //vertify if the account id exist
                Console.WriteLine("Enter transfer amount : ");

                decimal Amount = Convert.ToDecimal(Console.ReadLine());  //vertify if the amount has over the balance
                var fromAccount = DbHelper.GetAllAccounts(context)
                    .Where(a => a.Id == fromAccountId)
                    .FirstOrDefault();
                if (fromAccount != null)
                {
                    if (fromAccount.Balance >= Amount)
                    {
                        fromAccount.Balance -= Amount;
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("The money you want to transfer has over your balance, please enter E to exit or C to continue: ");
                        string option = Console.ReadLine().ToUpper();   //need to add a loop here

                    }
                }

                var toAccount = DbHelper.GetAllAccounts(context)
                    .Where(a => a.Id == toAccountId)
                    .FirstOrDefault();

                if (toAccount != null)
                {
                    toAccount.Balance += Amount;
                    context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your transfer has successed! The current amount of your two accounts are: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Account Id\tAccount Name\tAvailable balance");
                    Console.WriteLine($"{fromAccount.Id} = \t\t{fromAccount.Name}\t\t{fromAccount.Balance}");
                    Console.WriteLine($"{toAccount.Id} = \t\t{toAccount.Name}\t\t{toAccount.Balance}");
                    Console.ResetColor();
                    
                }

               
            }
        }
        public static void AccountInfo(BankContext context, User user) // Mojtaba
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Listing the existing accounts.
            Console.WriteLine($"{user.FirstName}s current accounts");
            Console.WriteLine("");
            var accounts = context.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.Accounts)
                .SingleOrDefault()
                .Accounts
                .ToList();

            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{accounts[i].Name} Balance:{accounts[i].Balance:C2}");
                Console.WriteLine("_____________________________________");
                
            }
            Console.ResetColor();
            //Asking if user wants to creat a new account

            Console.WriteLine("[S] to show full information Account");
            Console.WriteLine("[M] to go back to main menu");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "s":
                    Console.WriteLine("Under construction...");
                    break;

                //returning back to "mainMenu"
                case "m":
                    UserMenu(user);
                    break;
            }

        }

       

    }
}   