USE [master]
GO
/****** Object:  Database [CookBook]    Script Date: 03.03.2021 1:35:57 ******/
CREATE DATABASE [CookBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Receipt', FILENAME = N'C:\Users\Пользователь\Receipt.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Receipt_log', FILENAME = N'C:\Users\Пользователь\Receipt_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CookBook] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CookBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CookBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CookBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CookBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CookBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CookBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [CookBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CookBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CookBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CookBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CookBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CookBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CookBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CookBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CookBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CookBook] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CookBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CookBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CookBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CookBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CookBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CookBook] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CookBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CookBook] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CookBook] SET  MULTI_USER 
GO
ALTER DATABASE [CookBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CookBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CookBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CookBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CookBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CookBook] SET QUERY_STORE = OFF
GO
USE [CookBook]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CookBook]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 03.03.2021 1:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[User_id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Time] [time](7) NULL,
	[Callories] [int] NULL,
	[Proteins] [decimal](18, 0) NULL,
	[Fats] [decimal](18, 0) NULL,
	[Carbohydrates] [decimal](18, 0) NULL,
	[Ingredients] [nvarchar](max) NULL,
	[Steps] [nvarchar](max) NULL,
	[Image] [varbinary](max) NULL,
	[Date_create] [smalldatetime] NOT NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Receipt_1] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC,
	[Name] ASC,
	[Date_create] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03.03.2021 1:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Users] FOREIGN KEY([User_id])
REFERENCES [dbo].[Users] ([User_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Users]
GO
USE [master]
GO
ALTER DATABASE [CookBook] SET  READ_WRITE 
GO
