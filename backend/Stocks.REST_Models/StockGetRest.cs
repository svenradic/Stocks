﻿using System.ComponentModel.DataAnnotations;

namespace Stocks.WebAPI.RESTModels
{
    public class StockGetRest
    {
        public Guid? Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public double? CurrentPrice { get; set; }
        public long? MarketCap { get; set; }
        public Guid? TraderId { get; set; }
    }
}
