using Autifac_Demo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctoinalView
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "192.168.1.24";
            connectionStringBuilder.InitialCatalog = "corsairhr";
            connectionStringBuilder.UserID = "jb";
            connectionStringBuilder.Password = "JB8421";
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            FunctionalViewCreator creator = new FunctionalViewCreator(connection, 1, 1);
            creator.command = "select * from abs where nobr='960002' and bdate between '20171201' and '20171231'";
            creator.functionalColumns = new List<FunctionalColumnProperty>();
            creator.functionalColumns.Add(new FunctionalColumnProperty
            {
                ColumnName = "Test1",
                Formula = "TOL_HOURS*30"
            });
            var result = creator.GetView(new System.Collections.Generic.Dictionary<string, object>());
            var result1 = creator.GetFunctionalView(result);
        }
    }
}
