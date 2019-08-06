using WebAPIVue.DAL.CRUD;

namespace WebAPIVue.DAL.User.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public long DepartmentId { get; set; }
    }
}
