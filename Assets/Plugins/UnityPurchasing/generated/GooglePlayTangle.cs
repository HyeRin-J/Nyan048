#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("MrG/sIAysbqyMrGxsCNpCTOnhq7g+ohM6ppmGvsRY4YY7Zsdp89KZ12NhbufxKJ2Tuk58PE/SF51q/2B6zdR2vjlUF2nvc3C7NHZasPt/Rd5G6T+QmyN7oc4P6tWPyOarX6ptu8CfysTRJVqG/0bUUDxqNzB36ynW7QgVSriSU1TrURXbfS41jPiuFT+gucwwLp62PtIBzOGofNeqVoeAIAysZKAvba5mjb4Nke9sbGxtbCzSFroMn/NNo2HRONavTBsVapfoxznoGpRykSuiwMaa+QKNQqoO7Mrpg5EihN6yI9qYEOKA0l98iwil5JYwSeJ0INQZ9fMvc/zREJ/ZAmVnSIC0x8hbn52HpYpxaahwS0zoEx1dmx2vtFnqUYi+7KzsbCx");
        private static int[] order = new int[] { 1,7,5,12,9,5,9,12,12,13,13,13,12,13,14 };
        private static int key = 176;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
