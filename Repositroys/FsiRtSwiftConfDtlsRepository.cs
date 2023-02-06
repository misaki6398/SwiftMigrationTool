using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Models;
using SwiftMigrationTool.Utilitys;

namespace SwiftMigrationTool.Repositorys
{
    public class FsiRtSwiftConfDtlsRepository : BaseRepository<FsiRtSwiftConfDtls>
    {

        private readonly string _getMaxIdSql = @"
        select 
            MAX(N_SWIFT_KEY)
        from 
            FSI_RT_SWIFT_CONF_DTLS
        ";

        private readonly string _readSql = @"
        select 
            * 
        from 
            FSI_RT_SWIFT_CONF_DTLS 
        where N_SWIFT_MSG_ID = :swiftMsgId
        ";

        private readonly string _insertSql = @"
        
            INSERT INTO fsi_rt_swift_conf_dtls (
                n_swift_key,
                c_swift_msg_config,
                c_modified_swift_msg_config,
                c_ind_msg_config,
                c_ind_blk_config,
                d_end_date,
                f_latest_identifier,
                d_last_updated_date,
                n_swift_msg_id,
                d_start_date,
                v_msg_ref,
                v_trxn_ref
            ) VALUES (
                :N_SWIFT_KEY,
                :C_SWIFT_MSG_CONFIG,
                :C_MODIFIED_SWIFT_MSG_config,
                :C_IND_MSG_CONFIG,
                :C_IND_BLK_CONFIG,
                :D_END_DATE,
                :F_LATEST_IDENTIFIER,
                :D_LAST_UPDATED_DATE,
                :N_SWIFT_MSG_ID,
                :D_START_DATE,
                :V_MSG_REF,
                :V_TRXN_REF
            )
        ";

         private readonly string _deleteSql = @"
            delete 
            from 
                FSI_RT_SWIFT_CONF_DTLS 
            where N_SWIFT_MSG_ID = :swiftMsgId
        ";


        public FsiRtSwiftConfDtlsRepository(IDbTransaction transaction) : base(transaction)
        {
            
        }

        public FsiRtSwiftConfDtls Query(int swiftMsgId)
        {
            try
            {
                return Connection.Query<FsiRtSwiftConfDtls>(_readSql, new {swiftMsgId = swiftMsgId} , Transaction).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int GetMaxId()
        {
            try
            {
                return Connection.Query<int>(_getMaxIdSql, new {} , Transaction).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(FsiRtSwiftConfDtls FsiRtSwiftConfDtls)
        {
            FsiRtSwiftConfDtlsLob fsiRtSwiftConfDtlsLob = new FsiRtSwiftConfDtlsLob()
            {
                N_SWIFT_KEY = FsiRtSwiftConfDtls.N_SWIFT_KEY,
                C_SWIFT_MSG_CONFIG = OracleDBUtility.ConvertToClob(FsiRtSwiftConfDtls.C_SWIFT_MSG_CONFIG, (OracleConnection)this.Connection),
                C_MODIFIED_SWIFT_MSG_CONFIG = OracleDBUtility.ConvertToClob(FsiRtSwiftConfDtls.C_MODIFIED_SWIFT_MSG_CONFIG, (OracleConnection)this.Connection),
                C_IND_MSG_CONFIG = OracleDBUtility.ConvertToClob(FsiRtSwiftConfDtls.C_IND_MSG_CONFIG, (OracleConnection)this.Connection),
                C_IND_BLK_CONFIG = OracleDBUtility.ConvertToClob(FsiRtSwiftConfDtls.C_IND_BLK_CONFIG, (OracleConnection)this.Connection),
                D_END_DATE = FsiRtSwiftConfDtls.D_END_DATE,
                F_LATEST_IDENTIFIER = FsiRtSwiftConfDtls.F_LATEST_IDENTIFIER,
                D_LAST_UPDATED_DATE = FsiRtSwiftConfDtls.D_LAST_UPDATED_DATE,
                N_SWIFT_MSG_ID = FsiRtSwiftConfDtls.N_SWIFT_MSG_ID,
                D_START_DATE = FsiRtSwiftConfDtls.D_START_DATE,
                V_MSG_REF = FsiRtSwiftConfDtls.V_MSG_REF,
                V_TRXN_REF = FsiRtSwiftConfDtls.V_TRXN_REF
            };
            var parameter = new OracleDynamicParameters();
            // Get the type and PropertyInfo.
            Type t = fsiRtSwiftConfDtlsLob.GetType();
            PropertyInfo[] propInfos = t.GetProperties();
            foreach (PropertyInfo propInfo in propInfos)
            {
                parameter.Add(propInfo.Name, propInfo.GetGetMethod().Invoke(fsiRtSwiftConfDtlsLob, null));
            }
            return Connection.Execute(_insertSql, parameter, Transaction);
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