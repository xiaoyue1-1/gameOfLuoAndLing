using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[InitializeOnLoad]
public static class SkyBlurDiagnostics
{
    static SkyBlurDiagnostics()
    {
        EditorApplication.delayCall += () =>
        {
            var data = AssetDatabase.LoadAssetAtPath<ScriptableRendererData>("Assets/Settings/CameraBordersRenderer.asset");
            foreach (var feature in data.rendererFeatures)
                Debug.Log($"SkyBlurDiagnostic feature={feature} type={feature?.GetType()} active={feature?.isActive}");
            Debug.Log($"SkyBlurDiagnostic pipeline={GraphicsSettings.currentRenderPipeline}");
            var pipeline = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
            var renderer = pipeline.GetRenderer(0);
            var field = typeof(ScriptableRenderer).GetProperty("rendererFeatures", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var features = field.GetValue(renderer) as System.Collections.ICollection;
            Debug.Log($"SkyBlurDiagnostic renderer={renderer.GetType()} features={features.Count}");
            foreach (ScriptableRendererFeature feature in (System.Collections.IEnumerable)features)
                Debug.Log($"SkyBlurDiagnostic liveFeature={feature.GetType()} active={feature.isActive}");
            var texture = Shader.GetGlobalTexture("_GlobalUniversalBlurTexture");
            Debug.Log($"SkyBlurDiagnostic renderPipeline={RenderPipelineManager.currentPipeline?.GetType()} texture={texture}");
            var gameView = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            gameView.Show();
            gameView.Focus();
            gameView.Repaint();
        };
    }
}
