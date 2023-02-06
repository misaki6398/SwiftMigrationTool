using System;

namespace SwiftMigrationTool.Models
{
    public class FsiRtSwiftExpr
    {
        public int N_EXPR_ID { get; set; }
        public string V_EXPR_CODE { get; set; }
        public string V_EXPR_NAME { get; set; }
        public string V_EXPR_DESC { get; set; }
        public string V_TAG_ID { get; set; }
        public string V_FIELD_NAME { get; set; }
        public string V_EXPR_FORMAT { get; set; }
        public int? V_FORMAT_OCCUR { get; set; }
        public string V_EXPR_REGEX { get; set; }
        public string F_ENABLED { get; set; }
        public DateTime? D_CREATED { get; set; }
        public DateTime? D_MODIFIED { get; set; }
        public string V_CREATED_BY { get; set; }
        public string V_LAST_MODIFIED_BY { get; set; }
        public int? N_SWIFT_MSG_ID { get; set; }
        
        
    }
}