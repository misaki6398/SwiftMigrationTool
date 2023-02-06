using System;

namespace SwiftMigrationTool.Models
{
    public class FsiRtMatchService
    {
        public int N_MATCH_TABLE_ID { get; set; }
        public string V_TAG_ID { get; set; }
        public string V_FIELD_NAME { get; set; }
        public string F_ENABLED { get; set; }
        public int? N_EXPR_ID { get; set; }
        public DateTime? D_CREATED { get; set; }
        public DateTime? D_MODIFIED { get; set; }
        public string V_CREATED_BY { get; set; }
        public string V_LAST_MODIFIED_BY { get; set; }
        public int? N_WEBSERVICE_ID { get; set; }
        public int? N_SWIFT_MSG_ID { get; set; }
        public int? N_DIRECTION { get; set; }
        public string V_IDENTIFIER_CODE { get; set; }
        public string V_JURISDICTION { get; set; }
    }
}