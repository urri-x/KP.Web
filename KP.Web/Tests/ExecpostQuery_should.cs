using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using KP.WebApi.Models.Order;
using KP.WebApi.Query;
using NUnit.Framework;

namespace KP.WebApi.Tests
{


    [TestFixture]
    public class ExecpostQueryShould
    {
        [SetUp]
        public void SetUp()
        {
        }



        [Test]
        public void Test()
        {

            var orders = new OrdersQuery()
                .All()
                .Where(o=>o.Code.Equals("ORD_TOTAL_REORGANIZATION"))
                .ToList();
            
            
            
        }
        
         

        

    }
}