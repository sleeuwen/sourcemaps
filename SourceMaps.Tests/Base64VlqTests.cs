using System;
using System.Collections.Generic;
using Xunit;

namespace SourceMaps.Tests
{
    public class Base64VlqTests
    {
        [Fact]
        public void ToBase64()
        {
            for (var i = 0; i < 64; i++)
            {
                _ = Base64Vlq.ToBase64(i);
            }
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(64)]
        public void ToBase64_OutOfRange_ThrowsOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Base64Vlq.ToBase64(value); });
        }

        [Theory]
        [MemberData(nameof(VlqData))]
        public void Decode(int expected, string input)
        {
            var result = Base64Vlq.DecodeValue(input.AsSpan(), out _);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> VlqData = new List<object[]>
        {
            new object[] { -255, "/P" },
            new object[] { -254, "9P" },
            new object[] { -253, "7P" },
            new object[] { -252, "5P" },
            new object[] { -251, "3P" },
            new object[] { -250, "1P" },
            new object[] { -249, "zP" },
            new object[] { -248, "xP" },
            new object[] { -247, "vP" },
            new object[] { -246, "tP" },
            new object[] { -245, "rP" },
            new object[] { -244, "pP" },
            new object[] { -243, "nP" },
            new object[] { -242, "lP" },
            new object[] { -241, "jP" },
            new object[] { -240, "hP" },
            new object[] { -239, "/O" },
            new object[] { -238, "9O" },
            new object[] { -237, "7O" },
            new object[] { -236, "5O" },
            new object[] { -235, "3O" },
            new object[] { -234, "1O" },
            new object[] { -233, "zO" },
            new object[] { -232, "xO" },
            new object[] { -231, "vO" },
            new object[] { -230, "tO" },
            new object[] { -229, "rO" },
            new object[] { -228, "pO" },
            new object[] { -227, "nO" },
            new object[] { -226, "lO" },
            new object[] { -225, "jO" },
            new object[] { -224, "hO" },
            new object[] { -223, "/N" },
            new object[] { -222, "9N" },
            new object[] { -221, "7N" },
            new object[] { -220, "5N" },
            new object[] { -219, "3N" },
            new object[] { -218, "1N" },
            new object[] { -217, "zN" },
            new object[] { -216, "xN" },
            new object[] { -215, "vN" },
            new object[] { -214, "tN" },
            new object[] { -213, "rN" },
            new object[] { -212, "pN" },
            new object[] { -211, "nN" },
            new object[] { -210, "lN" },
            new object[] { -209, "jN" },
            new object[] { -208, "hN" },
            new object[] { -207, "/M" },
            new object[] { -206, "9M" },
            new object[] { -205, "7M" },
            new object[] { -204, "5M" },
            new object[] { -203, "3M" },
            new object[] { -202, "1M" },
            new object[] { -201, "zM" },
            new object[] { -200, "xM" },
            new object[] { -199, "vM" },
            new object[] { -198, "tM" },
            new object[] { -197, "rM" },
            new object[] { -196, "pM" },
            new object[] { -195, "nM" },
            new object[] { -194, "lM" },
            new object[] { -193, "jM" },
            new object[] { -192, "hM" },
            new object[] { -191, "/L" },
            new object[] { -190, "9L" },
            new object[] { -189, "7L" },
            new object[] { -188, "5L" },
            new object[] { -187, "3L" },
            new object[] { -186, "1L" },
            new object[] { -185, "zL" },
            new object[] { -184, "xL" },
            new object[] { -183, "vL" },
            new object[] { -182, "tL" },
            new object[] { -181, "rL" },
            new object[] { -180, "pL" },
            new object[] { -179, "nL" },
            new object[] { -178, "lL" },
            new object[] { -177, "jL" },
            new object[] { -176, "hL" },
            new object[] { -175, "/K" },
            new object[] { -174, "9K" },
            new object[] { -173, "7K" },
            new object[] { -172, "5K" },
            new object[] { -171, "3K" },
            new object[] { -170, "1K" },
            new object[] { -169, "zK" },
            new object[] { -168, "xK" },
            new object[] { -167, "vK" },
            new object[] { -166, "tK" },
            new object[] { -165, "rK" },
            new object[] { -164, "pK" },
            new object[] { -163, "nK" },
            new object[] { -162, "lK" },
            new object[] { -161, "jK" },
            new object[] { -160, "hK" },
            new object[] { -159, "/J" },
            new object[] { -158, "9J" },
            new object[] { -157, "7J" },
            new object[] { -156, "5J" },
            new object[] { -155, "3J" },
            new object[] { -154, "1J" },
            new object[] { -153, "zJ" },
            new object[] { -152, "xJ" },
            new object[] { -151, "vJ" },
            new object[] { -150, "tJ" },
            new object[] { -149, "rJ" },
            new object[] { -148, "pJ" },
            new object[] { -147, "nJ" },
            new object[] { -146, "lJ" },
            new object[] { -145, "jJ" },
            new object[] { -144, "hJ" },
            new object[] { -143, "/I" },
            new object[] { -142, "9I" },
            new object[] { -141, "7I" },
            new object[] { -140, "5I" },
            new object[] { -139, "3I" },
            new object[] { -138, "1I" },
            new object[] { -137, "zI" },
            new object[] { -136, "xI" },
            new object[] { -135, "vI" },
            new object[] { -134, "tI" },
            new object[] { -133, "rI" },
            new object[] { -132, "pI" },
            new object[] { -131, "nI" },
            new object[] { -130, "lI" },
            new object[] { -129, "jI" },
            new object[] { -128, "hI" },
            new object[] { -127, "/H" },
            new object[] { -126, "9H" },
            new object[] { -125, "7H" },
            new object[] { -124, "5H" },
            new object[] { -123, "3H" },
            new object[] { -122, "1H" },
            new object[] { -121, "zH" },
            new object[] { -120, "xH" },
            new object[] { -119, "vH" },
            new object[] { -118, "tH" },
            new object[] { -117, "rH" },
            new object[] { -116, "pH" },
            new object[] { -115, "nH" },
            new object[] { -114, "lH" },
            new object[] { -113, "jH" },
            new object[] { -112, "hH" },
            new object[] { -111, "/G" },
            new object[] { -110, "9G" },
            new object[] { -109, "7G" },
            new object[] { -108, "5G" },
            new object[] { -107, "3G" },
            new object[] { -106, "1G" },
            new object[] { -105, "zG" },
            new object[] { -104, "xG" },
            new object[] { -103, "vG" },
            new object[] { -102, "tG" },
            new object[] { -101, "rG" },
            new object[] { -100, "pG" },
            new object[] { -99, "nG" },
            new object[] { -98, "lG" },
            new object[] { -97, "jG" },
            new object[] { -96, "hG" },
            new object[] { -95, "/F" },
            new object[] { -94, "9F" },
            new object[] { -93, "7F" },
            new object[] { -92, "5F" },
            new object[] { -91, "3F" },
            new object[] { -90, "1F" },
            new object[] { -89, "zF" },
            new object[] { -88, "xF" },
            new object[] { -87, "vF" },
            new object[] { -86, "tF" },
            new object[] { -85, "rF" },
            new object[] { -84, "pF" },
            new object[] { -83, "nF" },
            new object[] { -82, "lF" },
            new object[] { -81, "jF" },
            new object[] { -80, "hF" },
            new object[] { -79, "/E" },
            new object[] { -78, "9E" },
            new object[] { -77, "7E" },
            new object[] { -76, "5E" },
            new object[] { -75, "3E" },
            new object[] { -74, "1E" },
            new object[] { -73, "zE" },
            new object[] { -72, "xE" },
            new object[] { -71, "vE" },
            new object[] { -70, "tE" },
            new object[] { -69, "rE" },
            new object[] { -68, "pE" },
            new object[] { -67, "nE" },
            new object[] { -66, "lE" },
            new object[] { -65, "jE" },
            new object[] { -64, "hE" },
            new object[] { -63, "/D" },
            new object[] { -62, "9D" },
            new object[] { -61, "7D" },
            new object[] { -60, "5D" },
            new object[] { -59, "3D" },
            new object[] { -58, "1D" },
            new object[] { -57, "zD" },
            new object[] { -56, "xD" },
            new object[] { -55, "vD" },
            new object[] { -54, "tD" },
            new object[] { -53, "rD" },
            new object[] { -52, "pD" },
            new object[] { -51, "nD" },
            new object[] { -50, "lD" },
            new object[] { -49, "jD" },
            new object[] { -48, "hD" },
            new object[] { -47, "/C" },
            new object[] { -46, "9C" },
            new object[] { -45, "7C" },
            new object[] { -44, "5C" },
            new object[] { -43, "3C" },
            new object[] { -42, "1C" },
            new object[] { -41, "zC" },
            new object[] { -40, "xC" },
            new object[] { -39, "vC" },
            new object[] { -38, "tC" },
            new object[] { -37, "rC" },
            new object[] { -36, "pC" },
            new object[] { -35, "nC" },
            new object[] { -34, "lC" },
            new object[] { -33, "jC" },
            new object[] { -32, "hC" },
            new object[] { -31, "/B" },
            new object[] { -30, "9B" },
            new object[] { -29, "7B" },
            new object[] { -28, "5B" },
            new object[] { -27, "3B" },
            new object[] { -26, "1B" },
            new object[] { -25, "zB" },
            new object[] { -24, "xB" },
            new object[] { -23, "vB" },
            new object[] { -22, "tB" },
            new object[] { -21, "rB" },
            new object[] { -20, "pB" },
            new object[] { -19, "nB" },
            new object[] { -18, "lB" },
            new object[] { -17, "jB" },
            new object[] { -16, "hB" },
            new object[] { -15, "f" },
            new object[] { -14, "d" },
            new object[] { -13, "b" },
            new object[] { -12, "Z" },
            new object[] { -11, "X" },
            new object[] { -10, "V" },
            new object[] { -9, "T" },
            new object[] { -8, "R" },
            new object[] { -7, "P" },
            new object[] { -6, "N" },
            new object[] { -5, "L" },
            new object[] { -4, "J" },
            new object[] { -3, "H" },
            new object[] { -2, "F" },
            new object[] { -1, "D" },
            new object[] { 0, "A" },
            new object[] { 1, "C" },
            new object[] { 2, "E" },
            new object[] { 3, "G" },
            new object[] { 4, "I" },
            new object[] { 5, "K" },
            new object[] { 6, "M" },
            new object[] { 7, "O" },
            new object[] { 8, "Q" },
            new object[] { 9, "S" },
            new object[] { 10, "U" },
            new object[] { 11, "W" },
            new object[] { 12, "Y" },
            new object[] { 13, "a" },
            new object[] { 14, "c" },
            new object[] { 15, "e" },
            new object[] { 16, "gB" },
            new object[] { 17, "iB" },
            new object[] { 18, "kB" },
            new object[] { 19, "mB" },
            new object[] { 20, "oB" },
            new object[] { 21, "qB" },
            new object[] { 22, "sB" },
            new object[] { 23, "uB" },
            new object[] { 24, "wB" },
            new object[] { 25, "yB" },
            new object[] { 26, "0B" },
            new object[] { 27, "2B" },
            new object[] { 28, "4B" },
            new object[] { 29, "6B" },
            new object[] { 30, "8B" },
            new object[] { 31, "+B" },
            new object[] { 32, "gC" },
            new object[] { 33, "iC" },
            new object[] { 34, "kC" },
            new object[] { 35, "mC" },
            new object[] { 36, "oC" },
            new object[] { 37, "qC" },
            new object[] { 38, "sC" },
            new object[] { 39, "uC" },
            new object[] { 40, "wC" },
            new object[] { 41, "yC" },
            new object[] { 42, "0C" },
            new object[] { 43, "2C" },
            new object[] { 44, "4C" },
            new object[] { 45, "6C" },
            new object[] { 46, "8C" },
            new object[] { 47, "+C" },
            new object[] { 48, "gD" },
            new object[] { 49, "iD" },
            new object[] { 50, "kD" },
            new object[] { 51, "mD" },
            new object[] { 52, "oD" },
            new object[] { 53, "qD" },
            new object[] { 54, "sD" },
            new object[] { 55, "uD" },
            new object[] { 56, "wD" },
            new object[] { 57, "yD" },
            new object[] { 58, "0D" },
            new object[] { 59, "2D" },
            new object[] { 60, "4D" },
            new object[] { 61, "6D" },
            new object[] { 62, "8D" },
            new object[] { 63, "+D" },
            new object[] { 64, "gE" },
            new object[] { 65, "iE" },
            new object[] { 66, "kE" },
            new object[] { 67, "mE" },
            new object[] { 68, "oE" },
            new object[] { 69, "qE" },
            new object[] { 70, "sE" },
            new object[] { 71, "uE" },
            new object[] { 72, "wE" },
            new object[] { 73, "yE" },
            new object[] { 74, "0E" },
            new object[] { 75, "2E" },
            new object[] { 76, "4E" },
            new object[] { 77, "6E" },
            new object[] { 78, "8E" },
            new object[] { 79, "+E" },
            new object[] { 80, "gF" },
            new object[] { 81, "iF" },
            new object[] { 82, "kF" },
            new object[] { 83, "mF" },
            new object[] { 84, "oF" },
            new object[] { 85, "qF" },
            new object[] { 86, "sF" },
            new object[] { 87, "uF" },
            new object[] { 88, "wF" },
            new object[] { 89, "yF" },
            new object[] { 90, "0F" },
            new object[] { 91, "2F" },
            new object[] { 92, "4F" },
            new object[] { 93, "6F" },
            new object[] { 94, "8F" },
            new object[] { 95, "+F" },
            new object[] { 96, "gG" },
            new object[] { 97, "iG" },
            new object[] { 98, "kG" },
            new object[] { 99, "mG" },
            new object[] { 100, "oG" },
            new object[] { 101, "qG" },
            new object[] { 102, "sG" },
            new object[] { 103, "uG" },
            new object[] { 104, "wG" },
            new object[] { 105, "yG" },
            new object[] { 106, "0G" },
            new object[] { 107, "2G" },
            new object[] { 108, "4G" },
            new object[] { 109, "6G" },
            new object[] { 110, "8G" },
            new object[] { 111, "+G" },
            new object[] { 112, "gH" },
            new object[] { 113, "iH" },
            new object[] { 114, "kH" },
            new object[] { 115, "mH" },
            new object[] { 116, "oH" },
            new object[] { 117, "qH" },
            new object[] { 118, "sH" },
            new object[] { 119, "uH" },
            new object[] { 120, "wH" },
            new object[] { 121, "yH" },
            new object[] { 122, "0H" },
            new object[] { 123, "2H" },
            new object[] { 124, "4H" },
            new object[] { 125, "6H" },
            new object[] { 126, "8H" },
            new object[] { 127, "+H" },
            new object[] { 128, "gI" },
            new object[] { 129, "iI" },
            new object[] { 130, "kI" },
            new object[] { 131, "mI" },
            new object[] { 132, "oI" },
            new object[] { 133, "qI" },
            new object[] { 134, "sI" },
            new object[] { 135, "uI" },
            new object[] { 136, "wI" },
            new object[] { 137, "yI" },
            new object[] { 138, "0I" },
            new object[] { 139, "2I" },
            new object[] { 140, "4I" },
            new object[] { 141, "6I" },
            new object[] { 142, "8I" },
            new object[] { 143, "+I" },
            new object[] { 144, "gJ" },
            new object[] { 145, "iJ" },
            new object[] { 146, "kJ" },
            new object[] { 147, "mJ" },
            new object[] { 148, "oJ" },
            new object[] { 149, "qJ" },
            new object[] { 150, "sJ" },
            new object[] { 151, "uJ" },
            new object[] { 152, "wJ" },
            new object[] { 157, "6J" },
            new object[] { 158, "8J" },
            new object[] { 159, "+J" },
            new object[] { 160, "gK" },
            new object[] { 161, "iK" },
            new object[] { 162, "kK" },
            new object[] { 163, "mK" },
            new object[] { 164, "oK" },
            new object[] { 165, "qK" },
            new object[] { 166, "sK" },
            new object[] { 167, "uK" },
            new object[] { 168, "wK" },
            new object[] { 169, "yK" },
            new object[] { 170, "0K" },
            new object[] { 171, "2K" },
            new object[] { 172, "4K" },
            new object[] { 173, "6K" },
            new object[] { 174, "8K" },
            new object[] { 175, "+K" },
            new object[] { 176, "gL" },
            new object[] { 177, "iL" },
            new object[] { 178, "kL" },
            new object[] { 179, "mL" },
            new object[] { 180, "oL" },
            new object[] { 181, "qL" },
            new object[] { 182, "sL" },
            new object[] { 183, "uL" },
            new object[] { 184, "wL" },
            new object[] { 185, "yL" },
            new object[] { 186, "0L" },
            new object[] { 187, "2L" },
            new object[] { 188, "4L" },
            new object[] { 189, "6L" },
            new object[] { 190, "8L" },
            new object[] { 191, "+L" },
            new object[] { 192, "gM" },
            new object[] { 193, "iM" },
            new object[] { 194, "kM" },
            new object[] { 195, "mM" },
            new object[] { 196, "oM" },
            new object[] { 197, "qM" },
            new object[] { 198, "sM" },
            new object[] { 199, "uM" },
            new object[] { 200, "wM" },
            new object[] { 201, "yM" },
            new object[] { 202, "0M" },
            new object[] { 203, "2M" },
            new object[] { 204, "4M" },
            new object[] { 205, "6M" },
            new object[] { 206, "8M" },
            new object[] { 207, "+M" },
            new object[] { 208, "gN" },
            new object[] { 209, "iN" },
            new object[] { 210, "kN" },
            new object[] { 211, "mN" },
            new object[] { 212, "oN" },
            new object[] { 213, "qN" },
            new object[] { 214, "sN" },
            new object[] { 215, "uN" },
            new object[] { 216, "wN" },
            new object[] { 217, "yN" },
            new object[] { 218, "0N" },
            new object[] { 219, "2N" },
            new object[] { 220, "4N" },
            new object[] { 221, "6N" },
            new object[] { 222, "8N" },
            new object[] { 223, "+N" },
            new object[] { 224, "gO" },
            new object[] { 225, "iO" },
            new object[] { 226, "kO" },
            new object[] { 227, "mO" },
            new object[] { 228, "oO" },
            new object[] { 229, "qO" },
            new object[] { 230, "sO" },
            new object[] { 231, "uO" },
            new object[] { 232, "wO" },
            new object[] { 233, "yO" },
            new object[] { 234, "0O" },
            new object[] { 235, "2O" },
            new object[] { 236, "4O" },
            new object[] { 237, "6O" },
            new object[] { 238, "8O" },
            new object[] { 239, "+O" },
            new object[] { 240, "gP" },
            new object[] { 241, "iP" },
            new object[] { 242, "kP" },
            new object[] { 243, "mP" },
            new object[] { 244, "oP" },
            new object[] { 245, "qP" },
            new object[] { 246, "sP" },
            new object[] { 247, "uP" },
            new object[] { 248, "wP" },
            new object[] { 249, "yP" },
            new object[] { 250, "0P" },
            new object[] { 251, "2P" },
            new object[] { 252, "4P" },
            new object[] { 253, "6P" },
            new object[] { 254, "8P" },
            new object[] { 255, "+P" }
        };
    }
}
