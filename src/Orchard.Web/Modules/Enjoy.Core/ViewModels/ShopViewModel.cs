using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class ShopViewModel
    {
        public  int Id { get; set; }
        public  Merchant Merchant { get; set; }
        public  string ShopName { get; set; }
        public  string Leader { get; set; }
        public  string Address { get; set; }
        public  string Coordinate { get; set; }
    }
}