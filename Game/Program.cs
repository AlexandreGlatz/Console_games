using Render;

class Program
{
    static void Main(string[] args)
    {
        uint x = 0;
        float f = 0;
        bool r = true;
        while (true)
        {
            if (f >= 100000.0f)
            {
                x = r ? x + 1 : x - 1;

                if (x + 10 >= Graphics.ScreenWidth) r = false;
                else if (x <= 0) r = true;
                Graphics.Instance.Clear();
                Graphics.Instance.WriteBox(0, 0, 10, 10);
                Graphics.Instance.Present();
                f = 0;
            }
            f += 0.016f;
        }
    }
}