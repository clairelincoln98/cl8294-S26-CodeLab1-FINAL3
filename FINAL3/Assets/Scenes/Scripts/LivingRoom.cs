using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Living Room")]
public class LivingRoom : Location
{
    //button variables
    GameObject specialButton;
    GameObject itemButton;
    GameObject useItemButton;
    public Vector2 buttonLocation = new Vector2(-749, 3);
     
    
    //text variables
    public string defaultText = "Fireplace";
    public string phase0Text = "There seems to be something in here, but you can't get past the metal screen.";
    public string useItemText = "Use Pliers";
    public string usedItemText= "Looks like someone tried to burn a journal of some kind. It might help me get out of here, but I need to put out this fire.";
    public string phase2Text = "It's not a journal, it's a photo album.";
    public string useitemText2 = "Take album?";
    public string phase3Text = "Nothing left here but soggy wood.";
    public string gainedItem = "ALBUM";
    
    

//a function that determines what happens when the player enters the living room

    public override void OnEnter(GameManager gm) 
    {
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
        //Debug.Log("OnExitLivingRoom");
        //destroys the special button
        Destroy(specialButton);
        
        //destroys key button (in case the player doesn't click it)
        Destroy(itemButton);
        Destroy(useItemButton);
    }

    

    public void RevealText(GameManager gm)
    {
        string newText = phase0Text;
        
        //calls check items from game manager to see what items the player has
        gm.CheckItems();
        
        
        //checks if the player has pliers
        if (gm.hasPliers)
        {
            currentState = stateEnum.hasItemOne;
            string itemName = "";
            string currentText = "Looks like someone tried to burn a journal of some kind. It might help me get out of here, but I need to put out this fire.";
            Debug.Log(currentText);
            string buttonText = useItemText;
            
            gm.SpecialClick(currentText, buttonText, itemName);
        }
        
        if (gm.hasWater)
        {   
            currentState = stateEnum.hasItemTwo;
            //gm.itemsOwned.Remove("PLIERS");
            string currentText = "It's not a journal, it's a photo album.";
            string buttonText = "Take Album?";
            string itemName = "Album";
            gm.SpecialClick(currentText, buttonText, itemName);
            if (gm.hasLetter)
            {
                newText = "It's not a journal, it's a photo album. And this looks like Lila from the letter.";
            }
            else
            {
                newText = "It's not a journal, it's a photo album.";
            }
            gm.locationDescriptionDisplay.text = newText;
            
        }
        
        else
        {
            newText = phase3Text;
        }
       
        
        gm.locationDescriptionDisplay.text = newText;

       
    }


    public override void DestroyButton()
    {
        Destroy(itemButton);
    }
    
    public override void DestroyUseItemButton()
    {
        Destroy(useItemButton);
    }

    
    
    
}
