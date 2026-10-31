using UnityEngine;

/// <summary>Desktop window controls, available in every scene.</summary>
public sealed class GameWindowController : MonoBehaviour
{
    public const int DefaultWidth = 1280;
    public const int DefaultHeight = 720;

    private int windowWidth = DefaultWidth;
    private int windowHeight = DefaultHeight;
    private bool switching;
    private FullScreenMode requestedMode;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
#if UNITY_STANDALONE && !UNITY_EDITOR
        var root = new GameObject(nameof(GameWindowController));
        DontDestroyOnLoad(root);
        root.AddComponent<GameWindowController>();
#endif
    }

    private void Awake()
    {
        // Override Unity's remembered fullscreen state on each launch.
        int desktopWidth = Display.main.systemWidth;
        int desktopHeight = Display.main.systemHeight;
        if (desktopWidth > 0 && desktopHeight > 0)
        {
            float scale = Mathf.Min(1f, desktopWidth * 0.8f / DefaultWidth,
                desktopHeight * 0.8f / DefaultHeight);
            windowWidth = Mathf.Max(1, Mathf.RoundToInt(DefaultWidth * scale));
            windowHeight = Mathf.Max(1, Mathf.RoundToInt(DefaultHeight * scale));
        }
        ChangeMode(windowWidth, windowHeight, FullScreenMode.Windowed);
    }

    private void Update()
    {
        // SetResolution is applied at the end of the frame. Wait before sampling size.
        if (switching)
        {
            if (Screen.fullScreenMode == requestedMode) switching = false;
            return;
        }

        if (!Screen.fullScreen)
        {
            windowWidth = Mathf.Max(1, Screen.width);
            windowHeight = Mathf.Max(1, Screen.height);
        }

        if (!Input.GetKeyDown(KeyCode.F11)) return;

        if (Screen.fullScreen)
            ChangeMode(windowWidth, windowHeight, FullScreenMode.Windowed);
        else
        {
            Resolution desktop = Screen.currentResolution;
            ChangeMode(desktop.width, desktop.height, FullScreenMode.FullScreenWindow);
        }
    }

    private void ChangeMode(int width, int height, FullScreenMode mode)
    {
        requestedMode = mode;
        switching = true;
        Screen.SetResolution(width, height, mode);
    }
}
