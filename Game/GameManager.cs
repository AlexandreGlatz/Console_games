using System.Diagnostics;
using Graphics;

namespace Game;

public class GameManager
{
	public Double DeltaTime { get; private set; }

	private Stopwatch _watch;
	private bool _isOpen = true;
	private Renderer _renderer;

	public GameManager()
	{
		_watch = new Stopwatch();
		_renderer = Renderer.Instance;
		AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
	}

	public void Run()
	{
		_watch.Start();
		GameLoop();
	}

	private void GameLoop()
	{
		while(_isOpen)
		{
			_watch.Restart();

			// ======Rendering Loop
			_renderer.Clear();
				_renderer.WriteString("Hello world", TermColors.GREEN, 0, 0);
			_renderer.Present();

			// =====

			DeltaTime = _watch.Elapsed.TotalMilliseconds / 1000.0d;
		}
	}

	static void ProcessExitHandler( object sender , EventArgs e )
    {
		GameManager manager = sender as GameManager;
		if (manager == null) return;
		manager._isOpen = false;
    }
}
