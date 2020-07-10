using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MoximityHotel
{
    class Program
    {
        public static List<Guest> guestRoster;

        static void Main(string[] args)
        {

            bool runningProgram = true;
            while(runningProgram)
            {
                string xmlGuestRoster = @"C:\Users\abledsoe1\OneDrive - Knex\^Personal iDrive\Documents\Coding Projects\MoximityHotel\MoximityHotel\GuestRoster.xml";

                Console.WriteLine("What would you like to do?");
                PrintMenuOptions();
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch(choice)
                {
                    case 1:
                        #region Add A Guest
                        bool addingGuests = true;
                        while(addingGuests)
                        {
                            Guest newGuest = GetGuestDetails();
                            AddGuestToRoster(xmlGuestRoster, newGuest);
                            Console.WriteLine("You have successfully added this guest!");
                            Console.WriteLine("");
                            Console.WriteLine("Would you like to add another?");
                            string decision = Console.ReadLine().ToLower();
                            if(decision == "y")
                            {
                                addingGuests = true;
                            }
                            else
                            {
                                addingGuests = false;
                                break;
                            }
                        }
                        #endregion
                        Console.Clear();
                        continue;
                    case 2:
                        #region Display Selected Guest Information
                        Console.Write("Enter the GuestID number you wish to display info for: ");
                        int number = int.Parse(Console.ReadLine());
                        //Find the guest you are looking for on the roster using the ID number in XElement form
                        var guestInList = GetXElementGuest(xmlGuestRoster, number);
                        //Deserialize the XElement into a Guest object
                        Guest guestToDisplay = DeSerializer(guestInList);
                        //Print out the information for the guest object to the screen in our ToString format we created
                        Console.WriteLine(guestToDisplay.ToString(guestToDisplay));
                        Console.WriteLine("");
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                        #endregion
                        Console.Clear();
                        continue;
                    case 3:
                        #region Remove A Guest
                        #endregion
                        Console.Clear();
                        continue;
                    case 4:
                        runningProgram = false;
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("You are successfully logged out. Thank you for using our program!");
            Console.WriteLine("Have a great day!");
            Environment.Exit(0);
        }
        public static void PrintMenuOptions()
        {
            string[] options = new string[] {
                "Add A New Guest",
                "Display Guest Information",
                "Remove a Guest",
                "Close The Program"
            };
            for (int i = options.GetLowerBound(0); i <= options.GetUpperBound(0); i++)
                Console.WriteLine("   {0,2}: {1}", i+1, options[i]);
        }
        public static Guest GetGuestDetails()
        {
            Guest newGuest = new Guest();

            Console.Write("Enter Guest ID: ");
            int guestID = int.Parse(Console.ReadLine());
            Console.Write("Enter Guest First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Guest Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Guest Phone Number: ");
            long phoneNumber = long.Parse(Console.ReadLine());

            newGuest.GuestID = guestID;
            newGuest.FirstName = firstName;
            newGuest.LastName = lastName;
            newGuest.PhoneNumber = phoneNumber;
            newGuest.CheckedInStatus = false;

            return newGuest;
        }
        public static void AddGuestToRoster(string xmlGuestRoster, Guest newGuest)
        {
            var xmlDocument = new XDocument();
            if (!File.Exists(xmlGuestRoster))
            {
                xmlDocument = new XDocument(
                    new XElement("MoximityGuests",
                        new XElement("Guest", new XAttribute("Id", newGuest.GuestID),
                            new XElement("FirstName", newGuest.FirstName),
                            new XElement("LastName", newGuest.LastName),
                            new XElement("PhoneNumber", newGuest.PhoneNumber),
                            new XElement("CheckedInStatus", newGuest.CheckedInStatus)
                    )));
                xmlDocument.Save(xmlGuestRoster);
            }
            else
            {
                xmlDocument = XDocument.Load(xmlGuestRoster);
                var guests = xmlDocument.Descendants("MoximityGuests").FirstOrDefault();
                guests.Add(new XElement("Guest", new XAttribute("Id", newGuest.GuestID),
                    new XElement("FirstName", newGuest.FirstName),
                    new XElement("LastName", newGuest.LastName),
                    new XElement("PhoneNumber", newGuest.PhoneNumber),
                    new XElement("CheckedInStatus", newGuest.CheckedInStatus)
                    ));
                xmlDocument.Save(xmlGuestRoster);
            }
        }
        public static XElement GetXElementGuest(string xmlGuestRoster, int number)
        {
            XElement root = XElement.Load(xmlGuestRoster);
            IEnumerable<XElement> guests =
                from guest in root.Elements("Guest")
                where (int)guest.Attribute("Id") == number
                select guest;

            foreach (XElement guest in guests)
            {
                return guest;
            }
            throw new Exception();
        }
        static Guest DeSerializer(XElement element)
        {
            var serializer = new XmlSerializer(typeof(Guest));
            return (Guest)serializer.Deserialize(element.CreateReader());
        }
    }
}
