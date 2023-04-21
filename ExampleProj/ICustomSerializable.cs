using System.Diagnostics.CodeAnalysis;

namespace ExampleSln;

public interface ICustomSerializable<T> : ISerializable<T> where T : ICustomSerializable<T>
{
    static bool ISerializable<T>.SInitialize()
    {
        return T.SCustomInitialize();
    }

    static int ISerializable<T>.SGetSize()
    {
        return T.SCustomGetSize();
    }

    static bool ISerializable<T>.SSerialize(in ReadOnlySpan<T> src, in Span<byte> dest, out int bytesWritten)
    {
        bytesWritten = 0;

        int  sliceSize = T.SGetSize();
        bool retval    = true;

        foreach (T item in src)
        {
            if (T.SCustomSerialize(dest[bytesWritten..(bytesWritten + sliceSize)], item))
                bytesWritten += sliceSize;
            else
                retval = false;
        }

        return retval;
    }

    static bool ISerializable<T>.SDeserialize(in ReadOnlySpan<byte> src, in Span<T> dest, int srcLength)
    {
        int  sliceSize = T.SGetSize();
        int  curPos    = 0;
        int  curIdx    = 0;
        bool retval    = true;

        while (curPos < srcLength)
        {
            if (!T.SCustomDeserialize(src[curPos..(curPos + sliceSize)], out T? item))
            {
                retval = false;
                continue;
            }

            dest[curIdx++] = item;
            curPos        += sliceSize;
        }

        return retval && curIdx != 0;
    }

    public static abstract bool SCustomInitialize();
    public static abstract int  SCustomGetSize();
    public static abstract bool SCustomSerialize(in Span<byte> dest, in T t, bool canFilter = true);
    public static abstract bool SCustomDeserialize(in ReadOnlySpan<byte> src, [NotNullWhen(true)] out T? t, bool canFilter = true);
}