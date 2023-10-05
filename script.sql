USE [ChuongTrinhDatVeXemPhim]
GO
/****** Object:  Table [dbo].[Categorys]    Script Date: 10/5/2023 3:32:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorys](
	[MaLoai] [int] NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_Categorys] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 10/5/2023 3:32:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[MaGhe] [int] NOT NULL,
	[TenGhe] [nvarchar](50) NULL,
	[HangGhe] [int] NULL,
	[SoGhe] [int] NULL,
	[LoaiGhe] [int] NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_Seats] PRIMARY KEY CLUSTERED 
(
	[MaGhe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD  CONSTRAINT [FK_Seats_Categorys] FOREIGN KEY([LoaiGhe])
REFERENCES [dbo].[Categorys] ([MaLoai])
GO
ALTER TABLE [dbo].[Seats] CHECK CONSTRAINT [FK_Seats_Categorys]
GO
