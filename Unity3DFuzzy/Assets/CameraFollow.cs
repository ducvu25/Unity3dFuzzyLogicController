using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform transPlayer;
    public float smoth;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, transform.position.z - transPlayer.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, (offset + transPlayer.position).z), smoth*Time.deltaTime);
    }
}
