using System;
using Raylib_cs;
namespace SauceSharp.Components;

public class Inputfield_Oneline : Component
{
    public string CurrText;
    public float FontSize;
    public Color Colour, BackgroundColour;
    public float Spacing;
    public float HoldDeleteFrames, DeleteSpeed, TypeCooldown;
    public int LineThresh;
    public int MaxChars;
    //private int currlines;
    private float t, e, x;

    public Inputfield_Oneline(string currText, float fontsize, Color col, Color bgcol, int maxcharacters, float spacing = 1, float holddeletethresh = 400, float typecooldown = 6f, float delspeed = 25)
    {
        CurrText = currText;
        FontSize = fontsize;
        Colour = col;
        BackgroundColour = bgcol;
        HoldDeleteFrames = holddeletethresh;
        DeleteSpeed = delspeed;
        //LineThresh = linethresh;
        TypeCooldown = typecooldown;
        MaxChars = maxcharacters;
        Spacing = spacing;
    }

    public override void DrawUpdate()
    {
        if(Owner == null) { return; }
        var stuff = Game.TheGame.GetCharsPressed;
        if (stuff.Count > 0 && x<=0 && CurrText.Length <= MaxChars) { x = TypeCooldown; CurrText += new string(stuff.ToArray());  }
        x--;
        if((Game.TheGame.GetKeyDown(KeyboardKey.Backspace) || Game.TheGame.GetKeyDown(KeyboardKey.Delete)) && CurrText.Length>0) { CurrText = CurrText[..^1]; }
        if((Game.TheGame.GetKey(KeyboardKey.Backspace) || Game.TheGame.GetKey(KeyboardKey.Delete))) { t++; if (t >= HoldDeleteFrames && CurrText.Length>0) { e--; if (e <= 0) { e = DeleteSpeed; CurrText = CurrText[..^1]; } } }
        else { t = 0; }
        //if(Game.TheGame.GetKeyDown(KeyboardKey.Enter) || Game.TheGame.GetKeyDown(KeyboardKey.KpEnter)) { CurrText += '\n'; CurrText += '\n'; CurrText += '\n'; }
        Game.TheGame.Draw_Text(Owner.Pos, CurrText, FontSize, Colour, null, Spacing);
    }
}

