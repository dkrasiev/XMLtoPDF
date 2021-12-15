namespace XMLtoPDF
{
    class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Company { get; private set; }

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
