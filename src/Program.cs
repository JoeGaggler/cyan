using System.Numerics;
using Raylib_cs;

Raylib.SetTargetFPS(60 + 1);
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

var chars = File.ReadAllText("Program.cs");

var vec4 = new Vector2(0, 0);
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(new Color(32, 32, 32, 255));

    // draw text
    var scroll = Raylib.GetMouseWheelMoveV();
    vec4 += scroll * fontSize;
    Raylib.DrawTextEx(font, chars, vec4, fontSize, 1, Color.LightGray);

    // fps
    var fps = Raylib.GetFPS();
    var fpsString = $"FPS: {fps}";
    var fpsSize = Raylib.MeasureTextEx(font, fpsString, fontSize, 1);
    var fpsVec = new Vector2(Raylib.GetScreenWidth(), 0);
    fpsVec.X -= fpsSize.X + 5;
    Raylib.DrawTextEx(font, fpsString, fpsVec, fontSize, 1, Color.LightGray);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
