using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public GameObject manager;
    // Use this for initialization
    void Start()
    {
        Vector3 savedPositon = new Vector3(PlayerPrefs.GetFloat("playerx"), PlayerPrefs.GetFloat("playery"), PlayerPrefs.GetFloat("playerz"));
        manager.transform.position = savedPositon;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("playerx", manager.transform.position.x);
        PlayerPrefs.SetFloat("playery", manager.transform.position.y);
        PlayerPrefs.SetFloat("playerz", manager.transform.position.z);
    }
}
