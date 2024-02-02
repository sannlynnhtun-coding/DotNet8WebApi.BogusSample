# DotNet8 Bogus Sample


https://github.com/bchavez/Bogus

```csharp
public enum Gender
{
    Male,
    Female
}

//Set the randomizer seed if you wish to generate repeatable data sets.
Randomizer.Seed = new Random(8675309);

var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };

var orderIds = 0;
var testOrders = new Faker<Order>()
    //Ensure all properties have rules. By default, StrictMode is false
    //Set a global policy by using Faker.DefaultStrictMode
    .StrictMode(true)
    //OrderId is deterministic
    .RuleFor(o => o.OrderId, f => orderIds++)
    //Pick some fruit from a basket
    .RuleFor(o => o.Item, f => f.PickRandom(fruit))
    //A random quantity from 1 to 10
    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
    //A nullable int? with 80% probability of being null.
    //The .OrNull extension is in the Bogus.Extensions namespace.
    .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f));


var userIds = 0;
var testUsers = new Faker<User>()
    //Optional: Call for objects that have complex initialization
    .CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))

    //Use an enum outside scope.
    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())

    //Basic rules using built-in generators
    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
    .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
    .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
    .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")

    //Use a method outside scope.
    .RuleFor(u => u.CartId, f => Guid.NewGuid())
    //Compound property with context, use the first/last name properties
    .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
    //And composability of a complex collection.
    .RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
    //Optional: After all rules are applied finish with the following action
    .FinishWith((f, u) =>
        {
            Console.WriteLine("User Created! Id={0}", u.Id);
        });

var user = testUsers.Generate();
Console.WriteLine(user.DumpAsJson());

/* OUTPUT:
User Created! Id=0
 *
{
  "Id": 0,
  "FirstName": "Audrey",
  "LastName": "Spencer",
  "FullName": "Audrey Spencer",
  "UserName": "Audrey_Spencer72",
  "Email": "Audrey82@gmail.com",
  "Avatar": "https://s3.amazonaws.com/uifaces/faces/twitter/itstotallyamy/128.jpg",
  "CartId": "863f9462-5b88-471f-b833-991d68db8c93",
  "SSN": "923-88-4231",
  "Gender": 0,
  "Orders": [
    {
      "OrderId": 0,
      "Item": "orange",
      "Quantity": 8
    },
    {
      "OrderId": 1,
      "Item": "banana",
      "Quantity": 2
    },
    {
      "OrderId": 2,
      "Item": "kiwi",
      "Quantity": 9
    }
  ]
} */
```
