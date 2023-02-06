using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;

namespace SwiftMigrationTool.Repositorys
{
    public class DimSanctionsSwiftDetailsRepository : BaseRepository<DimSanctionsSwiftDetails>
    {
        private readonly string _readSql = @"
            select 
                * 
            from 
                dim_sanctions_swift_details 
            where n_sanction_swift_msg_type = :n_sanction_swift_msg_type
            and   F_LATEST_IDENTIFIER = 'Y'
        ";

        private readonly string _getMaxIdSql = @"
            SELECT
                max(n_sanction_swift_msg_id)        
            FROM
                dim_sanctions_swift_details
        ";

        private readonly string _insertSql = @"
            INSERT INTO dim_sanctions_swift_details (
                n_sanction_swift_msg_id,
                v_sanction_swift_msg_desc,
                d_start_date,
                d_end_date,
                f_latest_identifier,
                v_remarks,
                d_last_updated_date,
                n_sanction_swift_msg_type
            ) VALUES (
                :n_sanction_swift_msg_id,
                :v_sanction_swift_msg_desc,
                :d_start_date,
                :d_end_date,
                :f_latest_identifier,
                :v_remarks,
                :d_last_updated_date,
                :n_sanction_swift_msg_type
            )
        ";

        private readonly string _deleteSql = @"
            delete 
            from 
                dim_sanctions_swift_details 
            where N_SANCTION_SWIFT_MSG_ID = :swiftMsgId
        ";

        public DimSanctionsSwiftDetailsRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public int GetMaxId()
        {
            try
            {
                return Connection.Query<int>(_getMaxIdSql, new { }, Transaction).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DimSanctionsSwiftDetails Query(int n_sanction_swift_msg_type)
        {
            try
            {
                return Connection.Query<DimSanctionsSwiftDetails>(_readSql, new { n_sanction_swift_msg_type = n_sanction_swift_msg_type }, Transaction).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(IEnumerable<DimSanctionsSwiftDetails> models)
        {
            try
            {
                return Connection.Execute(_insertSql, models, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(DimSanctionsSwiftDetails model)
        {
            try
            {
                return Connection.Execute(_insertSql, model, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(int swiftMsgId)
        {
            try
            {
                return Connection.Execute(_deleteSql, new { swiftMsgId = swiftMsgId }, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}