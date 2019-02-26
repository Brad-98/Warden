using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLookWithMouse : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;

    Vector3 headPosition;

    public float mouseSensitivity = 1.6f;
    public float cameraSmoothing = 2.0f;

    public GameObject player;
   
	// Update is called once per frame
	void Update ()
    {
        if (mouseLook.y <= -90)
        {
            mouseLook.y = -90;
        }
        else if(mouseLook.y >= 90)
        {
            mouseLook.y = 90;
        }

        var mousePosition = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mousePosition = Vector2.Scale(mousePosition, new Vector2(mouseSensitivity * cameraSmoothing, mouseSensitivity * cameraSmoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, mousePosition.x, 1f / cameraSmoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mousePosition.y, 1f / cameraSmoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(-mouseLook.y, -90 , 90), Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
