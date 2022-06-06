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
        AlbumArtist = 7
    }
    class Program
    {
        static private readonly string removedPath = @"DataProcessor\bin\Debug\netcoreapp3.1";
        static private readonly string dataPath = @"Database\MusicPlayer\Data";
        static private readonly string sqlDataPath = @"Database\MusicPlayer\Scripts\DatabaseData.sql";
        static private readonly string[] tables = { "Genre", "Artist", "Song", "GenreSong", "ArtistSong"};
        static private readonly string[] queryPatterns = {
            "INSERT [dbo].[{0}] ([{0}ID], [GenreName], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', 0);",
            "INSERT [dbo].[{0}] ([{0}ID], [ArtistName], [Thumbnail], [IsDeleted]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', 0)",
            "INSERT [dbo].[{0}] ([{0}ID], [SongName], [Thumbnail], [Link], [LinkBeat], [LinkLyric], [Duration], [ReleaseDate], [Likes], [Downloads], [Listens], [IsDeleted], [IsRecognizable]) VALUES (N\'{1}\', N\'{2}\', N\'{3}\', N\'{4}\', N\'{5}\', N\'{6}\', 0, \'{7}\', 0, \'{8}\', \'{9}\', 0, 1)",
            "INSERT [dbo].[{0}] ([GenreID], [SongID]) VALUES (N\'{1}\', N\'{2}\');",
            "INSERT [dbo].[{0}] ([ArtistID], [SongID]) VALUES (N\'{1}\', N\'{2}\');",
        };
        static private string fullDataPath;
        static private string fullDataSqlPath;
        static void Main(string[] args)
        {
            fullDataPath = Directory.GetCurrentDirectory().Replace(removedPath, dataPath);
            fullDataSqlPath = Directory.GetCurrentDirectory().Replace(removedPath, sqlDataPath);
            for(int i = 0; i < 3; i++)
            {
                GenBaseInsertQueries(i);
            }
            GenGenreSongInsertQueries();
            GenArtistSongInsertQueries();
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

    }
}
