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
        Destroy(inspectButton);
        Destroy(inspectButton2);
    }

    


    public void RevealText(GameManager gm)
    {
        gm.CheckItems();
       
        string newText = phase0Text;
        
        
        //calls check items from game manager to see what items the player has
        
        
        //USE THE LETTER ONCE TO NOTICE THE PAINTING
        if (gm.hasLetter && gm.hasAlbum)
        {
            newText = firstInspectionText;
            gm.itemsOwned.Remove("LETTER");
            //button that prompts player to take down the painting/take a closer look
            inspectButton2 = ButtonCreator.instance.CreateButton(inspectText);
            Button keyButtonComp = inspectButton2.GetComponent<Button>();
            inspectButton2.transform.localPosition = itemButtonLocation;
            
            //adds letter 2 to inventory to trigger next inspection 
            keyButtonComp.onClick.AddListener(() => gm.UseItem(secondInspectText));
            
        }
        
        //USE THE LETTER TWICE TO TAKE THE PAINTING DOWN
        if (gm.hasLetter2)
        {
            //take a second look and get map
            specialUseItem(gm);
            
        }
        

        if (gm.hasMap)
        {
            newText = "The painting left behind a white spot. These walls are dirtier than I thought.";
        }
        gm.locationDescriptionDisplay.text = newText;


    }


  
    public override void specialUseItem(GameManager gm)
    {
            Debug.Log("double painting triggered");
            inspectButton = ButtonCreator.instance.CreateButton(takeItemText);
            inspectButton.transform.localPosition = itemButtonLocation;
            Button itemButtonComp = inspectButton.GetComponent<Button>();
            //calls takeItem in game manager to add key to inventory when take key is pressed 
            itemButtonComp.onClick.AddListener(() => gm.secondTakeItemReveal(secondInspectionText, "MAP"));
            
    }
    // public override void secondTakeItem(GameManager gm)
    // {
    //     Debug.Log("double painting triggered");
    //     inspectButton = ButtonCreator.instance.CreateButton(takeItemText);
    //     inspectButton.transform.localPosition = itemButtonLocation;
    //     Button itemButtonComp = inspectButton.GetComponent<Button>();
    //     //calls takeItem in game manager to add key to inventory when take key is pressed 
    //     itemButtonComp.onClick.AddListener(() => gm.TakeItem("MAP"));
    //         
    // }
    
    public override void DestroyButton()
    {
        Destroy(inspectButton);
    }
    
    public override void DestroyUseItemButton()
    {
        Destroy(inspectButton2);
    }

    
    
}
