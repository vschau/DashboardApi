using System.Collections.Generic;
using System.Linq;
using DashboardApi.Helpers;

namespace DashboardApi.Models
{
    public class DbInitializer
    {
        private readonly DashboardContext _ctx;

        public DbInitializer(DashboardContext ctx)
        {
            _ctx = ctx;
        }

        private static List<string> states = HelperMethods.states;

        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }

            if (!_ctx.Orders.Any())
            {
                SeedOrders(nOrders);
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
        }

        internal void SeedCustomers(int n)
        {
            var customers = BuildCustomerList(n);

            foreach (var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }
        }

        internal void SeedOrders(int n)
        {
            var orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _ctx.Orders.Add(order);
            }
        }

        internal void SeedServers()
        {
            var servers = BuildServerList();

            foreach (var server in servers)
            {
                _ctx.Servers.Add(server);
            }
        }

        internal static List<Customer> BuildCustomerList(int n)
        {
            var customers = new List<Customer>();

            for (var i = 1; i <= n; i++)
            {
                var name = HelperMethods.MakeCustomerName();

                customers.Add(new Customer
                {
                    Name = name,
                    State = HelperMethods.GetRandom(states),
                    Email = HelperMethods.MakeEmail(name)
                });
            }

            return customers;
        }

        internal List<Order> BuildOrderList(int n)
        {
            var orders = new List<Order>();

            for (var i = 1; i <= n; i++)
            {
                var placed = HelperMethods.GetRandOrderPlaced();
                var completed = HelperMethods.GetRandOrderCompleted(placed);

                orders.Add(new Order
                {
                    Total = HelperMethods.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed,
                    CustomerId = HelperMethods.GetRandomCustomerId(_ctx)
                });
            }

            return orders;
        }

        internal static List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Name = "Dev-Web",
                    IsOnline = true
                },

                new Server
                {
                    Name = "Dev-Analysis",
                    IsOnline = true
                },

                new Server
                {
                    Name = "Dev-Mail",
                    IsOnline = true
                },

                new Server
                {
                    Name = "QA-Web",
                    IsOnline = true
                },

                new Server
                {
                    Name = "QA-Analysis",
                    IsOnline = true
                },

                new Server
                {
                    Name = "QA-Mail",
                    IsOnline = true
                },

                new Server
                {
                    Name = "Prod-Web",
                    IsOnline = true
                },

                new Server
                {
                    Name = "Prod-Analysis",
                    IsOnline = true
                },

                new Server
                {
                    Name = "Prod-Mail",
                    IsOnline = true
                },
            };
        }
    }
}
