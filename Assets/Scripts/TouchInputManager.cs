using UnityEngine;

public class TouchInputManager : MovingInStakePanel
{
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            InStakePanel();
        }
    }
}
