using Ports.Output;
namespace DataAccessSqlite;
public class SqliteUnitOfWork : IUnitOfWork
{
    private readonly SqliteData _db;
    public SqliteUnitOfWork(SqliteData db)=>_db = db;
    public void SaveChange(CancellationToken ct = default) => _db.SaveChanges();
}
