using Microsoft.Extensions.Configuration;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class EnvironmentService
    {
        public EnvironmentService(IConfiguration configuration)
        {
            Smtp = configuration.GetSection("Email").GetSection("Smtp").Value;
            EnableSsl = bool.Parse(configuration.GetSection("Email").GetSection("EnableSsl").Value);
            Port = int.Parse(configuration.GetSection("Email").GetSection("Port").Value);
            Login = configuration.GetSection("Email").GetSection("Login").Value;
            Senha = configuration.GetSection("Email").GetSection("Senha").Value;
            PossuiLimiteSuperiorAtingimento = bool.Parse(configuration.GetSection("LimiteValores").GetSection("PossuiLimiteSuperiorAtingimento").Value);
            ValorLimiteSuperiorAtingimento = decimal.Parse(configuration.GetSection("LimiteValores").GetSection("ValorLimiteSuperiorAtingimento").Value);
            PossuiLimiteInferiorAtingimento = bool.Parse(configuration.GetSection("LimiteValores").GetSection("PossuiLimiteInferiorAtingimento").Value);
            ValorLimiteInferiorAtingimento = decimal.Parse(configuration.GetSection("LimiteValores").GetSection("ValorLimiteInferiorAtingimento").Value);
            PossuiLimiteSuperiorPonderadoIndividual = bool.Parse(configuration.GetSection("LimiteValores").GetSection("PossuiLimiteSuperiorPonderadoIndividual").Value);
            ValorLimiteSuperiorPonderadoIndividual = decimal.Parse(configuration.GetSection("LimiteValores").GetSection("ValorLimiteSuperiorPonderadoIndividual").Value);
            PossuiLimiteInferiorPonderadoIndividual = bool.Parse(configuration.GetSection("LimiteValores").GetSection("PossuiLimiteInferiorPonderadoIndividual").Value);
            ValorLimiteInferiorPonderadoIndividual = decimal.Parse(configuration.GetSection("LimiteValores").GetSection("ValorLimiteInferiorPonderadoIndividual").Value);
        }

        public string Smtp { get; private set; }

        public bool EnableSsl { get; private set; }

        public int Port { get; private set; }

        public string Login { get; private set; }

        public string Senha { get; private set; }

        public bool PossuiLimiteSuperiorAtingimento { get; private set; }

        public decimal ValorLimiteSuperiorAtingimento { get; private set; }

        public bool PossuiLimiteInferiorAtingimento { get; private set; }

        public decimal ValorLimiteInferiorAtingimento { get; private set; }

        public bool PossuiLimiteSuperiorPonderadoIndividual { get; private set; }

        public decimal ValorLimiteSuperiorPonderadoIndividual { get; private set; }

        public bool PossuiLimiteInferiorPonderadoIndividual { get; private set; }

        public decimal ValorLimiteInferiorPonderadoIndividual { get; private set; }
    }
}
