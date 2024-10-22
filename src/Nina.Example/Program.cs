// See https://aka.ms/new-console-template for more information
using Nina.SourceCode;

Console.WriteLine("Hello, World!");

var person = new SetBuilder<Person>()
    .WithProperty(p => p.Name, s => "Edu")
    .WithProperty(p => p.Married, m => true)
    .WithProperty(p => p.BirthDate, b => new DateTime(2005, 9, 22))
    .WithProperty(p => p.Partner, p =>
    {
        return new SetBuilder<Person>()
            .WithProperty(p => p.Name, n => "Livia")
            .WithProperty(p => p.Married, m => true)
            .WithProperty(p => p.Gender, Gender.Female)
            .Build();
    })
    .WithProperty(p => p.Friends, bf =>
    {
        bf.Add(f =>
        {
            return new SetBuilder<Person>()
                .WithProperty(p => p.Name, n => "Guilherme")
                .Build();
        });

        bf.Add(f =>
        {
            return new SetBuilder<Person>()
                .WithProperty(p => p.Name, n => "Alan")
                .WithProperty(p => p.Married, m => true)
                .Build();
        });
    })
    .WithProperty(p => p.FriendsOfFriends, bfof =>
    {
        bfof.Add(bf =>
        {
            return
            [
                 new SetBuilder<Person>()
                .WithProperty(p => p.Name, n => "João")
                .Build(),

                 new SetBuilder<Person>()
                .WithProperty(p => p.Name, n => "Paulo")
                .Build()
            ];
        });

        bfof.Add(bf =>
        {
            return
            [
                 new SetBuilder<Person>()
                .WithProperty(p => p.Name, n => "Pedro")
                .WithProperty(p => p.Married, m => true)
                .Build()
            ];
        });
    })
    .Build();

Console.WriteLine(person);

class Person
{
    public string Name { get; set; }

    public bool Married { get; set; }

    public Person Partner { get; set; }

    public ICollection<Person> Friends { get; set; }

    public ICollection<ICollection<Person>> FriendsOfFriends { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }

    private string Secret {  get; set; } 
}

enum Gender
{
    Male,
    Female
}