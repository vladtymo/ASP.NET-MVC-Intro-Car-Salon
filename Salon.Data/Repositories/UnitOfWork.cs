using AspNet_MVC_App.Data;
using AspNet_MVC_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SalonDbContext context;// = new SalonDbContext()

        private Repository<Car> carRepository;
        private Repository<Category> categoryRepository;
        private Repository<User> userRepository;

        public UnitOfWork(SalonDbContext context)
        {
            this.context = context;

            carRepository = new Repository<Car>(context);
            categoryRepository = new Repository<Category>(context);
            userRepository = new Repository<User>(context);
        }

        public Repository<Car> CarRepository => carRepository;

        public Repository<Category> CategoryRepository => categoryRepository;

        public Repository<User> UserRepository => userRepository;

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
