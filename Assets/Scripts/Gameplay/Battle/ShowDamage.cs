using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour {

    public Text DamageNumber;
    public Text AttackNumber;
    public Text DefendNumber;
    public Text HealNumber;
    public Text ElemenResist;
    public Text ElemenWeak;

    public Text NoStaminaText;

    public CanvasGroup Parent;
    public float DSpeed;

    public RectTransform ParentPoint;
    public Vector3 OrgPos;

    public bool ToLeft;
    public bool ToRight;

    public float speed;

    public float TimerSpeed;
    public float Timer = 0;
    public float DespawnTimer;

    //Turn Counter
    public bool EnemyAttack;

	// Use this for initialization
	void Start () {
        ParentPoint = this.GetComponent<RectTransform>();
        OrgPos = ParentPoint.position;
        Timer = 0;
	}

    public void Calculate(int Damage, int Health, int Defence)
    {
        //Damage Indicator
        int ArmrDmg;
        int HPDmg;
        //Get Enemy Health And Armour
        int EARMR = Defence;
        int EHP = Health;
        //Damage To Armour
        if (EARMR != 0)
        {
            ArmrDmg = EARMR - Damage;
            if (ArmrDmg <= 0)
            {
                DefendText(-EARMR);
            }
            else
            {
                DefendText(-Damage);
            }
        }
        //Damage To Health
        HPDmg = EARMR - Damage;
        if (HPDmg < 0)
        {
            if (-HPDmg > EHP)
            {
                AttackText(-EHP);
            }
            else
            {
                AttackText(HPDmg);
            }
        }
    }

    public void CalculateHeal(int Healing, int Health)
    {
        if (Healing > 0)
        {
            int HealDMG;
            if ((Healing + Health) > 20)
            {
                HealDMG = (20 - Health);
                HealText(HealDMG);
            }
            else
            {
                HealText(Healing);
            }
        }
    }

    public void Weakness()
    {
        ElemenWeak.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void Resistance()
    {
        ElemenResist.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }
    //Move All Damage Here
    public void DamageText(int Damage)
    {
        if (Damage != 0)
        {
            DamageNumber.text = Damage.ToString();
            DamageNumber.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
    }

    public void AttackText(int Damage)
    {
        if (Damage != 0)
        {
            AttackNumber.text = Damage.ToString();
            AttackNumber.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
    }
    public void DefendText(int Damage)
    {
        if (Damage != 0)
        {
            if(Damage < 0)
            {
                DefendNumber.text = Damage.ToString();
            }
            else
            {
                DefendNumber.text = "+" + Damage.ToString();
            }
            DefendNumber.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
    }
    public void HealText(int Damage)
    {
        if (Damage != 0)
        {
            HealNumber.text = "+" + Damage.ToString();
            HealNumber.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
    }

    public void NoStamina()
    {
        if(EnemyAttack == false)
        {
        NoStaminaText.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        }
    }

    public void OnEnable()
    {
        //Debug.Log("Its ON");
        int LeftOrRight = Random.Range(1, 10);
        if(LeftOrRight % 2 == 0)
        {
            ToRight = false;
            ToLeft = true;
        }
        else
        {
            ToRight = true;
            ToLeft = false;
        }

        if(NoStaminaText.IsActive() == true)
        {
            ToLeft = false;
            ToRight = false;
        }
    }

    public void Move()
    {
        if(ToLeft == true)
        {
            this.transform.position += transform.up * speed;
            this.transform.position += -transform.right * speed;
        }
        if(ToRight == true)
        {
            this.transform.position += transform.up * speed;
            this.transform.position += transform.right * speed;
        }
        else
        {
            this.transform.position += transform.up * speed;
        }
        Parent.alpha -= DSpeed;
    }
 
    public void ResetNum()
    {
        DamageNumber.text = "0";
        AttackNumber.text = "0";
        DefendNumber.text = "0";
        HealNumber.text = "0";
        DamageNumber.gameObject.SetActive(false);
        AttackNumber.gameObject.SetActive(false);
        DefendNumber.gameObject.SetActive(false);
        HealNumber.gameObject.SetActive(false);
        ElemenResist.gameObject.SetActive(false);
        ElemenWeak.gameObject.SetActive(false);
        NoStaminaText.gameObject.SetActive(false);
        Parent.alpha = 1;
    }

    public void Update()
    {
        if(this.gameObject.activeSelf == true)
        {
			if(BattleSceneManager.Instance.IsPlayerTurn == false) BattleSceneManager.Instance.CanEnemyPlay = false;
            Move();
            Timer += TimerSpeed;
            if (Timer >= DespawnTimer)
            {
                Timer = 0.0f;
                this.gameObject.SetActive(false);
                this.GetComponent<RectTransform>();
                this.transform.position = OrgPos;
                ResetNum();
				if(BattleSceneManager.Instance.IsPlayerTurn == false) BattleSceneManager.Instance.CanEnemyPlay = true;
				//BattleSceneManager.Instance.CanPlayerPlay = true;
            }
        }
    }
}
