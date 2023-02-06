using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace SwiftMigrationTool.Utilitys
{
    public class OracleDBUtility
    {
        public static OracleClob ConvertToClob(string mystring, OracleConnection con)
        {
            byte[] newvalue = System.Text.Encoding.Unicode.GetBytes(mystring);
            var clob = new OracleClob(con);
            clob.Write(newvalue, 0, newvalue.Length);

            return clob;
        }
    }
}