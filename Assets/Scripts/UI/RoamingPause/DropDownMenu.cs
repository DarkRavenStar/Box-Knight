using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour {

    //public Button MenuButton;
    public GameObject menu;
    public GameObject option;
    public GameObject deckBuilding;
    int menuCounter;
    int optionCounter;
	
	// Update is called once per frame
	public void DropMenu()
    {
        menuCounter++;
        if(menuCounter == 1)
        {
            menu.gameObject.SetActive(true);
        }
        else if(menuCounter >= 2)
        {
            menu.gameObject.SetActive(false);
            menuCounter = 0;
        }
    }

    public void OptionMenu()
    {
        optionCounter++;
        if(optionCounter == 1)
            option.gameObject.SetActive(true);
        else if(optionCounter >= 2)
        {
            option.gameObject.SetActive(false);
            optionCounter = 0;
        }
    }

    public void DeckToMenu()
    {
        deckBuilding.gameObject.SetActive(false);
    }
    public void MenuToDeck()
    {
        deckBuilding.gameObject.SetActive(true);
    }
}
