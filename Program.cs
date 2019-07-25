using System;
using System.Collections.Generic;

namespace Lab9
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Hometown { get; set; }
        public string FavoriteFood { get; set; }
        public string Birthday { get; set; }
        public Person(string name, int age, string hometown, string favoriteFood, string birthday)
        {
            Name = name;
            Age = age;
            Hometown = hometown;
            FavoriteFood = favoriteFood;
            Birthday = birthday;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Create students and give them properties
            Person abrahamLincoln = new Person("Abe", 56, "Sinking Spring Farm", "Hot Dogs", "February 12, 1809");
            Person cleopatra = new Person("Cleo", 39, "Alexandria", "Koshari", "August 10, 69BC");
            Person theodoreRoosevelt = new Person("Teddy", 60, "New York City", "elk", "October 27, 1858");
            Person johnFKennedy = new Person("Jack", 46, "Brookline", "Crab", "May 29, 1917");
            Person joanOfArc = new Person("Joan", 19, "Domremy, Duchy of Bar", "steak", "January 6, 1412");
            //assign students to their position in a list
            List<Person> students = new List<Person> { abrahamLincoln, cleopatra, theodoreRoosevelt, johnFKennedy, joanOfArc };

            //set necessary variables to be used later
            Person matchedPerson = null;
            bool runAgain = true;
            bool moreInfo = true;
            bool loopAgain = true;

            while (runAgain == true)
            {
                //determine which student the user is inquiring about
                while (matchedPerson == null)
                {
                    Console.WriteLine("if you would like to add a student, type ADD in all caps, otherwise, press enter to continue.");
                    if (Console.ReadLine() == "ADD")
                        students = AddNewStudent(students);
                    else
                    {
                        matchedPerson = GetStudent(students);
                        moreInfo = true;
                    }
                }

                while (moreInfo)
                {
                    //display information selected by the user about that student
                    DisplayStudentInfo(matchedPerson);

                    bool loopYesNo = true;
                    while (loopYesNo)
                    {
                        //while we still have the student matched, ask the user if they want additional information about this student.
                        string response = GetUserInput($"\nwould you like additional information about {matchedPerson.Name}? 'yes' or 'no'");

                        //if they say 'yes', display the menu of properties again
                        if (response.ToLower() == "yes")
                        {
                            loopYesNo = false;
                            moreInfo = true;
                        }

                        //if they say 'no', break out of the loop
                        else if (response.ToLower() == "no")
                        {
                            loopYesNo = false;
                            moreInfo = false;
                        }

                        else
                            loopYesNo = true;
                    }
                }
                while (loopAgain)
                {
                    string YesNo = GetUserInput("would you like information about a different student or to enter a new student? 'yes' or 'no'");
                    if (YesNo == "yes")
                    {
                        loopAgain = false;
                        runAgain = true;
                        matchedPerson = null;
                    }
                    else if (YesNo == "no")
                    {
                        loopAgain = false;
                        runAgain = false;
                    }
                    else
                    {
                        loopAgain = true;
                    }
                }
            }
        }
        public static Person GetStudent(List<Person> students)
        {
            bool isNumber = false;
            bool isValid = false;

            //provide a menu of students
            Console.Clear();
            DisplayStudentMenu(students);

            //Ask the user to select a student
            string userInput = GetUserInput("Which Student would you like information about?");

            //Determine if the user entered a number
            isNumber = ValidateIsInt(userInput);

            //Determine if the number corresponds to an option
            isValid = ValidateIsOption(userInput, students);

            //if the user input valid data, we then match the input to a student
            if (isValid)
            {
                return SelectPerson(userInput, students);
            }
            //if the user doesn't input valid data, we call this function again
            else
            {
                return GetStudent(students);
            }
        }
        public static void DisplayStudentInfo(Person matchedPerson)
        {
            //reset validity because we are using the same variable but determining validity of a different input
            bool isValid = false;
            bool isNumber = false;

            while (!isValid)
            {
                //provide a menu of properties
                Console.Clear();
                DisplayPropertiesMenu();

                //Ask the user to select a property
                string userInput = GetUserInput($"What information would you like to know about {matchedPerson.Name}?");

                //Determine if the user entered a number
                isNumber = ValidateIsInt(userInput);

                //Determine if the number is in range. If it's in range, userInput corresponds to a property
                if (isNumber)
                {
                    isValid = ValidateIsInRange(int.Parse(userInput), 5);
                }

                //if it's not a number, we check if input is a property option
                if (!isNumber)
                {
                    isValid = ValidateIsProperty(userInput);
                }

                //if the user has input a valid property, display the selcted information
                if (isValid)
                {
                    DisplaySelectedInfo(matchedPerson, userInput);
                }
            }
        }
        public static void DisplayStudentMenu(List<Person> people)
        {
            //run through all students in the list and display the option they correspond to and the name of the student
            int i = 1;
            foreach (Person person in people)
            {
                Console.WriteLine($"{i}. {person.Name}");
                i++;
            }
        }
        public static void DisplayPropertiesMenu()
        {
            //this menu won't change so we hard code it.
            Console.WriteLine(
               $"1.Name\n" +
               $"2.Age\n" +
               $"3.Hometown\n" +
               $"4.Favorite Food\n" +
               $"5.Birthday\n");
        }
        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        public static bool ValidateIsInt(string input)
        {
            return int.TryParse(input, out int selection);
        }
        public static bool ValidateIsInRange(int selection, int listLength)
        {
            //use the length of the list for when new students get added
            if ((selection >= 1) && (selection <= listLength))
                return true;
            else
                return false;
        }
        public static bool ValidateIsStudent(string input, List<Person> students)
        {
            //set the bool to invalid because we haven't validated yet
            bool isValid = false;
            //set i to 0 because we start at the first index of student
            int i = 0;
            //break out of the loop if we return a valid answer or if we get to the end of our list
            while (!isValid && i < students.Count)
            {
                isValid = string.Equals(input.ToLower(), students[i].Name.ToLower());
                i++;
            }
            //return whether we found a match to the input
            return isValid;
        }
        public static Person SelectPerson(string input, List<Person> students)
        {
            //we haven't found a person, so we are setting the match to null.
            Person matchedPerson = null;

            //try to parse the input. If we can, it's an integer and that's the selection the user intended.
            if (int.TryParse(input, out int selection))
            {
                matchedPerson = students[selection - 1];
            }

            //if we can't parse the input, that's a name, and we try to match the names
            else
            {
                foreach (Person student in students)
                {
                    if (input.ToLower() == student.Name.ToLower())
                    {
                        matchedPerson = student;
                    }
                }
            }
            //input has already been validated at this point, so we shouldn't have a null reference exception here.
            return matchedPerson;
        }
        public static bool ValidateIsProperty(string input)
        {
            //check if the entry matches name, age, hometown, birthday, or favorite food
            if ((input.ToLower() == "age") ||
               (input.ToLower() == "name") ||
               (input.ToLower() == "hometown") ||
               (input.ToLower() == "birthday") ||
               (input.ToLower() == "favorite food"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void DisplaySelectedInfo(Person matchedPerson, string userInput)
        {
            //output various information based on the request of the user.
            if ((userInput == "1") || (userInput.ToLower() == "name"))
            {
                Console.WriteLine($"{matchedPerson.Name}'s name is {matchedPerson.Name}. but I think you already knew that.");
            }
            if ((userInput == "2") || (userInput.ToLower() == "age"))
            {
                Console.WriteLine($"{matchedPerson.Name} is {matchedPerson.Age} years old. Something tells me they won't be in class much longer.");
            }
            if ((userInput == "3") || (userInput.ToLower() == "hometown"))
            {
                Console.WriteLine($"{matchedPerson.Name} was born in {matchedPerson.Hometown}.");
            }
            if ((userInput == "4") || (userInput.ToLower() == "favorite food"))
            {
                Console.WriteLine($"{matchedPerson.Name} loves to eat {matchedPerson.FavoriteFood}.");
            }
            if ((userInput == "5") || (userInput.ToLower() == "birthday"))
            {
                Console.WriteLine($"{matchedPerson.Name} was born on {matchedPerson.Birthday}.");
            }
        }
        public static bool ValidateIsOption(string userInput, List<Person> students)
        {
            //Determine if the number is in range. If it's in range, userInput corresponds to a student
            if (int.TryParse(userInput, out int number))
            {
                return ValidateIsInRange(int.Parse(userInput), students.Count);
            }

            //if it's not a number, we check if input is a student's name
            else
            {
                return ValidateIsStudent(userInput, students);
            }
        }
        public static List<Person> AddNewStudent(List<Person> students)
        {
            string temp = "";
            Person newStudent = new Person("", 0, "", "", "");
            Console.Clear();
            while (newStudent.Name == "")
            {
                Console.WriteLine("Please enter student's name.");
                temp = Console.ReadLine();
                if (temp != "")
                {
                    newStudent.Name = temp;
                }
                else
                {
                    newStudent.Name = "";
                }
            }

                while (newStudent.Age == 0)
            {
                Console.WriteLine("Please enter student's age.");
                if (int.TryParse((Console.ReadLine()), out int age))
                {
                    newStudent.Age = age;
                }
                else
                { newStudent.Age = 0; }
            }

            temp = "";
            while (newStudent.Hometown == "")
            {
                Console.WriteLine("Please enter student's Hometown.");
                temp = Console.ReadLine();
                if (temp != "")
                {
                    newStudent.Hometown = temp;
                }
                else
                {
                    newStudent.Hometown = "";
                }
            }

            temp = "";
            while (newStudent.FavoriteFood == "")
            {
                Console.WriteLine("Please enter student's Favorite Food.");
                temp = Console.ReadLine();
                if (temp != "")
                {
                    newStudent.FavoriteFood = temp;
                }
                else
                {
                    newStudent.FavoriteFood = "";
                }
            }

            temp = "";
            while (newStudent.Birthday == "")
            {
                Console.WriteLine("Please enter student's Birthday.");
                temp = Console.ReadLine();
                if (temp != "")
                {
                    newStudent.Birthday = temp;
                }
                else
                {
                    newStudent.Birthday = "";
                }
            }

            students.Add(newStudent);
            return students;
        }
    }
}