using LibraryApplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using LibraryApplication.Interfaces;



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

                            else if(userSearchText.ToUpper() == "MENU")
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
                        
                        break;

                    case 4:
                        string userIsbn = string.Empty;
                        int userId = 0;
                        while (string.IsNullOrEmpty(userIsbn) || userId == 0)
                        {
                            Console.Clear();
                            _application.AllBooks();
                            Console.WriteLine("\nCheck the above list of Books and enter the ISBN of the book you want to rent.");
                            userIsbn = Console.ReadLine();

                            Console.WriteLine("\nEnter your MyLibrary Id: ");
                            string _userId = Console.ReadLine();
                            bool isUserIdInputANumber = int.TryParse(_userId, out userId);

                            if (string.IsNullOrEmpty(userIsbn) || userId == 0 || !isUserIdInputANumber)
                            {
                                Console.Clear();
                                Console.WriteLine("\nThe information you entered is invalid. Press any key to try again.");
                                Console.ReadKey();
                            }
                            
                            else
                            {
                                Console.Clear();
                                _application.RentBook(userIsbn, userId);
                                Console.WriteLine("\n\nPress any key to return to the Menu");
                                Console.ReadKey();

                                break;

                            }

                        }


                        break;


                    default:
                        break;
                }
            }
        }

    }
}
