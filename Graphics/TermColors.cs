namespace Graphics;

public class TermColors
{

	private TermColors(string value) { Value = value; }

	public string Value { get; private set; }

    public static TermColors NL          { get { return new TermColors(Environment.NewLine); }}
    public static TermColors NORMAL      { get { return new TermColors("\x1b[39m"); }}
    public static TermColors RED         { get { return new TermColors("\x1b[91m"); }}
    public static TermColors GREEN       { get { return new TermColors("\x1b[92m"); }}
    public static TermColors YELLOW      { get { return new TermColors("\x1b[93m"); }}
    public static TermColors BLUE        { get { return new TermColors("\x1b[94m"); }}
    public static TermColors MAGENTA     { get { return new TermColors("\x1b[95m"); }}
    public static TermColors CYAN        { get { return new TermColors("\x1b[96m"); }}
    public static TermColors GREY        { get { return new TermColors("\x1b[97m"); }}
    public static TermColors BOLD        { get { return new TermColors("\x1b[1m");  }}
    public static TermColors NOBOLD      { get { return new TermColors("\x1b[22m"); }}
    public static TermColors UNDERLINE   { get { return new TermColors("\x1b[4m");  }}
    public static TermColors NOUNDERLINE { get { return new TermColors("\x1b[24m"); }}
    public static TermColors REVERSE     { get { return new TermColors("\x1b[7m");  }}
    public static TermColors NOREVERSE   { get { return new TermColors("\x1b[27m"); }}
}
