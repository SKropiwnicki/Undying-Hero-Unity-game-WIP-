using UnityEngine;
using System.Collections;

public class CameraAR : MonoBehaviour
{
    public int targetResX;
    public int targetResY;
    private float targetAspect;

    void Awake()
    {
        targetAspect = (1.0f * targetResX / targetResY);
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            camera.orthographicSize = camera.orthographicSize / scaleHeight;
        }
    }
}