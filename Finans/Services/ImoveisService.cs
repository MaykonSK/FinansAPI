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
                    Imovei imovel = _mapper.Map<Imovei>(imovelDto);

                    int EnderecoID = cadastrarEndereco(imovel.Endereco);

                    if (EnderecoID != 0)
                    {
                        imovel.EnderecoId = EnderecoID;

                        _context.Imoveis.Add(imovel);
                        var resultado = _context.SaveChanges().ToResult();

                        if (resultado.IsSuccess)
                        {
                            return Result.Ok().WithSuccess("Imovel cadastrado");
                        }

                        return Result.Fail("Erro ao salvar imovel");
                    }
                    return Result.Fail("Endereço não cadastrado");
                    
                }
                return Result.Fail("Imovel inválido para registro");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IEnumerable<ViewImovei> recuperarImoveis(int userId)
        {
            IEnumerable<ViewImovei> lista = _context.ViewImoveis.Where(x => x.UsuarioId == userId);
            return lista;
        }

        public int cadastrarEndereco(Endereco endereco)
        {
            int id = 0;

            if (endereco != null)
            {
                foreach (var chr in new string[] { "(", ")", "-", " " })
                {
                    endereco.Cep = endereco.Cep.Replace(chr, "");
                }

                var verificaEndereco = _context.Enderecos.FirstOrDefault(x => x.Rua == endereco.Rua && x.Bairro == endereco.Bairro && x.Numero == endereco.Numero);

                if (verificaEndereco == null)
                {
                    _context.Enderecos.Add(endereco);
                    _context.SaveChanges();
                    id = endereco.Id;
                } else
                {
                    id = verificaEndereco.Id;
                }

                return id;
            }
            return id;
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
