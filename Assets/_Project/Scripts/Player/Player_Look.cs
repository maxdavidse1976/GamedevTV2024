using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateTowardsMouse();
        MoveCursor();
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;

            Vector3 direction = (target - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    void MoveCursor()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (Player.Instance.crosshair.canvas.renderMode == RenderMode.ScreenSpaceCamera ||
            Player.Instance.crosshair.canvas.renderMode == RenderMode.WorldSpace)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Player.Instance.crosshair.canvas.transform as RectTransform,
                mousePosition,
                Player.Instance.crosshair.canvas.worldCamera,
                out Vector2 localPoint
            );
            Player.Instance.crosshair.rectTransform.localPosition = localPoint;
        }
        else
        {
            // For Screen Space - Overlay
            Player.Instance.crosshair.transform.position = mousePosition;
        }
    }
}
