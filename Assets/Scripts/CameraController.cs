using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float speed = 5f;
    private Vector3 _position;

    void Start()
    {
        //offset = transform.position - Player.transform.position;
        _position = Player.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var currentPosition = Player.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position, currentPosition, speed * Time.deltaTime);
        var currentRotation = Quaternion.LookRotation(Player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, speed * Time.deltaTime);
        //transform.position = Player.transform.position + offset;
    }
}
