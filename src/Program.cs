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

var characterSpacing = 1;
var fontSize = 16;
var font = Raylib.LoadFontEx("/System/Library/Fonts/Monaco.ttf", (int)(fontSize * dpi.Y), [], 512);
// var font = Raylib.LoadFontEx("/System/Library/Fonts/Supplemental/Times New Roman.ttf", (int)(fontSize * dpi.Y), [], 512);
Raylib.SetTextureFilter(font.Texture, TextureFilter.Bilinear);

var chars = File.ReadAllText("Program.cs");

var vec4 = new Vector2(0, 0);
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(new Color(32, 32, 32, 255));

    var scroll = Raylib.GetMouseWheelMoveV();
    vec4 += scroll * fontSize;

    if (Raylib.IsKeyDown(KeyboardKey.Space))
    {
        // draw text
        var lines = chars.Split('\n');
        var vec5 = vec4;
        foreach (var line in lines)
        {
            var vec6 = vec5;
            foreach (var ch in line)
            {
                var chString = ch.ToString();
                var size = Raylib.MeasureTextEx(font, chString, fontSize, 1);
                Raylib.DrawTextEx(font, chString, vec6, fontSize, 0, Color.White);
                vec6.X += size.X + characterSpacing;
            }
            vec5.Y += fontSize;
        }
    }
    else
    {
        // draw text
        Raylib.SetTextLineSpacing(fontSize);
        Raylib.DrawTextEx(font, chars, vec4, fontSize, characterSpacing, Color.LightGray);
    }

    // fps
    var fps = Raylib.GetFPS();
    var fpsString = $"FPS: {fps}";
    var fpsSize = Raylib.MeasureTextEx(font, fpsString, fontSize, 1);
    var fpsVec = new Vector2(Raylib.GetScreenWidth(), 0);
    fpsVec.X -= fpsSize.X + 5;
    Raylib.DrawTextEx(font, fpsString, fpsVec, fontSize, 1, Color.RayWhite);

    // dpi
    var dpiString = $"DPI: {dpi}";
    var dpiSize = Raylib.MeasureTextEx(font, dpiString, fontSize, 1);
    var dpiVec = new Vector2(Raylib.GetScreenWidth(), fpsVec.Y + fpsSize.Y);
    dpiVec.X -= dpiSize.X + 5;
    Raylib.DrawTextEx(font, dpiString, dpiVec, fontSize, 1, Color.RayWhite);

    Raylib.EndDrawing();

    if (Raylib.IsKeyPressed(KeyboardKey.Escape))
    {
        break;
    }
}

Raylib.CloseWindow();
