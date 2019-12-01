using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private float targetAlpha;
    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<Image>();

        this.targetAlpha = this.image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
    }

    IEnumerator StartFading()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            this.targetAlpha = 0f;
            yield return new WaitForSeconds(0.05f);
        }
        for (float f = 1f; f <= 1f; f += 0.05f)
        {
            this.targetAlpha = 1.0f;
            yield return new WaitForSeconds(1f);
        }
    }

    public void Fade()
    {
        StartCoroutine("StartFading");
    }
}
