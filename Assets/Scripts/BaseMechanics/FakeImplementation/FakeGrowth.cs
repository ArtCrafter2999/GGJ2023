using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGrowth : MonoBehaviour, IGrowth
{
    public float GrowthTime { get; set; } = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("Growht in time: " + GrowthTime);
        }
    }
}
