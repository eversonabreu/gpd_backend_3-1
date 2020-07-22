using System;
using System.Threading.Tasks;

namespace GPD.Backend.Domain
{
    public static class Utils
    {
        public static DateTime ObterDataNoUltimoDiaDoMes(int mes, int ano)
        {
            switch (mes)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: return new DateTime(ano, mes, 31);

                case 4:
                case 6:
                case 9:
                case 11: return new DateTime(ano, mes, 30);

                case 2: return DateTime.IsLeapYear(ano) ? new DateTime(ano, mes, 29) : new DateTime(ano, mes, 28);

                default: throw new Exception($"Mês: {mes} é inválido");
            }
        }

        public static string ObterAbreviacaoMes(int mes)
        {
            return mes switch
            {
                1 => "Jan",
                2 => "Fev",
                3 => "Mar",
                4 => "Abr",
                5 => "Mai",
                6 => "Jun",
                7 => "Jul",
                8 => "Ago",
                9 => "Set",
                10 => "Out",
                11 => "Nov",
                12 => "Dez",
                _ => throw new Exception($"Mês: {mes} é inválido."),
            };
        }

        public static string ObterDescricaoMes(int mes)
        {
            return mes switch
            {
                1 => "Janeiro",
                2 => "Fevereiro",
                3 => "Março",
                4 => "Abril",
                5 => "Maio",
                6 => "Junho",
                7 => "Julho",
                8 => "Agosto",
                9 => "Setembro",
                10 => "Outubro",
                11 => "Novembro",
                12 => "Dezembro",
                _ => throw new Exception($"Mês: {mes} é inválido."),
            };
        }
    }
}
