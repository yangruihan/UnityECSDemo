using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SelfRotationBehaviour : MonoBehaviour
{
    public int MaxRotationSpeed;

    private int _rotationSpeed;

    void Start ()
    {
        _rotationSpeed = Random.Range(0, MaxRotationSpeed);
    }
    
    void Update () 
    {
        transform.localRotation = math.mul(
                math.normalize(transform.localRotation), 
                math.axisAngle(math.up(), _rotationSpeed * Time.deltaTime)
        );
    }
}
