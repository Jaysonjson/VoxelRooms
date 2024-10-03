using Godot;
using System;

public partial class Vec2Argument : IArgumentCodec<Vector2>
{

    public static Vec2Argument INSTANCE = new Vec2Argument();
    
    public String Deserialize(Vector2 vec) {
        return "vec2(" + vec.X + "," + vec.Y + ")";
    }

    public Vector2 Serialize(String s) {
        if(s.Contains(',')) {
            s = s[5..s.Length];
            string[] coords = s.Split(",");
            coords[1] = coords[1].Substring(coords[1].Length - 2, coords[1].Length - 1);
            return new Vector2(coords[0].ToFloat(), coords[1].ToFloat());
        }
        return new Vector2(0, 0);
    }
}
