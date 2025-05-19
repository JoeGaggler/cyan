using System.Numerics;
using Raylib_cs;

Raylib.SetTargetFPS(61);
Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.HighDpiWindow | ConfigFlags.VSyncHint);
Raylib.InitWindow(1027, 768, "Hello World");
Raylib.SetWindowMinSize(100, 100);
Raylib.SetWindowMaxSize(0, 0);
Raylib.SetExitKey(KeyboardKey.Null);

var dpi = Raylib.GetWindowScaleDPI();
Console.WriteLine($"DPI: {dpi}");

var fontSize = 16;
var font = Raylib.LoadFontEx("/System/Library/Fonts/Monaco.ttf", (int)(fontSize * dpi.Y), [], 512);

Raylib.SetTextureFilter(font.Texture, TextureFilter.Bilinear);

var chars = """
abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !@#$%^&*()_+[]{}|;':\",./<>? 
""";

float x = 0;
string buff = string.Empty;
buff = chars;
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(new Color(32, 32, 32, 255));

    var measure = Raylib.MeasureTextEx(font, "Hello, world!", fontSize, 0);

    Vector2 vec0 = new Vector2(0, 0);
    Vector2 vec1 = new Vector2(10, 0);
    Vector2 vec2 = new Vector2(10, 25);
    Vector2 vec3 = new Vector2(x, 50);

    Raylib.DrawTextEx(font, $"Hello, world! {measure} {Raylib.GetTime():0.000}", vec1, fontSize, 1, Color.LightGray);
    Raylib.DrawTextEx(font, $"{Raylib.GetFPS()}", vec2, fontSize, 1, Color.LightGray);
    Raylib.DrawTextPro(font, $"{x:0.000}", vec3, vec0, 0, fontSize, 1, Color.LightGray);

    x += 100 * Raylib.GetFrameTime() * 0.2f;
    if (x > 400) x = 0;

    var vec4 = new Vector2(0, 60);

    Raylib.BeginScissorMode(100, 100, 1000, 1000);

    var vec5 = new Vector2(0 + x, 60);
    var vec6 = new Vector2(900 + x, 60);
    var vec7 = new Vector2(1800 + x, 60);
    for (int j = 0; j < 100; j++)
    {
        Raylib.DrawTextEx(font, buff, vec5, fontSize, 1, Color.LightGray);
        Raylib.DrawTextEx(font, buff, vec6, fontSize, 1, Color.LightGray);
        Raylib.DrawTextEx(font, buff, vec7, fontSize, 1, Color.LightGray);
        vec5.Y += fontSize;
        vec6.Y += fontSize;
        vec7.Y += fontSize;
    }

    Raylib.EndScissorMode();

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
