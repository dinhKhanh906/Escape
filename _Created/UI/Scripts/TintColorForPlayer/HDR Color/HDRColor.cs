using UnityEngine;
public class HDRColor
{
    public const byte k_MaxByteForOverexposedColor = 191; //internal Unity const 
    public static float GetIntensity(Color hdrColor)
    {
        // Get color intensity
        float maxColorComponent = hdrColor.maxColorComponent;
        float scaleFactorToGetIntensity = k_MaxByteForOverexposedColor / maxColorComponent;
        float currentIntensity = Mathf.Log(255f / scaleFactorToGetIntensity) / Mathf.Log(2f);

        return currentIntensity;
    }
    public static Color GetOriginalColor(Color hdrColor)
    {
        // Get original color with ZERO intensity
        float intensity = GetIntensity(hdrColor);
        float currentScaleFactor = Mathf.Pow(2, intensity);
        Color originalColorWithoutIntensity = hdrColor / currentScaleFactor;

        return originalColorWithoutIntensity;
    }
    public static void SetIntensity(ref Color hdrColorRequire, float intensity)
    {
        Color originalColorWithoutIntensity = GetOriginalColor(hdrColorRequire);
        // Set color intensity
        float newScaleFactor = Mathf.Pow(2, intensity);
        hdrColorRequire = originalColorWithoutIntensity * newScaleFactor;
    }
    public static void IncreaseIntensity(ref Color hdrColorRequire, float intensityIncrease)
    {
        Color originalColorWithoutIntensity = GetOriginalColor(hdrColorRequire);
        float currentIntensity = GetIntensity(hdrColorRequire);

        // Add color intensity increase
        float modifiedIntensity = currentIntensity + intensityIncrease;

        // Set color intensity
        float newScaleFactor = Mathf.Pow(2, modifiedIntensity);
        hdrColorRequire = originalColorWithoutIntensity * newScaleFactor;
    }
}