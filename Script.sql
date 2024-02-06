
USE master

GO

CREATE DATABASE DbTektonLabs

GO

USE DbTektonLabs

GO

CREATE TABLE Product
( 
	ProductId			int IDENTITY ( 1,1 ) ,
	Name                varchar(50)  NOT NULL ,
	Description         varchar(100)  NOT NULL ,
	Stock				int  NOT NULL ,
	Price				decimal(10,2)  NULL ,
	Status				char(1)  NOT NULL ,
	CreationDate        datetime  NOT NULL ,
	CreationUser        varchar(30)  NOT NULL ,
	ModificationDate    datetime  NULL ,
	ModificationUser    varchar(30)  NULL 
)

GO

ALTER TABLE Product ADD CONSTRAINT XPKProduct PRIMARY KEY  CLUSTERED (ProductId ASC)

GO

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Stock]
           ,[Price]
           ,[Status]
           ,[CreationDate]
           ,[CreationUser])
     VALUES
           ('Mouse'
           ,'Mouse inalambrico'
           ,10
           ,499.99
           ,'1'
           ,GETDATE()
           ,'CVALLEJO')

GO

CREATE OR ALTER PROCEDURE [dbo].[SelectProductById]
@ProductId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT [ProductId]
		  ,[Name]
		  ,[Description]
		  ,[Stock]
		  ,[Price]
		  ,[Status]
		  ,[CreationDate]
		  ,[CreationUser]
		  ,[ModificationDate]
		  ,[ModificationUser]
	  FROM [dbo].[Product]
	WHERE 
		[ProductId] = @ProductId
END

GO

CREATE OR ALTER PROCEDURE [dbo].[InsertProduct]
@ProductId INT OUT,
@Name VARCHAR(50),
@Description VARCHAR(100),
@Stock INT,
@Price DECIMAL(10,2),
@Status CHAR(1),
@CreationUser VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Stock]
           ,[Price]
           ,[Status]
           ,[CreationDate]
           ,[CreationUser])
     VALUES
           (@Name
           ,@Description
           ,@Stock
           ,@Price
           ,@Status
           ,GETDATE()
           ,@CreationUser)

	SET @ProductId = SCOPE_IDENTITY();
END

GO

CREATE OR ALTER PROCEDURE [dbo].[UpdateProduct]
@ProductId INT,
@Name VARCHAR(50),
@Description VARCHAR(100),
@Stock INT,
@Price DECIMAL(10,2),
@Status CHAR(1),
@ModificationUser VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Product]
	SET [Name] = @Name
      ,[Description] = @Description
      ,[Stock] = @Stock
      ,[Price] = @Price
      ,[Status] = @Status
      ,[ModificationDate] = GETDATE()
      ,[ModificationUser] = @ModificationUser
	WHERE 
		[ProductId] = @ProductId
END


GO


SELECT * FROM [dbo].[Product]