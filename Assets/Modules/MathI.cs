using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

internal class MathI
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(int a) => (a & 1) == 0;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(int a) => (a & 1) != 0;
}

