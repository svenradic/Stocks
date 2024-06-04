﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Stocks.Model;
using Stocks.Repository.Common;

namespace Stocks.Repository
{
    public class TraderRepository : IRepository<Trader>
    {
        private readonly string connectionString;

        public TraderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Trader> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Trader>> GetAll()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<Trader> traders = new List<Trader>();
            await conn.OpenAsync();

            command.CommandText = "SELECT  t.\"Id\" , t.\"Name\" , t.\"DateOfBirth\", s.\"Id\", s.\"Symbol\", s.\"CompanyName\", s.\"CurrentPrice\", s.\"MarketCap\", s.\"TraderId\" FROM \"Trader\" t" +
                " LEFT JOIN \"Stock\" s ON t.\"Id\" = s.\"TraderId\"" +
                "WHERE t.\"IsActive\" = @isActive AND s.\"IsActive\" = @isActive";
            command.Parameters.AddWithValue("@isActive", true);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var traderId = reader.GetGuid(0);
                    Trader? existingTrader = traders.FirstOrDefault(t => t.Id == traderId);

                    if (existingTrader == null)
                    {
                        var trader = new Trader
                        {
                            Id = traderId,
                            Name = reader.GetString(1),
                            DateOfBirth = reader.GetDateTime(2),
                            Stocks = new List<Stock>()
                        };

                        if (!reader.IsDBNull(3))
                        {
                            var stock = new Stock
                            {
                                Id = reader.GetGuid(3),
                                Symbol = reader.GetString(4),
                                CompanyName = reader.GetString(5),
                                CurrentPrice = reader.GetDouble(6),
                                MarketCap = (long)reader.GetDouble(7),
                                TraderId = reader.GetGuid(8)
                            };

                            trader.Stocks.Add(stock);
                        }

                        traders.Add(trader);
                    }
                    else if(!reader.IsDBNull(3))
                    {
                        var stock = new Stock
                        {
                            Id = reader.GetGuid(3),
                            Symbol = reader.GetString(4),
                            CompanyName = reader.GetString(5),
                            CurrentPrice = reader.GetDouble(6),
                            MarketCap = (long)reader.GetDouble(7),
                            TraderId = reader.GetGuid(8)
                        };

                        existingTrader.Stocks.Add(stock);
                    }
                }
            }

            return traders;
        }

        public Task<int> Post(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> Put(Stock stock, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
