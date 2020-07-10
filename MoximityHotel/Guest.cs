using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MoximityHotel
{
    public class Guest
    {
        [XmlAttribute("Id")]
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public bool CheckedInStatus { get; set; }

        public Guest(int guestID, string firstName, string lastName, long phoneNumber, bool checkedInStatus)
        {
            this.GuestID = guestID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            CheckedInStatus = false;
        }

        public Guest()
        {

        }

        public override string ToString()
        {
            Guest guest = new Guest();
            return string.Format(" Guest ID: {0} \n Guest First Name: {1} \n Guest Last Name: {2} \n Guest Phone: {3} \n Checked In: {4}", $"{guest.GuestID}", $"{guest.FirstName}", $"{guest.LastName}", $"{guest.PhoneNumber}", $"{guest.CheckedInStatus}");
        }

        public string ToString(Guest guest)
        {
            return string.Format(" Guest ID: {0} \n Guest First Name: {1} \n Guest Last Name: {2} \n Guest Phone: {3} \n Checked In: {4}", $"{guest.GuestID}", $"{guest.FirstName}", $"{guest.LastName}", $"{guest.PhoneNumber}", $"{guest.CheckedInStatus}");
        }

        public void Save()
        {
            string xmlGuestRoster = @"C:\Users\abledsoe1\OneDrive - Knex\^Personal iDrive\Documents\Coding Projects\MoximityHotel\MoximityHotel\GuestRoster.xml";
            using (var stream = new FileStream(xmlGuestRoster, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(Guest));
                serializer.Serialize(stream, this);
            }
        }

        public Guest Load()
        {
            string xmlGuestRoster = @"C:\Users\abledsoe1\OneDrive - Knex\^Personal iDrive\Documents\Coding Projects\MoximityHotel\MoximityHotel\GuestRoster.xml";

            using (var stream = new FileStream(xmlGuestRoster, FileMode.Open))
            {
                var deserializer = new XmlSerializer(typeof(Guest));
                return (Guest)deserializer.Deserialize(stream);
            }
        }
    }
}
