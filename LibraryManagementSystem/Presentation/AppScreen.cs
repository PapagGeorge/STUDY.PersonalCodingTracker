using LibraryApplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using LibraryApplication.Interfaces;
using System.Text.RegularExpressions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;



namespace Presentation
{
    public class AppScreen
    {
        private readonly IApplication _application;
        public AppScreen(IApplication application)
        {
            _application = application;
        }

        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "MyLibrary App";
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\n-----------------Welcome to MyLibrary App-----------------\n\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        internal static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("-----------------Member's Menu-----------------\n");
            Console.WriteLine("1. Search For A Book");
            Console.WriteLine("2. List Of Available Books");
            Console.WriteLine("3. List Of All MyLibrary Books");
            Console.WriteLine("4. Rent A Book");
            Console.WriteLine("5. Return A Book");

            Console.WriteLine("\n\n-----------------Administrator's Menu-----------------\n");
            Console.WriteLine("6. Register Member");
            Console.WriteLine("7. Delete Member");
            Console.WriteLine("8. Member Search");
            Console.WriteLine("9. Increase Book Copies");
            Console.WriteLine("10. Decrease Book Copies");
            Console.WriteLine("11. Insert New Book");
            Console.WriteLine("\n\n12. Exit MyLibrary Application");


        }

        private int GetMenuChoice()
        {

            while (true)
            {
                Console.WriteLine("\n\nPlease enter your choice 1 - 12 and press ENTER");
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        internal void RunMenuChoice()
        {
            Welcome();
            while (true)
            {
                DisplayMenu();
                var choice = GetMenuChoice();
                switch (choice)
                {
                    case 1:
                        string userSearchText = string.Empty;
                        while (string.IsNullOrEmpty(userSearchText))
                        {
                            Console.Clear();
                            Console.WriteLine("\n\nEnter the ISBN or Title of the book you want to search. You can type MENU if you " +
                                "want to return to the Menu.");
                            userSearchText = Console.ReadLine();

                            if (string.IsNullOrEmpty(userSearchText))
                            {
                                Console.WriteLine("Your search is invalid. Press any key to try again or type MENU if you " +
                                    "want to return to the menu.");
                                Console.ReadKey();
                            }

                            else if(userSearchText.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            else
                            {
                                _application.SearchBook(userSearchText);
                                Console.WriteLine("Press any key to return to the Menu");
                                Console.ReadKey();
                                
                                break;

                            }

                        }

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("\n-----------------List of Books Available-----------------\n\n");
                        _application.AvailableBooks();
                        Console.WriteLine("Press any key to return to the Menu");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("\n-----------------List of all MyLibrary Books-----------------\n\n");
                        _application.AllBooks();
                        Console.WriteLine("Press any key to return to the Menu");
                        Console.ReadKey();
                        
                        break;

                    case 4:
                        string rentUserIsbn = string.Empty;
                        int rentUserId = 0;
                        while (string.IsNullOrEmpty(rentUserIsbn) || rentUserId == 0)
                        {
                            Console.Clear();
                            _application.AllBooks();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\nCheck the above list of Books and enter the ISBN of the book you want to rent.");
                            rentUserIsbn = Console.ReadLine();

                            if (rentUserIsbn.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            Console.WriteLine("\nEnter your MyLibrary Id: ");
                            string _userId = Console.ReadLine();

                            if (_userId.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            bool isUserIdInputANumber = int.TryParse(_userId, out rentUserId);

                            if (string.IsNullOrEmpty(rentUserIsbn) || rentUserId == 0 || !isUserIdInputANumber)
                            {
                                Console.Clear();
                                Console.WriteLine("\nThe information you entered is invalid. Press any key to try again.");
                                Console.ReadKey();
                            }
                            
                            else
                            {
                                Console.Clear();
                                _application.RentBook(rentUserIsbn, rentUserId);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();

                                break;

                            }

                        }


                        break;

                    case 5:
                        string returnUserIsbn = string.Empty;
                        int returnUserId = 0;
                        while (string.IsNullOrEmpty(returnUserIsbn) || returnUserId == 0)
                        {
                            Console.Clear();
                            
                            _application.AllBooks();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\nCheck the above list of Books and enter the ISBN of the book you want to return.");
                            returnUserIsbn = Console.ReadLine();
                            if (returnUserIsbn.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            Console.WriteLine("\nEnter your MyLibrary Id: ");
                            string _returnUserId = Console.ReadLine();

                            if (_returnUserId.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }
                            bool isUserIdInputANumber = int.TryParse(_returnUserId, out returnUserId);

                            if (string.IsNullOrEmpty(returnUserIsbn) || returnUserId == 0 || !isUserIdInputANumber)
                            {
                                Console.Clear();
                                Console.WriteLine("\nThe information you entered is invalid. Press any key to try again.");
                                Console.ReadKey();
                            }

                            else
                            {
                                Console.Clear();
                                _application.ReturnBook(returnUserIsbn, returnUserId);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();

                                break;

                            }

                        }

                        break;

                    case 6:

                       
                             
                        while (true)
                            
                        {
                            Console.Clear();

                            User newUser = new User();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\nInsert the following information in order to register new user.");
                            Console.WriteLine("Note: All fields are required.");
                            

                            Console.Write("\n\nFirst Name: ");
                            string firstName = Console.ReadLine();

                            Console.Write("Last Name: ");
                            string lastName = Console.ReadLine();

                            Console.Write("Email: ");
                            string email = Console.ReadLine();

                          
                            if (!IsValidEmail(email))
                            {
                                Console.WriteLine("The email you entered is not valid. Please try again. " +
                                    "Press any key to continue.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Mobile Phone: ");
                            string mobilePhone = Console.ReadLine();
                            string pattern = @"^\d+$";
                            bool isNumeric = Regex.IsMatch(mobilePhone, pattern);

                            if (!isNumeric || mobilePhone.Length > 15)
                            {
                                Console.WriteLine("The mobile phone you entered is not valid. Please try again. " +
                                    "Press any key to continue.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Address: ");
                            string address = Console.ReadLine();

                            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)
                                || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mobilePhone) || string.IsNullOrEmpty(address))
                            {
                                Console.WriteLine("\nThe information you entered is invalid. Press any key to try again.");
                                Console.ReadKey();
                                continue;
                            }

                            if (firstName.Trim().ToUpper() == "MENU" || lastName.Trim().ToUpper() == "MENU" ||
                                email.Trim().ToUpper() == "MENU" || mobilePhone.Trim().ToUpper() == "MENU" ||
                                address.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }



                            else
                            {
                                Console.Clear();
                                newUser.UserFirstName = firstName;
                                newUser.UserLastName = lastName;
                                newUser.UserEmail = email;
                                newUser.UserMobilePhone = mobilePhone;
                                newUser.UserAddress = address;
                                _application.RegisterUser(newUser);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();

                                break;

                            }
                            
                        }
                        break;

                    case 7:
                        while (true)
                        {
                            Console.Clear();
                            _application.GetAllUsers();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\n\nCheck the above list of members and insert the MyLibrary Id " +
                                "of the user you want to delete.");
                            string _deleteUserId = Console.ReadLine();
                            int deleteUserId;
                            bool isDeleteUserIdNumber = int.TryParse(_deleteUserId, out deleteUserId);

                            if (_deleteUserId.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            if (isDeleteUserIdNumber)
                            {
                                _application.DeleteUser(deleteUserId);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("The MyLibrary Id you entered is invalid. Press any key to try again.");
                                Console.ReadLine();
                            }
                            
                        }


                        break;

                    case 8:
                        string userSearchText2 = string.Empty;
                        while (string.IsNullOrEmpty(userSearchText2))
                        {
                            Console.Clear();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\n\nEnter the User Id or Mobile Phone of the member you want to search.");
                            Console.WriteLine("Note: If you want to search by Mobile Phone type letter (p) first.");
                            Console.WriteLine("ex: p12345689");
                            userSearchText = Console.ReadLine();

                            if (string.IsNullOrEmpty(userSearchText))
                            {
                                Console.WriteLine("Your search is invalid. Press any key to try again or type MENU if you " +
                                    "want to return to the menu.");
                                Console.ReadKey();
                            }

                            else if (userSearchText.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            else
                            {
                                Console.Clear();
                                _application.SearchUser(userSearchText);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();

                                break;

                            }

                        }

                        break;
                    
                    case 9:
                        while (true)
                        {

                            Console.Clear();
                            _application.AllBooks();
                            Console.WriteLine("\n-----You can type (MENU) if you want to return to the Menu.-----");
                            Console.WriteLine("\n\nCheck the above list of MyLibrary books.");
                            Console.Write("Insert the Book ISBN you want to increase copies: ");
                            string bookIsbn = Console.ReadLine();

                            if (bookIsbn.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            Console.Write("\n\nInsert the number of copies you want to add: ");
                            string increaseNumberOfCopies = Console.ReadLine();
                            int _increseNumberOfCopies;
                            bool checkIncreaseAmount = int.TryParse(increaseNumberOfCopies, out _increseNumberOfCopies);

                            if(increaseNumberOfCopies.Trim().ToUpper() == "MENU")
                            {
                                break;
                            }

                            if(string.IsNullOrEmpty(bookIsbn) || !checkIncreaseAmount)
                            {
                                Console.WriteLine("The information you entered is invalid. Press enter to try again.");
                                Console.ReadLine();
                                continue;
                            }
                            else
                            {
                                _application.IncreaseBookCopies(bookIsbn, _increseNumberOfCopies);
                                Console.WriteLine("The copies have been added successfully. Press enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        }



                        break;




                        break;

                    default:
                        break;
                }
            }
        }

        static bool IsValidEmail(string email)
        {         
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
