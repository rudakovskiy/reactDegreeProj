namespace GreenHealthApi
{    
    public interface IConnectionStringGetter
    {
        string Get();
    }

    public class StaticConnectionStringGetter : IConnectionStringGetter
    {
        private readonly string _conStr;

        public StaticConnectionStringGetter(string conStr)
        {
            _conStr = conStr;
        }

        public string Get()
        {
            return _conStr;
        }
    }
}