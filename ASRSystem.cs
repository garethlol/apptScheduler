using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WDT_A1_s3252820
{
    class ASRSystem
    {
        List<Person> people = new List<Person>();
        List<Slot> slots = new List<Slot>();

        public List<Person> People
        {
            get
            {
                return people;
            }
        }

        public List<Slot> Slots
        {
            get
            {
                return slots;
            }
        }

        public Slot getSlot(string room, string startTime, string endTime, string staffID)
        {
            foreach (Slot s in slots)
            {
                if ((s.Room == room) && (s.StartTime == startTime) && (s.EndTime == endTime) && (s.StaffID == staffID))
                {
                    return s;
                }
            }

            Console.WriteLine("Slot not found\n");
            return null;
        }

        public void init()
        {
            // Loading of users.txt + Validation of it occurs here

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("users.txt");

                while ((line = file.ReadLine()) != null)
                {
                    // Console.WriteLine(line);

                    string[] split = line.Split(new Char[] { ',' });

                    int iterator = 0;

                    string staffPattern = "^e\\d{7}$";
                    string studentPattern = "^s\\d{7}$";

                    string tempName = "";
                    string tempID = "";
                    string tempEmail = "";
                    bool staffFlag = false;

                    foreach (string s in split)
                    {
                        if (s.Trim() != "")
                        {
                            // Console.WriteLine(s);
                            if (iterator == 0)
                            {
                                tempName = s;
                            }

                            if (iterator == 1)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                // Appending the appropriate email address to the end to compare with the actual email
                                if (System.Text.RegularExpressions.Regex.IsMatch(s, staffPattern) == true)
                                {
                                    tempID = s;
                                    sb.Append(tempID);
                                    sb.Append("@ems.rmit.edu.au");
                                    tempEmail = sb.ToString();
                                    staffFlag = true;
                                }
                                else if (System.Text.RegularExpressions.Regex.IsMatch(s, studentPattern) == true)
                                {
                                    tempID = s;
                                    sb.Append(tempID);
                                    sb.Append("@student.rmit.edu.au");
                                    tempEmail = sb.ToString();
                                }
                                else
                                {

                                }
                            }

                            if (iterator == 2)
                            {

                                // It will check if the tempEmail that was created in the previous statement 
                                // is similar to the provided email in the text file, if not, means invalid data
                                if (tempEmail != s)
                                {
                                    Console.WriteLine("Invalid data found in file, exiting program\n");
                                    Environment.Exit(0);
                                }

                                if (staffFlag == false)
                                {
                                    People.Add(new Student(tempName, tempID, tempEmail));
                                }
                                else
                                {
                                    People.Add(new Staff(tempName, tempID, tempEmail));
                                }

                            }
                            iterator++;
                        }
                    }

                    counter++;
                }

                file.Close();


                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("--------------------------------------------------------\n");

                menuStart();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The file: \"users.txt\" was not found");
                Console.WriteLine("Please check if that file exists\n");
                Console.WriteLine("\nExiting Program...");
                Environment.Exit(0);
            }
        }

        public bool menuStart()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Main Menu:");

            Console.WriteLine("\t1. List Rooms");
            Console.WriteLine("\t2. List Slots");
            Console.WriteLine("\t3. Staff Menu");
            Console.WriteLine("\t4. Student Menu");
            Console.WriteLine("\t5. Exit\n");

            Console.Write("Enter Option: ");

            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
            {
                Console.WriteLine("Invalid input, please enter option from 1 to 5\n");
                Console.Write("Enter Option: ");
                choice = Console.ReadLine();
            }

            Console.WriteLine("Your choice was {0}\n", choice);

            switch(choice)       
            {         
             case "1":   
                listRooms();
                break;                  
             case "2":            
                listSlots();
                break;          
             case "3":            
                staffMenu();
                break;          
             case "4":            
                studentMenu();          
                break;    
             case "5":
                Console.WriteLine("Terminating ASR, have a nice day");
                Environment.Exit(0);
                return true;
            }

            menuStart();
            return false;

        }

        public void listRooms()
        {
            /* Hard coded the rooms since there are only 4 rooms 
            ** at all times for the purpose of this assignment
            */

            Console.WriteLine("-------------");
            Console.WriteLine("1. List Rooms");
            Console.WriteLine("-------------\n");

            Console.WriteLine("Existing Rooms:");
            Console.WriteLine("\tRoom Name");

            Console.WriteLine("\tA");
            Console.WriteLine("\tB");
            Console.WriteLine("\tC");
            Console.WriteLine("\tD");
        }

        public void listSlots()
        {
            Console.WriteLine("-------------");
            Console.WriteLine("2. List Slots");
            Console.WriteLine("-------------\n");

            Console.WriteLine("\nExisting Slots");
            Console.Write("Enter date for slots (dd-mm-yyyy): ");

            string datePattern = "^\\d{2}-\\d{2}-\\d{4}$";

            string choice = Console.ReadLine();

            while (System.Text.RegularExpressions.Regex.IsMatch(choice, datePattern) == false)
            {
                System.Console.WriteLine("Date entered is not valid, please re-enter\n");
                Console.Write("Enter date for slots (dd-mm-yyyy): ");
                choice = Console.ReadLine();
            }

            Console.WriteLine("\nExisting slots on {0}:\n", choice);
            Console.WriteLine("\tRoom\tStart\tEnd\tStaff ID\tBookings");

            // If there are no slots, print <No Existing Slots>

            if (slots.Count() == 0)
            {
                Console.WriteLine("\n\t<No Existing Slots>");
            }
            else
            {
                foreach (Slot s in slots)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", s.Room, s.StartTime, s.EndTime, s.StaffID, s.StudentID);
                }
            }

            Console.WriteLine();
        }

        // List all users method for testing purposes
        public void listAllUsers()
        {
            foreach (Person p in people)
            {
                Console.WriteLine("Name: {0}\nPersonNumber: {1}\nPerson Email: {2}\n", p.Name, p.ID, p.Email);
            }

        }

        public bool staffMenu()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Staff Menu:");

            Console.WriteLine("\t1. List Staff");
            Console.WriteLine("\t2. Room Availability");
            Console.WriteLine("\t3. Create Slot");
            Console.WriteLine("\t4. Remove Slot");
            Console.WriteLine("\t5. Exit\n");

            Console.Write("Enter Option: ");

            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
            {
                Console.WriteLine("Invalid input, please enter option from 1 to 5\n");
                Console.Write("Enter Option: ");
                choice = Console.ReadLine();
            }

            Console.WriteLine("Your choice was {0}\n", choice);

            if (choice == "1")
            {
                listAllStaff();
            }
            else if (choice == "2")
            {
                roomAvailability();
            }
            else if (choice == "3")
            {
                createSlot();
            }
            else if (choice == "4")
            {
                removeSlot();
            }
            else if (choice == "5")
            {
                Console.WriteLine("Returning to the main menu");
                menuStart();
                return true;
            }

            staffMenu();
            return false;

        }

        public void listAllStaff()
        {

            Console.WriteLine("-------------");
            Console.WriteLine("1. List Staff");
            Console.WriteLine("-------------\n");

            try
            {
                int counter = 0;
                foreach (Person p in people)
                {
                    string staffPattern = "^e\\d{7}$";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                   
                    // Check if starts with e, if matches the first expression, then its a staff 
                    if (System.Text.RegularExpressions.Regex.IsMatch(p.ID, staffPattern) == true)
                    {
                        if (counter == 0)
                        {
                            Console.WriteLine("\tName\t\tID\t\tEmail");
                            Console.WriteLine("\t----\t\t--\t\t-----");
                            counter++;
                        }
                        
                        Console.WriteLine("\t{0}\t\t{1}\t{2}", p.Name, p.ID, p.Email);
                    }

                }
                Console.WriteLine();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine();
            }
        }

        public void roomAvailability()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("2. Room Availability");
            Console.WriteLine("--------------------\n");

            Console.WriteLine("\tRoom\tStart\tEnd\tStaff");
            foreach (Slot s in slots)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}",s.Room, s.StartTime, s.EndTime, s.StaffID);
            }

            Console.WriteLine();

        }

        public bool createSlot()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("3. Create Slot");
            Console.WriteLine("--------------\n");

            Console.Write("Enter date for slot (dd-mm-yyyy): ");

            string datePattern = "^\\d{2}-\\d{2}-\\d{4}$";
            string dateChoice = Console.ReadLine();

            while (System.Text.RegularExpressions.Regex.IsMatch(dateChoice, datePattern) == false)
            {
                System.Console.WriteLine("Date entered is not valid, please re-enter\n");
                Console.Write("Enter date for slot (dd-mm-yyyy): ");
                dateChoice = Console.ReadLine();
            }

            Console.Write("Enter time for slot (hh:mm): ");

            string timePattern = "^\\d{2}:\\d{2}$";
            string timeChoice = Console.ReadLine();

            while (System.Text.RegularExpressions.Regex.IsMatch(timeChoice, timePattern) == false)
            {
                System.Console.WriteLine("Time entered is not valid, please re-enter\n");
                Console.Write("Enter time for slot (hh:mm): ");
                timeChoice = Console.ReadLine();
            }

            string[] time = timeChoice.Split(':');
            int endTime = Convert.ToInt16(time[0]);

            endTime = endTime + 1;
            string endTimeString = Convert.ToString(endTime);

            
            if (System.Text.RegularExpressions.Regex.IsMatch(endTimeString, timePattern) == false)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(endTimeString);
                sb.Append(":00");

                endTimeString = sb.ToString();
            }

            Console.Write("Enter Staff ID: ");

            string staffPattern = "^e\\d{7}$";
            string staffChoice = Console.ReadLine();

            while ((System.Text.RegularExpressions.Regex.IsMatch(staffChoice, staffPattern) == false) || staffCheck(staffChoice) == false)
            {

                System.Console.WriteLine("Staff name entered is invalid/doesn't exist, please re-enter\n");
                Console.Write("Enter Staff ID: ");
                staffChoice = Console.ReadLine();
            }

            Console.Write("Enter Room Name: ");

            string roomChoice = Console.ReadLine();

            while (roomChoice != "A" && roomChoice != "B" && roomChoice != "C" && roomChoice != "D")
            {
                System.Console.WriteLine("Please enter a room name (A to D): \n");
                Console.Write("Enter Room Name: ");
                roomChoice = Console.ReadLine();
            }

            // Check if slot has already been booked by another student
            foreach (Slot s in slots)
            {
                if ((s.Room == roomChoice) && (s.StartTime == timeChoice) && (s.EndTime == endTimeString))
                {
                    Console.WriteLine("Slot has already been utilised, please pick another time\n");
                    return false;
                }
            }


            slots.Add(new Slot(roomChoice, timeChoice, endTimeString, staffChoice));
            Console.WriteLine("Slot created successfully!\n");
            return true;
        }

        public bool removeSlot()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("4. Remove Slot");
            Console.WriteLine("--------------\n");

            Console.Write("Enter date for slot to be removed (dd-mm-yyyy): ");

            string datePattern = "^\\d{2}-\\d{2}-\\d{4}$";
            string dateChoice = Console.ReadLine();

            while (System.Text.RegularExpressions.Regex.IsMatch(dateChoice, datePattern) == false)
            {
                System.Console.WriteLine("Date entered is not valid, please re-enter\n");
                Console.Write("Enter date for slot (dd-mm-yyyy): ");
                dateChoice = Console.ReadLine();
            }

            Console.Write("Enter Start time for slot (hh:mm): ");

            string timePattern = "^\\d{2}:\\d{2}$";
            string timeChoice = Console.ReadLine();

            while (System.Text.RegularExpressions.Regex.IsMatch(timeChoice, timePattern) == false)
            {
                System.Console.WriteLine("Time entered is not valid, please re-enter\n");
                Console.Write("Enter time for slot (hh:mm): ");
                timeChoice = Console.ReadLine();
            }

            string[] time = timeChoice.Split(':');
            int endTime = Convert.ToInt16(time[0]);

            endTime = endTime + 1;
            string endTimeString = Convert.ToString(endTime);


            if (System.Text.RegularExpressions.Regex.IsMatch(endTimeString, timePattern) == false)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(endTimeString);
                sb.Append(":00");
                endTimeString = sb.ToString();
            }

            Console.Write("Enter Staff ID: ");

            string staffPattern = "^e\\d{7}$";
            string staffChoice = Console.ReadLine();

            while ((System.Text.RegularExpressions.Regex.IsMatch(staffChoice, staffPattern) == false) || staffCheck(staffChoice) == false)
            {

                System.Console.WriteLine("Staff name entered is invalid/doesn't exist, please re-enter\n");
                Console.Write("Enter Staff ID: ");
                staffChoice = Console.ReadLine();
            }

            Console.Write("Enter Room Name: ");

            string roomChoice = Console.ReadLine();

            while (roomChoice != "A" && roomChoice != "B" && roomChoice != "C" && roomChoice != "D")
            {
                System.Console.WriteLine("Please enter a room name (A to D): \n");
                Console.Write("Enter Room Name: ");
                roomChoice = Console.ReadLine();
            }

            // Check if slot exists
            foreach (Slot s in slots)
            {
                if ((s.Room == roomChoice) && (s.StartTime == timeChoice) && (s.EndTime == endTimeString) && (s.StaffID == staffChoice))
                {
                    slots.Remove(getSlot(roomChoice, timeChoice, endTimeString, staffChoice));
                    Console.WriteLine("Slot successfully removed!\n");
                    return true;
                }
            }
            Console.WriteLine("Slot cannot be found, please check\n");
            return false;

            
        }

        public bool staffCheck(string staffID)
        {

            foreach (Person p in people)
            {
                string staffPattern = "^e\\d{7}$";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // Check if starts with e, if matches the first expression, then its a staff 
                if (System.Text.RegularExpressions.Regex.IsMatch(p.ID, staffPattern) == true)
                {
                    if (p.ID == staffID)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool studentMenu()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Student Menu:");

            Console.WriteLine("\t1. List Students");
            Console.WriteLine("\t2. Staff Availability");
            Console.WriteLine("\t3. Make Booking");
            Console.WriteLine("\t4. Cancel Booking");
            Console.WriteLine("\t5. Exit\n");

            Console.Write("Enter Option: ");

            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
            {
                Console.WriteLine("Invalid input, please enter option from 1 to 5\n");
                Console.Write("Enter Option: ");
                choice = Console.ReadLine();
            }

            Console.WriteLine("Your choice was {0}\n", choice);

            if (choice == "1")
            {
                listAllStudents();
            }
            else if (choice == "2")
            {

            }
            else if (choice == "3")
            {

            }
            else if (choice == "4")
            {

            }
            else if (choice == "5")
            {
                Console.WriteLine("Returning to the main menu");
                menuStart();
                return true;
            }

            studentMenu();
            return false;

        }

        public void listAllStudents()
        {

            try
            {
                int counter = 0;
                foreach (Person p in people)
                {
                    string studentPattern = "^s\\d{7}$";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();


                    // Check if starts with e, if matches the first expression, then its a student
                    if (System.Text.RegularExpressions.Regex.IsMatch(p.ID, studentPattern) == true)
                    {
                        if (counter == 0)
                        {
                            Console.WriteLine("\tName\t\tID\t\tEmail");
                            Console.WriteLine("\t----\t\t--\t\t-----");
                            counter++;
                        }

                        Console.WriteLine("\t{0}\t\t{1}\t{2}", p.Name, p.ID, p.Email);
                    }

                }
                Console.WriteLine();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine();
            }

        }
    }
}
