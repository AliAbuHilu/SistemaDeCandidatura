using AutoMapper;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Data.Txt;

namespace WebApp.Services
{
    public class PessoaService
    {
        private readonly PessoaDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public PessoaService(IMapper mapper)
        {
            _dataAccess = new PessoaDataAccess("Data/pessoas.txt");
            _mapper = mapper;
        }

        public PessoaService()
        {
        }

        public List<PessoaViewModel> Listar() =>
            _dataAccess.ObterTodos().Select(p => _mapper.Map<PessoaViewModel>(p)).ToList();

        public PessoaViewModel? ObterPorId(int id)
        {
            var pessoa = _dataAccess.ObterPorId(id);
            return pessoa == null ? null : _mapper.Map<PessoaViewModel>(pessoa);
        }

        public void Criar(PessoaViewModel viewModel)
        {
            var pessoa = _mapper.Map<Pessoa>(viewModel);
            _dataAccess.Adicionar(pessoa);
        }

        public void Atualizar(PessoaViewModel viewModel)
        {
            var pessoa = _mapper.Map<Pessoa>(viewModel);
            _dataAccess.Atualizar(pessoa);
        }

        public void Excluir(int id)
        {
            _dataAccess.Excluir(id);
        }
    }
}
