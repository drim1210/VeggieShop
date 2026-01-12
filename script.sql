USE [master]
GO
/****** Object:  Database [VeggieShop]    Script Date: 12/01/2026 8:01:31 CH ******/
CREATE DATABASE [VeggieShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VeggieShop', FILENAME = N'D:\Program Files D\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\VeggieShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VeggieShop_log', FILENAME = N'D:\Program Files D\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\VeggieShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [VeggieShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VeggieShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VeggieShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VeggieShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VeggieShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VeggieShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VeggieShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [VeggieShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VeggieShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VeggieShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VeggieShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VeggieShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VeggieShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VeggieShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VeggieShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VeggieShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VeggieShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VeggieShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VeggieShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VeggieShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VeggieShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VeggieShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VeggieShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VeggieShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VeggieShop] SET RECOVERY FULL 
GO
ALTER DATABASE [VeggieShop] SET  MULTI_USER 
GO
ALTER DATABASE [VeggieShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VeggieShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VeggieShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VeggieShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VeggieShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VeggieShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VeggieShop', N'ON'
GO
ALTER DATABASE [VeggieShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [VeggieShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [VeggieShop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryCode] [nvarchar](20) NOT NULL,
	[CategoryName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CategoryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [nvarchar](20) NOT NULL,
	[CustomerName] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](300) NULL,
	[Points] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoodsReceiptDetails]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsReceiptDetails](
	[ReceiptDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiptDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoodsReceipts]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsReceipts](
	[ReceiptId] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptCode] [nvarchar](20) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[ReceiptDate] [datetime2](7) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Note] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ReceiptCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](20) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Unit] [nvarchar](30) NOT NULL,
	[PurchasePrice] [decimal](18, 2) NOT NULL,
	[SalePrice] [decimal](18, 2) NOT NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesInvoiceDetails]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoiceDetails](
	[InvoiceDetailId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesInvoices]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceCode] [nvarchar](50) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Note] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[InvoiceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierCode] [nvarchar](20) NOT NULL,
	[SupplierName] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SupplierCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/01/2026 8:01:31 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PasswordHash] [varbinary](32) NOT NULL,
	[PasswordSalt] [varbinary](16) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[RoleName] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_CreatedAt]  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_Points]  DEFAULT ((0)) FOR [Points]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CreatedAt]  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GoodsReceipts] ADD  DEFAULT (sysdatetime()) FOR [ReceiptDate]
GO
ALTER TABLE [dbo].[GoodsReceipts] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Unit]  DEFAULT (N'kg') FOR [Unit]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Purchase]  DEFAULT ((0)) FOR [PurchasePrice]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Sale]  DEFAULT ((0)) FOR [SalePrice]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Stock]  DEFAULT ((0)) FOR [StockQty]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_CreatedAt]  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[Suppliers] ADD  CONSTRAINT [DF_Suppliers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Suppliers] ADD  CONSTRAINT [DF_Suppliers_CreatedAt]  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedAt]  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('Staff') FOR [RoleName]
GO
ALTER TABLE [dbo].[GoodsReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_GRD_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[GoodsReceiptDetails] CHECK CONSTRAINT [FK_GRD_Product]
GO
ALTER TABLE [dbo].[GoodsReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_GRD_Receipt] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[GoodsReceipts] ([ReceiptId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GoodsReceiptDetails] CHECK CONSTRAINT [FK_GRD_Receipt]
GO
ALTER TABLE [dbo].[GoodsReceipts]  WITH CHECK ADD  CONSTRAINT [FK_GR_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
GO
ALTER TABLE [dbo].[GoodsReceipts] CHECK CONSTRAINT [FK_GR_Supplier]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_Products]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[SalesInvoices] ([InvoiceId])
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoices]
GO
ALTER TABLE [dbo].[SalesInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoices_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesInvoices] CHECK CONSTRAINT [FK_SalesInvoices_Customers]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
USE [master]
GO
ALTER DATABASE [VeggieShop] SET  READ_WRITE 
GO
