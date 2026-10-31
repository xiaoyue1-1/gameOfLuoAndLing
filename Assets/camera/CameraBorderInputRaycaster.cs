using UnityEngine;
using UnityEngine.UI;

// RenderTexture camera pixels differ from the viewport shown on the display.
// Use a non-rendering camera with that display viewport for UI hit testing.
public sealed class CameraBorderInputRaycaster : GraphicRaycaster
{
    public Camera InputCamera { get; set; }
    public override Camera eventCamera => InputCamera != null ? InputCamera : base.eventCamera;
}
