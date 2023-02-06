using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;

namespace SwiftMigrationTool.Repositorys
{
    public class FsiRtMatchServiceRepository : BaseRepository<FsiRtMatchService>
    {
        private readonly string _readSql = @"
        select 
            * 
        from 
            FSI_RT_MATCH_SERVICE 
        where 
            N_SWIFT_MSG_ID = :swiftMsgId
        order by n_webservice_id,v_field_name
        ";

        private readonly string _insertSql = @"
        INSERT INTO fsi_rt_match_service (
            n_match_table_id,
            v_tag_id,
            v_field_name,
            f_enabled,
            n_expr_id,
            d_created,
            d_modified,
            v_created_by,
            v_last_modified_by,
            n_webservice_id,
            n_swift_msg_id,
            n_direction,
            v_identifier_code,
            v_jurisdiction
        ) VALUES (
            SEQ_MATCH_SERVICE.NEXTVAL,
            :v_tag_id,
            :v_field_name,
            :f_enabled,
            :n_expr_id,
            :d_created,
            :d_modified,
            :v_created_by,
            :v_last_modified_by,
            :n_webservice_id,
            :n_swift_msg_id,
            :n_direction,
            :v_identifier_code,
            :v_jurisdiction
        )
        ";

        private readonly string _getMaxIdSql = @"
        SELECT
            MAX(N_MATCH_TABLE_ID)
        FROM
            FSI_RT_MATCH_SERVICE
        ";

        private readonly string _deleteSql = @"
            delete 
            from 
                FSI_RT_MATCH_SERVICE 
            where N_SWIFT_MSG_ID = :swiftMsgId
        ";


        public FsiRtMatchServiceRepository(IDbTransaction transaction) : base(transaction)
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

        public IEnumerable<FsiRtMatchService> Query(int swiftMsgId)
        {
            try
            {
                return Connection.Query<FsiRtMatchService>(_readSql, new { swiftMsgId = swiftMsgId }, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(IEnumerable<FsiRtMatchService> models)
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