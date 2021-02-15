USE [TechnicalCodingTest]
GO

/****** Object:  Table [dbo].[UserMaster]    Script Date: 14/02/2021 2:05:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserMaster](
	[UserMasterId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[UserPassword] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserMaster_UserMasterId] PRIMARY KEY CLUSTERED 
(
	[UserMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UserMaster_EmailID] UNIQUE NONCLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_Table_1_ModifiedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO

ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedTime]
GO


CREATE TABLE [dbo].[CarMaster](
	[CarMasterId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Color] [varchar](50) NOT NULL,
	[YearMade] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_CarMaster_CarMasterId] PRIMARY KEY CLUSTERED 
(
	[CarMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_CarMaster_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CarMaster] ADD  CONSTRAINT [DF_CarMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[CarMaster] ADD  CONSTRAINT [DF_CarMaster_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO

ALTER TABLE [dbo].[CarMaster] ADD  CONSTRAINT [DF_CarMaster_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedTime]
GO


