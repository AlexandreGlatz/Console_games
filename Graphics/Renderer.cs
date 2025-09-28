using System.Text;

namespace Graphics;

public struct ScreenBuffer(uint width, uint height)
{
    public Char[,] Screen { get; set; } = new Char[width, height];
}

public struct Char
{
	public Char(char c, TermColors color)
	{
		Character = c;
		Color = color;
	}

	public char Character {get; private set;}
	public TermColors Color {get; private set;}

	public static Char Empty = new Char(' ', TermColors.NORMAL);
}

public class Renderer
{
    public const uint ScreenWidth = 133;
    public const uint ScreenHeight = 30;

    private ScreenBuffer[] _swapChains;
    private int _currentBufferIndex = 0;
    
    private Char[,] _cbuffer => _swapChains[_currentBufferIndex].Screen;

    public static Renderer Instance { get; } = new Renderer();
    
    //============ CHARS ==============
    private const char _hbar = '─';
    private const char _vbar = '│';
    private const char _blAngle = '└';
    private const char _brAngle = '┘';
    private const char _tlAngle = '┌';
    private const char _trAngle = '┐';
    private const char _emptyBlock = '░';
    private const char _filledBlock = '█';
    private const char _clearChar = ' ';

    private Renderer()
    {
        _swapChains = new ScreenBuffer[] { new(ScreenWidth, ScreenHeight), new(ScreenWidth, ScreenHeight) };
        Console.SetBufferSize((int)ScreenWidth, (int)ScreenHeight);
        Console.SetWindowSize((int)ScreenWidth, (int)ScreenHeight);
        Console.CursorVisible = false;
    }

    public void WriteChar(char c, TermColors color, uint x, uint y)
    {
        _cbuffer[x, y] = new Char(c, color);
    }

    public void WriteString(string text, TermColors color, uint x, uint y)
    {
        for (int i = 0; i < text.Length; i++)
        {
            _cbuffer[x + i, y] = new Char(text[i], color);
        }
    }

    public void WriteBar(uint px, uint py, int max, int current, TermColors color)
    {
        for (int i = 0; i < max; i++)
        {
            _cbuffer[px + i, py] = new Char(i < current ? _filledBlock : _emptyBlock, color);
        }
    }
	
	public void WriteImage(uint px, uint py, Image image, TermColors color)
	{
		for (int y = 0; y < image.Height; y++)
		{
		   for (int x = 0; x < image.Width; x++)
		   {
			   if (px + image.Width > Renderer.ScreenWidth || py + image.Height > Renderer.ScreenHeight) continue;
               if (x > image.Data[y].Count-1) continue;
			   _cbuffer[px + x, py + y] = new Char(image.Data[y][x], color);
		   } 
		}
	}
    
    public void WriteBox(uint px, uint py, int width, int height, TermColors color)
    {
        height /= 2;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //= corner detection
                if (x == 0 && y == 0) _cbuffer[px + x, py + y] = new Char(_tlAngle, color);
                else if (x == width - 1 && y == 0) _cbuffer[px + x, py + y] = new Char(_trAngle, color);
                else if (x == 0 && y == height - 1) _cbuffer[px + x, py + y] = new Char(_blAngle, color);
                else if (x == width - 1 && y == height - 1) _cbuffer[px + x, py + y] = new Char(_brAngle, color);
                // sides
                else if ((x == 0 || x == width - 1) && !(y == 0 || y == height - 1)) _cbuffer[px + x, py + y] = new Char(_vbar, color);
                else if ((y == 0 || y == height - 1) && !(x == 0 || x == width - 1)) _cbuffer[px + x, py + y] = new Char(_hbar, color);
            }
        }
    }

    public void Clear()
    {
        for (int y = 0; y < ScreenHeight; y++)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                _cbuffer[x, y] = Char.Empty;
            }
        }
    }

    public void Present()
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < ScreenHeight; y++)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
				Char c = _cbuffer[x, y];
                sb.Append(c.Color.Value + c.Character);
            }
        }
        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());
        _currentBufferIndex = 1 - _currentBufferIndex;
    }
}
