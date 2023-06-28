CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductID] INT NOT NULL,
	[Quantity] INT NOT NULL DEFAULT 1,
	[PurchasePrice] MONEY NOT NULL,
	[PurchaseDate] DATETIME2(7) NOT NULL DEFAULT getutcdate()
)
