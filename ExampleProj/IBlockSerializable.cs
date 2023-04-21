using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ExampleSln;

public interface IBlockSerializable<T> : ISerializable<T> where T : struct
{
    static int ISerializable<T>.SGetSize()
    {
        return Unsafe.SizeOf<T>();
    }

    static bool ISerializable<T>.SSerialize(in ReadOnlySpan<T> src, in Span<byte> dest, out int bytesWritten)
    {
        bytesWritten = Unsafe.SizeOf<T>() * src.Length;
        Debug.Assert(bytesWritten <= dest.Length);

        MemoryMarshal.AsBytes(src).CopyTo(dest);
        return true;
    }

    static bool ISerializable<T>.SDeserialize(in ReadOnlySpan<byte> src, in Span<T> dest, int srcLength)
    {
        int destsize = Unsafe.SizeOf<T>() * dest.Length;
        Debug.Assert(destsize >= srcLength);

        MemoryMarshal.Cast<byte, T>(src).CopyTo(dest);
        return true;
    }
}
