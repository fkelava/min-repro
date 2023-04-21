namespace ExampleSln;

public readonly struct ImplementingStruct : IBlockSerializable<ImplementingStruct>
{
    public ImplementingStruct(ushort a,
                              float  b,
                              double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public readonly double C;
    public readonly float  B;
    public readonly ushort A;

    public static bool SInitialize()
    {
        return true;
    }
}