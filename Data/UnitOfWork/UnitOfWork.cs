using Data.Context;
using Data.Repository;
using Domain.Entities;

namespace Data.UnitOfWork
{
    public class UnitOfWork
    {
        public Repository<Tarefa> TarefaRepo;

        public AppDbContext context;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public Repository<Tarefa> TarefaRepository
        {
            get { return TarefaRepo ??= new Repository<Tarefa>(context);  }
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
