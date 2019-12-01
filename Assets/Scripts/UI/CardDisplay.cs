using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

	public CardParent card;

	[Header("Template for switching")]
	public Sprite conditionCardTemplate;
	public Sprite normalCardTemplate;

	[Header("Enable to show card data")]
	public bool EnableName = true;
	public bool EnableArtwork = true;
	public bool EnableStamina = true;
	public bool EnableDescription = true;

	[Header("Prefab UI Target")]
	public Text nameText;
	public Text descriptionText;
	public Text staminaText;
	public Image artworkImage;
	public Image template;
	public Transform effect;

    //Added To make Card blink
    public Image FrontBlink;
    public bool IsColored = false;
    private float SetTimer = 3.0f;
    public float Timer = 0.0f;
    private float TimerSpeed = 0.5f;
    private int SetBlinkAmount = 2;
    public int BlinkAmount = 0;

    //Greyout Image
    public Image Greyout;

	// Use this for initialization
	void Start () {
		if (card.IsConditionCard == false) {
			CardData tempCard = ((CardData)card);

			template.sprite = normalCardTemplate;

			if (EnableName) {
				nameText.text = tempCard.name;
			}

			if (EnableArtwork) {
				artworkImage.sprite = tempCard.artwork;
			}

			if (EnableStamina) {
				staminaText.text = tempCard.staminaCost.ToString();
			}

			if (EnableDescription) {
				descriptionText.text = tempCard.description;
			}

			foreach (Transform child in effect)
			{
				child.gameObject.SetActive (false);
			}

			if (tempCard.actionEffect.Count > 0)
			{
				for (int i = 0; i < tempCard.actionEffect.Count; i++)
				{
					int type = (int)tempCard.actionEffect [i].actionType;
					effect.GetChild (type).gameObject.SetActive (true);
				}
			}

			if (tempCard.statusEffect.Count > 0)
			{
				for (int i = 0; i < tempCard.statusEffect.Count; i++)
				{
					int type = (int)tempCard.statusEffect [i].statusType;
					effect.GetChild (type+9).gameObject.SetActive (true);
				}
			}
		}

		if (card.IsConditionCard == true) {
			ConditionCardData tempCard = ((ConditionCardData)card);

			template.sprite = conditionCardTemplate;

			if (EnableName) {
				nameText.text = tempCard.name;
			}

			if (EnableArtwork) {
				artworkImage.sprite = tempCard.artwork;
			}

			if (EnableDescription) {
				descriptionText.text = tempCard.description;
			}

			if (EnableStamina) {
				staminaText.text = "";
			}

			foreach (Transform child in effect)
			{
				child.gameObject.SetActive (false);
			}
		}
	}

    public void Restart()
    {
		if (card.IsConditionCard == false) {
			CardData tempCard = ((CardData)card);

			template.sprite = normalCardTemplate;

			if (EnableName) {
				nameText.text = tempCard.name;
			}

			if (EnableArtwork) {
				artworkImage.sprite = tempCard.artwork;
			}

			if (EnableStamina) {
				staminaText.text = tempCard.staminaCost.ToString();
			}

			if (EnableDescription) {
				descriptionText.text = tempCard.description;
			}

			foreach (Transform child in effect)
			{
				child.gameObject.SetActive (false);
			}

			if (tempCard.actionEffect.Count > 0)
			{
				for (int i = 0; i < tempCard.actionEffect.Count; i++)
				{
					int type = (int)tempCard.actionEffect [i].actionType;
					effect.GetChild (type).gameObject.SetActive (true);
				}
			}

			if (tempCard.statusEffect.Count > 0)
			{
				for (int i = 0; i < tempCard.statusEffect.Count; i++)
				{
					int type = (int)tempCard.statusEffect [i].statusType;
					effect.GetChild (type+9).gameObject.SetActive (true);
				}
			}
		}

		if (card.IsConditionCard == true) {
			ConditionCardData tempCard = ((ConditionCardData)card);

			template.sprite = conditionCardTemplate;

			if (EnableName) {
				nameText.text = tempCard.name;
			}

			if (EnableArtwork) {
				artworkImage.sprite = tempCard.artwork;
			}

			if (EnableDescription) {
				descriptionText.text = tempCard.description;
			}

			if (EnableStamina) {
				staminaText.text = "";
			}
			foreach (Transform child in effect)
			{
				child.gameObject.SetActive (false);
			}
		}
    }

    //For Card Blink
    public void Blink()
    {
        FrontBlink.color = Color.red;
        IsColored = true;
    }

    public void ReColor()
    {
        FrontBlink.color = Color.white;
    }

    public void GreyOut()
    {
        Greyout.color = new Color(0.35f, 0.35f, 0.35f, 0.75f);
    }

    public void Restore()
    {
        Greyout.color = new Color(0.35f, 0.35f, 0.35f, 0.0f);
    }

    public void Update()
    {
        if(IsColored == true)
        {
            Timer += TimerSpeed;
            if(Timer == SetTimer)
            {
                Timer = 0.0f;
                ReColor();
                if(BlinkAmount == SetBlinkAmount)
                {
                    BlinkAmount = 0;
                    IsColored = false;
                }
                else
                {
                    BlinkAmount++;
                    Blink();
                }
            }
        }
    }
}
