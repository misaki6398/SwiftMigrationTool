using System;

namespace SwiftMigrationTool.Models
{
    public class DimSanctionsSwiftDetails
    {
        public int N_SANCTION_SWIFT_MSG_ID { get; set; }
        public string V_SANCTION_SWIFT_MSG_DESC { get; set; }
        public DateTime? D_START_DATE { get; set; }
        public DateTime? D_END_DATE { get; set; }
        public string F_LATEST_IDENTIFIER { get; set; }
        public string V_REMARKS { get; set; }
        public DateTime? D_LAST_UPDATED_DATE { get; set; }
        public int? N_SANCTION_SWIFT_MSG_TYPE { get; set; }
    }
}