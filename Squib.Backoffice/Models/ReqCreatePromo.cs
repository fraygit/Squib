﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Backoffice.Models
{
    public class ReqCreatePromo
    {
        public string Id { get; set; }
        public string OrganisationId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ImageText { get; set; }
        public string Category { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Max { get; set; }
        public string Details { get; set; }
    }
}