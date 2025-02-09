using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenCaptureToRT : MonoBehaviour {
    public RenderTexture compositeRT;

    // This function is called after the camera has finished rendering.
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        // Copy the final rendered image to the composite render texture.
        if (compositeRT != null) {
            Graphics.Blit(source, compositeRT);
        }
        // Continue with normal rendering.
        Graphics.Blit(source, destination);
    }
}