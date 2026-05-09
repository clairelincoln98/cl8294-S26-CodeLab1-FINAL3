using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Bathroom")]
public class Bathroom : Location
{
    GameObject specialButton;
    GameObject takeItemButton;
   
    public string defaultText = "Bath";
    public string phase0Text = "I can't stand the smell.";
    public string phase1Text = "I could use the water here, but I need something.";
    public string phase2Text = "This should work.";
    //public string takeItemText = "Use Bucket?";
    public string phase3Text = "There's nothing useful in here";
public Vector2 buttonLocation;
public Vector2 itemButtonLocation = new Vector2(-445, -10);
public string takeItemText = "Use Bucket?";
 
//a function that determines what happens when the player enters the living room

    public override void OnEnter(GameManager gm) 
    {
        Debug.Log("BathroomEnter");
        //calls CreateButton from game manager and feeds it the location button and the button's default name text
        specialButton = ButtonCreator.instance.CreateButton(defaultText);
        specialButton.transform.localPosition = buttonLocation;
        Button specialButtonComp = specialButton.GetComponent<Button>();
        //has the button comp listen for a function to call when clicked
        specialButtonComp.onClick.AddListener(() => RevealText(gm));
        
    }
    public override void OnExit(GameManager gm)
    {
        
        //destroys the special button
        Destroy(specialButton);
        
        //destroys key button (in case the player doesn't click it)
        Destroy(takeItemButton);
        
    }
    
    


    public void RevealText(GameManager gm)
    {
        string newText = phase0Text;
        gm.CheckItems();
        //calls check items from game manager to see what items the player has
        
        //Debug.Log("RevealText");

        if (gm.hasPliers)
        {
            newText = phase1Text;
        }

        if (gm.hasPliers && gm.hasBucket)
        {
            //sets the description text to the next phase of fireplace response
            newText = phase2Text;
            gm.locationDescriptionDisplay.text = newText;
            //NOW THAT THE CABINET IS UNLOCKED:
            
            //checks that the pliers haven't already been picked up
            
                //calls CreateButton from game manager to make the "take PLIERS" button
                takeItemButton = ButtonCreator.instance.CreateButton(takeItemText);
                
                takeItemButton.transform.localPosition = itemButtonLocation;
                
                
                Button itemButtonComp = takeItemButton.GetComponent<Button>();
                //calls takeItem in game manager to add key to inventory when take key is pressed 
                itemButtonComp.onClick.AddListener(() => gm.TakeItem("WATER"));
                
                //the item button has now been pressed
                
            
        }
        
        if (gm.hasWater)
        {
            //sets the description text to the next phase of fireplace response
            newText = phase3Text;
        }
        
        
        gm.locationDescriptionDisplay.text = newText;

       
    }
    
    public override void DestroyButton()
    {
        Destroy(takeItemButton);
    }
    
}