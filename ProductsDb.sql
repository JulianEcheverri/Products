USE [Products]
GO
/****** Object:  Table [dbo].[Logins]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logins](
	[UserLoginId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[LogOutDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Logins] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Nationalities]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nationalities](
	[NationalityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Nationalities] PRIMARY KEY CLUSTERED 
(
	[NationalityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Persons]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[Document] [nvarchar](12) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
	[DateBirth] [datetime] NOT NULL,
	[Gender] [tinyint] NOT NULL,
	[NationalityId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Persons] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[CreatorUserId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductsReserve]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsReserve](
	[ProductReserveId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProductsReserve] PRIMARY KEY CLUSTERED 
(
	[ProductReserveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersClaims]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UsersClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersLogins]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UsersLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UsersRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VerificationKeys]    Script Date: 4/11/2019 12:36:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerificationKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[VerificationName] [nvarchar](max) NOT NULL,
	[Verification] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.VerificationKeys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Logins] ON 

GO
INSERT [dbo].[Logins] ([UserLoginId], [UserId], [LoginDate], [LogOutDate]) VALUES (1, 1, CAST(N'2019-11-04 12:25:50.000' AS DateTime), CAST(N'2019-11-04 12:26:22.557' AS DateTime))
GO
INSERT [dbo].[Logins] ([UserLoginId], [UserId], [LoginDate], [LogOutDate]) VALUES (2, 1, CAST(N'2019-11-04 12:26:29.387' AS DateTime), CAST(N'2019-11-04 12:29:14.170' AS DateTime))
GO
INSERT [dbo].[Logins] ([UserLoginId], [UserId], [LoginDate], [LogOutDate]) VALUES (3, 1, CAST(N'2019-11-04 12:29:24.007' AS DateTime), CAST(N'2019-11-04 12:31:12.650' AS DateTime))
GO
INSERT [dbo].[Logins] ([UserLoginId], [UserId], [LoginDate], [LogOutDate]) VALUES (4, 1, CAST(N'2019-11-04 12:31:50.803' AS DateTime), CAST(N'2019-11-04 12:34:45.760' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Logins] OFF
GO
SET IDENTITY_INSERT [dbo].[Nationalities] ON 

GO
INSERT [dbo].[Nationalities] ([NationalityId], [Name]) VALUES (2, N'Argentina')
GO
INSERT [dbo].[Nationalities] ([NationalityId], [Name]) VALUES (1, N'Colombia')
GO
SET IDENTITY_INSERT [dbo].[Nationalities] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

GO
INSERT [dbo].[Persons] ([PersonId], [Document], [Name], [LastName], [Age], [DateBirth], [Gender], [NationalityId], [CreationDate], [ModificationDate]) VALUES (1, N'1017211805', N'Julian', N'Echeverri', 26, CAST(N'1993-07-14 00:00:00.000' AS DateTime), 1, 1, CAST(N'2019-11-04 12:25:39.053' AS DateTime), CAST(N'2019-11-04 12:29:07.223' AS DateTime))
GO
INSERT [dbo].[Persons] ([PersonId], [Document], [Name], [LastName], [Age], [DateBirth], [Gender], [NationalityId], [CreationDate], [ModificationDate]) VALUES (2, N'000000', N'UserOne', N'UserOne', 29, CAST(N'1990-07-14 00:00:00.000' AS DateTime), 1, 2, CAST(N'2019-11-04 12:30:16.250' AS DateTime), NULL)
GO
INSERT [dbo].[Persons] ([PersonId], [Document], [Name], [LastName], [Age], [DateBirth], [Gender], [NationalityId], [CreationDate], [ModificationDate]) VALUES (3, N'111111', N'OtherUser', N'OtherUser', 119, CAST(N'1900-11-11 00:00:00.000' AS DateTime), 3, 1, CAST(N'2019-11-04 12:31:01.863' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Amount], [CreatorUserId], [CreationDate], [ModificationDate]) VALUES (1, N'Product 1', N'Product 1', 2, 1, CAST(N'2019-11-04 12:34:25.917' AS DateTime), NULL)
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Amount], [CreatorUserId], [CreationDate], [ModificationDate]) VALUES (2, N'Product 2', N'Product 2', 1, 1, CAST(N'2019-11-04 12:34:25.917' AS DateTime), NULL)
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Amount], [CreatorUserId], [CreationDate], [ModificationDate]) VALUES (3, N'Product 3', N'Product 3', 50, 1, CAST(N'2019-11-04 12:34:25.917' AS DateTime), NULL)
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Amount], [CreatorUserId], [CreationDate], [ModificationDate]) VALUES (4, N'Product 4', N'Product 4', 1, 1, CAST(N'2019-11-04 12:34:25.917' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (2, N'RegularUser')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([UserId], [PersonId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [Name]) VALUES (1, 1, N'julianecheverri@outlook.com', 0, N'AAwZzErDxBVrOJuwgKVyzwwrv41m1Z/LlCg91jm81P20jeBgK/fg1UCXkIahlWzGkg==', N'6cab2204-7d50-4f04-a683-0edd8d06f8b9', NULL, 0, 0, NULL, 0, 0, N'JulianEcheverri')
GO
INSERT [dbo].[Users] ([UserId], [PersonId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [Name]) VALUES (2, 2, N'otherone@gmail.com', 0, N'AOEjfbpi9spL9c7PGwp4Z/YYNQzR/FwauFh8D/amYImlTVgr+zceOM1U4ECKGWW3yg==', N'3c08efad-0955-4de8-a18c-08ccac23a9af', NULL, 0, 0, NULL, 0, 0, N'UserOne')
GO
INSERT [dbo].[Users] ([UserId], [PersonId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [Name]) VALUES (3, 3, N'julian.echeverri@pascualbravo.edu.co', 0, N'AIKGD+5BQVv37oB5nI8Qsz2k0hq2Vq9CW6Rol7raPoY31GnFXUL2Uz3FbX4caRSx+g==', N'54f7f221-b0a8-4ce6-9e12-20e685976754', NULL, 0, 0, NULL, 0, 0, N'OtherUser')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UsersRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[UsersRoles] ([UserId], [RoleId]) VALUES (2, 2)
GO
INSERT [dbo].[UsersRoles] ([UserId], [RoleId]) VALUES (3, 2)
GO
SET IDENTITY_INSERT [dbo].[VerificationKeys] ON 

GO
INSERT [dbo].[VerificationKeys] ([Id], [Key], [VerificationName], [Verification]) VALUES (1, N'0', N'JulianDavidEcheverriGarcia', N'JulianDavidEcheverriGarcia0')
GO
SET IDENTITY_INSERT [dbo].[VerificationKeys] OFF
GO
ALTER TABLE [dbo].[Logins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Logins_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Logins] CHECK CONSTRAINT [FK_dbo.Logins_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Persons_dbo.Nationalities_NationalityId] FOREIGN KEY([NationalityId])
REFERENCES [dbo].[Nationalities] ([NationalityId])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_dbo.Persons_dbo.Nationalities_NationalityId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Users_CreatorUserId] FOREIGN KEY([CreatorUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Users_CreatorUserId]
GO
ALTER TABLE [dbo].[ProductsReserve]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductsReserve_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductsReserve] CHECK CONSTRAINT [FK_dbo.ProductsReserve_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductsReserve]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductsReserve_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ProductsReserve] CHECK CONSTRAINT [FK_dbo.ProductsReserve_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Persons_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([PersonId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Persons_PersonId]
GO
ALTER TABLE [dbo].[UsersClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsersClaims_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersClaims] CHECK CONSTRAINT [FK_dbo.UsersClaims_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[UsersLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsersLogins_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersLogins] CHECK CONSTRAINT [FK_dbo.UsersLogins_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsersRoles_dbo.Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_dbo.UsersRoles_dbo.Roles_RoleId]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UsersRoles_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_dbo.UsersRoles_dbo.Users_UserId]
GO
