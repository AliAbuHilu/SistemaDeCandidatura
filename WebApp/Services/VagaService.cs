using AutoMapper;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Data.Txt;

namespace WebApp.Services
{
    public class VagaService
    {
        private readonly VagaDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public VagaService(IMapper mapper)
        {
            _dataAccess = new VagaDataAccess("Data/vagas.txt");
            _mapper = mapper;
        }

        

        public List<VagaViewModel> Listar() =>
            _dataAccess.ObterTodos().Select(v => _mapper.Map<VagaViewModel>(v)).ToList();

        public VagaViewModel? ObterPorId(int id)
        {
            var vaga = _dataAccess.ObterPorId(id);
            return vaga == null ? null : _mapper.Map<VagaViewModel>(vaga);
        }

        public void Criar(VagaViewModel viewModel)
        {
            var vaga = _mapper.Map<Vaga>(viewModel);
            _dataAccess.Adicionar(vaga);
        }

        public void Atualizar(VagaViewModel viewModel)
        {
            var vaga = _mapper.Map<Vaga>(viewModel);
            _dataAccess.Atualizar(vaga);
        }

        public void Excluir(int id)
        {
            _dataAccess.Excluir(id);
        }
    }
}
