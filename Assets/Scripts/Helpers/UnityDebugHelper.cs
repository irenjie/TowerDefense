using UnityEngine;

namespace Helper {
    public class UnityDebugHelper {
        public static void Log(Object message) {
#if Debug_Mode || Develop_Mode
            Debug.Log(message);
#endif
        }

        public static void LogWarning(Object message) {
#if Debug_Mode || Develop_Mode
            Debug.LogWarning(message);
#endif
        }

        public static void LogError(Object message) {
            Debug.LogError(message);
        }

        public static void LogException(System.Exception exception) {
            Debug.LogException(exception);
        }

        public static void LogAssertion(Object message) {
#if Debug_Mode || Develop_Mode
            Debug.LogAssertion(message);
#endif
        }

    }
}