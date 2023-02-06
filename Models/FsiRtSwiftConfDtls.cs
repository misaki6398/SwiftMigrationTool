using System;
using Oracle.ManagedDataAccess.Types;

namespace SwiftMigrationTool.Models
{
    public class FsiRtSwiftConfDtlsLob
    {
        public int N_SWIFT_KEY { get; set; }
        public OracleClob C_SWIFT_MSG_CONFIG { get; set; }
        public OracleClob C_MODIFIED_SWIFT_MSG_CONFIG { get; set; }
        public OracleClob C_IND_MSG_CONFIG { get; set; }
        public OracleClob C_IND_BLK_CONFIG { get; set; }
        public DateTime? D_END_DATE { get; set; }
        public string F_LATEST_IDENTIFIER { get; set; }
        public DateTime? D_LAST_UPDATED_DATE { get; set; }
        public int? N_SWIFT_MSG_ID { get; set; }
        public DateTime? D_START_DATE { get; set; }
        public string V_MSG_REF { get; set; }
        public string V_TRXN_REF { get; set; }
    }

    public class FsiRtSwiftConfDtls
    {
        public int N_SWIFT_KEY { get; set; }
        public string C_SWIFT_MSG_CONFIG { get; set; }
        public string C_MODIFIED_SWIFT_MSG_CONFIG { get; set; }
        public string C_IND_MSG_CONFIG { get; set; }
        public string C_IND_BLK_CONFIG { get; set; }
        public DateTime? D_END_DATE { get; set; }
        public string F_LATEST_IDENTIFIER { get; set; }
        public DateTime? D_LAST_UPDATED_DATE { get; set; }
        public int? N_SWIFT_MSG_ID { get; set; }
        public DateTime? D_START_DATE { get; set; }
        public string V_MSG_REF { get; set; }
        public string V_TRXN_REF { get; set; }
    }
}