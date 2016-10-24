using System;
using System.Collections;
using System.Collections.Generic;

using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    public class ProductsListViewModel : IEnumerable
    {
        public  IEnumerable<Product> Products { get; set; }
        public  PagingInfo PagingInfo { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}