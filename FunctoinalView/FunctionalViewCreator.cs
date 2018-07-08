using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using FunctoinalView;

namespace Autifac_Demo
{
    public class FunctionalViewCreator
    {
        IDbConnection _dbConnection;
        int _ViewId;
        int _FunctionId;
        public string command = "";
        public List<FunctionalColumnProperty> functionalColumns = new List<FunctionalColumnProperty>();
        public FunctionalViewCreator(IDbConnection dbConnection, int ViewId, int FunctionId)
        {
            _dbConnection = dbConnection;
            _ViewId = ViewId;
            _FunctionId = FunctionId;
        }
        public DataTable GetFunctionalView(DataTable data)
        {
            foreach (var column in functionalColumns)
            {
                DataColumn dc = new DataColumn(column.ColumnName);
                data.Columns.Add(dc);
            }

            foreach (DataRow r in data.Rows)
            {
                foreach (var column in functionalColumns)
                {
                    NCalc.Expression expression = new NCalc.Expression(column.Formula);
                    foreach (DataColumn dc in data.Columns)
                    {
                        expression.Parameters.Add(dc.ColumnName, r[dc.ColumnName]);
                    }
                    r[column.ColumnName] = expression.Evaluate();
                }
            }
            return data;
        }
        public DataTable GetView(Dictionary<string, object> parameters)
        {
            var reader = _dbConnection.ExecuteReader(command);
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }
    }
}

namespace FunctoinalView
{
    public class FunctionalColumnProperty
    {
        public string ColumnName { get; internal set; }
        public string Formula { get; internal set; }
    }
}