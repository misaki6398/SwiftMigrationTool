using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using SwiftMigrationTool.Utilitys;

namespace SwiftMigrationTool.Repositorys
{
    public class FccmUnitOfWork : IUnitOfWork
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private DimSancSwiftMsgDetailsRepository _dimSancSwiftMsgDetailsRepository;
        private DimSanctionsSwiftDetailsRepository _dimSanctionsSwiftDetailsRepository;
        private FsiRtMatchServiceRepository _fsiRtMatchServiceRepository;
        private FsiRtMsgTagsConfRepository _fsiRtMsgTagsConfRepository;
        private FsiRtSwiftConfDtlsRepository _fsiRtSwiftConfDtlsRepository;
        private FsiRtSwiftExprRepository _fsiRtSwiftExprRepository;
        
        private bool _disposed = false;

        public FccmUnitOfWork()
        {
            _connection = new OracleConnection(ConfigUtility.SourceConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public FccmUnitOfWork(string ConnectionString)
        {
            _connection = new OracleConnection(ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public DimSancSwiftMsgDetailsRepository DimSancSwiftMsgDetailsRepository
        {
            get { return _dimSancSwiftMsgDetailsRepository ?? (_dimSancSwiftMsgDetailsRepository = new DimSancSwiftMsgDetailsRepository(_transaction)); }
        }
        public DimSanctionsSwiftDetailsRepository DimSanctionsSwiftDetailsRepository
        {
            get { return _dimSanctionsSwiftDetailsRepository ?? (_dimSanctionsSwiftDetailsRepository = new DimSanctionsSwiftDetailsRepository(_transaction)); }
        }
        public FsiRtMatchServiceRepository FsiRtMatchServiceRepository
        {
            get { return _fsiRtMatchServiceRepository ?? (_fsiRtMatchServiceRepository = new FsiRtMatchServiceRepository(_transaction)); }
        }
        public FsiRtMsgTagsConfRepository FsiRtMsgTagsConfRepository
        {
            get { return _fsiRtMsgTagsConfRepository ?? (_fsiRtMsgTagsConfRepository = new FsiRtMsgTagsConfRepository(_transaction)); }
        }
        public FsiRtSwiftConfDtlsRepository FsiRtSwiftConfDtlsRepository
        {
            get { return _fsiRtSwiftConfDtlsRepository ?? (_fsiRtSwiftConfDtlsRepository = new FsiRtSwiftConfDtlsRepository(_transaction)); }
        }
        public FsiRtSwiftExprRepository FsiRtSwiftExprRepository
        {
            get { return _fsiRtSwiftExprRepository ?? (_fsiRtSwiftExprRepository = new FsiRtSwiftExprRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _dimSancSwiftMsgDetailsRepository = null;
            _dimSanctionsSwiftDetailsRepository = null;
            _fsiRtMatchServiceRepository = null;
            _fsiRtMsgTagsConfRepository = null;
            _fsiRtSwiftConfDtlsRepository = null;
            _fsiRtSwiftExprRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~FccmUnitOfWork()
        {
            dispose(false);
        }
    }
}