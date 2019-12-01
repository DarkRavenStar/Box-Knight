using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReward : MonoBehaviour {

    public DeckData AllCards;
    public DeckData PlayerInventory;

    public CardDisplay Card;

    public HealthCounter Player;
    public PlayerData PlayerData;

    public LevelingUp PlayerLevel;

    public bool rewarded;
    public bool inRoaming = false;

    private void Start()
    {
        rewarded = false;
        if (inRoaming == false)
        {
            PlayerData = BattleSceneManager.Instance.GetPlayerData();
        }
    }
    [ContextMenu("GetCard")]
    public void RewardCard()
    {
        if (rewarded == false)
        {
            int CardsInDeck = AllCards.cards.Count;
            CardData Reward = (CardData)AllCards.cards[Random.Range(0, CardsInDeck)];
            Card.card = Reward;
            Card.gameObject.SetActive(true);
            PlayerInventory.cards.Add(Reward);
            rewarded = true;
        }
    }

    public void EnemyRewardHealth(int amount)
    {
        if (rewarded == false)
        {
            PlayerLevel.AddEnemyEXP();
            Player.HealthIncrease(amount, PlayerData.maxHealth);
        }
    }

    public void BossRewardHealth(int amount)
    {
        if (rewarded == false)
        {
            PlayerLevel.AddBossEXP();
            Player.HealthIncrease(amount, PlayerData.maxHealth);
        }
    }

    public void ResetRewards()
    {
        rewarded = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        RewardCard();
    }

    public void Update()
    {
        if(inRoaming == true)
        {
            if(Card.GetComponent<CanvasGroup>().alpha > 0.0f && Card.gameObject.activeInHierarchy == true)
            {
                Card.GetComponent<CanvasGroup>().alpha -= 0.005f;
            }
            else if(Card.GetComponent<CanvasGroup>().alpha == 0.0f)
            {
                Card.gameObject.SetActive(false);
                Card.GetComponent<CanvasGroup>().alpha = 1.0f;
            }
        }
    }
}
