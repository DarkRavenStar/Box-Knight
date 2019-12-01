using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private float xAngle;
    private float yAngle;

    private float xAngleTemp;
    private float yAngleTemp;

    private float Distance;
    //private float ScreenDiagonal;

    // Use this for initialization
    void Start () {
        xAngle = 0.0f;
        xAngleTemp = 0.0f;
        yAngle = 0.0f;
        yAngleTemp = 0.0f;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        //ScreenDiagonal = Mathf.Atan2(Screen.height, Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                endPos = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (endPos.x - startPos.x) * 180.0f / Screen.width;
                yAngle = yAngleTemp - (endPos.y - startPos.y) * 180.0f / Screen.height;
                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }
        /*Zoom
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
                endPos = Input.GetTouch(1).position;
            }
            if (Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Distance = Mathf.Atan2(endPos.y, startPos.x) / ScreenDiagonal;
                this.transform.position.z += Distance
            }
        }
        */
        /*! For Multitouch
        Touch touch = Input.GetTouch(0);

        Touch[] TouchInput = Input.touches;

        for(int i=0; i<Input.touchCount; i++)
        {
            if(i > 0)
            {
                if(Input.GetAxis("Horizontal") < -0.1)
                {
                    
                    CameraPos.Rotate(new Vector3(-1, 0, 0));
                }
                else if(Input.GetAxis("Horizontal") > 0.1)
                {
                    CameraPos.Rotate(new Vector3(1, 0, 0));
                }

                if (Input.GetAxis("Vertical") < -0.1)
                {
                    CameraPos.Rotate(new Vector3(0, -1, 0));
                }
                else if (Input.GetAxis("Vertical") > 0.1)
                {
                    CameraPos.Rotate(new Vector3(0, 1, 0));
                }
            }
        }
        */

	}
}
