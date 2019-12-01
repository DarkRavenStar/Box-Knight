using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourCounter : MonoBehaviour
{

    public int Armour;
    public Text armourText;
    public Image armourImage;
    int ArmourData;

    public bool isPlayer;
    public bool isBlock;

    public EnemyData EnemyData;

    //UI Feedback
    public UISystem UIs;

    public void Start()
    {
        isBlock = false;
        if(isPlayer == false)
        {
            Armour = EnemyData.enemyArmour;
        }
        armourText.text = Armour.ToString();
        UpdateArmour();
        CheckArmour();
    }

    public void ArmourIncrease(int ArmourValue)
    {
        if (ArmourValue > 0)
        {
            Armour += ArmourValue;
            UpdateArmour();
            CheckArmour();
        }
    }

    public void ArmourDecrease(int ArmourValue)
    {
        if (ArmourValue > 0)
        {
            Armour -= ArmourValue;
            isBlock = true;
            if (Armour < 0)
            {
                Armour = 0;
            }
            UpdateArmour();
            UIs.AttackAppear();
            CheckArmour();
        }
    }

    public void UpdateArmour()
    {
        armourText.text = Armour.ToString();
    }

    public void CheckArmour()
    {
        if(Armour > 0)
        {
            armourImage.gameObject.SetActive(true);
            UIs.ArmourAppear();
        }
        else if (Armour == 0)
        {
            armourImage.gameObject.SetActive(false);
        }
    }

}
