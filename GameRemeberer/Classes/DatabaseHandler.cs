using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameRemeberer
{
    public static class DatabaseHandler
    {
        public static string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\GameRemeberer\GameRemeberer\Database1.mdf;Integrated Security=True";
        public static void AddToDatabase(List<Game> games)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var sql = $"INSERT INTO Games(Name, Platform, ReleaseDate, Rating,  Owned) VALUES(@Name, @Platform, @ReleaseDate, @Rating, @Owned)";
                    conn.Execute(sql, games, tran);
                    tran.Commit();
                }
            }
        }

        public static void ReadGames(List<Game> games)
        {
            using (var conn = new SqlConnection(connString))
            {
                var sql = "select * from Games";
                var gamesFromDatabase = conn.Query<Game>(sql);
                foreach (Game game in gamesFromDatabase)
                {
                    games.Add(game);
                }
            }
        }
        public static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Games", con);
            con.Open();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public static void UpdateSingleRow(int ID, string update)
        {
            string sql = "UPDATE Games SET Owned = @Owned WHERE ID = @ID";
            using (IDbConnection con = new SqlConnection(connString))
            {
                int rowsAffected = con.Execute(sql, new {ID, Owned = update});
            }   
        }
    }
}
