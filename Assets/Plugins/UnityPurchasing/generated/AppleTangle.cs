#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("emZvKklveH5jbGNpa35jZWQqS38N5ncziYFZKtkyzru1kEUAYfUh9ogLCgwDIIxCjP1pbg8LOov4OiAMNyxtKoA5YP0HiMXU4akl81lgUW59fSRrenpmbyRpZWcla3p6Zm9pa2NsY2lrfmNlZCpLf35iZXhjfnM7vRG3mUguGCDNBRe8R5ZUacJBih1TrQ8Ddh1KXBsUftm9gSkxTanfZSplbCp+Ym8qfmJvZCprenpmY2lrcypreXl/Z295KmtpaW96fmtkaW9+Y2xjaWt+bypocyprZHMqemt4fgIhDAsPDw0ICxwUYn5+enkwJSV9BwwDIIxCjP0HCwsPDwoJiAsLClaBE4PU80Fm/w2hKDoI4hI08loD2TqIDrE6iAmpqgkICwgICwg6BwwDuzpS5lAOOIZiuYUX1G959W1Ub7aflHAGrk2BUd4cPTnBzgVHxB5j20PSfJU5Hm+rfZ7DJwgJCwoLqYgLJippb3h+Y2xjaWt+byp6ZWZjaXNoZm8qeX5rZG5reG4qfm94Z3kqaypJSzqICyg6BwwDIIxCjP0HCwsLBZc3+SFDIhDC9MS/swTTVBbcwTcMOgUMCV8XGQsL9Q4POgkLC/U6FzobDAlfDgAZAEt6emZvKkNkaSQ7WG9mY2tkaW8qZWQqfmJjeSppb3gu6OHbvXrVBU/rLcD7Z3Ln7b8dHYV5i2rMEVEDJZi48k5C+moylB//wxN4/1cE33VVkfgvCbBfhUdXB/sgjEKM/QcLCw8PCjpoOwE6AwwJXyw6LgwJXw4BGRdLenpmbypJb3h+Pzg7Pjo5PFAdBzk/Ojg6Mzg7PjpPdBVGYVqcS4POfmgBGolLjTmAi2RuKmllZG5jfmNlZHkqZWwqf3lvFY+JjxGTN009+KORSoQm3ruaGNKi1nQoP8Av39MF3GHeqC4pG/2rpiRKrP1NR3UCVDoVDAlfFykOEjocymk5ff0wDSZc4dAFKwTQsHkTRb91S6KS89vAbJYuYRvaqbHuESDJFaGpe5hNWV/LpSVLufLx6XrH7KlGemZvKlhlZX4qSUs6FB0HOjw6PjgVm9EUTVrhD+dUc44n4TyoXUZf5jyTRidyveeGkdb5fZH4fNh9OkXLfmJleGN+czscOh4MCV8OCRkHS3p4a2l+Y2lvKnl+a35vZ29kfnkkOnA6iAt8OgQMCV8XBQsL9Q4OCQgLtP55keTYbgXBc0U+0qg083L1YcICVDqICxsMCV8XKg6ICwI6iAsOOm4/KR9BH1MXuZ79/JaUxVqwy1JabYUCvir9waYmKmV6vDULOoa9ScUlOovJDAIhDAsPDw0ICDqLvBCLuYoeIdpjTZ58A/T+YYckSqz9TUd1DAlfFwQOHA4eIdpjTZ58A/T+YYccOh4MCV8OCRkHS3p6Zm8qWGVlfg8KCYgLBQo6iAsACIgLCwrum6MDvzCn/gUECpgBuyscJH7fNgfRaBxmbypDZGkkOyw6LgwJXw4BGRdLeiprZG4qaW94fmNsY2lrfmNlZCp6OTxQOmg7AToDDAlfDgwZCF9ZOxnTPHXLjV/TrZOzOEjx0t97lHSrWA4MGQhfWTsZOhsMCV8OABkAS3p6WqCA39Du9toDDT26f38r");
        private static int[] order = new int[] { 4,51,34,56,36,11,59,52,41,40,39,35,47,16,36,26,18,27,44,42,44,22,29,40,38,53,55,42,51,39,52,53,43,49,54,53,57,42,38,59,55,48,53,46,51,50,49,51,59,53,53,52,52,57,55,56,57,59,58,59,60 };
        private static int key = 10;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
