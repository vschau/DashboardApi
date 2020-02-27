using DashboardApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace DashboardApi.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Mary",
                    Email = "mary@gmail.com",
                    State = "CA"
                },
                new Customer
                {
                    Id = 2,
                    Name = "John",
                    Email = "john@gmail.com",
                    State = "NY"
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Total = 10.99M,
                    Placed = new DateTime(2019, 12, 11),
                    Completed = new DateTime(2019, 12, 12)
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    Total = 20.99M,
                    Placed = new DateTime(2018, 1, 1),
                    Completed = new DateTime(2018, 2, 2)
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 1,
                    Total = 30.99M,
                    Placed = new DateTime(2020, 2, 4)
                }
            );

            modelBuilder.Entity<Server>().HasData(
                new Server
                {
                    Id = 1,
                    Name = "Dev",
                    IsOnline = true,
                },
                new Server
                {
                    Id = 2,
                    Name = "Preprod",
                    IsOnline = true,
                },
                new Server
                {
                    Id = 3,
                    Name = "Prod",
                    IsOnline = true,
                },
                new Server
                {
                    Id = 4,
                    Name = "QA",
                    IsOnline = false,
                }
            );
        }
    }
}
