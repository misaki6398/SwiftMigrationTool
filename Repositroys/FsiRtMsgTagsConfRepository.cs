using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;

namespace SwiftMigrationTool.Repositorys
{
    public class FsiRtMsgTagsConfRepository : BaseRepository<FsiRtMsgTagsConf>
    {
        private readonly string _readSql = @"
            select 
                * 
            from 
                FSI_RT_MSG_TAGS_CONF 
            where n_swift_msg_id = :swiftMsgId
         ";

        private readonly string _getMaxIdSql = @"
         SELECT
            MAX(n_msg_tags_conf_id)  
        FROM
            fsi_rt_msg_tags_conf
        ";
        private readonly string _insertSql = @"
        INSERT INTO fsi_rt_msg_tags_conf (
            n_msg_tags_conf_id,
            v_tag_id,
            v_field_name,
            n_expr_id,
            d_created,
            d_modified,
            v_created_by,
            v_last_modified_by,
            n_field_desc_key,
            n_swift_msg_id,
            n_direction
        ) VALUES (
            :n_msg_tags_conf_id,
            :v_tag_id,
            :v_field_name,
            :n_expr_id,
            :d_created,
            :d_modified,
            :v_created_by,
            :v_last_modified_by,
            :n_field_desc_key,
            :n_swift_msg_id,
            :n_direction
        )
        ";

        private readonly string _deleteSql = @"
            delete 
            from 
                FSI_RT_MSG_TAGS_CONF 
            where N_SWIFT_MSG_ID = :swiftMsgId
        ";

        public FsiRtMsgTagsConfRepository(IDbTransaction transaction) : base(transaction)
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

        public IEnumerable<FsiRtMsgTagsConf> Query(int swiftMsgId)
        {
            try
            {
                return Connection.Query<FsiRtMsgTagsConf>(_readSql, new { swiftMsgId = swiftMsgId }, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(IEnumerable<FsiRtMsgTagsConf> models)
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