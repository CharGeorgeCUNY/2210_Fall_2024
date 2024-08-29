using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover2 : MonoBehaviour
{
    public int x = 0;
    public string test = "testHere";
    public float testFloat = 0.1f;
    public float speedUnitsPerSecond = 1f;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Local = Vector3.zero;
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Local = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Local = Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Local = Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Local = Vector3.right;
        }
        Local = Local * Time.deltaTime;
        transform.Translate(Local);


    }
}
