using System.ComponentModel.DataAnnotations;

namespace Gtw.GestorTarifas.Domain.Enums
{
    public enum PeriodicidadeEnum
    {
		[Display(Name = "No Ato")]
		NoAto = 1,

		[Display(Name = "Diária")]
		Diaria = 2,

		[Display(Name = "Semanal")]
		Semanal = 3,

		[Display(Name = "Quinzenal")]
		Quinzenal = 4,

		[Display(Name = "Mensal")]
		Mensal = 5,

		[Display(Name = "Bimestral")]
		Bimestral = 6,

		[Display(Name = "Trimestral")]
		Trimestral = 7,

		[Display(Name = "Semestral")]
		Semestral = 8,

		[Display(Name = "Anual")]
		Anual = 9
	}
}