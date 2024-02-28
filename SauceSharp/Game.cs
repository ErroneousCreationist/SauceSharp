using System.Numerics;
using Raylib_cs;

namespace SauceSharp
{
	public class Game
	{
        public List<Scene> SCENES;
        public int CurrentScene;
        public static Game TheGame;
        public Vector2 CAMERA_POSITION;

        public Scene GetCurrentScene
        {
            get
            {
                return SCENES[CurrentScene];
            }
        }

        public Game(int width, int height, string windowname, List<Scene> scenes, int scene)
		{
            TheGame = this;
            SCENES = scenes;
            CurrentScene = scene;
            SCENES[CurrentScene].Loaded(this);

            Raylib.InitWindow(width, height, windowname);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(SCENES[CurrentScene].bgCol);
                SCENES[CurrentScene].Draw();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        public virtual void LoadScene(int index)
        {
            if (index == CurrentScene) { return; }
            SCENES[CurrentScene].Unloaded();
            CurrentScene = index;
            SCENES[CurrentScene].Loaded(this);
        }

        public List<KeyboardKey> GetKeysPressed
        {
            get
            {
                List<KeyboardKey> returned = new List<KeyboardKey>();
                while (true)
                {
                    int i = Raylib.GetKeyPressed();
                    if (i == 0) { break; }
                    returned.Add((KeyboardKey)i);
                }
                return returned;
            }
        }

        public List<KeyboardKey> GetKeysDown
        {
            get
            {
                List<KeyboardKey> returned = new List<KeyboardKey>();
                while (true)
                {
                    int i = Raylib.GetKeyPressed();
                    if (i == 0) { break; }
                    if (!Raylib.IsKeyDown((KeyboardKey)i)) { continue; }
                    returned.Add((KeyboardKey)i);
                }
                return returned;
            }
        }

        public List<char> GetCharsPressed
        {
            get
            {
                List<char> returned = new List<char>();
                while (true)
                {
                    int i = Raylib.GetCharPressed();
                    if (i == 0) { break; }
                    returned.Add((char)i);
                }
                return returned;
            }
        }

        public List<char> GetCharsDown
        {
            get
            {
                List<char> returned = new List<char>();
                while (true)
                {
                    int i = Raylib.GetCharPressed();
                    int x = Raylib.GetKeyPressed();
                    if (i == 0) { break; }
                    if (!Raylib.IsKeyDown((KeyboardKey)x)) { continue; }
                    returned.Add((char)i);
                }
                return returned;
            }
        }

        public void SetClipboard(string clipboard)
        {
            Raylib.SetClipboardText(clipboard);
        }

        public void SetExitKey(KeyboardKey key)
        {
            Raylib.SetExitKey(key);
        }

        public void SetMasterVolume(float vol)
        {
            Raylib.SetMasterVolume(vol);
        }

        public void CloseWindow()
        {
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Funny joke hee hee ha ha
        /// </summary>
        public void PRANK()
        {
            Console.WriteLine("Try pasting anywhere :)");
            Raylib.SetClipboardText("🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆👲🐒🍆👲🐒🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆🍆");
        }

        public string GetClipboard
        {
            get { return Raylib.GetClipboardText_(); }
        }

        public Vector2 MousePosition
        {
            get { return Raylib.GetMousePosition(); }
        }

        public void SetMouseScale(Vector2 scale)
        {
            Raylib.SetMouseScale(scale.X, scale.Y);
        }

        public void SetMousePosition(Vector2 scale)
        {
            Raylib.SetMousePosition((int)scale.X, (int)scale.Y);
        }

        public void SetMouseCursor(MouseCursor cursor)
        {
            Raylib.SetMouseCursor(cursor);
        }

        public bool GetMouseButtonDown(MouseButton b)
        {
            return Raylib.IsMouseButtonDown(b);
        }

        public bool GetMouseButton(MouseButton b)
        {
            return Raylib.IsMouseButtonPressed(b);
        }

        public bool GetMouseButtonUp(MouseButton b)
        {
            return Raylib.IsMouseButtonReleased(b);
        }

        public bool GetKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyPressed(key);
        }

        public bool GetKey(KeyboardKey key)
        {
            return Raylib.IsKeyDown(key);
        }

        public bool GetKeyUp(KeyboardKey key)
        {
            return Raylib.IsKeyReleased(key);
        }

        public void Draw_Text(Vector2 pos, string text, float fontsize, Color col, Font? font = null, float spacing = 1)
        {
            Raylib.DrawTextEx(font == null ? Raylib.GetFontDefault() : font.Value, text, pos, fontsize, spacing, col);
        }

        public void Draw_Rectangle(Vector2 pos, float rot, Vector2 Size, Color colour)
        {
            Raylib.DrawRectanglePro(new Rectangle(pos, Size), CAMERA_POSITION, rot, colour);
        }

        public void Draw_Circle(Vector2 pos, float radius, Color colour)
        {
            Raylib.DrawCircleV(CAMERA_POSITION + pos, radius, colour);
        }

        public void Draw_Circle_Outline(Vector2 pos, float radius, Color colour)
        {
            Raylib.DrawCircleLinesV(CAMERA_POSITION + pos, radius, colour);
        }

        public void Draw_Circle_Sector(Vector2 pos, float radius, float startangle, float endangle, Color colour, int segments = 64)
        {
            Raylib.DrawCircleSector(CAMERA_POSITION + pos, radius, startangle, endangle, segments, colour);
        }

        public void Draw_Circle_Sector_Outline(Vector2 pos, float radius, float startangle, float endangle, Color colour, int segments = 64)
        {
            Raylib.DrawCircleSectorLines(CAMERA_POSITION + pos, radius, startangle, endangle, segments, colour);
        }

        public void Draw_Poly(Vector2 pos, float rot, int sides, float radius, Color colour)
        {
            Raylib.DrawPoly(CAMERA_POSITION + pos, sides, radius, rot, colour);
        }

        public void Draw_Poly_Outline(Vector2 pos, float rot, int sides, float radius, float thickness, Color colour)
        {
            Raylib.DrawPolyLinesEx(CAMERA_POSITION + pos, sides, radius, rot, thickness, colour);
        }

        public void Draw_Line(Vector2 start, Vector2 end, float thickness, Color col)
        {
            Raylib.DrawLineEx(CAMERA_POSITION + start, CAMERA_POSITION + end, thickness, col);
        }

        public void Draw_Lines(List<Vector2> Points, float thickness, Color col)
        {
            for (int i = 1; i < Points.Count; i++)
            {
                Draw_Line(CAMERA_POSITION + Points[i], CAMERA_POSITION + Points[i - 1], thickness, col);
            }
        }

        public void Draw_Lines(Vector2[] Points, float thickness, Color col)
        {
            for (int i = 1; i < Points.Length; i++)
            {
                Draw_Line(CAMERA_POSITION + Points[i], CAMERA_POSITION + Points[i - 1], thickness, col);
            }
        }

        public void Draw_Texture(Vector2 pos, float rot, float scale, Color tint, Texture2D texture)
        {
            Raylib.DrawTextureEx(texture, CAMERA_POSITION+pos, rot, scale, tint);
        }
    }
}

