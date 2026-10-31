using UnityEditor;
using UnityEngine;

public static class GameWindowSettings
{
    [MenuItem("Tools/Game/Configure Window")]
    public static void Configure()
    {
        PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
        PlayerSettings.defaultScreenWidth = GameWindowController.DefaultWidth;
        PlayerSettings.defaultScreenHeight = GameWindowController.DefaultHeight;
        PlayerSettings.defaultIsNativeResolution = false;
        PlayerSettings.resizableWindow = true;
        PlayerSettings.allowFullscreenSwitch = true;
        AssetDatabase.SaveAssets();
        Debug.Log($"Game window configured: mode={PlayerSettings.fullScreenMode}, " +
            $"size={PlayerSettings.defaultScreenWidth}x{PlayerSettings.defaultScreenHeight}, " +
            $"resizable={PlayerSettings.resizableWindow}, " +
            $"nativeResolution={PlayerSettings.defaultIsNativeResolution}");
    }
}
