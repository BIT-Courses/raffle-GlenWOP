using System;
using System.IO;
using System.Threading;

namespace Programming_Assignment_Final
{
    class Program
    {
        
        public struct Prize
        {
            public string prizeName;
            public int quantity;
            public int lowerBound;
            public int upperBound;
        }
        

        public struct Student
        {
            public String firstName;
            public String lastName;
            public String phoneNumber;
        }

        public static void WriteToFile(Student[] students)
        {
            int count = 0;
            StreamWriter sw = new StreamWriter("../class.txt"); 
            for (int i = 0; i < students.Length; i++)
            {
                sw.WriteLine(students[i].firstName);
                sw.WriteLine(students[i].lastName);
                sw.WriteLine(students[i].phoneNumber);
                count++;
            }
            sw.Close(); 

        }

        public static void Customers(Student[] students) // Displays Students and Orders it by last names alphabetically
        {
            Console.Clear();
            Student[] alphaStudents = students;
            Array.Sort<Student>(alphaStudents, (student1, student2) => student1.lastName.CompareTo(student2.lastName));
            foreach(Student s in alphaStudents)
            {
                Console.WriteLine(s.firstName + " " + s.lastName + " " + s.phoneNumber);
            }
            Console.ReadLine();
        }

        public static Student[] Newclass(Student[] students) 
        {
            int count = 0;
            students = new Student[21]; /*class.txt has 21 sets of firstname, lastname and phone numbers*/
            String tempName = "";
            String tempLast = "";
            String tempPhone;
            StreamReader sr = new StreamReader("../class.txt"); /*fix file location for the class.txt file*/
            while (count < 21)
            {
                tempName = sr.ReadLine();
                tempLast = sr.ReadLine();
                tempPhone = sr.ReadLine(); 
                students[count].firstName = tempName;
                students[count].lastName = tempLast;
                students[count].phoneNumber = tempPhone;
                count++;
            }
            sr.Close();
            return students;
        }

        public static Prize[] GeneratePrizeList(Prize[] prizes)
        {
            int count = 0;
            prizes = new Prize[14];
            String tempPrizeName = "";
            int tempQuantity = 0;
            int tempLowerBound = 0;
            int tempUpperBound = 0;
            StreamReader sr = new StreamReader("../prizes.txt");
            while (count < 14)
            {
                tempPrizeName = sr.ReadLine();
                tempQuantity = Convert.ToInt32(sr.ReadLine());
                tempLowerBound = tempUpperBound + 1;
                tempUpperBound = Convert.ToInt32(sr.ReadLine());
                prizes[count].prizeName = tempPrizeName;
                prizes[count].quantity = tempQuantity;
                prizes[count].lowerBound = tempLowerBound;
                prizes[count].upperBound = tempUpperBound;
                count++;
            }

            
            sr.Close();
            return prizes;
        }



        public static void Phonenumber(Student[] students) // Lets a user enter the first and last name of the individual whos number they want to change
        {
            Console.Clear();
            Console.WriteLine("Enter the first and last name of the customer you want to update the Phone Number of");
            String temp = Console.ReadLine();
            String temp2 = "";
            bool found = false;
            for(int i = 0; i < 21; i++)
            {
                temp2 = students[i].firstName + " " + students[i].lastName;
                if(temp == temp2)
                {
                    Console.WriteLine("Enter " + temp + "'s new phone number");
                    String num = Console.ReadLine();
                    students[i].phoneNumber = num;
                    found = true;
                }
            }
            if(found)
            {
                Console.WriteLine("Phone number successfully updated");
            }
            else
            {
                Console.WriteLine("No customer by that name found.");
            }
            Console.ReadLine();
            WriteToFile(students);

          
                   
        }

        

        public static int[] Sellingtickets(Student[] students) //Picks 10 random customers from the students array and lists them
        {

            Random rand = new Random();
            int[] ticketPurchasers = new int[10];
            bool flag = false;
            for (int i = 0; i < 10; i++)
            {
                int r = rand.Next(1, 22);
                for(int i2 = 0; i2 < 10; i2++) //loop to check if number already used
                {
                    if(ticketPurchasers[i2] == r) //checks if a number has already been used
                    {
                        flag = true; // marks that a number has already been used
                    }
                }
                if(flag)
                {
                    i--; //resets the random number generator for this instance in the loop
                    flag = false; // resets the flag
                }
                else
                {
                    ticketPurchasers[i] = r; //adds the number generated into the array if already not in use
                }
            }
            Console.Clear();
            Console.WriteLine("These are the people who will buy tickets\n");
            foreach(int i in ticketPurchasers)
            {
                Console.WriteLine(students[i-1].firstName + " " + students[i-1].lastName);
            }

            Console.ReadLine();           
            return ticketPurchasers;

        }

        public static void Raffle(Student[] students, int[] ticketPurchasers, Prize[] prizes)  // activity diagram for this one
        {
           
            int roll = 0;
            Random rand = new Random();
            foreach(int count in ticketPurchasers)
            {
                int count2 = 0;
                Console.WriteLine(students[count-1].firstName + " " + students[count-1].lastName + "'s prizes are:");
                while (count2 < 4)
                {
                    roll = rand.Next(0, 3616001);
                    for(int i = 0; i < 14; i++)
                    {
                        if (roll >= prizes[i].lowerBound) 
                        {
                            if (roll <= prizes[i].upperBound)
                            {
                                if (prizes[i].quantity > 0)
                                {
                                    Console.WriteLine("Prize " + (count2+1) + ": " + prizes[i].prizeName);
                                    prizes[i].quantity = prizes[i].quantity - 1;
                                }
                                else
                                {
                                    Console.WriteLine("Prize " + (count2+1) + ": " + "better luck next time");
                                }
                            }
                            
                        }
                    }
                    count2++;
                }
                Console.ReadLine();
            }
        }



        public static void Menusystem(Student[] students, Prize[] prizes) //The main menu using a switch
        {
            int[] ticketPurchasers = null;
            int taskNumber;
            if(students == null)
            {
                students = Newclass(students);
            }
            if(prizes == null)
            {
                prizes = GeneratePrizeList(prizes);
            }
            do
            {


                Console.WriteLine("Welcome to the main menu! ");              
                Console.WriteLine();
                Console.WriteLine("What would you like to do? :");
                Console.WriteLine();              
                Console.WriteLine("To list all potential customers enter '1' ");
                Console.WriteLine("To locate a person and update their phone number enter '2' ");
                Console.WriteLine("To sell tickets to ten different inviduals enter '3' ");
                Console.WriteLine("To generate prizes for the ten individuals enter '4' (MUST HAVE ALREADY DONE OPTION 3)");
                Console.WriteLine("Otherwise to exit to the main menu please enter '0'");

                
                
                
                taskNumber = Convert.ToInt32(Console.ReadLine());
                switch (taskNumber)

                {
                    case 0:
                        Menusystem(students, prizes);
                        break;

                    case 1:
                        Customers(students);
                        break;

                    case 2:

                        Phonenumber(students);
                        break;
                    case 3:
                        ticketPurchasers = Sellingtickets(students);
                        break;
                    case 4:
                        Raffle(students, ticketPurchasers, prizes);
                        break;

                    default:
                        Menusystem(students, prizes);
                        break;



                }
                Console.Clear();

            } while (taskNumber != 0);
            Console.Clear();

        }
        public static void Main(string[] args)
        {

            Student[] students = null;
            Prize[] prizes = null;
            Menusystem(students, prizes);
            
        }
    }
}

/*Menu Program - Minimum requirements allows the user to do the following:
     * 1.  List all the potential customers. Sort the data by last name. (class.txt file in programming assignment final > bin > debug)
     * 2.  Locate a person in the list and update their phone number. Save these changes back to the file.
     * 3.  You have to sell ten tickets to ten different individuals. 
     *     Randomly generate 10 numbers; these numbers must range from 1 to the number of people in the array (21).  
     *     You can not have duplicate numbers being drawn. Find and list the people who will buy your tickets.
     * 4.  Loop through your ten ticket holders showing their Loop 4 results. 
     */
