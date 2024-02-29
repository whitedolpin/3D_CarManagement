
using Repository.Models;

namespace Repository.Repository
{
    public class UnitOfWork 
    {
        private Prn3dContext context = new Prn3dContext();

        private GenericRepository<Position> positionRepository;
        private GenericRepository<Car> carRepository;

        public GenericRepository<Position> PositionRepository
        {
            get
            {

                if (this.positionRepository == null)
                {
                    this.positionRepository = new GenericRepository<Position>(context);
                }
                return positionRepository;
            }
        }
        public GenericRepository<Car> CarRepository
        {
            get
            {

                if (this.carRepository == null)
                {
                    this.carRepository = new GenericRepository<Car>(context);
                }
                return carRepository;
            }
        }

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

 
 