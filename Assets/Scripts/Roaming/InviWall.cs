using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviWall : MonoBehaviour
{
    GameObject Camera;
    Renderer rend;
    Material material;
    Color color;

    private void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        rend = GetComponent<Renderer>();
        material = rend.material;
        color = rend.material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            rend.enabled = false;
            Debug.Log("i'm In");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            rend.enabled = true;
            Debug.Log("I'm Out");
        }
    }
}
