using AutoMapper;
using Finans.DTO;
using Finans.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Finans.Services
{
    public class ContasPagarService
    {
        private FinansContext _context;
        private IMapper _mapper;

        public ContasPagarService(FinansContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result cadastrarConta(ContasPagar contaspagar)
        {
            try
            {
                if (contaspagar != null)
                {
                    _context.ContasPagars.Add(contaspagar);
                    var resultado = _context.SaveChanges().ToResult();

                    if (resultado.IsSuccess)
                    {
                        return Result.Ok();
                    }

                    return Result.Fail("Erro ao salvar conta");
                }

                return Result.Fail("Conta inválida para registro");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<ContasPagar> recuperarContas()
        {
            return _context.ContasPagars.ToList();
        }

        public bool atualizarConta(int id, ContasPagarDto contaspagarDto)
        {
            ContasPagar conta = _context.ContasPagars.FirstOrDefault(x => x.Id == id);

            if (conta != null)
            {
                _mapper.Map(contaspagarDto, conta);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Result deletarConta(int id)
        {
            ContasPagar conta = _context.ContasPagars.FirstOrDefault(x => x.Id == id);
            
            if (conta != null)
            {
                _context.ContasPagars.Remove(conta).ToResult();
                var resultado = _context.SaveChanges().ToResult();

                if (resultado.IsSuccess)
                {
                    return Result.Ok();
                }
            }
            
            return Result.Fail("Falha ao deletar conta");
        }
    }
}
