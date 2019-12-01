using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDmgCalculation : MonoBehaviour {
    
    private ElementData PlayerType;
    private ElementData EnemyType;

    public ShowDamage PlayerElementDiff;
    public ShowDamage EnemyElementDiff;
    
    public PlayerData Player;
    public EnemyData Enemy;
    
	void Start()
	{
		Player = BattleSceneManager.Instance.GetPlayerData ();
		Enemy = BattleSceneManager.Instance.enemyData;
	}

	public void PlayerCharge(ElementData element)
    {
        PlayerType = element;
        Player.elementType = PlayerType;
    }

	public ElementType Element()
	{
		return Player.elementType.elements;
	}

    public void EnemyCharge(ElementData Element)
    {
        EnemyType = Element;
        Enemy.elementType = EnemyType;
    }

    public void Discharge()
    {
        PlayerType = null;
    }

	public int PlayerAttackCalculate(int DamageData, PlayerData Caster, EnemyData Target)
    {
        float TempDamage = DamageData;
        int Damage = 0;
        PlayerType = Caster.elementType;
        EnemyType = Target.elementType;

        if (EnemyType.elements == ElementType.NONE)
        {
            //Debug.Log("No Element");
            TempDamage = DamageData;
            Damage = (int)TempDamage;
            return Damage;
        }
        else
        {
            if (PlayerType.elements == EnemyType.elementWeakness[0].elements)
            {
                //Debug.Log("Not Effective Element");
                EnemyElementDiff.Weakness();
                TempDamage = DamageData * EnemyType.elementWeakness[0].elementBuff;
            }
            else if (PlayerType.elements == EnemyType.elementStrength[0].elements)
            {
                //Debug.Log("Effective Element");
                EnemyElementDiff.Resistance();
                TempDamage = DamageData * EnemyType.elementStrength[0].elementBuff;
            }
            Damage = (int)TempDamage;
            return Damage;
        }
    }

    public int EnemyAttackCalculate(int DamageData, EnemyData Caster, PlayerData Target)
    {
        float TempDamage = DamageData;
        int Damage = 0;
        EnemyType = Caster.elementType;
        PlayerType = Target.elementType;

        if (EnemyType.elements == ElementType.NONE)
        {
            //Debug.Log("No Element");
            TempDamage = DamageData;
            Damage = (int)TempDamage;
            return Damage;
        }
        else
        {
            if (EnemyType.elements == PlayerType.elementWeakness[0].elements)
            {
                //Debug.Log("Not Effective Element");
                EnemyElementDiff.Weakness();
                TempDamage = DamageData * PlayerType.elementWeakness[0].elementBuff;
            }
            else if (EnemyType.elements == PlayerType.elementStrength[0].elements)
            {
                //Debug.Log("Effective Element");
                EnemyElementDiff.Resistance();
                TempDamage = DamageData * PlayerType.elementStrength[0].elementBuff;
            }
            Damage = (int)TempDamage;
            return Damage;
        }
    }
}
