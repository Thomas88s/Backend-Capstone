USE [Master]
GO

IF db_id('StonkMarket') IS NULL
	CREATE DATABASE [StonkMarket]
GO

USE [StonkMarket]
GO

CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] int NOT NULL,
  [DisplayName] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [CreatedDate] datetime NOT NULL DEFAULT (sysdatetime()),
  [Email] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserStonk] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [StonkId] int NOT NULL,
  [UserId] int NOT NULL,
  [TopPerformer] bit NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [Stonk] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Price] decimal NOT NULL,
  [Date] datetime NOT NULL,
  [OneYear] decimal,
  [FiveYear] decimal,
  [TenYear] decimal
)
GO

CREATE TABLE [TopStonk] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [StonkId] int NOT NULL,
  [UserId] int NOT NULL,
  [PercentageIncrease] int NOT NULL
)
GO

CREATE TABLE [Message] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Content] nvarchar(255) NOT NULL,
  [Date] datetime NOT NULL,
  [SenderId] int NOT NULL,
  [ReceiverId] int NOT NULL,
  [CreatedDate] datetime DEFAULT (sysdatetime())
)
GO

ALTER TABLE [UserStonk] ADD FOREIGN KEY ([StonkId]) REFERENCES [Stonk] ([Id])
GO

ALTER TABLE [UserStonk] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [TopStonk] ADD FOREIGN KEY ([StonkId]) REFERENCES [Stonk] ([Id])
GO

ALTER TABLE [TopStonk] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Message] ADD FOREIGN KEY ([SenderId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Message] ADD FOREIGN KEY ([ReceiverId]) REFERENCES [User] ([Id])
GO
