using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;

namespace SwiftMigrationTool.Repositorys
{
    public class DimSancSwiftMsgDetailsRepository : BaseRepository<DimSancSwiftMsgDetails>
    {
        private readonly string _readSql = @"
        SELECT 
            * 
        FROM 
            dim_sanc_swift_msg_details 
        WHERE 
        v_sanc_swift_msg_type in :msgTypes
        AND F_LATEST_IDENTIFIER = 'Y'
        ";

        private readonly string _insertSql = @"
        INSERT INTO dim_sanc_swift_msg_details (
            n_sanc_swift_msg_id,
            v_sanc_swift_msg_type,
            v_sanc_swift_msg_desc,
            d_start_date,
            d_end_date,
            f_latest_identifier,
            v_remarks,
            d_last_updated_date,
            v_repeat_type
        ) VALUES (
            :n_sanc_swift_msg_id,
            :v_sanc_swift_msg_type,
            :v_sanc_swift_msg_desc,
            :d_start_date,
            :d_end_date,
            :f_latest_identifier,
            :v_remarks,
            :d_last_updated_date,
            :v_repeat_type
        )
        ";

        public DimSancSwiftMsgDetailsRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IEnumerable<DimSancSwiftMsgDetails> Query(IEnumerable<string> msgTypes)
        {
            try
            {
                return Connection.Query<DimSancSwiftMsgDetails>(_readSql, new { msgTypes = msgTypes }, Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DimSancSwiftMsgDetails Query(string msgType)
        {
            try
            {                
                return Connection.Query<DimSancSwiftMsgDetails>(_readSql, new { msgTypes = msgType }, Transaction).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(IEnumerable<DimSancSwiftMsgDetails> models)
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

        public int Insert(DimSancSwiftMsgDetails model)
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
    }
}