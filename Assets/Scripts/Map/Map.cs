using System;
[Serializable]
public class Map
{
    public MapTile[] List;
    public int Length => List.Length;

    public MapTile this[int i]
    {
        get => List[i];
        set => List[i] = value;
    }
}
