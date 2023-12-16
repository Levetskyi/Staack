using UnityEngine;

public class HueColorGenerator : IColorGenerator
{
    private float hueIncrement = 0.01f; // Increment value for hue variation
    private float saturation = 0.75f; // Saturation value (max value)
    private float brightness = 0.9f; // Brightness value (max value)

    private float currentHue = 0.0f; // Initial hue value

    public Color GenerateNewColor()
    {
        Color newColor = Color.HSVToRGB(currentHue, saturation, brightness);

        // Increment the hue for the next block
        currentHue = (currentHue + hueIncrement) % 1.0f;

        return newColor;
    }
}
