using System.Data;

class Program
{
    static void Main(string[] args)
    {
        //DU SKAL BRUGE DENNE FIL: https://gist.github.com/elifiner/cc90fdd387449158829515782936a9a4 TIL NAVNE OG EFTERNAVNE
        List<string> firstNames = ReadNamesFromFile("C:\\Users\\Default\\Downloads\\first-names.txt");
        List<string> lastNames = ReadNamesFromFile("C:\\Users\\Default\\Downloads\\last-names.txt");
        List<Person> people = GeneratePeopleList(firstNames, lastNames, 10);

        foreach (var Person in people)
        {
            foreach (var person in people)
            {
                Console.WriteLine($"ID: {person.ID}");
                Console.WriteLine($"Name: {person.UppercaseFirstname} {person.UppercaseLastname}");
                Console.WriteLine($"Age: {person.Age}");
                Console.WriteLine($"Birthday: {person.Birthday.ToShortDateString()}");
                Console.WriteLine($"Email: {person.Email}");
                Console.WriteLine($"Address: {person.Address}");
                Console.WriteLine($"City: {person.City}");
                Console.WriteLine($"Zipcode: {person.Zipcode}");
                
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadLine();
        }




        static List<string> ReadNamesFromFile(string filename)
        {
            List<string> names = new List<string>();
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    names.Add(line.Trim());
                }
            }
            return names;
        }





        static List<Person> GeneratePeopleList(List<string> firstNames, List<string> lastNames, int count)
        {
            List<Person> people = new List<Person>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Count)];
                string lastName = lastNames[random.Next(lastNames.Count)];
                int id = random.Next(1, 10000);
              
                string email = GenerateRandomEmail(firstName, lastName);
                string address = GenerateRandomAddress(addressPrefix, addressSuffix);
                string city;
                int zipCode;
                CityZipGenerator(out city, out zipCode);

                DateTime today = DateTime.Today;
                int age = random.Next(18, 91);
                DateTime birthday = today.AddYears(-age);



                people.Add(new Person
                {
                    ID = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Birthday = birthday,
                    Email = email,
                    City = city,
                    Zipcode = zipCode,
                    Address = address,


                });
            }

            return people;
        }
    }


    static Dictionary<string, int> cityZipCodes = new Dictionary<string, int>()
    {
        { "Kolding", 6000 },
        { "Haderslev", 6100 },
        { "Middelfart", 5500 },
        { "Odense", 5000 },
        { "Vejle", 7100 },
        { "Vejle Ø", 7120 },
        { "Fredericia", 7000 },
        { "Århus V", 8210 },
        { "Århus C", 8000 },
        { "Taulov", 7000 },
       };

    static string[] Cities = { "Kolding", "Haderslev", "Middelfart", "Odense", "Vejle", "Vejle Ø", "Fredericia", "Århus V", "Århus C", "Taulov" };

    static void CityZipGenerator(out string city, out int zipCode)
    {
        Random random = new Random();
        int index = random.Next(Cities.Length);
        city = Cities[index];
        zipCode = cityZipCodes[city];
    }

    static string[] addressPrefix = { "Oktober", "November", "Skov", "Fjord", "Hare", "Jordbær", "Blåbær", "Vinkel" };
    static string[] addressSuffix = { "vænget", "vej", "hjørnet", "brynet", "gade", "strædet", "marken" };

    static string GenerateRandomAddress(string[] addressPrefix, string[] addressSuffix) //Kombinerer to del-ord og et tal til en realistisk addresse
    {
        Random random = new Random();
        string prefix = addressPrefix[random.Next(addressPrefix.Length)];
        string suffix = addressSuffix[random.Next(addressSuffix.Length)];

        return $"{prefix}{suffix} {random.Next(1,99)}";
    }











    static string GenerateRandomEmail(string firstName, string lastName)
    {
        Random random = new Random();
        string[] emailProviders = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "livemail.com", "supermail.com", "zmail.com" };

        string email = "";
        int choice = random.Next(3); // Tilfældig email følger en af de 3 formater

        switch (choice)
        {
            case 0:
                email = $"{firstName}.{lastName}";
                break;
            case 1:
                email = $"{firstName}{random.Next(1, 99)}";
                break;
            case 2:
                email = $"{lastName}{random.Next(1, 99)}";
                break;
        }

        if (random.Next(2) == 1) // 50% chance for at tilføje tal til emailen
            email += random.Next(1, 9);

        email += "@" + emailProviders[random.Next(emailProviders.Length)]; // Tilføj en tilfældig email-leverandør

        return email;
    }














    class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public int Zipcode { get; set; }

        public string UppercaseFirstname => CapitalizeFirstLetter(FirstName); //Navnelisterne er med små bogstaver og skal derfor ændres 
        public string UppercaseLastname => CapitalizeFirstLetter(LastName);

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);





        }
    }
}














