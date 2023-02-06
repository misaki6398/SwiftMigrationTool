using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftMigrationTool.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        DimSancSwiftMsgDetailsRepository DimSancSwiftMsgDetailsRepository { get; }
        DimSanctionsSwiftDetailsRepository DimSanctionsSwiftDetailsRepository { get; }
        FsiRtMatchServiceRepository FsiRtMatchServiceRepository { get; }
        FsiRtMsgTagsConfRepository FsiRtMsgTagsConfRepository { get; }
        FsiRtSwiftConfDtlsRepository FsiRtSwiftConfDtlsRepository { get; }
        FsiRtSwiftExprRepository FsiRtSwiftExprRepository { get; }

        void Commit();
    }
}