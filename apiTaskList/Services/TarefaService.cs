using AutoMapper;
using Data.UnitOfWork;
using Domain.DTOs;
using Domain.Entities;
using FluentResults;

namespace apiTaskList.Services
{
    public class TarefaService
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork context;

        public TarefaService(IMapper mapper, UnitOfWork context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public Result Get(int pageNumber, int pageSize)
        {
            Result result;
            try
            {
                var tarefa = context.TarefaRepository.Get(pageNumber, pageSize);

                result = new Result().WithSuccess(new Success("Sucesso").WithMetadata("data", mapper.Map<List<ReadTarefaDTO>>(tarefa)));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(new Error(ex.Message));

                return result;
            }
        }

        public Result Post(CreateTarefaDTO tarefaDTO)
        {
            Result result;
            try
            {
                var tarefa = mapper.Map<Tarefa>(tarefaDTO);

                context.TarefaRepository.Add(tarefa);
                context.Commit();

                result = new Result().WithSuccess(new Success("Sucesso"));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(new Error(ex.Message));

                return result;
            }
        }
    }
}
