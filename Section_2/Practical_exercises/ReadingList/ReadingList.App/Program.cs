
using ReadingList.App.Interfaces;

namespace ReadingList.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDisplayer displayer = new Displayer();
            AppController controller = new (displayer);

            controller.Run();
        }
    }
}
