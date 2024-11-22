using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GenericsActivity1
{
    public class DictionaryRepository<TKey, TProduct> where TKey : IComparable<TKey>
    {
        public Dictionary<TKey, TProduct> dictionary;

        public DictionaryRepository()
        {
            dictionary = new Dictionary<TKey, TProduct>();

        }

        public void Add(TKey id, TProduct value)
        {


            if (dictionary.ContainsKey(id))
            {
                throw new ArgumentException("The item you Entered is already existing.");
            }
            dictionary[id] = value;

        }
        public TProduct Get(TKey id)
        {
            if (!dictionary.TryGetValue(id, out TProduct value))
            {
                throw new KeyNotFoundException("This specified key does not exist.");

            }

            return value;
        }
        public void Update(TKey id, TProduct newValue)
        {
            if (!dictionary.ContainsKey(id))
            {
                throw new KeyNotFoundException("The key you Entered does not exist.");

            }
            dictionary[id] = newValue;
        }
        public void Delete(TKey id)
        {
            if (!dictionary.ContainsKey(id))
            {
                throw new KeyNotFoundException("The key you Entered does not exist.");

            }

            dictionary.Remove(id);
        }
        public void DisplayAllItems()
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Key: {item.Key} Value: {item.Value}");

            }
        }
    }

    public class Product
    {
        public int ProductId
        {
            get;

            set;
        }

        public string ProductName
        {
            get;

            set;
        }
        public override string ToString()
        {
            return $"Product Id {ProductId} Product Name: {ProductName}";

        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            DictionaryRepository<int, Product> dictionaryRepository = new DictionaryRepository<int, Product>();
            bool running = true;
            while (running)
            {
                Console.WriteLine("CRUD Operation (Procducts)" +
                                   "\n1. Add your product" +
                                   "\n2. Display or Retrieve the product by its Key" +
                                   "\n3, Update the product" +
                                   "\n4. Delete the product" +
                                   "\n5. Display all products" +
                                   "\n6. Exit");

                Console.Write("Enter 1 - 6: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        Console.Write("Enter product Id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter product: ");
                        string product = Console.ReadLine();
                        var productRepository = new Product
                        {
                            ProductId = id,
                            ProductName = product
                        };
                        try
                        {

                            dictionaryRepository.Add(id, productRepository);
                            Console.WriteLine("Product added successfully.");

                        }
                        catch (ArgumentException e)
                        {

                            Console.WriteLine(e.Message);

                        }
                        Console.WriteLine();

                        break;
                    case 2:
                        Console.Write("Enter the Key id you want to retrieve: ");
                        int retrieveId = int.Parse(Console.ReadLine());
                        try
                        {
                            Product product1 = dictionaryRepository.Get(retrieveId);
                            Console.WriteLine($"Key ID: {retrieveId}" +
                                               $"\nProduct Name: {product1}");
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.Write("Enter the key ID you want to Update: ");
                        if (int.TryParse(Console.ReadLine(), out int keyId) && dictionaryRepository.dictionary.TryGetValue(keyId, out Product items))
                        {
                            Console.WriteLine($"Current ID: {items.ProductId}");
                            Console.Write("Enter a new id: ");
                            string newId = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newId))
                            {
                                int number = int.Parse(newId);
                                items.ProductId = number;
                            }
                            Console.WriteLine($"Current ID: {items.ProductName}");
                            Console.Write("Enter a new Name (leave a blank to keep current): ");

                            string newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {

                                items.ProductName = newName;
                            }
                            Console.WriteLine("Updated successfully");
                        }
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.Write("Enter the key you want to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int keyID) && dictionaryRepository.dictionary.ContainsKey(keyID))
                        {
                            dictionaryRepository.dictionary.Remove(keyID);
                            Console.WriteLine("Product deleted successfully");
                        }
                        else
                        {

                            Console.WriteLine("ID was not found.");
                        }
                        Console.WriteLine();
                        break;

                    case 5:
                        dictionaryRepository.DisplayAllItems();
                        Console.WriteLine();
                        break;

                    case 6:
                        running = false;
                        break;
                }

            }
        }
    }
}