using TestSiteParser.Model.Interface;

namespace TestSiteParser.Model
{
    internal sealed class MyModel : IModel
    {
        private readonly string _data;

        public MyModel(string data)
        {
            _data = data;
        }

        public string Data
        {
            get
            {
                return _data;
            }
        }
    }
}
