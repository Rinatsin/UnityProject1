using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, Range(1f, 360f)]
    public float rotationSpeed = 100f;

    [SerializeField, Range(-89f, 89f)]
    public float minVerticalAngle = -60f, maxVerticalAngle = 60f;

    private Vector2 angles = new Vector2(-10f, 0f);

    private void Awake()
    {
        transform.localRotation = Quaternion.Euler(angles);   
    }

    void LateUpdate()
    {
        ManualRotation();
        Quaternion lookRotation;
        if (ManualRotation())
        {
            ConstraintAngles();
            lookRotation = Quaternion.Euler(angles);
        } else
        {
            lookRotation = transform.localRotation;
        }
        transform.rotation = lookRotation;
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Vertical Camera"),
            Input.GetAxis("Mouse X")
            );
        const float e = 0.001f;

        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            angles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        return false;
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    void ConstraintAngles()
    {
        angles.x = Mathf.Clamp(angles.x, minVerticalAngle, maxVerticalAngle);
        if (angles.y < 0f)
        {
            angles.y += 360f;
        }
        else if (angles.y > 360f)
        {
            angles.y -= 360f;
        }
    }
}
