using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaloGlow : MonoBehaviour {

    public Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.enabled = false;
	}

    IEnumerator DeckGlow()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            image.enabled = true;
            yield return new WaitForSeconds(.05f);
        }
        for (float f = 1f; f <= 1f; f += 0.05f)
        {
            image.enabled = false;
            yield return new WaitForSeconds(1f);
        }
    }

	[ContextMenu("Glow")]
    public void Glow()
    {
        StartCoroutine("DeckGlow");
    }
}
