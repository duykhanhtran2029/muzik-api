/****** Object:  Table [dbo].[Album]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[AlbumID] [varchar](8) NOT NULL,
	[AlbumName] [nvarchar](max) NOT NULL,
	[ThumbnailS] [varchar](max) NULL,
	[ThumbnailM] [varchar](max) NULL,
	[ThumbnailL] [varchar](max) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlbumArtist]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumArtist](
	[AlbumID] [varchar](8) NOT NULL,
	[ArtistID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_AlbumArtist] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC,
	[ArtistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlbumSong]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumSong](
	[AlbumID] [varchar](8) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_AlbumSong] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[ArtistID] [varchar](8) NOT NULL,
	[ArtistName] [nvarchar](max) NOT NULL,
	[ThumbnailS] [varchar](max) NULL,
	[ThumbnailM] [varchar](max) NULL,
	[ThumbnailL] [varchar](max) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistSong]    Script Date: 5/6/2022 9:47:58 PM ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreID] [varchar](8) NOT NULL,
	[GenreName] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenreSong]    Script Date: 5/6/2022 9:47:58 PM ******/
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
/****** Object:  Table [dbo].[Like]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Like](
	[UserID] [varchar](8) NOT NULL,
	[SongID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Like] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlist]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlist](
	[PlaylistID] [varchar](8) NOT NULL,
	[PlaylistName] [nvarchar](max) NOT NULL,
	[UserID] [varchar](8) NOT NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PlaylistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaylistSong]    Script Date: 5/6/2022 9:47:58 PM ******/
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
/****** Object:  Table [dbo].[Song]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Song](
	[SongID] [varchar](8) NOT NULL,
	[SongName] [nvarchar](max) NOT NULL,
	[ThumbnailS] [varchar](max) NULL,
	[ThumbnailM] [varchar](max) NULL,
	[ThumbnailL] [varchar](max) NULL,
	[Link] [varchar](max) NULL,
	[LinkBeat] [varchar](max) NULL,
	[LinkLyric] [varchar](max) NULL,
	[Duration] [int] NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[Likes] [int] NULL,
	[Downloads] [int] NULL,
	[Listens] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/6/2022 9:47:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [varchar](8) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Userame] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Avatar] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[DateOfBirth] [smalldatetime] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Album] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Artist] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Genre] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Playlist] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [Duration]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [Likes]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [Downloads]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [Listens]
GO
ALTER TABLE [dbo].[Song] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Playlist]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])

