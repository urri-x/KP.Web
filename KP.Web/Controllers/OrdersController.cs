using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KP.WebApi.Helpers;
using KP.WebApi.Models;
using KP.WebApi.Models.Order;
using KP.WebApi.Query;

namespace KP.WebApi.Controllers
{
    public class OrdersController: ApiViewController
    {
        public IEnumerable<Order> GetOrders(int id = Int32.MaxValue)
        {
            return new OrdersQuery().All()
                .Accepted()
                .OrderByDescending(x => x.AcceptDate);
        }
        public Order GetOrderInfo(int id)
        {
            var order = new OrdersQuery().One(id);
            return order;
        }

        [HttpGet]
        public IEnumerable<Order> GetShtatOrders(int count=Int32.MaxValue)
        {
            var orderTypeCodes = new string[]
            {
                "65","ORD_HISTORY",
                "ORD_KILL_PODR",
                "ORD_KILL_POST",
                "ORD_RENAME",
                "ORD_SHTAT_CREATE",
                "ORD_PODR_CREATE",
                "ORD_TOTAL_REORGANIZATION", "ORD_FIRM_RENAME",
                "ORD_TOTAL_REORGANIZATION_ElSteel_2"
            };
            return new OrdersQuery().All()
                .Accepted()
                .Where(x => orderTypeCodes.Contains(x.Code))
                .OrderByDescending(x => x.AcceptDate);
        }

        [HttpGet]
        public IViewResult ViewOrders(int id=20)
        {
            return View("~/Views/Orders.cshtml", GetOrders().Take(id));
        }
        [HttpGet]
        public IViewResult ViewOrderInfo(int id)
        {
            return View("~/Views/OrderInfo.cshtml", GetOrderInfo(id));
        }

        [HttpGet]
        public IViewResult ViewShtatOrders(int id = 20)
        {
            return View("~/Views/Orders.cshtml", GetShtatOrders().Take(id));
        }

    }
}
