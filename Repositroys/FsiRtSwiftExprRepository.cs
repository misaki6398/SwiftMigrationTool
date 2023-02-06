using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;

namespace SwiftMigrationTool.Repositorys
{
    public class FsiRtSwiftExprRepository : BaseRepository<FsiRtSwiftExpr>
    {

        private readonly string _insertSql = @"
        INSERT INTO fsi_rt_swift_expr (
            n_expr_id,
            v_expr_code,
            v_expr_name,
            v_expr_desc,
            v_tag_id,
            v_field_name,
            v_expr_format,
            v_format_occur,
            v_expr_regex,
            f_enabled,
            d_created,
            d_modified,
            v_created_by,
            v_last_modified_by,
            n_swift_msg_id
        ) VALUES (
            :n_expr_id,
            :v_expr_code,
            :v_expr_name,
            :v_expr_desc,
            :v_tag_id,
            :v_field_name,
            :v_expr_format,
            :v_format_occur,
            :v_expr_regex,
            :f_enabled,
            :d_created,
            :d_modified,
            :v_created_by,
            :v_last_modified_by,
            :n_swift_msg_id
        )
        ";
        private readonly string _readSql = @"
        select 
            * 
        from 
            FSI_RT_swift_expr 
        where N_SWIFT_MSG_ID = :swiftMsgId
        ";

        private readonly string _getMaxIdSql = @"
            select 
                MAX(N_EXPR_ID) 
            from 
                fsi_rt_swift_expr
        ";

        private readonly string _deleteSql = @"
            delete 
            from 
                FSI_RT_swift_expr 
            where N_SWIFT_MSG_ID = :swiftMsgId
        ";
        public FsiRtSwiftExprRepository(IDbTransaction transaction) : base(transaction)
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

        public IEnumerable<FsiRtSwiftExpr> Query(int swiftMsgId)
        {
            try
            {
                return Connection.Query<FsiRtSwiftExpr>(_readSql, new { swiftMsgId = swiftMsgId }, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(IEnumerable<FsiRtSwiftExpr> models)
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