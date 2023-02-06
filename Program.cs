using System;
using System.Collections.Generic;
using System.Linq;
using SwiftMigrationTool.Models;
using SwiftMigrationTool.Repositorys;
using SwiftMigrationTool.Utilitys;

namespace SwiftMigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {

            FccmUnitOfWork source = new FccmUnitOfWork(ConfigUtility.SourceConnectionString);
            List<DimSancSwiftMsgDetails> sourceDimSancSwiftMsgDetails = source.DimSancSwiftMsgDetailsRepository.Query(ConfigUtility.MigrationMessageTypes).ToList();
            List<string> sourceMassageType = sourceDimSancSwiftMsgDetails.Select(c => c.V_SANC_SWIFT_MSG_TYPE).ToList();

            if (!CheckSourceHaveAllMessage(sourceMassageType, ConfigUtility.MigrationMessageTypes))
            {
                return;
            }

            FccmUnitOfWork destination = new FccmUnitOfWork(ConfigUtility.DestinationConnectionString);
            List<DimSancSwiftMsgDetails> destinationDimSancSwiftMsgDetails = destination.DimSancSwiftMsgDetailsRepository.Query(ConfigUtility.MigrationMessageTypes).ToList();
            List<string> desinationMassageType = destinationDimSancSwiftMsgDetails.Select(c => c.V_SANC_SWIFT_MSG_TYPE).ToList();

            var newMessageType = sourceMassageType.Except(desinationMassageType);
            var updateMessageType = sourceMassageType.Intersect(desinationMassageType);

            // Update
            DoDeleteMessageType(destination, updateMessageType);
            DoInsertMessageType(source, destination, updateMessageType);

            // Add new
            DoInsertMessageType(source, destination, newMessageType);

            destination.Commit();
            source.Dispose();
            destination.Dispose();
        }

        private static void DoDeleteMessageType(FccmUnitOfWork destination, IEnumerable<string> messageType)
        {
            if(messageType.Count().Equals(0)){
                return;
            }
            List<DimSancSwiftMsgDetails> destinationDimSancSwiftMsgDetails = destination.DimSancSwiftMsgDetailsRepository.Query(messageType).ToList();
            foreach (var destinationDimSancSwiftMsgDetail in destinationDimSancSwiftMsgDetails)
            {
                DimSanctionsSwiftDetails destinationDimSanctionsSwiftDetail = destination.DimSanctionsSwiftDetailsRepository.Query(destinationDimSancSwiftMsgDetail.N_SANC_SWIFT_MSG_ID);
                if(destinationDimSanctionsSwiftDetail == null){
                    continue;
                }
                destination.FsiRtSwiftExprRepository.Delete(destinationDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                destination.FsiRtMatchServiceRepository.Delete(destinationDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                destination.FsiRtMsgTagsConfRepository.Delete(destinationDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                destination.FsiRtSwiftConfDtlsRepository.Delete(destinationDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                destination.DimSanctionsSwiftDetailsRepository.Delete(destinationDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
            }

        }

        private static void DoInsertMessageType(FccmUnitOfWork source, FccmUnitOfWork destination, IEnumerable<string> messageType)
        {
            if(messageType.Count().Equals(0)){
                return;
            }
            List<DimSancSwiftMsgDetails> sourceDimSancSwiftMsgDetails = source.DimSancSwiftMsgDetailsRepository.Query(messageType).ToList();
            int maxDimSanctionsSwiftDetailsId = destination.DimSanctionsSwiftDetailsRepository.GetMaxId();
            int maxFsiRtSwiftConfDtlsId = destination.FsiRtSwiftConfDtlsRepository.GetMaxId();
            int maxFsiRtMsgTagsConfId = destination.FsiRtMsgTagsConfRepository.GetMaxId();
            int maxFsiRtMatchServiceId = destination.FsiRtMatchServiceRepository.GetMaxId();
            int maxFsiRtSwiftExprId = destination.FsiRtSwiftExprRepository.GetMaxId();

            foreach (var sourceDimSancSwiftMsgDetail in sourceDimSancSwiftMsgDetails)
            {
                // Deal DimSanctionsSwiftDetails
                DimSanctionsSwiftDetails sourceDimSanctionsSwiftDetail = source.DimSanctionsSwiftDetailsRepository.Query(sourceDimSancSwiftMsgDetail.N_SANC_SWIFT_MSG_ID);
                FsiRtSwiftConfDtls sourceFsiRtSwiftConfDtls = source.FsiRtSwiftConfDtlsRepository.Query(sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                IEnumerable<FsiRtMsgTagsConf> sourceFsiRtMsgTagsConfs = source.FsiRtMsgTagsConfRepository.Query(sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                IEnumerable<FsiRtMatchService> sourceFsiRtMatchServices = source.FsiRtMatchServiceRepository.Query(sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);
                IEnumerable<FsiRtSwiftExpr> sourceFsiRtSwiftExprs = source.FsiRtSwiftExprRepository.Query(sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID);


                // Change insert Id
                sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_ID = ++maxDimSanctionsSwiftDetailsId;
                sourceFsiRtSwiftConfDtls.N_SWIFT_MSG_ID = maxDimSanctionsSwiftDetailsId;
                sourceFsiRtSwiftConfDtls.N_SWIFT_KEY = ++maxFsiRtSwiftConfDtlsId;
                foreach (var sourceFsiRtMsgTagsConf in sourceFsiRtMsgTagsConfs)
                {
                    sourceFsiRtMsgTagsConf.N_MSG_TAGS_CONF_ID = ++maxFsiRtMsgTagsConfId;
                    sourceFsiRtMsgTagsConf.N_SWIFT_MSG_ID = maxDimSanctionsSwiftDetailsId;
                }

                foreach (var sourceFsiRtMatchService in sourceFsiRtMatchServices)
                {
                    sourceFsiRtMatchService.N_MATCH_TABLE_ID = ++maxFsiRtMatchServiceId;
                    sourceFsiRtMatchService.N_SWIFT_MSG_ID = maxDimSanctionsSwiftDetailsId;
                }

                foreach (var sourceFsiRtSwiftExpr in sourceFsiRtSwiftExprs)
                {
                    sourceFsiRtSwiftExpr.N_EXPR_ID = ++maxFsiRtSwiftExprId;
                    sourceFsiRtSwiftExpr.N_SWIFT_MSG_ID = maxDimSanctionsSwiftDetailsId;
                }

                var destinationDimSancSwiftMsgDetail = destination.DimSancSwiftMsgDetailsRepository.Query(sourceDimSancSwiftMsgDetail.V_SANC_SWIFT_MSG_TYPE);
                if (destinationDimSancSwiftMsgDetail == null) // 若查無 message type 則直接新增
                {
                    destination.DimSancSwiftMsgDetailsRepository.Insert(sourceDimSancSwiftMsgDetail);
                }
                else // 若目標已有該 messagetype 需將 id 換成目標 id 再 insert
                { 
                    sourceDimSanctionsSwiftDetail.N_SANCTION_SWIFT_MSG_TYPE = destinationDimSancSwiftMsgDetail.N_SANC_SWIFT_MSG_ID;
                }
                destination.DimSanctionsSwiftDetailsRepository.Insert(sourceDimSanctionsSwiftDetail);
                destination.FsiRtSwiftConfDtlsRepository.Insert(sourceFsiRtSwiftConfDtls);
                destination.FsiRtMsgTagsConfRepository.Insert(sourceFsiRtMsgTagsConfs);
                destination.FsiRtMatchServiceRepository.Insert(sourceFsiRtMatchServices);
                destination.FsiRtSwiftExprRepository.Insert(sourceFsiRtSwiftExprs);
            }
        }



        private static bool CheckSourceHaveAllMessage(List<string> sources, List<string> configs)
        {
            IEnumerable<string> except = sources.Except(configs);
            if (except.Count().Equals(0))
            {
                Console.Write("Check ok start migration ");
                foreach (var config in configs)
                {
                    Console.Write($"{config} ");
                }
                Console.WriteLine();
                return true;
            }
            else
            {
                foreach (var config in configs)
                {
                    if (sources.Contains(config))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"{config} not in the source db please check it.");
                    }
                }
                return false;
            }
        }
    }
}
