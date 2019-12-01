using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoamAnim : MonoBehaviour {
    public Animator anim;
    private Rigidbody playerVelo;
    public Joystick angle;
    public GameObject joystick;
    // Use this for initialization
    void Start () {
        playerVelo = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0f, angle.angles * Mathf.Rad2Deg, 0f);
        if (angle.direction.y > 1f || angle.direction.y < 1f && angle.direction.y != 0)
        {
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
        }
        else if(angle.direction.y == 0)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
        }
	}
}
