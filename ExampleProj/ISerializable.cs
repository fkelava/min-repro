namespace ExampleSln;

public interface ISerializable<T>
{
    public static abstract bool SInitialize();
    public static abstract int  SGetSize();
    public static abstract bool SSerialize(in ReadOnlySpan<T> src, in Span<byte> dest, out int bytesWritten);
    public static abstract bool SDeserialize(in ReadOnlySpan<byte> src, in Span<T> dest, int srcLength);
}
