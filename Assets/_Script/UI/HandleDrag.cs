using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDrag : MonoBehaviour
{
    static GameObject trashCan;
    static bool isDrag = false;
    private Camera cam;

    private void Update()
    {
        if (isDrag) return;
        isDrag = true;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            }
        }
    }
}
