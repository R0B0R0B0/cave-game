using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctioScript : MonoBehaviour
{
    public LineRenderer lineRenderer;

    float x = 0;

    //Amplitude
    [Range(0.01f,5)]
    public float a = 1;
    //Wavelenght
    [Range(0.01f, 5)]
    public float b = 2;


    //X,y
    [Range(-5, 5)]
    public float k = 0;
    [Range(-5, 5)]
    public float h = 0;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        for (int x = 0; x < 10; x++)
        {
           Debug.Log(a * Mathf.Sin((x - h) / b) + k);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        for (int x = -10; x < 10; x++)
        {           
            float y1 = a * Mathf.Sin((x - h) / b) + k;
            Vector3 y1Vector = new Vector3(x, y1, 0);
            lineRenderer.SetPosition(i, y1Vector);
            i++;
        }
    }
}
