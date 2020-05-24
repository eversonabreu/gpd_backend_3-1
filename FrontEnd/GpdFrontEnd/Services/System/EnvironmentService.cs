namespace GpdFrontEnd.Services.System
{
    public class EnvironmentService
    {
        public string Name { get; private set; }

        public string BaseUri { get; private set; }

        public EnvironmentService(string name, string baseUri)
        {
            Name = name;
            BaseUri = baseUri;
        }
    }
}
