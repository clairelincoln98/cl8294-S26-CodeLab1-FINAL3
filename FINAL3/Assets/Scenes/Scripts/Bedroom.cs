using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Bedroom")]
public class Bedroom : Location
{
    //button variables
    GameObject specialButton;
    GameObject itemButton;
    //GameObject useItemButton;
    public Vector2 buttonLocation = new Vector2(-749, 3);
    public Vector2 itemButtonLocation = new Vector2(4, -173);
    
    //text variables
    public string defaultText = "Drawer";
    public string phase0Text = "There's a small key in here.";
    //public string useItemText = "Use Key?";
    //public string usedItemText= "There's a flashlight in here. Hopefully it works.";
    public string itemText = "Take Key?";
    public string phase3Text = "Nothing useful here.";
    


 

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
        //destroys the special button
        Destroy(specialButton);
        
        //destroys key button (in case the player doesn't click it)
        Destroy(itemButton);
        
        //Destroy(useItemButton);
    }

    


    public void RevealText(GameManager gm)
    {
        string newText = phase0Text;
        
        //calls check items from game manager to see what items the player has
        gm.CheckItems();
        
            //sets the description text to the next phase of fireplace response
            
            itemButton = ButtonCreator.instance.CreateButton(itemText);
            itemButton.transform.localPosition = itemButtonLocation;
            Button itemButtonComp = itemButton.GetComponent<Button>();
            //calls takeItem in game manager to add key to inventory when take key is pressed 
           itemButtonComp.onClick.AddListener(() => gm.TakeItem("KEY", itemButton));
        
        
        if (gm.hasKey)
        {
            newText = phase3Text;
        }
       
        
        gm.locationDescriptionDisplay.text = newText;

       
    }


    public override void DestroyButton()
    {
        Destroy(itemButton);
    }
    
    // public override void DestroyUseItemButton()
    // {
    //     Destroy(useItemButton);
    // }

    
    
    
}
