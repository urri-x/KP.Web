﻿using System;
using KP.WebApi.Query;

namespace KP.WebApi.Models.Order
{
    public class Order
    {
        public int Id;
        public string Number;
        public DateTime CreateDate;
        public int Status;
        public string Description;
        public string Type;
        public string Code;
        public DateTime AcceptDate;
        public string LastModifiedByUser;
        public OrderParts Parts;
    }
}
