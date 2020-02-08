
namespace DAL.Interfaces
{
    public interface IDbEntity : IDbEntity<int>
    {

    }

    public interface IDbEntity<T>
    {
        T Id { get; set; }
    }
}
