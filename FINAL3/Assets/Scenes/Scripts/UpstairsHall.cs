using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/UpstairsHall")]
public class UpstairsHall : Location
{
    
 //button variables
    GameObject specialButton;
    GameObject inspectButton;
    GameObject inspectButton2;
    public Vector2 buttonLocation = new Vector2(-749, 3);
    public Vector2 itemButtonLocation = new Vector2(4, -173);
    
    //text variables
    public string defaultText = "Painting";
    public string phase0Text = "Another strange portrait. The paint looks and smells like it's decaying.";
    public string inspectText = "Inspect painting?";
    public string firstInspectionText= "This is Lila.";
    public string secondInspectText = "Look closer?";
    //public string phase3Text = "Nothing useful here.";
    public string secondInspectionText = "There's a map here.";
    public string takeItemText = "Take map?";
    //public string gainedItemName = "MAP";

    public bool specialLocation;
 

    
    public enum stateEnum{
		
        doesntKnowLila,
        lilaNameKnown,
        lilaFaceKnown,
        lilaKnown,
        NothingHere,
		
    }
    
    public stateEnum currentState  = stateEnum.doesntKnowLila;
//a function that determines what happens when the player enters the living room

    public override void OnEnter(GameManager gm) 
    {
        
        if (!gm.roomsLoaded.Contains(this.name))
        {
            //IF THE ROOM HAS NOT BEEN LOCKED YET, SET ME BACK TO ME ORIGINAL STATE 
            currentState  = stateEnum.doesntKnowLila;
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
        Destroy(inspectButton);
        Destroy(inspectButton2);
    }

    


    public void RevealText(GameManager gm)
    {
        gm.CheckItems();
       
        string newText = phase0Text;
        
        
        //calls check items from game manager to see what items the player has
        
        
        //USE THE LETTER ONCE TO NOTICE THE PAINTING
        if (currentState == stateEnum.doesntKnowLila)
        {
            gm.locationDescriptionDisplay.text = "Another strange portrait.";
            if (gm.hasLetter)
            {
                gm.locationDescriptionDisplay.text = "I wonder who this woman is.";
                currentState = stateEnum.lilaNameKnown;
            }

            if (gm.hasAlbum)
            {
                gm.locationDescriptionDisplay.text = "This must be Lila, I saw her in the photo album.";
                currentState = stateEnum.lilaFaceKnown;
            }
        }
        
        if (currentState == stateEnum.lilaNameKnown)
        {
            if (gm.hasLetter && !gm.hasAlbum)
            {
                gm.locationDescriptionDisplay.text = "I wonder who this woman is.";
            }

            if (gm.hasAlbum)
            {
                gm.locationDescriptionDisplay.text = "This must be Lila, I saw her in the photo album.";
                gm.UpdateTextCreateUseItemButton("There must be something special about this painting.", "Inspect painting?", "LETTER");
            }
            
        }
        
        if (currentState == stateEnum.lilaFaceKnown)
        {
            if (gm.hasAlbum && !gm.hasLetter)
            {
                gm.locationDescriptionDisplay.text = "This must be Lila, I saw her in the photo album.";
            }
            
        }

        if (currentState == stateEnum.lilaKnown)
        {
            gm.UpdateTextCreateUseItemButton("There's a map here, tucked behind the canvas.", "Take a closer look?", "ALBUM");
        }

        if (currentState == stateEnum.NothingHere)
        {
            gm.locationDescriptionDisplay.text =
                "There's a white spot on the wall where the painting used to hang. These walls are dirtier than I thought";
        }


    }


    public override void ItemUsed(GameManager gm, string useItemName)
    {
        //overrides location to mark that an item has been used and to change the state associated with said item
        if (useItemName == "LETTER" && currentState == stateEnum.lilaNameKnown)
        {
            currentState = stateEnum.lilaKnown;
        }
        if (useItemName == "ALBUM" && currentState == stateEnum.lilaKnown)
        {
            currentState = stateEnum.NothingHere;
        }
        
    }
    public override void DestroyButton()
    {
        Destroy(inspectButton);
    }
    
    public override void DestroyUseItemButton()
    {
        Destroy(inspectButton2);
    }

    
    
}
