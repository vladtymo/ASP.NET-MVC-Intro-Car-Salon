using AspNet_MVC_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Car> CarRepository { get; }
        Repository<Category> CategoryRepository { get; }
        Repository<User> UserRepository { get; }
        void Save();
    }
}
