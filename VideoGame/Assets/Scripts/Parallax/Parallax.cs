using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material mat;
    private float distance;

    [Range(0f,0.5f)]
    public float parallaxSpeed;

    void Start()
    {
        mat = GetComponent<Renderer>().material;    
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * parallaxSpeed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);

    }
}
