using Data.Context;

namespace Data.Repository
{
    public class Repository<T> where T : class
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public List<T> Get(int pageNumber, int pageSize)
        {
            return context.Set<T>().Skip(pageNumber*pageSize).Take(pageSize).ToList();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
    }
}
