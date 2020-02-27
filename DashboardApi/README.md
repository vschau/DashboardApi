## Errors
- The term 'Add-Migration' is not recognized as the name of a cmdlet
	> install Microsoft.EntityFrameworkCore.Tools package from nuget
- .Net Core 3.0 possible object cycle was detected which is not supported. This way it won't keep following nav properties?
	> install Microsoft.AspNetCore.Mvc.NewtonsoftJson
    services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );;
- Can't go into HttpPost when using Postman
	> Need to pick json as the type in postman
- 405 method not allowed
	> This means it can't get to any route.  Check your routing
- Even though datetime has required, when you add null, it add '1/1/0001 12:00:00 AM'
	> DateTime is a struct, structs are "value type", not "reference type", so their default value are not null, for DateTime it is 1/1/0001 12:00:00 AM, int has its default value as 0.
	> 1 way to solve is to make it nullable: public DateTime? Birthdate { get; set; }
	> if you don't want it to be nullable, use range or custom validator

## .Net Core
- Controller vs ControllerBase
public class Controller : ControllerBase
{
    public dynamic ViewBag { get; }
    public virtual ViewResult View(object model) { }
    // more
}
- AddControllers(), which is new in ASP.NET Core 3.0. In 2.x, you would typically call services.AddMvc() for all ASP.NET Core applications. However, this would configure the services for everything MVC used, such as Razor Pages and View rendering.

## Steps:

### Entity Framework
- Create Customer.cs model
- Add DashboardContext class
- Create ICustomerRepository.cs
- Create SQLCustomerRespositry to inherit ICustomerRepository
- Add connection string to appsettings.json
	- Alternative, use user-secrets to store connection strings (not sure how yet)
- Add `services.AddDbContextPool` and `services.AddSingleton` to Starup.cs
- Run `Add-Migration InitialCreate` or `dotnet ef migration add InitialCreate`
- Run `Update-Database` or `dotnet ef database update`
- Go to SQL Server ObjectExplorer (localdb)\\mssqllocaldb
### Seeding
- Create a static class called ModelBuilderExtensions to add Seed method to ModelBuilder
- In the context class, Override OnModelCreating and call ModelBuilder.Seed()
- `Add-Migration AlterCustomerSeedData`
- `Update-Database`
- Check DB to see if seed data is in

## Issues:
We have one to many relationship (1 customer has many orders)
When you edit customer, you can't edit orders
When you add customer, you can't add orders
Orders need to be added after customer exists
When you edit the order, you can't edit the customer associated with the order?

## Extra
- To start EF from scratch 'Update-Database 0'
- To remove the last migration that has not been applied to the DB `Remove-Migration`
- To remove migrations that have been applied to the DB: `Update-Database LastGoodMigration`
- To stop opening the browser whenever you run > open launchsetting.json
- Microsoft.CodeAnalysis.FxCopAnalyzers to help with api coding check
- After dropping database, if migration files are still there, run Update-Database to recreate and reseed data.  Or just run `update-database -Migration 0 | update-database`
- Cascading delete is the default behavior if you have foreign keys
- .Net standard lib or .Net core lib?  Alwaus use .Net standard
	>Use a .NET Standard library when you want to increase the number of apps that will be compatible with your library, and you are okay with a decrease in the .NET API surface area your library can access.
	>Use a .NET Core library when you want to increase the .NET API surface area your library can access, and you are okay with allowing only .NET Core apps to be compatible with your library.
- the null-coalescing assignment operator ??= assigns the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to null. The ??= operator doesn't evaluate its right-hand operand if the left-hand operand evaluates to non-null.
List<int> numbers = null;
int? a = null;

(numbers ??= new List<int>()).Add(5);
Console.WriteLine(string.Join(" ", numbers));  // output: 5

numbers.Add(a ??= 0);
Console.WriteLine(string.Join(" ", numbers));  // output: 5 0
Console.WriteLine(a);  // output: 0
- null conditional operator: ?.  Evaluable to null if the expression is null
example: var value = a?.x
If a is null, value is null.  If a is not null, value is a.x

## Postman
- Customers
Create
{
	"name": "Styrene",
	"email": "sty@gmail.com",
	"state": "CA"
}
- Order
Create
{
	"customerid": 3,
    "total": 99.99,
    "Placed": "1/1/2020"
}
Put
{
	"id": 5,
	"total": 100.99
}

## Swagbuckles/Swagger
- Launch the app, and navigate to http://localhost:61536/swagger.
- http://localhost:61536/swagger/v1/swagger.json.
- Port is from launchSettings.json
- For xml, in Build, need to checkmark xml output with path 'bin\DashboardApi.xml'
It'll put this in the .cproj file
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <DocumentationFile>bin\DashboardApi.xml</DocumentationFile>
  </PropertyGroup>
Xml allows us to add summary to controllers