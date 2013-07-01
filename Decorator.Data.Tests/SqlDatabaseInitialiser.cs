using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Decorator.Data.Tests
{
    public class SqlDatabaseInitialiser
    {
        public void InitialiseDatabase()
        {
            var cn = new SqlConnection(@"Data Source=localhost\sql2008r2;Initial Catalog=Decorator;Integrated Security=true");
            var assembly = Assembly.GetExecutingAssembly();
            var sr = new StreamReader(assembly.GetManifestResourceStream("Decorator.Data.Tests.CreateDatabase.sql"));
            var cmds = new List<SqlCommand>();
            string commandString = sr.ReadToEnd();

            foreach (var command in commandString.Replace("GO;", "|").Split('|'))
            {
                if (command != string.Empty)
                {
                    var cmd = new SqlCommand(command, cn);
                    cmds.Add(cmd);
                }
            }

            try
            {
                cn.Open();
                foreach (var cmd in cmds)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
