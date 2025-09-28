using Graphics;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
		Stopwatch watch = new Stopwatch();
		Image image = new Image("Assets/test.txt");

		double x = 0;
		watch.Start();
        while (true)
        {
			watch.Restart();
            Renderer.Instance.Clear();
            Renderer.Instance.WriteBox((uint) x * 5, 0, 10, 10, TermColors.GREEN);
			Renderer.Instance.WriteBar((uint) x * 5, 10, 10, 5, TermColors.RED);
			//Renderer.Instance.WriteImage(x, 0, image, TermColors.BLUE);
            Renderer.Instance.Present();
			

			x += watch.Elapsed.TotalMilliseconds / 1000.0d;
        }
    }
}
