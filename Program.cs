using System;

namespace _321Contact {
    internal class Program {
        struct Contact {
            public string firstName;
            public string lastName;
            public string address;
            public string city;
            public string state;
            public string zipCode;
            public string title;
        }
        
        static void Main(string[] args) {

            Contact[] contacts;
            string[] lines = new string[2];

            ReadData(lines);
            contacts = BuildRecord(lines);

            Menu(contacts, lines);

        }//end main

        static void ReadData(string[] lines) {

            StreamReader infile = new StreamReader("C:\\Users\\MCA\\Downloads\\contacts.dat");

            bool firstLine = false;
            //LOOP UNTILE END OF DATA IS REACHED
            while (infile.EndOfStream == false) {
                //READ A BYTE FROM THE FILE CAST TO A CHAR
                if (firstLine == false) {
                    lines[0] = infile.ReadLine();
                    firstLine = true;
                } else {
                    lines[1] = infile.ReadLine();
                }
            }

            //CLOSE THE FILE WHEN DONE
            infile.Close();
        }//end                     

        static Contact[] BuildRecord(string[] lines) {
            string[] recNum1 = lines[0].Split("#");
            string recNumS = recNum1[1];
            int recNum = int.Parse(recNumS);

            string[] records = lines[1].Split((char)30);
            Contact[] contacts = new Contact[records.Length];

            for (int i = 0; i < contacts.Length-1; i++) {
                string[] finalRecord = records[i].Split((char)31);

                contacts[i].firstName = finalRecord[0];
                contacts[i].lastName = finalRecord[1];
                contacts[i].address = finalRecord[2];
                contacts[i].city = finalRecord[3];
                contacts[i].state = finalRecord[4];
                contacts[i].zipCode = finalRecord[5];
                contacts[i].title = finalRecord[6];
            }
            return contacts;
        }

        static void SearchRecord(Contact[] contacts, string[] lines) {
            string query;

            query = Input("Search Name: ");
            query = query.ToLower();
            string[] querys = new string[query.Length + 1];
            querys[0] = ";";
            querys[1] = ";";

            for (int i = 0; i < query.Length; i++) {
                if (query[i] == ' ') {
                    querys = query.Split(" ");
                    break;
                }
            }

            for (int i = 0; i < contacts.Length -1; i++) {

                if (contacts[i].firstName.ToLower().Contains(query) || (contacts[i].lastName.ToLower().Contains(query))) {
                    DisplayRecord(contacts, i);

                } else if (contacts[i].firstName.ToLower().Contains(querys[0].ToLower()) && contacts[i].lastName.ToLower().Contains(querys[1].ToLower()) && !(querys[1] is null)) {
                    DisplayRecord(contacts, i);

                } else if (contacts[i].lastName.ToLower().Contains(querys[0].ToLower()) && contacts[i].firstName.ToLower().Contains(querys[1].ToLower()) && !(querys[1] is null)) {
                    DisplayRecord(contacts, i);

                }
            }
            Menu(contacts, lines);
        }

        static void DisplayRecord(Contact[] contacts, int i) {
                    Console.WriteLine($"Name    : {contacts[i].title} {contacts[i].firstName} {contacts[i].lastName}");
                    Console.WriteLine($"Address : {contacts[i].address}");
                    Console.WriteLine($"          {contacts[i].city}, {contacts[i].state}, {contacts[i].zipCode}\n");
        }

        static void Menu(Contact[] contacts, string[] lines) {
            string console;

            Console.WriteLine("3-2-1 CONTACT\n");
            
                do {
                    console = Input("\t1. Search record\n\t2. Add record\n\nPress 3 to close this application...\n");
                } while (console != "1" && console != "2" && console != "3");

                if (console == "1") {
                    SearchRecord(contacts, lines);
                    
                } else if (console == "2") {
                    AddRecord(contacts, lines);

                } else if (console == "3") {

                }
            }

        static void AddRecord(Contact[] contacts, string[] lines) {
            Contact newContact = new Contact();

            newContact.firstName = Input("Set first name: ");
            newContact.lastName = Input("Set last name: ");
            newContact.address = Input("Set street address: ");
            newContact.city = Input("Set city: ");
            newContact.state = Input("Set state: ");
            newContact.zipCode = Input("Set zipcode: ");
            newContact.title = Input("Set title: ");

            string finalRecords = $"{newContact.firstName}{(char)31}{newContact.lastName}{(char)31}{newContact.address}{(char)31}{newContact.city}{(char)31}{newContact.state}{(char)31}{newContact.zipCode}{(char)31}{newContact.title}{(char)30}";

            BuildNewRecord(contacts, finalRecords, lines);
            
            Menu(contacts, lines);
        }

        static void BuildNewRecord(Contact[] contacts, string finalRecords, string[] lines) {
            //contacts = BuildRecord(lines);
            lines[1] += finalRecords;
            string line = lines[1];
            FinalRecord(line);

        }

        static Contact[] FinalRecord(string line) {
            string[] records = line.Split((char)30);
            Contact[] contacts = new Contact[records.Length];

            for (int i = 0; i == contacts.Length; i++) {
                string[] finalRecord = records[i].Split((char)31);

                contacts[i].firstName = finalRecord[0];
                contacts[i].lastName = finalRecord[1];
                contacts[i].address = finalRecord[2];
                contacts[i].city = finalRecord[3];
                contacts[i].state = finalRecord[4];
                contacts[i].zipCode = finalRecord[5];
                contacts[i].title = finalRecord[6];
            }
            return contacts;
        }

        static string Input(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

    }
}