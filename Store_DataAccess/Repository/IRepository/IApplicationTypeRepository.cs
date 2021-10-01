using Store_Models;

namespace Store_DataAccess.Repository.IRepository
{
    public interface IApplicationTypeRepository:IRepository<ApplicationType>
    {
        void Update(ApplicationType obj);
    }
}
