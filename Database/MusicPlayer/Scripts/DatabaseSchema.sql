USE master;
GO
DROP DATABASE IF EXISTS MUSICPLAYER;
GO
CREATE DATABASE MUSICPLAYER;
GO
USE MUSICPLAYER;
/****** Object:  Table [dbo].[Artist]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[ArtistID] [varchar](8) NOT NULL,
	[ArtistName] [nvarchar](max) NOT NULL,
	[Thumbnail] [varchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistSong]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistSong](
	[ArtistID] [varchar](8) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_ArtistSong] PRIMARY KEY CLUSTERED 
(
	[ArtistID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[UserID] [varchar](100) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
	[Count]  [int] NOT NULL,
 CONSTRAINT [PK_UserSong_History] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreID] [varchar](8) NOT NULL,
	[GenreName] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenreSong]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenreSong](
	[GenreID] [varchar](8) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_GenreSong] PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Like]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Like](
	[UserID] [varchar](100) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_UserSong] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlist]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlist](
	[PlaylistID] [varchar](8) NOT NULL,
	[PlaylistName] [nvarchar](max) NOT NULL,
	[UserID] [varchar](100) NOT NULL,
	[Thumbnail] [varchar](max) NULL,
	[SortDescription] [varchar](max) NULL,
	[IsPrivate] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlaylistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaylistSong]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaylistSong](
	[PlaylistID] [varchar](8) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_PlaylistSong] PRIMARY KEY CLUSTERED 
(
	[PlaylistID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Song]    Script Date: 5/7/2022 10:27:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Song](
	[SongID] [varchar](8) NOT NULL,
	[SongName] [nvarchar](max) NOT NULL,
	[Thumbnail] [varchar](max) NULL,
	[Link] [varchar](max) NULL,
	[LinkBeat] [varchar](max) NULL,
	[LinkLyric] [varchar](max) NULL,
	[Duration] [int] NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[Likes] [int] NULL,
	[Downloads] [int] NULL,
	[Listens] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsRecognizable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Artist] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Genre] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Playlist] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ArtistSong]  WITH CHECK ADD FOREIGN KEY([ArtistID])
REFERENCES [dbo].[Artist] ([ArtistID])
GO
ALTER TABLE [dbo].[ArtistSong]  WITH CHECK ADD FOREIGN KEY([SongID])
REFERENCES [dbo].[Song] ([SongID])
GO
ALTER TABLE [dbo].[GenreSong]  WITH CHECK ADD FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genre] ([GenreID])
GO
ALTER TABLE [dbo].[GenreSong]  WITH CHECK ADD FOREIGN KEY([SongID])
REFERENCES [dbo].[Song] ([SongID])
GO
ALTER TABLE [dbo].[Like]  WITH CHECK ADD FOREIGN KEY([SongID])
REFERENCES [dbo].[Song] ([SongID])
GO
ALTER TABLE [dbo].[PlaylistSong]  WITH CHECK ADD FOREIGN KEY([PlaylistID])
REFERENCES [dbo].[Playlist] ([PlaylistID])
GO
ALTER TABLE [dbo].[PlaylistSong]  WITH CHECK ADD FOREIGN KEY([SongID])
REFERENCES [dbo].[Song] ([SongID])
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD FOREIGN KEY([SongID])
REFERENCES [dbo].[Song] ([SongID])

