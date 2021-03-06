using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessor
{
    enum TABLE
    {
        Genre = 0,
        Artist = 1,
        Song = 2,
        Album = 3,
        GenreSong = 4,
        ArtistSong = 5,
        AlbumSong = 6,
        AlbumArtist = 7,
        User = 8,
        History = 9,
        Playlist = 10,
        PlaylistSong = 11,
    }
    class Program
    {
        static private readonly string removedPath = @"DataProcessor\bin\Debug\netcoreapp3.1";
        static private readonly string dataPath = @"Database\MusicPlayer\Data";
        static private readonly string sqlDataPath = @"Database\MusicPlayer\Scripts\DatabaseData.sql";
        static private readonly string[] tables = { "Genre", "Artist", "Song", "GenreSong", "ArtistSong", "User", "History", "Playlist", "PlaylistSong"};
        static private readonly string[] queryPatterns = {
            "INSERT [dbo].[{0}] ([{0}ID], [GenreName], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', 0);",
            "INSERT [dbo].[{0}] ([{0}ID], [ArtistName], [Thumbnail], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', 0)",
            "INSERT [dbo].[{0}] ([{0}ID], [SongName], [Thumbnail], [Link], [LinkBeat], [LinkLyric], [Duration], [ReleaseDate], [Likes], [Downloads], [Listens], [IsDeleted], [IsRecognizable]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', N\'{4}\', N\'{5}\', N\'{6}\', 0, \'{7}\', 0, \'{8}\', \'{9}\', 0, 1)",
            "INSERT [dbo].[{0}] ([GenreID], [SongID]) VALUES (N\'{1}\', N\'{2}\');",
            "INSERT [dbo].[{0}] ([ArtistID], [SongID]) VALUES (N\'{1}\', N\'{2}\');",
            "INSERT [dbo].[{0}] ([{0}ID], [FirstName], [LastName], [{0}Name], [Password], [Avatar], [Email], [DateOfBirth], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', N\'{4}\', N\'{5}\', N\'{6}\', N\'{7}\', N\'{8}\', 0)",
            "INSERT [dbo].[{0}] ([UserID], [SongID], [Count]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\');",
            "INSERT [dbo].[{0}] ([{0}ID], [PlaylistName], [UserID], [Thumbnail], [SortDescription], [IsPrivate], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', N\'{4}\', N\'{5}\', N\'{6}\', 0);",
            "INSERT [dbo].[{0}] ([PlaylistID], [SongID]) VALUES (N\'{1}\', N\'{2}\');",
        };
        static private string fullDataPath;
        static private string fullDataSqlPath;
        static void Main(string[] args)
        {
            fullDataPath = Directory.GetCurrentDirectory().Replace(removedPath, dataPath);
            fullDataSqlPath = Directory.GetCurrentDirectory().Replace(removedPath, sqlDataPath);
            File.AppendAllText(fullDataSqlPath, "DELETE FROM [dbo].[Like]\nDELETE FROM [dbo].[PlaylistSong]\nDELETE FROM [dbo].[ArtistSong]\nDELETE FROM [dbo].[History]\nDELETE FROM [dbo].[GenreSong]\nDELETE FROM [dbo].[Playlist]\nDELETE FROM [dbo].[Genre]\nDELETE FROM [dbo].[Artist]\nDELETE FROM [dbo].[Song]\n");
            for (int i = 0; i < 3; i++)
            {
                GenBaseInsertQueries(i);
            }
            GenGenreSongInsertQueries();
            GenArtistSongInsertQueries();
            GenUserInsertQueries();
            GenHistoryInsertQueries();
            GenPlaylistInsertQueries();
            GenPlaylistSongInsertQueries();
            //GenSongInsertQueries();
        }

        static void GenBaseInsertQueries(int index)
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[{tables[index]}]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + tables[index].ToLower() + ".json"));

            foreach (var item in list)
            {
                string row = "";
                switch ((TABLE)index)
                {
                    case TABLE.Genre:
                        row = string.Format(queryPatterns[index], tables[index], item["ID"], item["Name"].ToString().Replace("'", "''"));
                        break;
                    case TABLE.Artist:
                        row = string.Format(queryPatterns[index], tables[index], item["ID"], item["Name"].ToString().Replace("'", "''"),
                        item["Thumbnail"].ToString().Contains("artist_default_2.png") ? item["Thumbnail"] : item["Thumbnail"].ToString().Replace("w360_r1x1", "w600_r1x1"));
                        break;
                    case TABLE.Song:
                        Random rand = new Random();
                        row = string.Format(queryPatterns[index], tables[index], item["Name"], item["Title"].ToString().Replace("'", "''"),
                        item["Thumbnail"],
                        item["Link"], item["LinkBeat"], item["LinkLyric"], new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((long)item["ReleaseDate"]), rand.Next(0, 1000), rand.Next(0, 10000));
                        break;
                    default:
                        break;
                }
                File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }

        static void GenGenreSongInsertQueries()
        {
            int index = 2;
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[GenreSong]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + tables[index].ToLower() + ".json"));

            foreach (var item in list)
            {
                string row = "";
                foreach (var genre in item["Genres"])
                {
                    row = string.Format(queryPatterns[3], tables[3], genre["id"], item["Name"]);
                    File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
                }
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }

        static void GenArtistSongInsertQueries()
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[ArtistSong]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + "artist_song.json"));

            foreach (var item in list)
            {
                string row = "";
                foreach (var song in item["Songs"])
                {
                    row = string.Format(queryPatterns[4], tables[4], item["ArtistID"], song["Name"]);
                    File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
                }
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }

        static void GenUserInsertQueries()
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[User]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + "user.json"));

            foreach (var item in list)
            {
                string row = "";
                Random randU = new Random();
                row = string.Format(queryPatterns[5], tables[5], item["UserId"], item["FirstName"],
                     item["LastName"], item["Username"], item["Password"],
                item["Avatar"], item["Email"], new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((long)item["DateOfBirth"]), randU.Next(0, 1000), randU.Next(0, 10000));
                File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }


        static void GenHistoryInsertQueries()
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[History]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + "history.json"));

            foreach (var item in list)
            {
                string row = "";
                row = string.Format(queryPatterns[6], tables[6], item["UserId"], item["SongId"],
                     item["Count"]);
                File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }

        static void GenPlaylistInsertQueries()
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[Playlist]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + "playlist.json"));

            foreach (var item in list)
            {
                string row = "";
                row = string.Format(queryPatterns[7], tables[7], item["ID"], item["Title"], item["UserID"], item["Thumbnail"], item["SortDescription"],
                     item["IsPrivate"]);
                File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }

        static void GenPlaylistSongInsertQueries()
        {
            File.AppendAllText(fullDataSqlPath, $"/****** Object:  Table [dbo].[PlaylistSong]    Script Date: {DateTime.Now} ******/\n" + Environment.NewLine);
            JToken list = JToken.Parse(File.ReadAllText(fullDataPath + "\\" + "playlist_song.json"));

            foreach (var item in list)
            {
                string row = "";
                foreach (var song in item["Songs"])
                {
                    row = string.Format(queryPatterns[8], tables[8], item["PlaylistID"], song["Name"]);
                    File.AppendAllText(fullDataSqlPath, row + Environment.NewLine);
                }
            }
            File.AppendAllText(fullDataSqlPath, "\n" + Environment.NewLine);
        }
        
    }
}
