using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public bool UseTargetFramerate = false;
    public int FPS = 60;

    private void Start()
    {
        if (UseTargetFramerate)
            Application.targetFrameRate = FPS;
        else
            Application.targetFrameRate = -1;
    }
}
