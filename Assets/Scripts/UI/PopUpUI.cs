using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour {

	public Vector3 TargetScale;
	public Vector3 ResetScale;
	public float FracJourney;
	public float Speed;
	public float AlphaSpeed;
	public CanvasGroup icon;
	public bool IsDone;

	// Use this for initialization
	void Start () {
		icon = this.GetComponent<CanvasGroup> ();
		IsDone = false;
		icon.alpha = 0.0f;
	}

	public void Reset()
	{
		IsDone = false;
		icon.alpha = 0.0f;
	}

	void Update()
	{
		if (IsDone == false)
		{
			if (icon.gameObject.activeInHierarchy == true)
			{
				FracJourney += Speed;
				if (FracJourney < 0.5f)
				{
					icon.transform.localScale = Vector3.Lerp(this.transform.localScale, TargetScale, FracJourney);
				}
				else if (FracJourney > 0.5f)
				{
					icon.transform.localScale = Vector3.Lerp(this.transform.localScale, ResetScale, FracJourney);
				}
				icon.alpha += AlphaSpeed;
				if (icon.transform.localScale == ResetScale && icon.alpha >= 1.0f)
				{
					icon.transform.localScale = ResetScale;
					FracJourney = 0.0f;
					IsDone = true;
				}
			}
		}
	}
}
