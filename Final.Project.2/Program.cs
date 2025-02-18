using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project._2
{
    abstract class Person<TId>
    {
        public string Name { get; set; }

        public TId Id { get; set; }

        public Person(string name, TId id)
        {
            Name = name;
            Id = id;
        }
    }

    class Courier<TId> : Person<TId>
    {
        public double Rating { get; private set; }

        public Courier(string name, TId id, double initialRating) : base(name, id)
        {
            Rating = initialRating;
        }

        public void UpdateRating(double newRating)
        {
            Rating = (Rating + newRating) / 2;
        }

        public override string ToString()
        {
            return $"Курьер: {Name}, ID: {Id}, Рейтинг: {Rating}";
        }
    }

    class Client<TId> : Person<TId>
    {
        public string Status { get; set; }

        public Client(string name, TId id, string status) : base(name, id)
        {
            Status = status;
        }

        public override string ToString()
        {
            return $"Клиент: {Name}, ID: {Id}, Статус: {Status}";
        }
    }

    public class ContactInfo
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ContactInfo(string phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }

    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    abstract class Delivery
    {
        public string Address;
    }

    class HomeDelivery : Delivery
    {
        public string CourierName { get; set; }

        public DateTime DeliveryTime { get; set; }

        public override string ToString()
        {
            return $"Адрес: {Address}, Курьер: {CourierName}, Время доставки: {DeliveryTime}";
        }
    }

    class PickPointDelivery : Delivery
    {
        public string PickupPointName { get; set; }

        public string CarrierCompany { get; set; }

        public override string ToString()
        {
            return $"Адрес: {Address}, Пункт выдачи: {PickupPointName}, Перевозчик: {CarrierCompany}";
        }
    }

    class ShopDelivery : Delivery
    {
        public string StoreName { get; set; }

        public override string ToString()
        {
            return $"Магазин: {StoreName}, Адрес: {Address}";
        }
    }

    class Order<TDelivery> where TDelivery : Delivery
    {
        public List<Product> Products { get; private set; } = new List<Product>();

        public TDelivery Delivery { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public decimal GetTotalPrice()
        {
            return Products.Sum(p => p.Price * p.Quantity);
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }
    }

    internal class Program
    {
        static void Main()
        {
            var client = new Client<string>("Иван Иванов", "ivan@example.com", "VIP");

            var products = new List<Product>
        {
            new Product("Телефон", 15000m, 1),
            new Product("Наушники", 2500m, 1),
            new Product("Зарядное устройство", 800m, 1)
        };

            var homeDelivery = new HomeDelivery
            {
                Address = "г. Москва, ул. Тверская, д. 25",
                CourierName = "Сергей Петров",
                DeliveryTime = DateTime.Now.AddDays(3)
            };

            var order = new Order<HomeDelivery>
            {
                Number = 123456,
                Description = "Новый смартфон с аксессуарами",
                Delivery = homeDelivery
            };

            foreach (var product in products)
            {
                order.AddProduct(product);
            }

            Console.WriteLine($"Общая сумма заказа: {order.GetTotalPrice()} руб.");

            order.DisplayAddress();

            Console.WriteLine(client);

            Console.WriteLine(homeDelivery);

            foreach (var product in order.Products)
            {
                Console.WriteLine(product.Name);
            }
            Console.ReadKey();
        }
    }
}
