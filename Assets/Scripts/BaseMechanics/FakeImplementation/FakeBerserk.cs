using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBerserk : MonoBehaviour, IBerserk
{
    public bool IsEnabled { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            print("Berserk enabled: " + IsEnabled);
        }
    }
}
