using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
     
    public enum stateEnum{
		
        Locked,
        Unlocked,
        FireDoused,
        NothingtoDo,
		
    }
    
    public stateEnum currentState  = stateEnum.Locked;
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
        Debug.Log("reveal text called");

                //CHECK MY STATE
                if (currentState == stateEnum.Locked)
                {
                    Debug.Log("locked");
                    gm.locationDescriptionDisplay.text = "There seems to be something in here, but I can't get past the metal screen.";
                    
                    //CHECK THE ITEM
                    if (gm.hasPliers)
                    {
                        string itemName = "PLIERS";
                        string currentText =
                            "Looks like someone tried to burn a journal of some kind. It might help me get out of here, but I need to put out this fire.";
                        Debug.Log(currentText);
                        string buttonText = useItemText;
                        gm.UpdateTextCreateUseItemButton(currentText, buttonText, itemName);
                    }

                    if (gm.hasWater)
                    {
                        gm.locationDescriptionDisplay.text = "There seems to be something in here, but I can't get past the metal screen.";
                    }
                }
            
                
                if (currentState == stateEnum.Unlocked)
                {
                    if (gm.hasWater)
                    {
                        //currentState = stateEnum.hasItemTwo;
                        //gm.itemsOwned.Remove("PLIERS");
                        string currentText = "It's not a journal, it's a photo album.";
                        string buttonText = "Take Album?";
                        string itemName = "Album";
                        gm.UpdateTextCreateTakeItemButton(currentText, buttonText, itemName);
                    }

                    else
                    {
                        gm.locationDescriptionDisplay.text = "I need to put this fire out.";
                    }
                   

                }
                // else
                // {
                //     gm.locationDescriptionDisplay.text = "I need to put this fire out";
                // }



                if (currentState == stateEnum.FireDoused)
                {

                    if (gm.hasAlbum)
                    {
                        currentState = stateEnum.NothingtoDo;
                        gm.locationDescriptionDisplay.text = "Nothing to do here";
                    }
                }
                
    }


    public override void DestroyButton()
    {
        Destroy(itemButton);
    }
    
    public override void DestroyUseItemButton()
    {
        Destroy(useItemButton);
    }

    public override void ItemUsed(GameManager gm, string useItemName)
    {
        if (useItemName == "PLIERS" && currentState == stateEnum.Locked)
        {
            currentState = stateEnum.Unlocked;
        }
        
        if (useItemName == "WATER" && currentState == stateEnum.Unlocked)
        {
            currentState = stateEnum.FireDoused;
        }
        
    }
    
    
}
