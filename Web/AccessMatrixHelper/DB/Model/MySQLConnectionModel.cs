using MySql.Data.MySqlClient;

namespace AccessMatrixHelper.DB.Model
{
    public class MySQLConnectionModel
    {
        // public static bool isNeed { get; set;}
        private static string db{get{return "DAM_DB";}}
        private static string host{get{return "192.168.1.7";}}
        private static string port{get{return "3306";}}
        private static string user{get{return "apiserver";}}
        private static string password{get{return "api";}}
        
        public static string connection{get{return $"server={host};port={port};user={user};database={db};password={password};";}}
    }
}