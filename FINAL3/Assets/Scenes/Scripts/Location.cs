using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Location", menuName = "Scriptable Objects/Location")]

public class Location : ScriptableObject
{
    
    
    //scriptable object is something that can store data in fields
    public string name;
    
    public bool hasDoors;
    public string description; //description of room
    public Location northLocation; //locations
    public Location westLocation;
    public Location eastLocation;
    public Location southLocation;

    public GameObject room;
    public Vector4 cameraColor;

    
    public enum stateEnum{
		
        Locked,
        hasItemOne,
        ItemOneUsed,
        hasItemTwo,
        ItemTwoUsed,
        NothingtoDo,
		
    }
    
    public stateEnum currentState = stateEnum.Locked;
    // public ButtonDef[] buttonList;

    public virtual void OnEnter(GameManager gm)
    {
        //Debug.Log("BASE LOCATION: " + GetType().Name);
    }
    public virtual void OnExit(GameManager gm)
    {
        //Debug.Log("BASE LOCATION: " + GetType().Name);
    }

    
    
    //TODO: A LIST OF SPECIAL INTERACTIONS FOR THIS PARTICULAR LOCATION THAT CORRELATES TO A BUTTON
    
    public Location instance;
    public void UpdateLocationDisplay(GameManager gm)
    {
        gm.locationNameDisplay.text = name;
        gm.locationDescriptionDisplay.text = description;
        
            gm.NorthButton.SetActive(northLocation != null); //calling the game manager to set the button active
            gm.EastButton.SetActive(eastLocation != null);
            gm.SouthButton.SetActive(southLocation != null);
            gm.WestButton.SetActive(westLocation != null);
        

    }
    
    public void ChangeCameraColor()
    
    {
        if (Camera.main != null) //checks if there is a camera
        {
            Camera.main.backgroundColor = cameraColor; //changes the background color of camera (background color of room)
        }
    
    }
    
    public virtual void DestroyButton()
    {
        
    }
    
    public virtual void DestroyUseItemButton()
    {
        
    }

    public virtual void specialUseItem(GameManager gm)
    {
        
    }
    
    public virtual void secondTakeItem(GameManager gm)
    {
        
    }

    
    
    //take item function
    //do item function
    //in location, create enum of the special type
   
    public void Special(string currentText, string buttonText, string itemName)
    {
        Debug.Log("special called)");
        if(currentState == stateEnum.hasItemOne)
       {
            GameManager.instance.UpdateTextCreateUseItemButton(currentText, buttonText);
            //GameManager.instance.UpdateButtonText()
            //GameManager.instance.UpdateButton(); //updates the button text and display text AND changes the enum state of the living room
            //IN GAME MANAGER, WHEN YOU DO UPDATE TEXT 
        }
       
        
        if(currentState == stateEnum.ItemOneUsed)
        {
            //Debug.Log("hasItemTwo");
            GameManager.instance.UpdateTextCreateTakeItemButton(currentText, buttonText, itemName); 
        }
        
        if(currentState == stateEnum.hasItemTwo)
        {
            Debug.Log("hasItemTwo");
            GameManager.instance.UpdateTextCreateTakeItemButton(currentText, buttonText, itemName);
        }
        
    }
    
    //IN LIVING ROOM HAS TEXT, INVENTORY ITEMS (ADD ITEMS THROUGH LOCATION.ADDITEMS)
}

