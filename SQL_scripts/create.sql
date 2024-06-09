CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "Stocks"(
	"Id" UUID NOT NULL DEFAULT uuid_generate_v4() PRIMARY KEY,
	"Symbol" VARCHAR(10) NOT NULL,
	"CompanyName" VARCHAR(30) NOT NULL,
	"CurrentPrice" NUMERIC(15, 2) NOT NULL,
	"MarketCap" BIGINT NOT NULL,
	"TraderId" UUID,
	"IsActive" BOOL NOT NULL DEFAULT TRUE,
	CONSTRAINT "FK_Stocks_Trader_TraderId"
		FOREIGN KEY ("TraderId")
		REFERENCES "Trader"("Id")
		
)

CREATE TABLE "Trader"(
	"Id" UUID NOT NULL DEFAULT uuid_generate_v4() PRIMARY KEY,
	"Name" VARCHAR(30) NOT NULL,
	"DateOfBirth" TIMESTAMP NOT NULL,
	"IsActive" BOOL NOT NULL DEFAULT TRUE
)