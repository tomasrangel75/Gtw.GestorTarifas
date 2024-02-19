using AutoMapper;
using Gtw.GestorTarifas.Domain.Dtos.SegurancaToken;
using Gtw.GestorTarifas.Domain.Models.Configuracao;

namespace Gtw.GestorTarifas.Domain.Profiles
{
    public class TokenAutenticacaoProfile : Profile
    {
        public TokenAutenticacaoProfile()
        {
            CreateMap<TokenRequest, ChavesGtwApiGestorTarifas>();
            CreateMap<ChavesGtwApiGestorTarifas, TokenRequest>();
        }
    }
}