using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Office")]
public class Office : Location
{
    //button variables
    GameObject specialButton;
    GameObject useItemButton;
    GameObject useItemButton2;
    public Vector2 buttonLocation;
    public Vector2 itemButtonLocation;
    
    //text variables
    public string defaultText = "Desk";
    public string phase0Text = "It's locked.";
    public string useItemText = "Use Key?";
    public string usedItemText= "There's a letter in here.";
    public string secondUseItemText = "Read letter?";
    public string phase3Text = "Nothing useful here.";
    public string secondItemDescritpion = "BLAH BLAH BLAH";
    public string secondItemName = "LETTER";

    
    public enum stateEnum{
		
        Locked,
        Unlocked,
        HasLetter,
		
    }
    
    public stateEnum currentState  = stateEnum.Locked;

 

//a function that determines what happens when the player enters the living room

    public override void OnEnter(GameManager gm) 
    {
        if (!gm.roomsLoaded.Contains(this.name))
        {
            //IF THE ROOM HAS NOT BEEN LOCKED YET, SET ME BACK TO ME ORIGINAL STATE 
            currentState  = stateEnum.Locked;
            //ADD ME TO THE LIST OF ROOMS LOADED
            gm.roomsLoaded.Add(this.name);
        }
        
        //Debug.Log("NewOnEnter");
        //calls CreateButton from game manager and feeds it the location button and the button's default name text
        specialButton = ButtonCreator.instance.CreateButton(defaultText);
        specialButton.transform.localPosition = buttonLocation;
        Button firePlaceButtonComp = specialButton.GetComponent<Button>();
        //has the button comp listen for a function to call when clicked
        firePlaceButtonComp.onClick.AddListener(() => RevealText(gm));
        
    }
    public override void OnExit(GameManager gm)
    {
        //destroys the special button
        Destroy(specialButton);
        
        //destroys key button (in case the player doesn't click it)
        Destroy(useItemButton);
        Destroy(useItemButton2);
    }

    


    public void RevealText(GameManager gm)
    {
        gm.CheckItems();
        
        //calls check items from game manager to see what items the player has

        if (currentState == stateEnum.Locked)
        {
            gm.locationDescriptionDisplay.text = "It's locked.";
            if (gm.hasKey)
            {
                gm.UpdateTextCreateUseItemButton("There's a letter in here.", "Use Key?", "KEY");
            
            }
            
        }

        if (currentState == stateEnum.Unlocked)
        {
            
            gm.UpdateTextCreateUseItemButton("Dear Lila - I know you hold secrets behind that smile.", "Read Letter?", "LETTER"); 
        }

        

        if (currentState == stateEnum.HasLetter)
        {
            gm.locationDescriptionDisplay.text = "This letter might be important.";
            gm.UpdateTextCreateUseItemButton("Letter contents", "Read Letter?", "LETTER");
        }


    }


    
    public override void ItemUsed(GameManager gm, string useItemName)
    {
        //overrides location to mark that an item has been used and to change the state associated with said item
        if (useItemName == "KEY" && currentState == stateEnum.Locked)
        {
            currentState = stateEnum.Unlocked;
        }
        
        if (useItemName == "KEY" && currentState == stateEnum.Unlocked)
        {
            currentState = stateEnum.HasLetter;
            gm.UpdateTextCreateUseItemButton("Letter contents", "Read Letter?", "LETTER");
        }
        
    }

    // public override void specialUseItem(GameManager gm)
    // {
    //     //Debug.Log(gm.itemUsed);
    //     useItemButton = ButtonCreator.instance.CreateButton(secondUseItemText);
    //     useItemButton.transform.localPosition = itemButtonLocation;
    //     Button itemButtonComp = useItemButton.GetComponent<Button>();
    //     //calls takeItem in game manager to add key to inventory when take key is pressed 
    //     itemButtonComp.onClick.AddListener(() => 
    // }
    public override void DestroyButton()
    {
        Destroy(useItemButton);
    }
    
    public override void DestroyUseItemButton()
    {
        Destroy(useItemButton2);
    }
    
}
