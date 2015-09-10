using TestSiteParser.Controller;

namespace TestSiteParser
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            MyController controller = new MyController();
            controller.Init();
            controller.Start();
        }
    }
}
