#r "nuget: Microsoft.Data.Sqlite, 9.0.0"
using Microsoft.Data.Sqlite;

var conn = new SqliteConnection("Data Source=Torchbearer.Api/Torchbearer.db");
conn.Open();

var cmd = conn.CreateCommand();
cmd.CommandText = "INSERT OR IGNORE INTO PlayerRoles (PlayerId, RoleId) VALUES (4, 3)";
cmd.ExecuteNonQuery();
cmd.CommandText = "INSERT OR IGNORE INTO PlayerRoles (PlayerId, RoleId) VALUES (4, 2)";
cmd.ExecuteNonQuery();

Console.WriteLine("Admin and DM roles assigned to user 4");
conn.Close();
