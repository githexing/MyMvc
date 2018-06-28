﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCommon;

namespace MVC.Controllers
{
    public class WebApiController : ApiController
    {
        Product[] products = new Product[]
          {
            new Product { Id = 1, Name = "农夫山泉", Category = "water", Price = 2 },
            new Product { Id = 2, Name = "钢笔", Category = "study", Price = 3.75M },
            new Product { Id = 3, Name = "烤肠", Category = "food", Price = 1 },
            new Product { Id = 4, Name = "崂山矿泉水", Category = "water", Price = 2 },
            new Product { Id = 5, Name = "铅笔", Category = "study", Price = 3.75M },
            new Product { Id = 6, Name = "烤羊肉串", Category = "food", Price = 1 }
          };


        public IEnumerable<Product> GetAllProducts()
        {
            JWTToken J = new JWTToken();
            var a = J.jiami();
            J.jiemi(a);
            return products;
        }
        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }


   

     


}