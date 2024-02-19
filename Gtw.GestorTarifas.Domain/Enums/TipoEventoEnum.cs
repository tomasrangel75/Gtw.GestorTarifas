namespace Gtw.GestorTarifas.Domain.Enums
{
    public enum TipoEventoEnum
    {
        PosPago = 1,
        PrePago
    }

    public static class EnumTipoEventoExtensions
    {
        public static string ToFriendlyString(this TipoEventoEnum v)
        {
            switch (v)
            {
                case TipoEventoEnum.PosPago: return "Pós-Pago";
                case TipoEventoEnum.PrePago: return "Pré-Pago";
                default: return "--.--";
            }
        }
    }
}