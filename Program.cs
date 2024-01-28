using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameinclasses
{

    class Program
    {
        static void SubMenuBeforeMainMenu(string submenu)
        {
            string message = submenu + " Menu";
            Console.WriteLine($"{message}");
            Console.WriteLine("---------------------");
        }

        static void SubMenu(string submenu)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string message = submenu + " Menu";
            Console.WriteLine($"{message}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---------------------");
            Console.ResetColor();
        }
       

    static void DisplaySeparator(string title)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n****************************************************");
        Console.WriteLine($"** {title.ToUpper()} **");
        Console.WriteLine("****************************************************");
        Console.ResetColor();
    }
    static string SignIn(string userName, string password, string[] userNames, string[] passwords, string[] roles, int userCount)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (userName == userNames[i] && password == passwords[i])
                {
                    return roles[i];
                }
            }
            return "undefined";
        }
        static string GetField(string record, int field)
        {
            int commaCount = 0;
            string item = "";

            foreach (char c in record)
            {
                if (c == ',')
                {
                    commaCount++;
                }
                else if (commaCount == field - 1)
                {
                    item += c;
                }
            }

            return item;
        }
        static void StoreData(string[] userNames, string[] passwords, string[] roles,ref int userCount)
        {
            try
            {
                using (StreamWriter userFile = new StreamWriter("myName.txt"))
                {
                    for (int i = 0; i < userCount; i++)
                    {
                        userFile.WriteLine($"{userNames[i]},{passwords[i]},{roles[i]}");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: Unable to open the user data file for writing. {e.Message}");
            }
        }

        // Function to read user data from a file into arrays
        static void ReadData(string[] userNames, string[] passwords, string[] roles, ref int userCount)
{
    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    string filePath = Path.Combine(desktopPath, "myName.txt");

    string record;
    try
    {
        using (StreamReader data = new StreamReader(filePath))
        {
            // Read each line from the file and extract fields into arrays
            while ((record = data.ReadLine()) != null)
            {
                userNames[userCount] = GetField(record, 1);
                passwords[userCount] = GetField(record, 2);
                roles[userCount] = GetField(record, 3);
                userCount++;

                // Check for the maximum limit of records
                if (userCount >= 100)
                {
                    Console.WriteLine("Error: Too many records in the file.");
                    break;
                }
            }
        }
    }
    catch (IOException e)
    {
        Console.WriteLine($"Error reading data from file: {e.Message}");
    }
}

        // Function to store user data into a file
        

        static bool IsValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            foreach (char c in username)
            {
                if (!char.IsLower(c) || !char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsValidPassword(string password)
        {
            // Add your password validation logic here
            return !string.IsNullOrEmpty(password);
        }

        static bool IsValidRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return false;
            }

            string lowercaseRole = role.ToLower();
            return lowercaseRole == "customer" || lowercaseRole == "manager";
        }

        static void SignUpHeader()
        {
            // Add your implementation for SignUpHeader
            Console.WriteLine("=== SignUp Header ===");
        }

        static bool SignUp(string userName, string password, string role, string[] userNames, string[] passwords, string[] roles, ref int userCount, int arraySize)
        {
            bool isPresent = false;

            // Check if the user already exists
            for (int i = 0; i < userCount; i++)
            {
                if (userNames[i] == userName && passwords[i] == password && roles[i] == role)
                {
                    isPresent = true;
                    break;
                }
            }

            if (isPresent)
            {
                return false;
            }
            else if (userCount < 100)
            {
                userNames[userCount] = userName;
                passwords[userCount] = password;
                roles[userCount] = role;
                StoreData(userNames, passwords, roles, ref userCount);
                userCount++;
                StoreData(userNames, passwords, roles,ref userCount);
                return true;
            }
            else
            {
                return false;
            }
        }
        static void LoadRestaurantInformationFromFile(ref string newLocation, ref string newContact, ref string newOpeningHours)
        {
            // Implement LoadRestaurantInformationFromFile logic in C#
            try
            {
                string filePath = "restaurant_info.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    newLocation = reader.ReadLine();
                    newContact = reader.ReadLine();
                    newOpeningHours = reader.ReadLine();
                }

                Console.WriteLine("Restaurant information loaded successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error loading restaurant information: {e.Message}");
            }
        }

        static void ChangeRestaurantInformation(ref string newLocation, ref string newContact, ref string newOpeningHours)
        {
            // Implement ChangeRestaurantInformation logic in C#
            Console.Write("Enter new location: ");
            newLocation = Console.ReadLine();

            Console.Write("Enter new contact information: ");
            newContact = Console.ReadLine();

            Console.Write("Enter new opening hours: ");
            newOpeningHours = Console.ReadLine();

            Console.WriteLine("Restaurant information changed successfully.");
        }

        static void SaveRestaurantInformationToFile(string newLocation, string newContact, string newOpeningHours)
        {
            // Implement SaveRestaurantInformationToFile logic in C#
            try
            {
                string filePath = "restaurant_info.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(newLocation);
                    writer.WriteLine(newContact);
                    writer.WriteLine(newOpeningHours);
                }

                Console.WriteLine("Restaurant information saved to file successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error saving restaurant information: {e.Message}");
            }
        }

        static void DisplayAllFeedback()
        {
            // Implement DisplayAllFeedback logic in C#
            try
            {
                string filePath = "feedback.txt";
                if (File.Exists(filePath))
                {
                    string[] feedbackLines = File.ReadAllLines(filePath);

                    if (feedbackLines.Length > 0)
                    {
                        Console.WriteLine("All Feedback:");
                        foreach (string line in feedbackLines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No feedback available.");
                    }
                }
                else
                {
                    Console.WriteLine("No feedback file found.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error displaying feedback: {e.Message}");
            }
        }
    
    static void ManagerInterface(string[] userNames, string[] passwords, string[] roles,
                                  string[] staffNames, string[] staffRoles, ref int staffCount,
                                  string[] menuNames, float[] menuPrices, int MAX_MENU_ITEMS,
                                  string[] orderNames, float[] orderPrices, string[] orderDates, ref int itemCount,
                                  int MaxStaffCount, ref string newLocation, ref string newContact, ref string newOpeningHours,
                                  int menuCount, ref string name, ref string address, ref string phoneNo, ref string cnic,
                                  ref string dateOfJoining, ref int experience, ref string education, ref double salary,
                                  int quantity, int itemIndex, ref string targetName, ref int menuIndex)
        {
            int managerOption = 0;

            while (managerOption != 10)
            {
                managerOption = ManagerMenu();
                if (managerOption == 1)
                {
                    Console.Clear();
                    DisplayMenu1(MAX_MENU_ITEMS, menuNames, menuPrices);
                    AddMenuItem(menuNames, menuPrices, ref menuCount, MAX_MENU_ITEMS);
                    Console.Clear();
                }
                else if (managerOption == 2)
                {
                    Console.Clear();
                    RemoveMenuItem(menuNames, menuPrices, ref menuCount, MAX_MENU_ITEMS);
                    Console.Clear();
                }
                else if (managerOption == 3)
                {
                    Console.Clear();
                    UpdateMenuPrices(menuCount, menuNames, menuPrices, ref menuIndex, MAX_MENU_ITEMS);
                    Console.Clear();
                }
                else if (managerOption == 4)
                {
                    Console.Clear();
                    DisplayMenu2(menuNames, menuPrices, menuCount, MAX_MENU_ITEMS);
                    Console.Clear();
                }
                else if (managerOption == 5)
                {
                    Console.Clear();
                    AddStaff(ref name, ref address, ref phoneNo, ref cnic, ref dateOfJoining, ref experience, ref education, ref salary);
                    DisplayStaff(name, address, phoneNo, cnic, dateOfJoining, experience, education, salary);
                    SaveStaffToFile(name, address, phoneNo, cnic, dateOfJoining, experience, education, salary);
                    Console.Clear();
                }
                else if (managerOption == 6)
                {
                    SaveStaffToFile(name, address, phoneNo, cnic, dateOfJoining, experience, education, salary);
                    Console.Write("Enter the name of the staff member to remove: ");
                    targetName = Console.ReadLine();
                    RemoveStaff(targetName);
                }
                else if (managerOption == 7)
                {
                    Console.Clear();
                    // Function to display staff member information
                    DisplayStaff(name, address, phoneNo, cnic, dateOfJoining, experience, education, salary);
                    Console.Clear();
                }
                else if (managerOption == 8)
                {
                    Console.Clear();
                    DisplaySeparator("LOADING RESTAURANT INFORMATION");
                    LoadRestaurantInformationFromFile(ref newLocation, ref newContact, ref newOpeningHours);
                    DisplaySeparator("CHANGING RESTAURANT INFORMATION");
                    ChangeRestaurantInformation(ref newLocation, ref newContact, ref newOpeningHours);
                    DisplaySeparator("SAVING RESTAURANT INFORMATION");
                    SaveRestaurantInformationToFile(newLocation, newContact, newOpeningHours);
                    Console.Clear();
                }
                else if (managerOption == 9)
                {
                    Console.Clear();
                    DisplaySeparator("DISPLAYING ALL FEEDBACK");
                    DisplayAllFeedback();
                    Console.Clear();
                }
                else if (managerOption == 10)
                {
                    Console.Clear();
                    Environment.Exit(0);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // Function to validate customer menu options
        static int ValidateCustomerOption()
        {
            int option;
            while (true)
            {
                Console.Write("Your Option: ");
                if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 9)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                }
            }
            return option;
        }

        // Placeholder methods to replace C++ specific functions
        static int ManagerMenu()
        {
            Console.WriteLine("Manager Menu");
            Console.WriteLine("1. Add Menu Item");
            Console.WriteLine("2. Remove Menu Item");
            Console.WriteLine("3. Update Menu Prices");
            Console.WriteLine("4. Display Menu");
            Console.WriteLine("5. Add Staff");
            Console.WriteLine("6. Remove Staff");
            Console.WriteLine("7. Display Staff");
            Console.WriteLine("8. Change Restaurant Information");
            Console.WriteLine("9. Display All Feedback");
            Console.WriteLine("10. Exit");

            int option;
            while (true)
            {
                Console.Write("Your Option: ");
                if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
                }
            }

            return option;
        }

        static void DisplayMenu1(int maxMenuItems, string[] menuNames, float[] menuPrices)
        {
            Console.WriteLine("Menu 1:");
            for (int i = 0; i < maxMenuItems; i++)
            {
                Console.WriteLine($"{menuNames[i]} - ${menuPrices[i]:F2}");
            }
        }

        static void AddMenuItem(string[] menuNames, float[] menuPrices, ref int menuCount, int maxMenuItems)
        {
            if (menuCount < maxMenuItems)
            {
                Console.Write("Enter new menu item name: ");
                string newItemName = Console.ReadLine();

                Console.Write("Enter new menu item price: ");
                if (float.TryParse(Console.ReadLine(), out float newItemPrice))
                {
                    menuNames[menuCount] = newItemName;
                    menuPrices[menuCount] = newItemPrice;
                    menuCount++;

                    Console.WriteLine("Menu item added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid price input. Menu item not added.");
                }
            }
            else
            {
                Console.WriteLine("Menu is full. Cannot add more items.");
            }
        }

        static void RemoveMenuItem(string[] menuNames, float[] menuPrices, ref int menuCount, int maxMenuItems)
        {
            Console.Write("Enter the index of the menu item to remove: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < menuCount)
            {
                for (int i = index; i < menuCount - 1; i++)
                {
                    menuNames[i] = menuNames[i + 1];
                    menuPrices[i] = menuPrices[i + 1];
                }

                menuCount--;

                Console.WriteLine("Menu item removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index. Menu item not removed.");
            }
        }

        static void UpdateMenuPrices(int menuCount, string[] menuNames, float[] menuPrices, ref int menuIndex, int maxMenuItems)
        {
            Console.Write("Enter the index of the menu item to update prices: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < menuCount)
            {
                menuIndex = index;

                Console.Write($"Enter new price for {menuNames[index]}: ");
                if (float.TryParse(Console.ReadLine(), out float newPrice))
                {
                    menuPrices[index] = newPrice;

                    Console.WriteLine("Menu item price updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid price input. Menu item price not updated.");
                }
            }
            else
            {
                Console.WriteLine("Invalid index. Cannot update prices.");
            }
        }

        static void DisplayMenu2(string[] menuNames, float[] menuPrices, int menuCount, int maxMenuItems)
        {
            Console.WriteLine("Menu 2:");
            for (int i = 0; i < menuCount; i++)
            {
                Console.WriteLine($"{menuNames[i]} - ${menuPrices[i]:F2}");
            }
        }

        static void AddStaff(ref string name, ref string address, ref string phoneNo, ref string cnic,
                              ref string dateOfJoining, ref int experience, ref string education, ref double salary)
        {
            Console.Write("Enter new staff member's name: ");
            name = Console.ReadLine();

            Console.Write("Enter new staff member's address: ");
            address = Console.ReadLine();

            Console.Write("Enter new staff member's phone number: ");
            phoneNo = Console.ReadLine();

            Console.Write("Enter new staff member's CNIC: ");
            cnic = Console.ReadLine();

            Console.Write("Enter new staff member's date of joining: ");
            dateOfJoining = Console.ReadLine();

            Console.Write("Enter new staff member's experience: ");
            if (int.TryParse(Console.ReadLine(), out int newExperience))
            {
                experience = newExperience;
            }

            Console.Write("Enter new staff member's education: ");
            education = Console.ReadLine();

            Console.Write("Enter new staff member's salary: ");
            if (double.TryParse(Console.ReadLine(), out double newSalary))
            {
                salary = newSalary;
            }

            Console.WriteLine("Staff member added successfully.");
        }

        static void DisplayStaff(string name, string address, string phoneNo, string cnic,
                                  string dateOfJoining, int experience, string education, double salary)
        {
            Console.WriteLine($"Staff Member Information:");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Phone Number: {phoneNo}");
            Console.WriteLine($"CNIC: {cnic}");
            Console.WriteLine($"Date of Joining: {dateOfJoining}");
            Console.WriteLine($"Experience: {experience} years");
            Console.WriteLine($"Education: {education}");
            Console.WriteLine($"Salary: ${salary:F2}");
        }

        static void SaveStaffToFile(string name, string address, string phoneNo, string cnic,
                                     string dateOfJoining, int experience, string education, double salary)
        {
            using (StreamWriter writer = new StreamWriter("staff.txt", true))
            {
                writer.WriteLine($"{name},{address},{phoneNo},{cnic},{dateOfJoining},{experience},{education},{salary}");
            }
        }

        static void RemoveStaff(string targetName)
        {
            string[] lines = File.ReadAllLines("staff.txt");
            bool found = false;

            using (StreamWriter writer = new StreamWriter("staff.txt"))
            {
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    string staffName = parts[0];

                    if (staffName != targetName)
                    {
                        writer.WriteLine(line);
                    }
                    else
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                Console.WriteLine($"Staff member '{targetName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Staff member '{targetName}' not found.");
            }
        }


          
        static void ManagerInterface()
        {
            // Add your implementation for ManagerInterface
            Console.WriteLine("=== Manager Interface ===");
        }

        static void CustomerInterface()
        {

            // Add your implementation for CustomerInterface
            Console.WriteLine("=== Customer Interface ===");
        }

        static int LoginMenu()
        {
            // Add your logic for displaying login menu and getting user input
            Console.WriteLine("1. Sign In");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            int option = 0;
            int userCount = 0,arraySize=100;
            string[] userNames = new string[100];
            string[] passwords = new string[100];
            string[] roles = new string[100];
            string userName, password, role;
            int maxMenuItems = 10; // Assuming a maximum of 10 menu items
            string[] menuNames = new string[maxMenuItems];
            float[] menuPrices = new float[maxMenuItems];
            int menuCount = 0;
            int menuIndex = 0;

            int maxStaffCount = 100; // Assuming a maximum of 100 staff members
            string[] staffNames = new string[maxStaffCount];
            string[] staffRoles = new string[maxStaffCount];
            int staffCount = 0;

            

            string[] orderNames = new string[100]; // Assuming a maximum of 100 orders
            float[] orderPrices = new float[100];
            string[] orderDates = new string[100];
            int itemCount = 0;

            string newLocation = "Initial Location";
            string newContact = "Initial Contact";
            string newOpeningHours = "Initial Opening Hours";

            string name = "", address = "", phoneNo = "", cnic = "", dateOfJoining = "", education = "";
            int experience = 0;
            double salary = 0.0;

            int quantity = 0, itemIndex = 0;
            string targetName = "";

            ReadData(userNames, passwords, roles, ref userCount);
            while (option != 3)
            {
                // Display login menu
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("*************************************************");
                Console.WriteLine("**                                             **");
                Console.WriteLine("**      Welcome to the Exquisite Restaurant!   **");
                Console.WriteLine("**      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~       **");
                Console.WriteLine("**      |  __  __  __  __  __  __  __  |       **");
                Console.WriteLine("**      | |\\ ||\\ ||\\ ||\\ ||\\ ||\\ ||\\ | |       **");
                Console.WriteLine("**      | | \\|| \\|| \\|| \\|| \\|| \\|| \\|| |      **");
                Console.WriteLine("**      |______________________________|       **");
                Console.WriteLine("**                                             **");
                Console.WriteLine("*************************************************");
                Console.ResetColor();

                Console.WriteLine("\n🌟 Indulge in the culinary delights of our exquisite menu. 🍽️");
                Console.WriteLine("✨ Experience the perfect blend of taste and ambiance. ✨\n");

                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                SubMenuBeforeMainMenu("Login");

                // Get user's choice from the login menu
                option = LoginMenu();

                if (option == 2)
                {
                    // User chooses to sign up
                    Console.Clear();
                    SubMenuBeforeMainMenu("SignUp");
                    SignUpHeader();

                    while (true)
                    {
                        Console.Write("Enter a username (lowercase letters only): ");
                        userName = Console.ReadLine();

                        // Validate the entered username
                        if (IsValidUsername(userName))
                        {
                            break;
                        }

                        Console.WriteLine("Invalid username. Please use lowercase letters only.");
                    }

                    while (true)
                    {
                        Console.Write("Enter a password: ");
                        password = Console.ReadLine();

                        // Validate the entered password
                        if (IsValidPassword(password))
                        {
                            break;
                        }

                        Console.WriteLine("Invalid password. Please try again.");
                    }

                    while (true)
                    {
                        Console.Write("Enter the role (customer or manager): ");
                        role = Console.ReadLine();

                        // Validate the entered role
                        if (IsValidRole(role))
                        {
                            break;
                        }

                        Console.WriteLine("Invalid role. Please enter 'customer' or 'manager'.");
                    }

                    // Attempt to sign up with the provided information
                    bool result = SignUp(userName, password, role, userNames, passwords, roles, ref userCount, arraySize);
                    if (result)
                    {
                        Console.WriteLine("Congratulations, your account has been successfully created.");
                    }
                    else
                    {
                        Console.WriteLine("User with the same credentials already exists. Please try again.");
                    }

                    Console.Clear();
                }
                else if (option == 1)
                {
                    // User chooses to sign in
                    Console.Clear();
                    Console.Write("Enter a user Name: ");
                    userName = Console.ReadLine();
                    Console.Write("Enter a password: ");
                    password = Console.ReadLine();

                    // Attempt to sign in with the provided information
                 string userRole = SignIn(userName, password, userNames, passwords, roles, userCount);
userRole = userRole.ToLower();


                    if (userRole == "manager")
                    {
                        // Manager interface
                        ManagerInterface(userNames, passwords, roles,
                          staffNames, staffRoles, ref staffCount,
                          menuNames, menuPrices, maxMenuItems,
                          orderNames, orderPrices, orderDates, ref itemCount,
                          maxStaffCount, ref newLocation, ref newContact, ref newOpeningHours,
                          menuCount, ref name, ref address, ref phoneNo, ref cnic,
                          ref dateOfJoining, ref experience, ref education, ref salary,
                          quantity, itemIndex, ref targetName, ref menuIndex);
                        Console.Clear();
                    }
                    else if (userRole == "customer")
                    {
                        // Customer interface
                        CustomerInterface();
                        Console.Clear();
                    }
                    else
                    {
                        // Invalid credentials
                        Console.WriteLine("Invalid credentials. User not found or incorrect password.");
                        Console.Clear();
                    }
                }
                else if (option == 3)
                {
                    // User chooses to exit
                    Console.WriteLine("*********************************");
                    Console.WriteLine("*                                *");
                    Console.WriteLine("* Farewell, until we meet again! *");
                    Console.WriteLine("*                                *");
                    Console.WriteLine("*********************************");
                    Environment.Exit(0);
                }
            }
        }
    }
}