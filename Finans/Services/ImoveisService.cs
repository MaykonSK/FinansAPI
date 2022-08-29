using AutoMapper;
using Finans.DTO;
using Finans.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finans.Services
{
    public class ImoveisService
    {
        private FinansContext _context;
        private readonly IMapper _mapper;

        public ImoveisService(FinansContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Result cadastrarImovel(PostImovelDTO imovelDto)
        {
            try
            {
                if (imovelDto != null)
                {

                    Imovel imovel = _mapper.Map<Imovel>(imovelDto);

                    Result resultadoEndereco = cadastrarEndereco(imovel.Endereco);

                    if (resultadoEndereco.IsSuccess)
                    {
                        var enderecoId = resultadoEndereco.Successes.ToString();

                        imovel.Id = Int32.Parse(enderecoId);

                        _context.Imoveis.Add(imovel);
                        var resultado = _context.SaveChanges().ToResult();

                        if (resultado.IsSuccess)
                        {
                            return Result.Ok().WithSuccess("Imovel cadastrado");
                        }

                        return Result.Fail("Erro ao salvar imovel");
                    }
                    
                }
                return Result.Fail("Imovel inválido para registro");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IEnumerable<Imovel> recuperarImoveis(int userId)
        {
            IEnumerable<Imovel> lista = _context.Imoveis.Where(x => x.UsuarioId == userId);
            return lista;
        }

        public Result cadastrarEndereco(Endereco endereco)
        {
            if (endereco != null)
            {
                _context.Enderecos.Add(endereco);
                _context.SaveChanges();

                var Id = endereco.Id;

                return Result.Ok().WithSuccess(Id.ToString());
            }
            return Result.Fail("Inconsistencia no endereço para cadastro");
        }

        //public double somaImoveis(int userId)
        //{
        //    double total = 0;

        //    IEnumerable<Imovei> lista = _context.Imoveis.Where(x => x.UsuarioId == userId);

        //    total = (double)lista.Sum(x => x.Valor);

        //    return total;
        //}

        //public Result atualizarImovel(int id, ContasPagarDto contaspagarDto)
        //{
        //    try
        //    {
        //        Imovei imovel = _context.Imoveis.FirstOrDefault(x => x.Id == id);

        //        if (imovel != null)
        //        {
        //            _mapper.Map(contaspagarDto, imovel);
        //            _context.ContasPagars.Update(imovel);
        //            _context.SaveChanges();
        //            return Result.Ok().WithSuccess("Conta atualizada");
        //        }
        //        return Result.Fail("Conta não encontrada");
        //    }
        //    catch (System.Exception)
        //    {

        //        throw;
        //    }


        //}

        //public Result deletarImovel(int id)
        //{
        //    try
        //    {
        //        ContasPagar conta = _context.ContasPagars.FirstOrDefault(x => x.Id == id);

        //        if (conta != null)
        //        {
        //            _context.ContasPagars.Remove(conta).ToResult();
        //            var resultado = _context.SaveChanges().ToResult();

        //            if (resultado.IsSuccess)
        //            {
        //                return Result.Ok().WithSuccess("Conta excluida");
        //            }
        //        }

        //        return Result.Fail("Falha ao deletar conta");
        //    }
        //    catch (System.Exception)
        //    {

        //        throw;
        //    }

        //}
    }
}
