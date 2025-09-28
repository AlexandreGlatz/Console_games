namespace Graphics;

public class Image
{
	public int Width {get; private set;}
	public int Height {get; private set;}
	public List<List<char>> Data {get; private set;}

	public Image(string path)
	{
		Data = new List<List<char>>();
		Load(path);
	}

	public void Load(string path)
	{
		StreamReader sr = new StreamReader(path);
		string? line = sr.ReadLine();

		int r = 0;
		while(line != null)
		{
			Data.Add(new List<char>());
			for (int i = 0; i < line.Length; i++)
			{
				Data[r].Add(line[i]);
				if (i > Width) Width = i+1;
			}
			r++;
			line = sr.ReadLine();
		}
		Height = Data.Count;
		sr.Close();
	}
}
