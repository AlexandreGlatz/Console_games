using System.Text;

namespace Render;

public struct ScreenBuffer(uint width, uint height)
{
    public char[,] Screen { get; set; } = new char[width, height];
}

public class Graphics
{
    public const uint ScreenWidth = 133;
    public const uint ScreenHeight = 30;

    private ScreenBuffer[] _swapChains;
    private int _currentBufferIndex = 0;
    
    private char[,] _cbuffer => _swapChains[_currentBufferIndex].Screen;

    public static Graphics Instance { get; } = new Graphics();
    
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

    private Graphics()
    {
        _swapChains = new ScreenBuffer[] { new(ScreenWidth, ScreenHeight), new(ScreenWidth, ScreenHeight) };
        Console.SetBufferSize((int)ScreenWidth, (int)ScreenHeight);
        Console.SetWindowSize((int)ScreenWidth, (int)ScreenHeight);
        Console.CursorVisible = false;
    }

    public void WriteChar(char c, uint x, uint y)
    {
        _cbuffer[x, y] = c;
    }

    public void WriteString(string text, uint x, uint y)
    {
        for (int i = 0; i < text.Length; i++)
        {
            _cbuffer[x + i, y] = text[i];
        }
    }

    public void WriteBar(uint px, uint py, int max, int current)
    {
        for (int i = 0; i < max; i++)
        {
            _cbuffer[px + i, py] = i < current ? _filledBlock : _emptyBlock;
        }
    }
    
    public void WriteBox(uint px, uint py, int width, int height)
    {
        height /= 2;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //= corner detection
                if (x == 0 && y == 0) _cbuffer[px + x, py + y] = _tlAngle;
                else if (x == width - 1 && y == 0) _cbuffer[px + x, py + y] = _trAngle;
                else if (x == 0 && y == height - 1) _cbuffer[px + x, py + y] = _blAngle;
                else if (x == width - 1 && y == height - 1) _cbuffer[px + x, py + y] = _brAngle;
                // sides
                else if ((x == 0 || x == width - 1) && !(y == 0 || y == height - 1)) _cbuffer[px + x, py + y] = _vbar;
                else if ((y == 0 || y == height - 1) && !(x == 0 || x == width - 1)) _cbuffer[px + x, py + y] = _hbar;
            }
        }
    }

    public void Clear()
    {
        for (int y = 0; y < ScreenHeight; y++)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                _cbuffer[x, y] = _clearChar;
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
                sb.Append(_cbuffer[x, y]);
            }
        }
        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());
        _currentBufferIndex = 1 - _currentBufferIndex;
    }
}