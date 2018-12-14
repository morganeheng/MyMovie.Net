
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2017 16:00:23
-- Generated from EDMX file: C:\Users\Morgane\Desktop\Piscine\MovieNET\theoph_m\MovieNET\MovieNET\MovieNetModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MovieLibrary];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Comment_Movie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Movie];
GO
IF OBJECT_ID(N'[dbo].[FK_Comment_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_Director]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [FK_Movie_Director];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [FK_Movie_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_MovieType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [FK_Movie_MovieType];
GO
IF OBJECT_ID(N'[dbo].[FK_MovieActor_Actor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MovieActor] DROP CONSTRAINT [FK_MovieActor_Actor];
GO
IF OBJECT_ID(N'[dbo].[FK_MovieActor_Movie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MovieActor] DROP CONSTRAINT [FK_MovieActor_Movie];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Actor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actor];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[Director]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Director];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Movie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movie];
GO
IF OBJECT_ID(N'[dbo].[MovieActor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieActor];
GO
IF OBJECT_ID(N'[dbo].[MovieType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieType];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Actor'
CREATE TABLE [dbo].[Actor] (
    [Id_actor] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(50)  NOT NULL,
    [Lastname] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Comment'
CREATE TABLE [dbo].[Comment] (
    [Id_comment] int IDENTITY(1,1) NOT NULL,
    [Id_movie] int  NOT NULL,
    [Id_user] int  NOT NULL,
    [Comment1] nvarchar(max)  NOT NULL,
    [Rating] int  NOT NULL
);
GO

-- Creating table 'Director'
CREATE TABLE [dbo].[Director] (
    [Id_director] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(50)  NOT NULL,
    [Lastname] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [Id_Image] int IDENTITY(1,1) NOT NULL,
    [URL] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Movie'
CREATE TABLE [dbo].[Movie] (
    [Id_movie] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Synopsis] nvarchar(max)  NOT NULL,
    [Rating] float  NOT NULL,
    [Duration] time  NOT NULL,
    [Id_type] int  NOT NULL,
    [Id_director] int  NOT NULL,
    [Id_image] int  NOT NULL
);
GO

-- Creating table 'MovieType'
CREATE TABLE [dbo].[MovieType] (
    [Id_type] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id_user] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'MovieActor'
CREATE TABLE [dbo].[MovieActor] (
    [Actor_Id_actor] int  NOT NULL,
    [Movie_Id_movie] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_actor] in table 'Actor'
ALTER TABLE [dbo].[Actor]
ADD CONSTRAINT [PK_Actor]
    PRIMARY KEY CLUSTERED ([Id_actor] ASC);
GO

-- Creating primary key on [Id_comment] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [PK_Comment]
    PRIMARY KEY CLUSTERED ([Id_comment] ASC);
GO

-- Creating primary key on [Id_director] in table 'Director'
ALTER TABLE [dbo].[Director]
ADD CONSTRAINT [PK_Director]
    PRIMARY KEY CLUSTERED ([Id_director] ASC);
GO

-- Creating primary key on [Id_Image] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [PK_Image]
    PRIMARY KEY CLUSTERED ([Id_Image] ASC);
GO

-- Creating primary key on [Id_movie] in table 'Movie'
ALTER TABLE [dbo].[Movie]
ADD CONSTRAINT [PK_Movie]
    PRIMARY KEY CLUSTERED ([Id_movie] ASC);
GO

-- Creating primary key on [Id_type] in table 'MovieType'
ALTER TABLE [dbo].[MovieType]
ADD CONSTRAINT [PK_MovieType]
    PRIMARY KEY CLUSTERED ([Id_type] ASC);
GO

-- Creating primary key on [Id_user] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id_user] ASC);
GO

-- Creating primary key on [Actor_Id_actor], [Movie_Id_movie] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [PK_MovieActor]
    PRIMARY KEY CLUSTERED ([Actor_Id_actor], [Movie_Id_movie] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_movie] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_Comment_Movie]
    FOREIGN KEY ([Id_movie])
    REFERENCES [dbo].[Movie]
        ([Id_movie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comment_Movie'
CREATE INDEX [IX_FK_Comment_Movie]
ON [dbo].[Comment]
    ([Id_movie]);
GO

-- Creating foreign key on [Id_user] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_Comment_User]
    FOREIGN KEY ([Id_user])
    REFERENCES [dbo].[User]
        ([Id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comment_User'
CREATE INDEX [IX_FK_Comment_User]
ON [dbo].[Comment]
    ([Id_user]);
GO

-- Creating foreign key on [Id_director] in table 'Movie'
ALTER TABLE [dbo].[Movie]
ADD CONSTRAINT [FK_Movie_Director]
    FOREIGN KEY ([Id_director])
    REFERENCES [dbo].[Director]
        ([Id_director])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_Director'
CREATE INDEX [IX_FK_Movie_Director]
ON [dbo].[Movie]
    ([Id_director]);
GO

-- Creating foreign key on [Id_image] in table 'Movie'
ALTER TABLE [dbo].[Movie]
ADD CONSTRAINT [FK_Movie_Image]
    FOREIGN KEY ([Id_image])
    REFERENCES [dbo].[Image]
        ([Id_Image])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_Image'
CREATE INDEX [IX_FK_Movie_Image]
ON [dbo].[Movie]
    ([Id_image]);
GO

-- Creating foreign key on [Id_type] in table 'Movie'
ALTER TABLE [dbo].[Movie]
ADD CONSTRAINT [FK_Movie_MovieType]
    FOREIGN KEY ([Id_type])
    REFERENCES [dbo].[MovieType]
        ([Id_type])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Movie_MovieType'
CREATE INDEX [IX_FK_Movie_MovieType]
ON [dbo].[Movie]
    ([Id_type]);
GO

-- Creating foreign key on [Actor_Id_actor] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [FK_MovieActor_Actor]
    FOREIGN KEY ([Actor_Id_actor])
    REFERENCES [dbo].[Actor]
        ([Id_actor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Movie_Id_movie] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [FK_MovieActor_Movie]
    FOREIGN KEY ([Movie_Id_movie])
    REFERENCES [dbo].[Movie]
        ([Id_movie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieActor_Movie'
CREATE INDEX [IX_FK_MovieActor_Movie]
ON [dbo].[MovieActor]
    ([Movie_Id_movie]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------