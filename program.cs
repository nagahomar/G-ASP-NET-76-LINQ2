
using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}

public class Order
{
    public int OrderID { get; set; }
    public decimal OrderValue { get; set; }
}

public class Customer
{
    public int CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string Country { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}

class Program
{
    static void Main()
    {
        List<Product> ProductList = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18, UnitsInStock = 39 },
            new Product { ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19, UnitsInStock = 17 },
            new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10, UnitsInStock = 13 },
            new Product { ProductID = 4, ProductName = "Chef Anton", Category = "Condiments", UnitPrice = 22, UnitsInStock = 0 },
            new Product { ProductID = 5, ProductName = "Grandma Cookies", Category = "Confections", UnitPrice = 25, UnitsInStock = 120 },
            new Product { ProductID = 6, ProductName = "Chocolate", Category = "Confections", UnitPrice = 12, UnitsInStock = 50 },
            new Product { ProductID = 7, ProductName = "Seafood Soup", Category = "Seafood", UnitPrice = 35, UnitsInStock = 20 },
            new Product { ProductID = 8, ProductName = "Salmon", Category = "Seafood", UnitPrice = 60, UnitsInStock = 15 },
            new Product { ProductID = 9, ProductName = "Clam Chowder", Category = "Seafood", UnitPrice = 28, UnitsInStock = 10 },
            new Product { ProductID = 10, ProductName = "King Crab", Category = "Seafood", UnitPrice = 80, UnitsInStock = 0 },
            new Product { ProductID = 11, ProductName = "Laptop", Category = "Electronics", UnitPrice = 1200, UnitsInStock = 10 },
            new Product { ProductID = 12, ProductName = "Phone", Category = "Electronics", UnitPrice = 800, UnitsInStock = 25 },
            new Product { ProductID = 13, ProductName = "Headphones", Category = "Electronics", UnitPrice = 150, UnitsInStock = 40 },
            new Product { ProductID = 14, ProductName = "T-Shirt", Category = "Clothing", UnitPrice = 30, UnitsInStock = 100 },
            new Product { ProductID = 15, ProductName = "Jeans", Category = "Clothing", UnitPrice = 60, UnitsInStock = 50 },
            new Product { ProductID = 16, ProductName = "Jacket", Category = "Clothing", UnitPrice = 90, UnitsInStock = 15 },
            new Product { ProductID = 17, ProductName = "C# Book", Category = "Books", UnitPrice = 45, UnitsInStock = 30 },
            new Product { ProductID = 18, ProductName = "Novel", Category = "Books", UnitPrice = 20, UnitsInStock = 60 },
            new Product { ProductID = 19, ProductName = "Coffee Beans", Category = "Beverages", UnitPrice = 15, UnitsInStock = 80 },
            new Product { ProductID = 20, ProductName = "Desk", Category = "Furniture", UnitPrice = 200, UnitsInStock = 12 }
        };

        List<Customer> CustomerList = new List<Customer>
        {
            new Customer { CustomerID = 1, CompanyName = "Alpha Company", Country = "Germany", Orders = new List<Order> { new Order { OrderID = 1, OrderValue = 500 }, new Order { OrderID = 2, OrderValue = 300 } } },
            new Customer { CustomerID = 2, CompanyName = "Beta Company", Country = "France", Orders = new List<Order> { new Order { OrderID = 3, OrderValue = 700 } } },
            new Customer { CustomerID = 3, CompanyName = "Gamma Company", Country = "Germany", Orders = new List<Order> { new Order { OrderID = 4, OrderValue = 200 } } },
            new Customer { CustomerID = 4, CompanyName = "Delta Company", Country = "UK", Orders = new List<Order> { new Order { OrderID = 5, OrderValue = 400 } } },
            new Customer { CustomerID = 5, CompanyName = "Epsilon Company", Country = "France", Orders = new List<Order> { new Order { OrderID = 6, OrderValue = 600 } } }
        };

        // Question 1
        var q1 = ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
        Console.WriteLine("Question 1:");
        foreach (var p in q1)
            Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

        // Question 2
        var q2 = ProductList.Skip(5).Take(5);
        Console.WriteLine("\nQuestion 2:");
        foreach (var p in q2)
            Console.WriteLine(p.ProductName);

        // Question 3
        var q3 = ProductList.OrderBy(p => p.UnitPrice)
                            .TakeWhile(p => p.UnitPrice < 25);
        Console.WriteLine("\nQuestion 3:");
        foreach (var p in q3)
            Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

        // Question 4
        bool q4 = ProductList.Where(p => p.Category == "Seafood")
                             .All(p => p.UnitsInStock > 0);
        Console.WriteLine("\nQuestion 4: " + q4);

        // Question 5
        int[] ids = { 3, 9, 13, 18 };
        bool q5 = ids.Contains(9);
        Console.WriteLine("\nQuestion 5: " + q5);

        // Question 6
        var q6 = ProductList.GroupBy(p => p.Category);
        Console.WriteLine("\nQuestion 6:");
        foreach (var group in q6)
            Console.WriteLine($"{group.Key}: {group.Count()}");




      
        // Question 07
        var q7 = ProductList.GroupBy(p => p.Category)
                            .Select(g => new
                            {
                                Category = g.Key,
                                ProductNames = g.Select(p => p.ProductName)
                            });
        Console.WriteLine("\nQuestion 7:");
        foreach (var group in q7)
        {
            Console.WriteLine(group.Category);
            foreach (var name in group.ProductNames)
                Console.WriteLine("  " + name);
        }



        // Question 8
        var q8 = ProductList.GroupBy(p => p.Category)
                            .Where(g => g.Count() > 3)
                            .Select(g => g.Key);
        Console.WriteLine("\nQuestion 8:");
        foreach (var category in q8)
            Console.WriteLine(category);

        // Question 9
        var q9 =
            from c in CustomerList
            group c by c.Country into countryGroup
            select new
            {
                Country = countryGroup.Key,
                Count = countryGroup.Count(),
                TotalOrderValue = countryGroup.Sum(c =>
                    c.Orders.Sum(o => o.OrderValue))
            };
        Console.WriteLine("\nQuestion 9:");
        foreach (var item in q9)
            Console.WriteLine($"{item.Country} - {item.Count} - {item.TotalOrderValue}");

        // Question 10
        int q10 = ProductList.Sum(p => p.UnitsInStock);
        Console.WriteLine("\nQuestion 10: " + q10);




      
                              // Question 11
        decimal cheapest = ProductList.Min(p => p.UnitPrice);
        decimal mostExpensive = ProductList.Max(p => p.UnitPrice);
        Console.WriteLine("\nQuestion 11:");
        Console.WriteLine("Cheapest: " + cheapest);
        Console.WriteLine("Most Expensive: " + mostExpensive);

        // Question 12
        var q12 = ProductList.Select(p => p.Category).Distinct();
        Console.WriteLine("\nQuestion 12:");
        foreach (var category in q12)
            Console.WriteLine(category);

                       // Question 13
        int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
        int[] setB = { 3, 6, 9, 12, 15, 13 };
        var q13 = setA.Except(setB);
        Console.WriteLine("\nQuestion 13:");
        foreach (var id in q13)
            Console.WriteLine(id);

        // Question 14
        string[] list1 = { "Germany", "France", "UK", "Spain" };
        string[] list2 = { "france", "SPAIN", "Italy" };
        var q14 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
        Console.WriteLine("\nQuestion 14:");
        foreach (var country in q14)
            Console.WriteLine(country);



      
        // Question 15
        Dictionary<int, Product> q15 = ProductList.ToDictionary(p => p.ProductID);
        if (q15.TryGetValue(18, out Product product))
            Console.WriteLine("\nQuestion 15: " + product.ProductName);



      
        // Question 16
        var q16 = ProductList.First(p => p.UnitPrice > 50);
        Console.WriteLine("\nQuestion 16: " + q16.ProductName);

        // Question 17
        var q17 = ProductList.FirstOrDefault(p => p.UnitPrice > 500);
        Console.WriteLine("\nQuestion 17: " + (q17 == null ? "null" : q17.ProductName));

        // Question 18
        var q18 = Enumerable.Range(1, 10).Select(n => $"7 x {n} = {7 * n}");
        Console.WriteLine("\nQuestion 18:");
        foreach (var line in q18)
            Console.WriteLine(line);

        // Question 19
        var q19 = Enumerable.Range(1, 30).Where(n => n % 2 == 0);
        Console.WriteLine("\nQuestion 19:");
        foreach (var number in q19)
            Console.WriteLine(number);


      
        // Question 20
        var q20 = ProductList.Take(3).Select(p => p.ProductName)
                             .Concat(CustomerList.Take(3).Select(c => c.CompanyName));
        Console.WriteLine("\nQuestion 20:");
        foreach (var name in q20)
            Console.WriteLine(name);


         
                          // Question 21
        var q21 = ProductList.Zip(CustomerList,
            (p, c) => $"{p.ProductName} sold to {c.CompanyName}");
        Console.WriteLine("\nQuestion 21:");
        foreach (var line in q21)
            Console.WriteLine(line);
    }
}
