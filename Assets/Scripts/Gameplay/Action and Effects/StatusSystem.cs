using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSystem : MonoBehaviour {
	
	public bool isPlayer;

	[Header("Negative Status Object")]
	public GameObject poisonIcon;
	public Text poisonCount;

	public GameObject bleedIcon;
	public Text bleedCount;

	public GameObject stunIcon;
	public Text stunCount;

	[Header("De-buff Object")]
	public GameObject fatigueIcon;
	public Text fatigueCount;

	public GameObject elementlessIcon;
	public Text elementlessCount;


	[Header("Buff Object")]
	public GameObject healthRegenIcon;
	public Text healthRegenCount;

	public GameObject counterIcon;
	public Text counterCount;

	public GameObject resistanceIcon;
	public Text resistanceCount;

	[Header("Booleans for status")]
	//Negative Status bool
	public bool isPoison = false;
	public bool isPoisonImmune = false;

	public bool isBleed = false;
	public bool isBleedImmune = false;

	public bool isStun = false;
	public bool isStunImmune = false;

	public bool stunned = false;

	//Buff(Positive)
	public bool isRegen;
	public bool isCounter;
	public bool isResistant;

	//Debuff
	public bool isFatigue;
	public bool isFatigueImmune;
	public bool isElementless;
	public bool isElementlessImmune;


	//Negative Status Timers
	[SerializeField] int PoisonTimer;
	[SerializeField] int PoisonImmuneTimer;

	[SerializeField] int BleedTimer;
	[SerializeField] int BleedImmuneTimer;

	[SerializeField] int StunTimer;
	[SerializeField] int StunImmuneTimer;

	[SerializeField] int FatigueTimer;
	[SerializeField] int FatigueImmuneTimer;

	[SerializeField] int ElementlessTimer;
	[SerializeField] int ElementlessImmuneTimer;

	//Buff Timers
	[SerializeField] int RegenTimer;
	[SerializeField] int CounterTimer;
	[SerializeField] int ResistanceTimer;

	[SerializeField] int numOfTurns;

	[SerializeField] int Guard;

    //Attached Stuff
    public HealthCounter Health;
    public PlayerData Player;
    public EnemyData Enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Guard = BattleSceneManager.Instance.GetPlayerData ().armor;
		if (isPlayer == true)
		{
			if (BattleSceneManager.Instance.IsPlayerTurn == true)
			{
				if (BattleSceneManager.Instance.CanPlayerPlay == true)
				{
					//CounterEffectCheck ();
				}
			}
		}

		if(isPlayer == false)
		{
			if (BattleSceneManager.Instance.IsPlayerTurn == false)
			{
				if (BattleSceneManager.Instance.CanEnemyPlay == true)
				{
					//CounterEffectCheck ();
				}
			}
		}
	}

	public void StatusApplied(StatusCardEffect statusEffect)
	{
		if (isResistant == false)
		{
			if (statusEffect.statusType == StatusType.BLEED)
			{
				if (isBleedImmune == false)
				{
					if (isBleed == false)
					{
						BleedTimer = statusEffect.numOfTurns;
						BleedImmuneTimer = statusEffect.numOfTurns;
						bleedIcon.SetActive (true);
						bleedCount.text = BleedTimer.ToString ();
						isBleed = true;
					}
				}
			}

			if (statusEffect.statusType == StatusType.POISON)
			{
				if (isPoisonImmune == false)
				{
					if (isPoison == false)
					{
						PoisonTimer = statusEffect.numOfTurns;
						PoisonImmuneTimer = statusEffect.numOfTurns;
						poisonIcon.SetActive (true);
						poisonCount.text = PoisonTimer.ToString ();
						isPoison = true;
					}
				}
			}

			if (statusEffect.statusType == StatusType.STUN)
			{
				if (isStunImmune == false)
				{
					if (isStun == false)
					{
						StunTimer = statusEffect.numOfTurns;
						StunImmuneTimer = 3;
						stunIcon.SetActive (true);
						stunCount.text = StunTimer.ToString ();
						isStun = true;
					}
				}
			}

			if (statusEffect.statusType == StatusType.ELEMENTLESS)
			{
				if (isElementlessImmune == false)
				{
					if (isElementless == false)
					{
						ElementlessTimer = statusEffect.numOfTurns;
						ElementlessImmuneTimer = 0;
						elementlessIcon.SetActive (true);
						elementlessCount.text = ElementlessTimer.ToString ();
						isElementless = true;
					}
				}
			}

			if (statusEffect.statusType == StatusType.FATIGUE)
			{
				if (isFatigueImmune == false)
				{
					if (isFatigue == false)
					{
						FatigueTimer = statusEffect.numOfTurns;
						FatigueImmuneTimer = statusEffect.numOfTurns;
						fatigueIcon.SetActive (true);
						fatigueCount.text = FatigueTimer.ToString ();
						isFatigue = true;
					}
				}
			}
		}

		if (statusEffect.statusType == StatusType.COUNTER)
		{
			if (isCounter == false)
			{
				CounterTimer = statusEffect.numOfTurns;
				counterIcon.SetActive (true);
				counterCount.text = CounterTimer.ToString ();
				isCounter = true;
			}
		}

		if (statusEffect.statusType == StatusType.HEALTH_REGEN)
		{
			if (isRegen == false)
			{
				RegenTimer = statusEffect.numOfTurns;
				healthRegenIcon.SetActive (true);
				healthRegenCount.text = RegenTimer.ToString ();
				isRegen = true;
			}
		}

		if (statusEffect.statusType == StatusType.RESISTANCE)
		{
			if (isResistant == false)
			{
				ResistanceTimer = statusEffect.numOfTurns;
				resistanceIcon.SetActive (true);
				resistanceCount.text = ResistanceTimer.ToString ();
				isResistant = true;
			}
		}
	}

	public void CounterEffectCheck()
	{
		if (isCounter == true) {

			if (Guard <= 0) {
				elementlessIcon.GetComponent<PopUpUI> ().Reset ();
				elementlessIcon.SetActive (false);
				isCounter = false;
			}
		}
	}

	public void StatusEffects()
	{
		if (poisonIcon != null)
		{
			if (isPoison == true) {
				if (PoisonTimer > 0) {
                    Health.HealthDecrease(1);
					PoisonTimer--;
					poisonIcon.GetComponent<PopUpUI> ().Reset ();
					poisonCount.text = PoisonTimer.ToString ();
				}

				if (PoisonImmuneTimer > 0 && PoisonTimer == 0) {
					PoisonImmuneTimer--;
				}

				if (PoisonImmuneTimer == 0 && PoisonTimer == 0) {
					isPoison = false;
					isPoisonImmune = false;
					poisonIcon.GetComponent<PopUpUI> ().Reset ();
					poisonIcon.SetActive (false);
				}
			}
		}

		if (bleedIcon != null)
		{
			if (isBleed == true) {
				if (BleedTimer > 0)
                {
                    Health.HealthDecrease(1);
                    BleedTimer--;
					bleedIcon.GetComponent<PopUpUI> ().Reset ();
					bleedCount.text = BleedTimer.ToString ();
				}

				if (BleedImmuneTimer > 0) {
					BleedImmuneTimer--;
				}

				if (BleedImmuneTimer == 0 && BleedTimer == 0) {
					isBleed = false;
					isBleedImmune = false;
					bleedIcon.GetComponent<PopUpUI> ().Reset ();
					bleedIcon.SetActive (false);
				}
			}
		}

		if (stunIcon != null)
		{
			if (isStun == true) {
				if (StunTimer > 0) {
					StunTimer--;
					stunIcon.GetComponent<PopUpUI> ().Reset ();
					stunCount.text = StunTimer.ToString ();
				}

				if (StunImmuneTimer > 0 && StunTimer == 0) {
					StunImmuneTimer--;
				}

				if (StunImmuneTimer == 0 && StunTimer == 0) {
					isStun = false;
					isStunImmune = false;
					stunIcon.GetComponent<PopUpUI> ().Reset ();
					stunIcon.SetActive (false);
				}
			}
		}

		if (fatigueIcon != null)
		{
			if (isFatigue == true) {
				if (FatigueTimer > 0) {

					FatigueTimer--;
					fatigueIcon.GetComponent<PopUpUI> ().Reset ();
					fatigueCount.text = FatigueTimer.ToString ();
				}

				if (FatigueImmuneTimer > 0 && FatigueTimer == 0) {
					FatigueImmuneTimer--;
				}

				if (FatigueImmuneTimer == 0 && FatigueTimer == 0) {
					isFatigue = false;
					isFatigueImmune = false;
					fatigueIcon.GetComponent<PopUpUI> ().Reset ();
					fatigueIcon.SetActive (false);
				}
			}
		}

		if (elementlessIcon != null) {
			if (isElementless == true) {
				if (ElementlessTimer > 0) {
					ElementlessTimer--;
					elementlessIcon.GetComponent<PopUpUI> ().Reset ();
					elementlessCount.text = ElementlessTimer.ToString ();
				}

				if (ElementlessTimer == 0) {
					isElementless = false;
					elementlessIcon.GetComponent<PopUpUI> ().Reset ();
					elementlessIcon.SetActive (false);
				}
			}
		}


		if (healthRegenIcon != null) {
			if (isRegen == true) {
				if (RegenTimer > 0)
                {
                    if(isPlayer == true)
                    {
                        Health.HealthIncrease(1,Player.maxHealth);
                    }
                    else if(isPlayer == false)
                    {
                        Health.HealthIncrease(1, Enemy.maxHealth);
                    }
                    RegenTimer--;
					healthRegenIcon.GetComponent<PopUpUI> ().Reset ();
					healthRegenCount.text = RegenTimer.ToString ();
				}

				if (RegenTimer == 0) {
					isRegen = false;
					elementlessIcon.GetComponent<PopUpUI> ().Reset ();
					elementlessIcon.SetActive (false);
				}
			}
		}


		if (counterIcon != null) {
			if (isCounter == true) {

				if (Guard > 0)
				{
					if (CounterTimer > 0) {
						CounterTimer--;
						counterIcon.GetComponent<PopUpUI> ().Reset ();
						counterCount.text = CounterTimer.ToString ();
					}

					if (CounterTimer == 0) {
						isCounter = false;
						elementlessIcon.GetComponent<PopUpUI> ().Reset ();
						elementlessIcon.SetActive (false);
					}
				}

				if (Guard <= 0) {
					isCounter = false;
					elementlessIcon.GetComponent<PopUpUI> ().Reset ();
					elementlessIcon.SetActive (false);
				}
			}
		}

		if (resistanceIcon != null) {
			if (isResistant == true) {
				if (ResistanceTimer > 0) {
					ResistanceTimer--;
					resistanceIcon.GetComponent<PopUpUI> ().Reset ();
					resistanceCount.text = ResistanceTimer.ToString ();
				}

				if (ResistanceTimer == 0) {
					isResistant = false;
					resistanceIcon.GetComponent<PopUpUI> ().Reset ();
					resistanceIcon.SetActive (false);
				}
			}
		}
	}
}
