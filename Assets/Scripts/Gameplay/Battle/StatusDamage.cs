using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDamage : MonoBehaviour {
    public HealthCounter Health;

    //Status
    public bool isPoison = false;
    public bool isBleed = false;
    public bool isStun = false;
    public bool stunned = false;
    //Status Timers
    public int PoisonTimer;
    public int BleedTimer;
    public int StunTimer;
    public int attackCount;
    //Buff(Positive)
    /*public bool isRegen;
    public bool isCounter;
    public bool isResistant;
    public bool isWeakExploit;
    //Buff(Neutral)
    public bool isNullify;
    //Debuff
    public bool isFatigue;
    public bool isElementless;*/
    private int chance;
    public bool applied = false;
    //public SwitchtoDeck turn;
    public GameObject poisonIcon;
    public GameObject bleedIcon;
    public GameObject stunIcon;
    public bool struck;

    //Damage Numbers
    public ShowDamage StatusDamageNumbers;
	/*
    private void Start()
    {
        poisonIcon.SetActive(false);
        bleedIcon.SetActive(false);
        stunIcon.SetActive(false);
        PoisonTimer = 3;
        BleedTimer = 3;
        StunTimer = 2;
    }

    private void Update()
    {
        if(turn.endTurn == false)
        {
            struck = false;
        }

        if(PoisonTimer == 0)
        {
            isPoison = false;
            poisonIcon.SetActive(false);
            PoisonTimer = 3;
        }

        if(BleedTimer == 0)
        {
            isBleed = false;
            bleedIcon.SetActive(false);
            BleedTimer = 1;
        }

        if(StunTimer == 0)
        {
            isStun = false;
            stunIcon.SetActive(false);
            StunTimer = 2;
            stunned = false;
        }
    }

	/*
    public void ProcRNG(CardDisplay Card)
    {
        chance = Random.Range(0, 30);
		CardData tempCard = ((CardData)Card.card);
		if(tempCard.procChance.procRate == ProcRate.LOW)
        {
            if(chance <= 30)
            {
                applied = true;
                StatusApplied(Card);
            }
        }
		else if (tempCard.procChance.procRate == ProcRate.MEDIUM)
        {
            if (chance <= 6)
            {
                applied = true;
                StatusApplied(Card);
            }
        }
		else if (tempCard.procChance.procRate == ProcRate.HIGH)
        {
            if (chance <= 15)
            {
                applied = true;
                StatusApplied(Card);
            }
        }
    }

    public void StatusApplied(CardDisplay Card)
    {
		CardData tempCard = ((CardData)Card.card);
        if(applied == true)
        {
			if(tempCard.statusType.statusEffect == StatusEffect.POISON)
            {
                isPoison = true;
                poisonIcon.SetActive(true);
            }
			else if (tempCard.statusType.statusEffect == StatusEffect.BLEED)
            {
                isBleed = true;
                bleedIcon.SetActive(true);
            }
			else if (tempCard.statusType.statusEffect == StatusEffect.STUN)
            {
                isStun = true;
                stunIcon.SetActive(true);
                stunned = true;
            }

            applied = false;
        }
    }

	*/
    /*
	public void StatusEffects()
    {
        if(isPoison == true)
        {
            if(turn.endTurn == true)
            {
                if(struck == false)
                {
                    if (PoisonTimer > 0)
                    {
                        if (turn.endTurn == true)
                        {
                            Health.HealthDecrease(3);

                            //StatusDamageNumbers.AttackText(-3);

                            PoisonTimer--;
                            struck = true;
                        }
                    }
                }
            }
        }
        if(isBleed == true)
        {
            if(BleedTimer > 0)
            {
                if (turn.endTurn == true)
                {
                    Health.HealthDecrease(2 * attackCount);

                    //StatusDamageNumbers.AttackText(-(2 * attackCount));

                    BleedTimer--;
                }
            }
        }
        if(isStun == true)
        {
            if(StunTimer > 0)
            {
                StunTimer--;
            }
        }
    }
	*/
}
