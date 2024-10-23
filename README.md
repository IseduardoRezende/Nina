# Nina

<h1>Nina is a library for fluent generic construction of objects of any type.</h1>

![Icon](https://raw.githubusercontent.com/IseduardoRezende/Nina/refs/heads/main/nina2GitNuget.jpg)

<h2>Implementation Example(s):</h2>
<br />

```csharp
//Defining the class:
public class Person
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

public enum Gender
{
    Male,
    Female
}

/*---------------------------------------------------------------------------- */

//Starting using the SetBuilder<T> class:
var person = new SetBuilder<Person>()
    .WithProperty(p => p.Name, s => "Edu")      //Can handle with any type
    .WithProperty(p => p.Married, m => true)
    .WithProperty(p => p.BirthDate, b => new DateTime(2005, 9, 22))
    .WithProperty(p => p.Partner, p =>
    {
        return new SetBuilder<Person>()
            .WithProperty(p => p.Name, n => "Livia")      
            .WithProperty(p => p.Married, m => true)
            .WithProperty(p => p.Gender, Gender.Female)     //Can pass a value directly  
            .Build();
    })
    .WithProperty(p => p.Friends, bf =>      //Can handle with Collections
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
    .WithProperty(p => p.FriendsOfFriends, bfof =>      //Can handle with nested Collections   
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
```
