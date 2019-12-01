using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCall : MonoBehaviour {

    public Image Fire;
    public Image Earth;
    public Image Water;

    private CanvasGroup Holder;

    public float AlphaUpdateAmount;

    public bool ElementSet;

    public bool Used;

	// Use this for initialization
	void Start () {
        Holder = this.GetComponent<CanvasGroup>();
        Holder.alpha = 0.0f;
        ElementSet = false;
        Used = false;
        Fire.gameObject.SetActive(false);
        Earth.gameObject.SetActive(false);
        Water.gameObject.SetActive(false);
	}
	[ContextMenu("CallFire")]
    public void CallFire()
    {
        Fire.gameObject.SetActive(true);
        Earth.gameObject.SetActive(false);
        Water.gameObject.SetActive(false);
        ElementSet = true;

        Used = true;
    }
    [ContextMenu("CallEarth")]
    public void CallEarth()
    {
        Fire.gameObject.SetActive(false);
        Earth.gameObject.SetActive(true);
        Water.gameObject.SetActive(false);
        ElementSet = true;

        Used = true;
    }
    [ContextMenu("CallWater")]
    public void CallWater()
    {
        Fire.gameObject.SetActive(false);
        Earth.gameObject.SetActive(false);
        Water.gameObject.SetActive(true);
        ElementSet = true;

        Used = true;
    }
    [ContextMenu("CallOff")]
    public void CallOff()
    {
        ElementSet = false;

        Used = true;
    }

    public void Update()
    {
        if(ElementSet == true)
        {
            if (Holder.alpha != 1.0f)
            {
                Holder.alpha += AlphaUpdateAmount;
            }
            else if(Used == true && Holder.alpha == 1.0f)
            {
                Used = false;
            }
        }
        else if(ElementSet == false)
        {
            if (Holder.alpha != 0.0f)
            {
                Holder.alpha -= AlphaUpdateAmount;
            }
            else if (Used == true && Holder.alpha == 0.0f)
            {
                Fire.gameObject.SetActive(false);
                Earth.gameObject.SetActive(false);
                Water.gameObject.SetActive(false);
                Used = false;
            }
        }
    }
}
