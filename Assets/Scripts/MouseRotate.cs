using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public float sensitivy = 9.0f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivy, 0);
    }
}
