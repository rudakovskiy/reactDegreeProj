namespace GreenHealthApi
{
    public interface IStaticContentRootUrlGetter
    {
        string Get();
    }
    
    public class StaticContentRootUrlGetter : IStaticContentRootUrlGetter
    {
        private readonly string _contentRootUrl;

        public StaticContentRootUrlGetter(string contentRootUrl)
        {
            _contentRootUrl = contentRootUrl;
        }

        public string Get() => _contentRootUrl;
    }
}