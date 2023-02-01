using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float _lenght;
    private float _startPos;
    Camera cam => Camera.main;
    // Start is called before the first frame update
    public float ParalaxEffect;
    void Start()
    {
        _startPos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - ParalaxEffect);
        float dist = cam.transform.position.x * ParalaxEffect;

        transform.position = new Vector3(_startPos+dist, cam.transform.position.y, transform.position.z);

        if (temp > _startPos + _lenght) _startPos += _lenght;
        else if (temp < _startPos - _lenght) _startPos -= _lenght;
    }
}
