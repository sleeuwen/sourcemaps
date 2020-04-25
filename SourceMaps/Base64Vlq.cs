using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceMaps
{
    internal static class Base64Vlq
    {
        private const int VlqBaseShift = 5;
        private const int VlqBase = 1 << VlqBaseShift;
        private const int VlqBaseMask = VlqBase - 1;
        private const int VlqContinuationBit = VlqBase;

        private const string Base64Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private static readonly Dictionary<char, int> Base64CharacterMap = Base64Characters
                .Select((c, i) => (c, i))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

        internal static int FromBase64(char base64Value)
        {
            if (Base64CharacterMap.TryGetValue(base64Value, out var result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(base64Value), "Invalid base64 character");
        }

        internal static int FromVlqSigned(int value)
        {
            var negate = (value & 1) == 1;
            value >>= 1;
            return negate ? -value : value;
        }

        internal static char ToBase64(int value)
        {
            if (value < 0 || value >= Base64Characters.Length)
                throw new ArgumentOutOfRangeException(nameof(value));

            return Base64Characters[value];
        }

        public static List<int> Decode(string input)
        {
            var result = new List<int>();

            var currentPosition = 0;
            while (input.Length > currentPosition)
            {
                result.Add(DecodeValue(input.AsSpan(currentPosition), out var offset));
                currentPosition += offset;
            }

            return result;
        }

        internal static int DecodeValue(ReadOnlySpan<char> value, out int position)
        {
            var result = 0;
            var continuation = false;
            var shift = 0;
            position = 0;

            do
            {
                var c = value[position++];
                var digit = FromBase64(c);
                continuation = (digit & VlqContinuationBit) != 0;
                digit &= VlqBaseMask;
                result += (digit << shift);
                shift += VlqBaseShift;
            } while (continuation);

            return FromVlqSigned(result);
        }
    }
}
