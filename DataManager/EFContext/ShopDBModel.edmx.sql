
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/04/2015 15:28:22
-- Generated from EDMX file: E:\DEVELOP\shop\DataManager\EFContext\ShopDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Shop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GoodCategory_Good]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GoodCategory] DROP CONSTRAINT [FK_GoodCategory_Good];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodCategory_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GoodCategory] DROP CONSTRAINT [FK_GoodCategory_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseCategory_Purchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseCategory] DROP CONSTRAINT [FK_PurchaseCategory_Purchase];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseCategory_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseCategory] DROP CONSTRAINT [FK_PurchaseCategory_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodPurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseSet] DROP CONSTRAINT [FK_GoodPurchase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GoodSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodSet];
GO
IF OBJECT_ID(N'[dbo].[CategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorySet];
GO
IF OBJECT_ID(N'[dbo].[PurchaseSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseSet];
GO
IF OBJECT_ID(N'[dbo].[GoodCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodCategory];
GO
IF OBJECT_ID(N'[dbo].[PurchaseCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseCategory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GoodSet'
CREATE TABLE [dbo].[GoodSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PurchaseSet'
CREATE TABLE [dbo].[PurchaseSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GoodId] int  NOT NULL
);
GO

-- Creating table 'GoodCategory'
CREATE TABLE [dbo].[GoodCategory] (
    [Good_Id] int  NOT NULL,
    [Category_Id] int  NOT NULL
);
GO

-- Creating table 'PurchaseCategory'
CREATE TABLE [dbo].[PurchaseCategory] (
    [Purchase_Id] int  NOT NULL,
    [Category_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'GoodSet'
ALTER TABLE [dbo].[GoodSet]
ADD CONSTRAINT [PK_GoodSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PurchaseSet'
ALTER TABLE [dbo].[PurchaseSet]
ADD CONSTRAINT [PK_PurchaseSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Good_Id], [Category_Id] in table 'GoodCategory'
ALTER TABLE [dbo].[GoodCategory]
ADD CONSTRAINT [PK_GoodCategory]
    PRIMARY KEY CLUSTERED ([Good_Id], [Category_Id] ASC);
GO

-- Creating primary key on [Purchase_Id], [Category_Id] in table 'PurchaseCategory'
ALTER TABLE [dbo].[PurchaseCategory]
ADD CONSTRAINT [PK_PurchaseCategory]
    PRIMARY KEY CLUSTERED ([Purchase_Id], [Category_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Good_Id] in table 'GoodCategory'
ALTER TABLE [dbo].[GoodCategory]
ADD CONSTRAINT [FK_GoodCategory_Good]
    FOREIGN KEY ([Good_Id])
    REFERENCES [dbo].[GoodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Category_Id] in table 'GoodCategory'
ALTER TABLE [dbo].[GoodCategory]
ADD CONSTRAINT [FK_GoodCategory_Category]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodCategory_Category'
CREATE INDEX [IX_FK_GoodCategory_Category]
ON [dbo].[GoodCategory]
    ([Category_Id]);
GO

-- Creating foreign key on [Purchase_Id] in table 'PurchaseCategory'
ALTER TABLE [dbo].[PurchaseCategory]
ADD CONSTRAINT [FK_PurchaseCategory_Purchase]
    FOREIGN KEY ([Purchase_Id])
    REFERENCES [dbo].[PurchaseSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Category_Id] in table 'PurchaseCategory'
ALTER TABLE [dbo].[PurchaseCategory]
ADD CONSTRAINT [FK_PurchaseCategory_Category]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseCategory_Category'
CREATE INDEX [IX_FK_PurchaseCategory_Category]
ON [dbo].[PurchaseCategory]
    ([Category_Id]);
GO

-- Creating foreign key on [GoodId] in table 'PurchaseSet'
ALTER TABLE [dbo].[PurchaseSet]
ADD CONSTRAINT [FK_GoodPurchase]
    FOREIGN KEY ([GoodId])
    REFERENCES [dbo].[GoodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodPurchase'
CREATE INDEX [IX_FK_GoodPurchase]
ON [dbo].[PurchaseSet]
    ([GoodId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------