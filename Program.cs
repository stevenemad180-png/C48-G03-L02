using LINQ.DataSources;
using LINQ.Models;

namespace C48_G03_L02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1 

            var result = Source.ProductList
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);

            foreach (var product in result)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }

            #endregion

            #region Q2 - Show Page 2 of Products

            int pageNumber = 2;
            int pageSize = 5;

            var result2 = Source.ProductList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            foreach (var product in result2)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }

            #endregion


            #region Q3 

            var q3 = Source.ProductList
                .OrderBy(p => p.UnitPrice)
                .TakeWhile(p => p.UnitPrice < 25);

            foreach (var product in q3)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }

            #endregion


            #region Q4 

            bool q4 = Source.ProductList
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);

            Console.WriteLine($"All Seafood products in stock: {q4}");

            #endregion


            #region Q5

            int[] ids = { 3, 9, 13, 18 };

            bool q5 = ids.Contains(9);

            Console.WriteLine($"Contains 9: {q5}");

            #endregion


            #region Q6 

            var q6 = Source.ProductList
                .GroupBy(p => p.Category);

            foreach (var group in q6)
            {
                Console.WriteLine($"{group.Key} - Count: {group.Count()}");

                foreach (var product in group)
                {
                    Console.WriteLine($"  {product.ProductName}");
                }
            }

            #endregion


            #region Q7 

            var q7 = Source.ProductList
                .GroupBy(p => p.Category, p => p.ProductName);

            foreach (var group in q7)
            {
                Console.WriteLine(group.Key);

                foreach (var name in group)
                {
                    Console.WriteLine($"  {name}");
                }
            }

            #endregion


            #region Q8

            var q8 = Source.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key);

            foreach (var category in q8)
            {
                Console.WriteLine(category);
            }

            #endregion


            #region Q9 

            var q9 =
                from customer in Source.CustomerList
                group customer by customer.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                };

            foreach (var group in q9)
            {
                Console.WriteLine($"Country: {group.Country}");
                Console.WriteLine($"Count: {group.Count}");
                Console.WriteLine($"Total Order Value: {group.TotalOrderValue}");
            }

            #endregion


            #region Q10 

            var q10 = Source.ProductList
                .Sum(p => p.UnitsInStock);

            Console.WriteLine($"Total Units In Stock: {q10}");

            #endregion


            #region Q11

            var cheapest = Source.ProductList
                .Min(p => p.UnitPrice);

            var mostExpensive = Source.ProductList
                .Max(p => p.UnitPrice);

            Console.WriteLine($"Cheapest Price: {cheapest}");
            Console.WriteLine($"Most Expensive Price: {mostExpensive}");

            #endregion


            #region Q12

            var q12 = Source.ProductList
                .Select(p => p.Category)
                .Distinct();

            foreach (var category in q12)
            {
                Console.WriteLine(category);
            }

            #endregion


            #region Q13 

            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var q13 = setA.Except(setB);

            foreach (var id in q13)
            {
                Console.WriteLine(id);
            }

            #endregion


            #region Q14 

            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };

            var q14 = list1
                .Except(list2, StringComparer.OrdinalIgnoreCase);

            foreach (var country in q14)
            {
                Console.WriteLine(country);
            }

            #endregion


            #region Q15

            Dictionary<int, Product> q15 = Source.ProductList
                .ToDictionary(p => p.ProductID);

            if (q15.TryGetValue(18, out Product product18))
            {
                Console.WriteLine(product18);
            }

            #endregion


            #region Q16 

            var q16 = Source.ProductList
                .First(p => p.UnitPrice > 50);

            Console.WriteLine(q16);

            #endregion


            #region Q17 

            var q17 = Source.ProductList
                .FirstOrDefault(p => p.UnitPrice > 500);

            Console.WriteLine(q17 == null ? "Not Found" : q17.ToString());

            // Test a condition with no matching product
            var q17NoMatch = Source.ProductList
                .FirstOrDefault(p => p.UnitPrice > 600);

            Console.WriteLine(q17NoMatch == null ? "Not Found" : q17NoMatch.ToString());

            #endregion


            #region Q18


            var q18 = Enumerable.Range(1, 10)
                .Select(n => $"7 x {n} = {7 * n}");

            foreach (var row in q18)
            {
                Console.WriteLine(row);
            }

            #endregion


            #region Q19

            var q19 = Enumerable.Range(1, 30)
                .Where(n => n % 2 == 0);

            foreach (var number in q19)
            {
                Console.WriteLine(number);
            }

            #endregion


            #region Q20 

            var q20 = Source.ProductList
                .Take(3)
                .Select(p => p.ProductName)
                .Concat(
                    Source.CustomerList
                        .Take(3)
                        .Select(c => c.CustomerName)
                );

            foreach (var name in q20)
            {
                Console.WriteLine(name);
            }

            #endregion


            #region Q21 

            var q21 = Source.ProductList
                .Zip(
                    Source.CustomerList,
                    (p, c) => $"{p.ProductName} sold to {c.CustomerName}"
                );

            foreach (var item in q21)
            {
                Console.WriteLine(item);
            }

            #endregion

        }

    }
}
