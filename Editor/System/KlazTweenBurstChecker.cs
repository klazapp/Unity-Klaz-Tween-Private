#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[InitializeOnLoad]
public class KlazTweenBurstChecker
{
    private static ListRequest listRequest;
    private const string BURST_CHECKED_KEY = "KlazTween_BurstChecked";

    static KlazTweenBurstChecker()
    {
        if (!EditorPrefs.GetBool(BURST_CHECKED_KEY, false))
        {
            CheckForBurstPackage();
        }
    }

    private static void CheckForBurstPackage()
    {
        listRequest = Client.List();
        EditorApplication.update += Progress;
    }

    private static void Progress()
    {
        if (!listRequest.IsCompleted)
            return;

        //Unsubscribe from EditorApplication.update
        EditorApplication.update -= Progress;
        EditorPrefs.SetBool(BURST_CHECKED_KEY, true);
        
        switch (listRequest.Status)
        {
            case StatusCode.Success:
                foreach (var package in listRequest.Result)
                {
                    if (package.name != "com.unity.burst") 
                        continue;
                    
                    Debug.Log("Burst package is installed: " + package.version);
                    SetScriptingDefineSymbol("KLAZAPP_ENABLE_JOBSYSTEM");
                    return;
                }

                Debug.Log("Burst package is not installed. It is recommended to install Unity's burst package for improved performance.");
                break;
            case >= StatusCode.Failure:
                Debug.LogError("Failed to check packages: " + listRequest.Error.message);
                break;
        }
    }

    private static void SetScriptingDefineSymbol(string defineSymbol)
    {
        var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        var definesList = definesString.Split(';').ToList();

        if (definesList.Contains(defineSymbol)) 
            return;
        
        definesList.Add(defineSymbol);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", definesList.ToArray()));
    }
}
#endif