using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace AccessMatrixHelper.DB.Method
{
    public static class MySQLMethod
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(AccessMatrixHelper.DB.Model.MySQLConnectionModel.connection);
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public async static Task Add(int ID, string Name, string ownerType)
        {            
            MySqlConnection con = GetConnection();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"Add{FirstCharToUpper(ownerType)}", con);
            
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("Name", Name);
                    cmd.ExecuteNonQuery();
            }
            catch{}
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public async static Task LogNewConnection(string URL)
        {
            MySqlConnection con = GetConnection();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"AddNewConnection", con);
            
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IP", AccessMatrixHelper.API.IP);
                cmd.Parameters.AddWithValue("URL", URL);
                cmd.ExecuteNonQuery();
            }
            catch{}
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public async static Task AddParam(int? ID, string Name, string Value, string ownerType, int ownerID)
        {
            MySqlConnection con = GetConnection();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"Add{FirstCharToUpper(ownerType)}Param", con);
            
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ParamID", ID);
                cmd.Parameters.AddWithValue("ParamName", Name);
                cmd.Parameters.AddWithValue("ParamValue", Value);
                cmd.Parameters.AddWithValue($"{FirstCharToUpper(ownerType)}ID", ownerID);
                cmd.ExecuteNonQuery();
            }
            catch{}
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public async static Task<int?> AddSystem(string Name)
        {
            int ID=0;
            MySqlConnection con = GetConnection();
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"AddSystem", con);
            
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", Name);
                    cmd.Parameters.Add(new MySqlParameter("ID", MySqlDbType.Int32));
                    cmd.Parameters["ID"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToInt32(cmd.Parameters["ID"].Value);
            }
            catch{}
            finally
            {
                con.Close();
                con.Dispose();
            }
                
            return ID;
        }

        public async static Task SaveSystem(DAM.Model.System system)
        {
            int? tmpID = null;
            int ID;
            while (tmpID == null)
            {
                tmpID = await AddSystem(system.Name);
            }
            ID = Convert.ToInt32(tmpID);

            foreach(DAM.Model.Param p in system.Params)
            {
                await AddParam(p.ID, p.Name, p.Value, "System", ID);
            }
            
            foreach(DAM.Model.Object o in system.Objects)
            {
                await Add(o.ID, o.Name, "Object");
                foreach(DAM.Model.Param p in o.Params)
                {
                    await AddParam(p.ID, p.Name, p.Value, "Object", o.ID);
                }
            }
            foreach(DAM.Model.User u in system.Users)
            {
                await Add(u.ID, u.Name, "User");
                foreach(DAM.Model.Param p in u.Params)
                {
                    await AddParam(p.ID, p.Name, p.Value, "User", u.ID);
                }
            }
        }
    }
}