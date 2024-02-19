using Gtw.GestorTarifas.Domain.Models.ClienteContaInfo;
using System;
using System.Collections.Generic;

namespace Gtw.GestorTarifas.Domain.Dtos.ClienteDto
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public CnpjCpf CnpjCpf { get; set; } = new CnpjCpf();
        public int IdParceiro { get; set; }
        public Parceiro Parceiro { get; set; } = new Parceiro();
        public string DsNome { get; set; }
        public int FlTipoCliente { get; set; }
        public Email Email { get; set; } = new Email();
        public bool FlAtivo { get; set; }
        public DateTime DhIncl { get; set; }
        public string CdUsuIncl { get; set; }
        public DateTime DhAlt { get; set; }
        public string CdUsuAlt { get; set; }
        public List<ClienteConta> ClienteConta { get; set; } = new List<ClienteConta>();
    }
}