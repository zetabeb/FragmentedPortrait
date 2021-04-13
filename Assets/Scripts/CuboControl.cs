using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboControl : MonoBehaviour
{
    private Transform cuboTransform;
    // Start is called before the first frame update
    void Start()
    {
        cuboTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cuboTransform.Translate(new Vector3(0, 0, 5) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cuboTransform.Translate(new Vector3(0, 0, -5) * Time.deltaTime);
        }
    }
}
