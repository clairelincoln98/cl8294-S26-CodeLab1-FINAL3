using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/Kitchen")]
public class Kitchen : Location
{
    GameObject specialButton;
    GameObject specialButton2;
    GameObject takeItemButton;
    GameObject takeItemButton2;
   
    public string defaultText = "Cabinet";
    public string phase0Text = "There's pliers in here.";
   // public string phase1Text = "The key from the fireplace fits in here. There's are pliers inside.";
    public string phase3Text = "There's nothing useful in here";
public Vector2 buttonLocation = new Vector2(-749, 3);
public Vector2 buttonLocation2 = new Vector2(-749, -3);
public Vector2 itemButtonLocation2 = new Vector2(-749, 3);
public Vector2 itemButtonLocation = new Vector2(4, -173);
public string itemText = "Take Pliers?";
 
//a function that determines what happens when the player enters the living room

    public override void OnEnter(GameManager gm) 
    {
        //Debug.Log("NewOnEnter");
        //calls CreateButton from game manager and feeds it the location button and the button's default name text
        specialButton = ButtonCreator.instance.CreateButton(defaultText);
        specialButton.transform.localPosition = buttonLocation;
        Button specialButtonComp = specialButton.GetComponent<Button>();
        //has the button comp listen for a function to call when clicked
        specialButtonComp.onClick.AddListener(() => RevealText(gm));
        
        specialButton2 = ButtonCreator.instance.CreateButton("Fridge");
        specialButton2.transform.localPosition = buttonLocation2;
        Button specialButtonComp2 = specialButton2.GetComponent<Button>();
        //has the button comp listen for a function to call when clicked
        specialButtonComp2.onClick.AddListener(() => RevealText2(gm));
        
    }
    public override void OnExit(GameManager gm)
    {
        
        //destroys the special button
        Destroy(specialButton);
        Destroy(specialButton2);
        
        //destroys key button (in case the player doesn't click it)
        Destroy(takeItemButton);
        Destroy(takeItemButton2);
        
    }
    
    


    public void RevealText(GameManager gm)
    {
        string newText = phase0Text;
        gm.CheckItems();
        //calls check items from game manager to see what items the player has
        
        //Debug.Log("RevealText");
        

        if ((gm.hasPliers == false))
        {
            //sets the description text to the next phase of fireplace response
            newText = phase0Text;
            gm.locationDescriptionDisplay.text = newText;
            //NOW THAT THE CABINET IS UNLOCKED:
            
            //checks that the pliers haven't already been picked up
            
                //calls CreateButton from game manager to make the "take PLIERS" button
                takeItemButton = ButtonCreator.instance.CreateButton(itemText);
                
                takeItemButton.transform.localPosition = itemButtonLocation;
                
                Button itemButtonComp = takeItemButton.GetComponent<Button>();
                //calls takeItem in game manager to add key to inventory when take key is pressed 
                itemButtonComp.onClick.AddListener(() => gm.TakeItem("PLIERS"));
                
                //the item button has now been pressed
                
            
        }
        
        
       
    }
    public void RevealText2(GameManager gm)
    {
        string newText = "There's a bucket in here";
            
        takeItemButton2 = ButtonCreator.instance.CreateButton("Take bucket?");
                
        takeItemButton2.transform.localPosition = itemButtonLocation2;
                
        Button itemButtonComp2 = takeItemButton2.GetComponent<Button>();
        //calls takeItem in game manager to add key to inventory when take key is pressed 
        itemButtonComp2.onClick.AddListener(() => gm.TakeItem("BUCKET"));
            
                
            
    }

    
    public override void DestroyButton()
    {
        Destroy(takeItemButton);
        Destroy(takeItemButton2);
        
    }
    // public override void DestroyUseItemButton()
    // {
    //     Destroy(takeItemButton2);
    // }
    //
}