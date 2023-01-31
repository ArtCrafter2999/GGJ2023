using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHarm : MonoBehaviour, IHarm
{
    public float LostHealth { get; set; } = 10;// per second

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            print("Harm by: " + LostHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
