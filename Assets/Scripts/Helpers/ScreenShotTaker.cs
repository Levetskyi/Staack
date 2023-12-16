using UnityEngine;

public class ScreenShotTaker : MonoBehaviour
{
    [SerializeField] string folderPath;
    [SerializeField] private int scaleMultyplier = 1;

    [ContextMenu("Take ScreeShot")]
    private void TakeScreenshot()
    {
        folderPath += "ScreenShot-";
        folderPath += System.Guid.NewGuid().ToString() + ".png";

        ScreenCapture.CaptureScreenshot(folderPath, scaleMultyplier);
    }
}
