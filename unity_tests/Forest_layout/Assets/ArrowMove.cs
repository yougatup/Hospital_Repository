using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour {
    public float Walkspeed = 1.0f;
    public float Rotatespeed = 15.0f;

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, Rotatespeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -Rotatespeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0,0, -Walkspeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0, Walkspeed * Time.deltaTime));
        }
    }

}
