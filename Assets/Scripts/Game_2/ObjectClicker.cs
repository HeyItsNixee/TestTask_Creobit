using System;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Action<bool> mouseClickState;
    private bool lastMouseState = false;

    private void Update()
    {
        if (lastMouseState != Input.GetMouseButton(0))
        {
            mouseClickState?.Invoke(Input.GetMouseButton(0));
            lastMouseState = Input.GetMouseButton(0);
        }
    }


}
