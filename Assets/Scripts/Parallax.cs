using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float posIni;
    public float parallax = 0.5f;
    private float tamanho;
    private GameObject cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        posIni = transform.position.x;
        tamanho = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float dist = cam.transform.position.x * parallax;

        transform.position = new Vector3(posIni + dist, transform.position.y, transform.position.z);

        if(cam.transform.position.x >= transform.position.x + tamanho)
        {
            posIni += tamanho;
        }
        else if(cam.transform.position.x <= transform.position.x - tamanho)
        {
            posIni -= tamanho;
        }
    }
}
