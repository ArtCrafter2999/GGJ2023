using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMovement : MonoBehaviour, IMovement
{
    public float MoveSpeed { get; set; } = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position += Input.GetAxis("Horizontal") * Vector3.right * MoveSpeed * Time.deltaTime;
    }
}
