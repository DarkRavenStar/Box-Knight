using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUp : MonoBehaviour
{
    private Image image;
    private Color oldColor;

    private void Start()
    {
        image = GetComponent<Image>();
        oldColor = image.color;
    }

    IEnumerator Switchback()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            image.color = Color.red;
            yield return new WaitForSeconds(0.05f);
        }
        for (float f = 1f; f <= 1f; f += 0.05f)
        {
            image.color = oldColor;
            yield return new WaitForSeconds(1f);
        }
    }

    public void Switch()
    {
        StartCoroutine("Switchback");
    }
}
