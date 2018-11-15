#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uTo0Owu5OjE5uTo6O54eFv0mVZgLuToZCzY9MhG9c73MNjo6Oj47OPz56uApSS9OblBa1RJYorUcakkfRS5qolGvMBtaJS7+BO0EVlAUl31bVO2qWx/AQizViVTgHhmOkseoovmqzaubTQ7spgbuXX7lWJywEKhyRy68GB2l4PjSZ65RG2kUjF+sjNn+HwKqceYVOwKQh1vYb8Y74+c9WR2ioOnkm6Bid4/4Wkv1lUCidAXHXrAweU3isg521PRCNPDcRxZiHHhBvqBXhGOautiEEh7eC2UcBoGEL8sTy8bB1aOz+geIT3y82Q914R/aRwxmXmBI2MQIOGR3rUg8gcqcBQwoSgrEy307Xdbb5b58BR0Tz/QeUTcdCVXw5W5MDDk4Ojs6");
        private static int[] order = new int[] { 1,1,5,11,9,12,6,8,11,13,12,13,12,13,14 };
        private static int key = 59;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
