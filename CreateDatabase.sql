USE [Master];

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'Decorator')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Decorator';
	ALTER DATABASE [Decorator] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [Decorator];
END;
GO;

CREATE DATABASE [Decorator] ;
GO;

USE [Decorator];
GO;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pizza')
BEGIN
	CREATE TABLE [dbo].[Pizza](
		[Id] uniqueidentifier NOT NULL PRIMARY KEY,
		[Size] int NULL,
		[Cheese] int NULL,
		[Tomato] int NULL,
		[OrderId] uniqueidentifier NULL
	); 
END;
GO;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PepperoniDecorator')
BEGIN
	CREATE TABLE [dbo].[PepperoniDecorator](
		[Id] uniqueidentifier NOT NULL PRIMARY KEY,
		[BasePizzaId] uniqueidentifier NULL,
		[ExtraSpicy] bit NULL,
		[OrderId] uniqueidentifier NULL
	);
END;
GO;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OliveDecorator')
BEGIN
	CREATE TABLE [dbo].[OliveDecorator](
		[Id] uniqueidentifier NOT NULL PRIMARY KEY,
		[BasePizzaId] uniqueidentifier NULL,
		[Colour] int NULL,
		[OrderId] uniqueidentifier NULL
	);
END;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Order')
BEGIN
	CREATE TABLE [dbo].[Order](
		[Id] uniqueidentifier NOT NULL PRIMARY KEY,
		[CustomerName] nvarchar(100) NULL,
		[DeliveryAddress] nvarchar(200) NULL
	);
END
GO;