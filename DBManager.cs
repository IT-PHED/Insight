using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Data;
using ERDBManager;
//using Oracle.DataAccess.Client;

namespace PHEDServe
{
   
        public sealed class DBManager : IDBManager, IDisposable
        {
            private IDbConnection idbConnection;

            private IDataReader idataReader;

            private IDbCommand idbCommand;

            private DataProvider providerType;

            private IDbTransaction idbTransaction = null;

            public IDbDataParameter[] idbParameters = null;

            private string strConnection;

            public IDbCommand Command
            {
                get
                {
                    return this.idbCommand;
                }
            }

            public IDbConnection Connection
            {
                get
                {
                    return this.idbConnection;
                }
            }

            public string ConnectionString
            {
                get
                {
                    return this.strConnection;
                }
                set
                {
                    this.strConnection = value;
                }
            }

            public IDataReader DataReader
            {
                get
                {
                    return JustDecompileGenerated_get_DataReader();
                }
                set
                {
                    JustDecompileGenerated_set_DataReader(value);
                }
            }

            public IDataReader JustDecompileGenerated_get_DataReader()
            {
                return this.idataReader;
            }

            public void JustDecompileGenerated_set_DataReader(IDataReader value)
            {
                this.idataReader = value;
            }

            public IDbDataParameter[] Parameters
            {
                get
                {
                    return this.idbParameters;
                }
            }

            public DataProvider ProviderType
            {
                get
                {
                    return this.providerType;
                }
                set
                {
                    this.providerType = value;
                }
            }

            public IDbTransaction Transaction
            {
                get
                {
                    return this.idbTransaction;
                }
            }

            public DBManager()
            {
            }

            public DBManager(DataProvider providerType)
            {
                this.providerType = providerType;
            }

            public DBManager(DataProvider providerType, string connectionString)
            {
                this.providerType = providerType;
                this.strConnection = connectionString;
            }

            public void AddParameters(int index, string paramName, object objValue, object paramDbType)
            {
                if (index < (int)this.idbParameters.Length)
                {
                    this.idbParameters[index].ParameterName = paramName;
                    DBManagerFactory.SetParameterType(this.providerType, paramDbType, this.idbParameters[index]);
                    if (objValue != null)
                    {
                        this.idbParameters[index].Value = objValue;
                    }
                }
            }

            public void AddParameters(int index, string paramName, object objValue, object paramDbType, ParameterDirection paramDirection)
            {
                if (index < (int)this.idbParameters.Length)
                {
                    this.idbParameters[index].ParameterName = paramName;
                    DBManagerFactory.SetParameterType(this.providerType, paramDbType, this.idbParameters[index]);
                    this.idbParameters[index].Direction = paramDirection;
                    if (objValue != null)
                    {
                        this.idbParameters[index].Value = objValue;
                    }
                }
            }

            private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
            {
                IDbDataParameter[] dbDataParameterArray = commandParameters;
                for (int i = 0; i < (int)dbDataParameterArray.Length; i++)
                {
                    IDbDataParameter value = dbDataParameterArray[i];
                    if ((value.Direction != ParameterDirection.InputOutput ? false : value.Value == null))
                    {
                        value.Value = DBNull.Value;
                    }
                    command.Parameters.Add(value);
                }
            }

            public void BeginTransaction()
            {
                if (this.idbTransaction == null)
                {
                    this.idbTransaction = DBManagerFactory.GetTransaction(this.ProviderType);
                }
                this.idbCommand.Transaction = this.idbTransaction;
            }

            public void Close()
            {
                if (this.idbConnection != null && this.idbConnection.State != ConnectionState.Closed)
                {
                    this.idbConnection.Close();
                    this.idbConnection.Dispose();
                    if (this.providerType == DataProvider.Oracle)
                    {
                        OracleConnection.ClearPool((OracleConnection)this.idbConnection);
                    }
                }
            }

            public void CloseReader()
            {
                if (this.DataReader != null)
                {
                    this.DataReader.Close();
                }
            }

            public void CommitTransaction()
            {
                if (this.idbTransaction != null)
                {
                    this.idbTransaction.Commit();
                }
                this.idbTransaction = null;
            }

            public void CreateParameters(int paramsCount)
            {
                this.idbParameters = new IDbDataParameter[paramsCount];
                this.idbParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
                this.Close();
                this.idbConnection.Dispose();
                this.idbCommand.Dispose();
                this.idbTransaction.Dispose();
                this.idbCommand = null;
                this.idbTransaction = null;
                this.idbConnection = null;
            }

            public DataSet ExecuteDataSet(CommandType commandType, string commandText)
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
                dataAdapter.SelectCommand = this.idbCommand;
                DataSet dataSet = new DataSet()
                {
                    EnforceConstraints = false
                };
                dataAdapter.Fill(dataSet);
                this.idbCommand.Parameters.Clear();
                return dataSet;
            }

            public int ExecuteNonQuery(CommandType commandType, string commandText)
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                int num = this.idbCommand.ExecuteNonQuery();
                this.idbCommand.Parameters.Clear();
                return num;
            }

            public IDataReader ExecuteReader(CommandType commandType, string commandText)
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                this.idbCommand.Connection = this.Connection;
                this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                this.DataReader = this.idbCommand.ExecuteReader();
                this.idbCommand.Parameters.Clear();
                return this.DataReader;
            }

            public object ExecuteScalar(CommandType commandType, string commandText)
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                this.PrepareCommand(this.idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                object obj = this.idbCommand.ExecuteScalar();
                this.idbCommand.Parameters.Clear();
                return obj;
            }

            public void Open()
            {
                this.idbConnection = DBManagerFactory.GetConnection(this.providerType);
                this.idbConnection.ConnectionString = this.ConnectionString;
                if (this.idbConnection.State != ConnectionState.Open)
                {
                    this.idbConnection.Open();
                }
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            }

            private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
            {
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                if (commandParameters != null)
                {
                    this.AttachParameters(command, commandParameters);
                }
            }

            public void RollBackTransaction()
            {
                if (this.idbTransaction != null)
                {
                    this.idbTransaction.Rollback();
                }
                this.idbTransaction = null;
            }

            void System.IDisposable.Dispose()
            {
                throw new NotImplementedException();
            }
        }

   
}