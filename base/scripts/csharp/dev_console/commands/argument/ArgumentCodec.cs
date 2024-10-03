using Godot;
using System;

public interface IArgumentCodec<T> {

    String Deserialize(Vector2 vec);
    T Serialize(String s);

}
