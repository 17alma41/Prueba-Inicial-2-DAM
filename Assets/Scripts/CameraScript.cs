using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    Vector3 targetPos;

    //[SerializeField] float haciaDelante;
    [SerializeField] float smoothing;

    void Update()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }
}
