using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMouseLock : MonoBehaviour
{
    public bool mouseLocked = true;

    private void Start()
    {
        ToggleMouse(!mouseLocked);
    }

    private void Update()
    {
        if (Input.GetButtonDown("space"))
        {
            ToggleMouse(mouseLocked);
        }
    }

    private void ToggleMouse(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseLocked = true;
        }
    }
}
