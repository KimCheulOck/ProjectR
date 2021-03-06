﻿public class Debug
{
    public static void Log(string description, params object[] param)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogFormat(description, param);
#endif
    }

    public static void Log(string description)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(description);
#endif
    }

    public static void Log(object description)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(description);
#endif
    }

    public static void LogError(string description, params object[] param)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogErrorFormat(description, param);
#endif
    }

    public static void LogError(string description)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogError(description);
#endif
    }

    public static void LogWarning(string description, params object[] param)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogWarningFormat(description, param);
#endif
    }
}
