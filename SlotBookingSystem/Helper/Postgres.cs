using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Data.Common;
using NpgsqlTypes;

namespace SlotBookingSystem.Helper
{

    public class Postgres : IDisposable
    {
        public NpgsqlConnection pgConn;
        public NpgsqlCommand pgCommand;

        public Postgres()
        {
            pgCommand = new NpgsqlCommand();
            pgConn = new NpgsqlConnection
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PGConnectionString"].ToString()
            };
            pgCommand.Connection = pgConn;
        }

        public void OpenConnection(string query)
        {
            try
            {
                pgConn.Open();
            }
            catch(NpgsqlException e)
            {
                throw e;
            }
        }
        
        public void CloseConnection()
        {
            if(pgConn !=null)
            {
                if(pgConn.State == ConnectionState.Open)
                {
                    pgConn.Close();
                }
                pgCommand.Dispose();
            }
        }

        public DataTable ExecuteReader(string query)
        {
            DbDataReader result;
            DataTable objDt = new DataTable();
            pgCommand.CommandText = query;
            try
            {
                OpenConnection(query);
                result = pgCommand.ExecuteReader();
                objDt.Load(result);
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
            return objDt;
        }

        public int ExecuteUpdate(string query)
        {
            int result;
            pgCommand.CommandText = query;
            try
            {
                OpenConnection(query);
                result = pgCommand.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public string ExecuteScalar(string query)
        {
            string result;
            pgCommand.CommandText = query;
            try
            {
                OpenConnection(query);
                result = pgCommand.ExecuteScalar().ToString();
                if (result != null)
                    result = result.ToString();
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public void AddParameter(string parameterName,NpgsqlDbType type, object parameterValue)
        {
            try
            {
                if(parameterValue!=null)
                {
                    NpgsqlParameter param = new NpgsqlParameter(parameterName, parameterValue);
                    param.NpgsqlDbType = type;
                    pgCommand.Parameters.Add(param);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}