﻿using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Spelar_Du_In_Bank.Data;
using Spelar_Du_In_Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;

namespace Spelar_Du_In_Bank.Utilities
{
    internal static class UserActions
    {
        public static void CreateNewAccount(BankContext context, User user)//Mojtaba :)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"{user.FirstName}s current accounts");
                //Listing the existing accounts.
                PrintAccountinfo.PrintAccount(context, user);

                //Asking if user wants to creat a new account
                string[] options = { "Create new account", "Main menu" };
                Console.WriteLine();
                int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

                switch (selectedIndex)
                {
                    case (0):
                        Console.Clear();
                        Console.WriteLine();
                        PrintAccountinfo.PrintAccount(context, user);
                        Console.CursorVisible = true;
                        string accName = AdminActions.GetNonEmptyInput("Enter account name:");
                        if (accName == null)
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        //creating new acc with 0 balance.
                        Account newAcc = new Account()
                        {
                            Name = accName,
                            Balance = 0,
                            UserId = user.Id
                        };
                        context.Accounts.Add(newAcc);
                        context.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Account:[{accName}] was created successfully!");
                        Console.ResetColor();
                        Console.WriteLine("Press ENTER to go back");
                        Console.ReadKey();
                        break;

                    //returning back to "mainMenu"
                    case (1):
                        MenuAction.RunUserMenu(user);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! Enter valid command.");
                        Console.ResetColor();
                        //added this so the error message displays before being ereased. /Mojtbaa
                        Thread.Sleep(1200);
                        Console.Clear();
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
                Console.WriteLine($"{user.FirstName}s current accounts");
                Console.WriteLine("");
                //Listing the existing accounts with "printAccountInfo" method.
                PrintAccountinfo.PrintAccount(context, user);

                //"buttons"
                string[] options = { "Deposit money", "Main menu" };
                int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

                switch (selectedIndex)
                {
                    case (0):
                        Console.CursorVisible = true;
                        Console.Clear();
                        PrintAccountinfo.PrintAccount(context, user);
                    //↓ added a goto function when the input is not valid, because we want it to go to
                    //↓ the part where the question was asked and not break out of the loop or go to the
                    //↓ very begning. /Mojtaba
                    WhichAccToDeposit: Console.Write("Enter account ID you wish to deposit into: ");
                        if (int.TryParse(Console.ReadLine(), out int accId))
                        {   //Fetching the input AccId that matches our users Id.
                            var account = context.Accounts
                                .Where(a => a.Id == accId && a.UserId == user.Id)
                                .SingleOrDefault();

                            if (account != null)
                            {
                                //added while loop so you can enter again if input is not numbers
                                while (true)
                                {     //added a goto function for the same reason as before.
                                HowmuchDeposit: Console.Write("How much do you want to deposit? ");
                                    //used a tryparse if entered input is invalid.
                                    if (decimal.TryParse(Console.ReadLine(), out decimal deposit) && deposit > 0)
                                    {
                                        account.Balance = account.Balance + deposit;
                                        context.SaveChanges();
                                        Console.WriteLine();
                                        Console.ResetColor();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"{deposit:c2} were added to {account.Name} account");
                                        Console.WriteLine($"Your new balance is: {account.Balance:C2}");
                                        Console.ResetColor();
                                        Console.WriteLine("Press ENTER to go back");
                                        Console.CursorVisible = false;
                                        //added "ReadKey" so the message displays before going to next step.
                                        //otherwise the message will not show. /Mojtaba
                                        Console.ReadKey();
                                        Console.Clear();
                                        context.SaveChanges();
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        PrintAccountinfo.PrintAccount(context, user);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input! Enter valid number.");
                                        Console.ResetColor();
                                        //↓ added a goto function when the input is not valid, because we want it to go to
                                        //↓ the part where the question was asked and not break out of the loop or go to the
                                        //↓ very begning.  /Mojtaba
                                        goto HowmuchDeposit;
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                PrintAccountinfo.PrintAccount(context, user);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("This account doesnt exist!");
                                Console.WriteLine("Enter a valid account name.");
                                Console.ResetColor();
                                //added a goto function for the same reason as before.
                                goto WhichAccToDeposit;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            PrintAccountinfo.PrintAccount(context, user);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("This account doesnt exist!");
                            Console.WriteLine("Enter a valid account name.");
                            Console.ResetColor();
                            goto WhichAccToDeposit;
                        }
                        break;
                    //returning back to "mainMenu"
                    case (1):
                        MenuAction action = new MenuAction();
                        MenuAction.RunUserMenu(user);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! Enter valid command.");
                        Console.ResetColor();
                        //added this so the error message displays before being ereased. /Mojtbaa
                        Thread.Sleep(1300);
                        Console.Clear();
                        break;
                }
            }
        }
        public static void WithdrawMoney(BankContext context, User user) //- Sean. 
        {

        StartOfWithdrawal: Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Listing the existing accounts.
            Console.WriteLine($"{user.FirstName}s current accounts");
            Console.WriteLine("");

            PrintAccountinfo.PrintAccount(context, user);
            Console.ResetColor();

            string[] options = { "Withdraw money", "Main menu" };
            int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

            switch (selectedIndex)
            {
                case (0):
                    Console.Clear();
                    PrintAccountinfo.PrintAccount(context, user);
                    Console.CursorVisible = true;

                    /////////////// USER INPUTS ACCOUNT ID TO WITHDRAW FROM //////////////////

                    Console.WriteLine("Please enter account ID you want to withdraw from: \nInput [M] to return to main menu:");
                    string accountId = Console.ReadLine(); // AccountID input
                    int intInput;

                    try
                    {
                        if (accountId.ToLower() == "m") // If user inputs M, program returns to main menu
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        intInput = Convert.ToInt32(accountId);
                    }
                    catch // If user inputs nonsense, method restarts restarts. 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. \nPress enter to start over:");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOfWithdrawal;
                    }

                    var account = context.Accounts // LINQ query that searches for bank account with corresponding account ID number
                .Where(a => a.Id == intInput && a.UserId == user.Id)
                .SingleOrDefault();

                    if (account == null) // if statement if searched account doesnt exist.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Account does not exist");
                        Console.WriteLine("Input any key to continue:");
                        Console.ResetColor();
                        Console.ReadKey();
                        goto StartOfWithdrawal;
                    }

                /////////////// USER INPUTS AMOUNT TO WITHDRAW //////////////////

                AmountToWithdraw: Console.WriteLine("Enter amount to withdraw: \nOr [M] to return to main menu:");
                    decimal withdrawal = 0;

                    try // Try-catch block that ensures user inputs numbers and not nonsense.
                    {
                        accountId = Console.ReadLine();
                        if (accountId.ToLower() == "m")
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        withdrawal = Convert.ToDecimal(accountId);
                        if (withdrawal <= 0) // Check If withdrawal is below zero
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input");
                            Console.ResetColor();
                            goto AmountToWithdraw;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter numbers and not letters:");
                        Console.ResetColor();
                        goto AmountToWithdraw;
                    }

                    /////////////// USER INPUTS PIN CODE TO AUTHORIZE WITHDRAWAL //////////////////

                    Console.WriteLine("Please enter PIN to continue:");

                    string pin = Console.ReadLine();

                    while (pin != user.Pin) // If user inputs wrong PIN code, invalid pin code message appears. Program forces user to input right pincode or to return to main menu
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid pin code! Please enter PIN code: \nOr [M] to return to main menu:");
                        Console.ResetColor();
                        pin = Console.ReadLine();
                        if (pin.ToLower() == "m")
                        {
                            MenuAction.RunUserMenu(user);
                        }

                    }
                    Console.Clear();
                    if (pin == user.Pin) // Correct pin code message
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Correct PIN code. Withdrawal authorized.");
                        Console.ResetColor();
                    }

                    if (account.Balance < withdrawal) // If account has less money than desired withdrawal amount, method is reset. 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Withdrawal failed. Insufficient funds in account:");
                        Console.ResetColor();
                        Console.WriteLine("Input any key to continue");
                        Console.ReadKey();
                        goto StartOfWithdrawal;
                    }

                    /////////////// CHANGE IS MADE TO ACCOUNT //////////////////

                    account.Balance -= withdrawal;

                    context.SaveChanges();

                    PrintAccountinfo.PrintAccount(context, user);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Withdrew {withdrawal:C2} from {account.Name}");
                    Console.WriteLine($"Current balance on {account.Name}: {account.Balance:C2}");
                    Console.ResetColor();
                    Console.WriteLine("Press enter to return to menu:");
                    Console.ReadKey();
                    MenuAction.RunUserMenu(user);
                    break;

                case (1):
                    MenuAction.RunUserMenu(user);
                    break;

                default:
                    WithdrawMoney(context, user);
                    break;
            }

        }
        public static void OwnTransfer(BankContext context, User user) // Jing.
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{user.FirstName}s current accounts");
            Console.WriteLine("");
            //Listing the existing accounts with "printAccountInfo" mehtod and return numbers of the account
            int returnAccountNum = PrintAccountinfo.PrintAccount(context, user);
            //"buttons"
            string[] options = { "Transfer whitin accounts", "Main menu" };
            int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

            MenuAction action = new MenuAction();
            switch (selectedIndex)
            {
                case (0):

                    if (returnAccountNum != 1)  // vertify if the user has more than 1 account
                    {
                        Console.Clear();
                        PrintAccountinfo.PrintAccount(context, user);
                        Console.CursorVisible = true;
                    //↓ added a goto function: when the input is not valid we want it go to
                    //↓ the part where the question was asked and not break out of the loop
                    //↓ or go to the very begining.
                    WhichAccToTransferFrom: Console.Write("Please enter ID of account you wish to transfer FROM: ");
                        string fromAcc = Console.ReadLine(); //vertify if the transfer from account ID exist
                        int fromAccId;
                        //use try catch if entered input is invalid
                        try
                        {
                            fromAccId = Convert.ToInt32(fromAcc);
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please press [Enter] to try again!");
                            Console.ReadKey();
                            Console.ResetColor();
                            goto WhichAccToTransferFrom;
                        }
                        var fromAccount = context.Accounts
                           .Where(a => a.Id == fromAccId && a.UserId == user.Id)
                           .SingleOrDefault();
                        decimal amount;

                        if (fromAccount != null)
                        {
                        //↓ added a goto function, same reason as before
                        WhichAccToTransferTo: Console.Write("Please enter ID  of account you wish to transfer TO: ");
                            string toAcc = Console.ReadLine();  //vertify if the transfer to account ID exist
                            int toAccId;
                            //use try catch if entered input is invalid
                            try
                            {
                                toAccId = Convert.ToInt32(toAcc);
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, please press [Enter] to try again!");
                                Console.ReadKey();
                                Console.ResetColor();
                                goto WhichAccToTransferTo;
                            }
                            var toAccount = context.Accounts
                               .Where(a => a.Id == toAccId && a.UserId == user.Id)
                            .SingleOrDefault();

                            if (toAccount != null)
                            {
                            //↓ added a goto function, same reason as before
                            HowMuchAmount: Console.Write("Enter transfer amount: "); //vertify if the amount has over the balance
                                //use a tryparse if enter input is invalid.
                                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0 && amount < fromAccount.Balance)
                                {
                                    fromAccount.Balance -= amount;
                                    context.SaveChanges();
                                    toAccount.Balance += amount;
                                    context.SaveChanges();

                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Your transfer was successful!");
                                    Console.WriteLine($"You transfered {amount:c2} from {fromAccount.Name} account to {toAccount.Name} account");
                                    Console.WriteLine($"Your new balance: {fromAccount.Name} account: {fromAccount.Balance:c2} & {toAccount.Name} account: {toAccount.Balance:c2}");
                                    Console.WriteLine();
                                    Console.ResetColor();
                                    Console.WriteLine("Enter any key back to the main menu....");
                                    Console.ReadKey();
                                    MenuAction.RunUserMenu(user);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Something went wrong, please try again");
                                    Console.ResetColor();
                                    //added a goto function for the sam reason as before
                                    goto HowMuchAmount;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Account Name, please try again");
                                Console.ResetColor();
                                //added a goto function for the sam reason as before
                                goto WhichAccToTransferTo;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Account Name, please try again");
                            Console.ResetColor();
                            //added a goto function for the sam reason as before
                            goto WhichAccToTransferFrom;
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sorry, you only have 1 account. Please create an new account first!");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Entery any key back to the main menu....");
                        Console.ReadKey();
                        MenuAction.RunUserMenu(user);
                    }
                    break;

                //retruning back to "mainMenu"
                case (1):

                    MenuAction.RunUserMenu(user);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Enter valid command.");
                    Console.ResetColor();
                    //added this so the error message displays before being erased
                    Thread.Sleep(1500);
                    OwnTransfer(context, user);
                    break;
            }
        }
        public static void AccountInfo(BankContext context, User user) // Max
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Listing the existing accounts.
            Console.WriteLine($"{user.FirstName}s current accounts");
            Console.WriteLine("");
            PrintAccountinfo.PrintAccount(context, user);
            Console.ResetColor();

            string[] options = { "Account information", "Main menu" };
            int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

            switch (selectedIndex)
            {
                case 0:
                    var userInfo = context.Users
                       .Where(i => i.Id == user.Id)
                       .Select(i => new { i.FirstName, i.LastName, i.Email, i.Phone, i.SSN, AccountCount = i.Accounts.Count() })
                       .FirstOrDefault();
                    Console.Clear();
                    // 2 Methods for coloring console texts. -Max
                    static void MakeYellow()
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    static void MakeWhite()
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    MakeYellow();
                    Console.WriteLine("Your information: ");

                    MakeYellow();
                    Console.Write($"Full Name: ");
                    MakeWhite();
                    Console.WriteLine($"{userInfo.FirstName} {userInfo.LastName}");

                    MakeYellow();
                    Console.Write($"Email: ");
                    MakeWhite();
                    Console.WriteLine($"{userInfo.Email}");

                    MakeYellow();
                    Console.Write($"Phone: ");
                    MakeWhite();
                    Console.WriteLine($"{userInfo.Phone}");

                    MakeYellow();
                    Console.Write($"SSN: ");
                    MakeWhite();
                    Console.WriteLine($"{userInfo.SSN}");
                    MakeYellow();
                    Console.WriteLine($"You currently have {userInfo.AccountCount} Accounts");
                    PrintAccountinfo.PrintAccount(context, user);
                    Console.ResetColor();
                    // Asks if user wants to go back - Max
                    Console.Write("\nEnter [M] to go back to main menu: ");
                    do
                    {
                        string gotoMenu = Console.ReadLine().ToLower();
                        if (gotoMenu == "m")
                        {

                            MenuAction.RunUserMenu(user);
                        }
                        else
                        {
                            Console.WriteLine("Not a valid input... Enter [M] to go back to Main Menu");
                        }
                    } while (true);
                    break;

                case 1:

                    MenuAction.RunUserMenu(user);
                    break;
            }
        }
        public static void ExternalTransferMoney(BankContext context, User user)//Sean
        {
        StartOfTransfer: Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"{user.FirstName}s current accounts");
            Console.WriteLine("");

            PrintAccountinfo.PrintAccount(context, user); // Listing user's current accounts
            Console.ResetColor();

            string[] options = { "Transfer Money", "Main menu" };
            int selectedIndex = MenuHelper.RunMenu(options, true, true, 1, 1);

            switch (selectedIndex)
            {
                case (0):
                    Console.Clear();
                    PrintAccountinfo.PrintAccount(context, user);
                    Console.CursorVisible = true;

                    /////////////// USER INPUTS ACCOUNT ID TO TRANSFER FROM //////////////////

                    Console.WriteLine("Please enter ID of account you want to transfer from: \nInput [M] to return to main menu:");
                    string accountId = Console.ReadLine(); // AccountID input
                    int intInput;

                    try
                    {
                        if (accountId.ToLower() == "m") // If user inputs M, program returns to main menu
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        intInput = Convert.ToInt32(accountId);
                    }
                    catch // If user inputs nonsense, method restarts. 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. \nPress enter to start over:");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOfTransfer;
                    }

                    var accountSend = context.Accounts // LINQ query that searches for bank account with corresponding account ID number
                .Where(a => a.Id == intInput && a.UserId == user.Id)
                .SingleOrDefault();

                    if (accountSend == null) // if statement if searched account doesnt exist.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Account does not exist");
                        Console.WriteLine("Input any key to continue:");
                        Console.ResetColor();
                        Console.ReadKey();
                        goto StartOfTransfer;
                    }

                /////////////// USER INPUTS AMOUNT TO TRANSFER //////////////////

                AmountToSend: Console.WriteLine("Enter amount to transfer: \nOr [M] to return to main menu:");
                    decimal transferAmount = 0;


                    try // Try-catch block ensures user inputs numbers and not nonsense
                    {
                        accountId = Console.ReadLine();
                        if (accountId.ToLower() == "m")
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        transferAmount = Convert.ToDecimal(accountId);
                        if (transferAmount <= 0) // Check If withdrawal is below zero
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input");
                            Console.ResetColor();
                            goto AmountToSend;

                        }

                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter numbers and not letters:");
                        Console.ResetColor();
                        goto AmountToSend;
                    }

                    /////////////// USER INPUTS ACCOUNT ID TO TRANSFER TO //////////////////

                    Console.WriteLine("Please enter account ID of account you wish to send money to: \nInput [M] to return to main menu");

                    string strReciever = Console.ReadLine();
                    int recieverId = 0;

                    try
                    {
                        if (strReciever.ToLower() == "m") // If user inputs M, program returns to main menu
                        {
                            MenuAction.RunUserMenu(user);
                        }
                        recieverId = Convert.ToInt32(strReciever);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. \nPress enter to start over:");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }


                    var reciever = context.Accounts // LINQ query that searches for bank account with corresponding account ID number
                .Where(a => a.Id == recieverId)
                .Include(a => a.user)
                .SingleOrDefault();



                    if (reciever == null || reciever.UserId == accountSend.UserId) // If account receieving money doesn't exist OR is one of the user's own accounts, an error message will show.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Account does not exist");
                        Console.WriteLine("Input any key to continue:");
                        Console.ResetColor();
                        Console.ReadKey();
                        goto StartOfTransfer;
                    }



                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{transferAmount:C2} will be sent to {reciever.user.FirstName}");
                    Console.ResetColor();

                    /////////////// USER INPUTS PIN TO AUTHORIZE TRANSFER //////////////////

                    Console.WriteLine("Please enter PIN to continue:");

                    string pin = Console.ReadLine();

                    while (pin != user.Pin) // While loop forces user to either input the correct PIN or return to main menu
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid pin code! Please enter PIN code: \nOr [M] to return to main menu:");
                        Console.ResetColor();
                        pin = Console.ReadLine();
                        if (pin.ToLower() == "m")
                        {
                            MenuAction.RunUserMenu(user);
                        }

                    }
                    Console.Clear();
                    if (pin == user.Pin)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Correct PIN code. Transfer authorized.");
                        Console.ResetColor();
                    }

                    if (accountSend.Balance < transferAmount) // If account being transferred FROM has less money than the amount the user wants to send, error message is shown.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Transfer failed. Insufficient funds in account:");
                        Console.ResetColor();
                        Console.WriteLine("Input any key to continue");
                        Console.ReadKey();
                        goto StartOfTransfer;
                    }

                    /////////////// CHANGES ARE MADE IN BOTH ACCOUNTS //////////////////

                    accountSend.Balance -= transferAmount;
                    reciever.Balance += transferAmount;

                    context.SaveChanges();

                    PrintAccountinfo.PrintAccount(context, user);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Transferred {transferAmount:C2} from {accountSend.Name}");
                    Console.WriteLine($"Current balance on {accountSend.Name}: {accountSend.Balance:C2}");
                    Console.ResetColor();
                    Console.WriteLine("Press enter to return to menu:");
                    Console.ReadKey();
                    MenuAction.RunUserMenu(user);
                    break;

                case (1):
                    MenuAction.RunUserMenu(user);
                    break;

                default:
                    ExternalTransferMoney(context, user);
                    break;
            }
        }
    }
}
