using AutoMapper;
using WebApp.Models;
using WebApp.DTOs;
using WebApp.ViewModels;

namespace WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pessoa, PessoaDto>().ReverseMap();
            CreateMap<Pessoa, PessoaViewModel>().ReverseMap();

            CreateMap<Vaga, VagaDTO>().ReverseMap();
            CreateMap<Vaga, VagaViewModel>().ReverseMap();

           CreateMap<Candidatura, CandidaturaDTO>().ReverseMap();
            CreateMap<Candidatura, CandidaturaViewModel>().ReverseMap();

        }
    }
}
