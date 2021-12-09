namespace XMLtoPDF
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }

        public Person(string name, int age, string company)
        {
            Name = name;
            Age = age;
            Company = company;
        }

        public override string ToString()
        {
            return $"{Name} {Age} {Company}";
        }
    }
}
