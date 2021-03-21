/*****************************************************************************
// File Name :         MouseLook.cs
// Author :            Kyle Grenier
// Creation Date :     once upon a time
//
// Brief Description : Enables the player to look around in first person with the mouse.
*****************************************************************************/
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float sensitivity { get { return PlayerSettings.instance.Sensitivity; } }
    [SerializeField] private float headUpperAngleLimit = 85f;
    [SerializeField] private float headLowerAngleLimit = -80f;

    private float yaw = 0f;
    private float pitch = 0f;

    private Quaternion bodyStartRotation;
    private Quaternion headStartRotation;

    //Taken from the child of this object.
    private Transform head;

    private void Start()
    {
        head = GetComponentInChildren<Camera>().transform;
        bodyStartRotation = transform.localRotation;
        headStartRotation = head.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Mouse X") * Time.deltaTime;
        float v = Input.GetAxis("Mouse Y") * Time.deltaTime;
        h = Mathf.Clamp(h, -1, 1) * sensitivity;
        v = Mathf.Clamp(v, -1, 1) * sensitivity;

        yaw += h;
        pitch -= v;

        //Clamp pitch so we can't look directly up or down.
        pitch = Mathf.Clamp(pitch, headLowerAngleLimit, headUpperAngleLimit);

        //Create rotations based on angle and axis.
        Quaternion bodyRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion headRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        transform.localRotation = bodyRotation * bodyStartRotation;
        head.localRotation = headRotation * headStartRotation;
    }
}