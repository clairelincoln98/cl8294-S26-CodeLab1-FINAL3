using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI locationNameDisplay;
    public TextMeshProUGUI locationDescriptionDisplay;
    public Location startingLocation;
    

    public Location currentLocation;

    public GameObject NorthButton;
    public GameObject EastButton;
    public GameObject SouthButton;
    public GameObject WestButton;

    public GameObject fireplace;
    public GameObject desk;
    public GameObject bath;
    public GameObject cabinent;
    public GameObject crack;
    public GameObject frontdoor;
    
    
    public string itemUsedText;
    public static GameManager instance;
    public bool hasPliers;
    public bool hasWater;
    public bool hasFlashlight;
    public bool hasKey;
    public bool hasBucket;
    public bool hasAlbum;
    public bool hasLetter;
    public bool hasLetter2;
    public bool hasPainting;
    public bool hasMap;
    public bool itemUsed;
    public bool isDoubleLocation;
    
    public GameObject livingRoom;
    public GameObject hallway;
    public GameObject bathroom;
    public GameObject bedroom;
    public GameObject kitchen;
    public GameObject foyer;
    public GameObject office;
    public GameObject upstairsHall;
    // A dictionary to represent what items they have.
    public List<string> itemsOwned = new List<string>();
    
    //private Dictionary<string, string> storyTagsCollected = new Dictionary<string, string>();
   
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemsOwned.Clear();
        if (instance == null)
        {
            instance  = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
        //changes in code you make to SO persist after play mode
        //Debug.Log("Current location:" + startingLocation);
        
       locationNameDisplay.text = startingLocation.name;
       locationDescriptionDisplay.text = startingLocation.description;
        
        startingLocation.UpdateLocationDisplay(this);
        currentLocation = startingLocation;
        

    }


    //key for which int matches with which direction (parameters for button to act upon)
    //North = 0
    //E = 1
    //W = 2
    //S = 3

    //what happens when you hit a direction button
    public void MoveDirection(int direction)
    {
        //destroys the special button specific to previous location
        currentLocation.OnExit(this);
        
        //deactives the visual room specific to previous location
        DeactivateRoom();
        
        
        if (direction == 0) //north
        {
            //sets the south location of this location's north location to the current position
            currentLocation.northLocation.southLocation = currentLocation; 
            currentLocation = currentLocation.northLocation;
            
        }

        if (direction == 1) //east
        {
            currentLocation.eastLocation.westLocation = currentLocation;
            currentLocation = currentLocation.eastLocation;
        }

        if (direction == 2) //west
        {
            currentLocation.westLocation.eastLocation = currentLocation;
            currentLocation = currentLocation.westLocation;
        }
        if (direction == 3) //south
        {
            currentLocation.southLocation.northLocation = currentLocation;
            currentLocation = currentLocation.southLocation;
        }
        
        //updates the display with room specific text
        currentLocation.UpdateLocationDisplay(this);
        //Debug.Log("Current location:" + currentLocation);
        
        // currentLocation.ChangeCameraColor(); //calls on location to change the camera color based on the current location's values
       
        //activates visuals for specific room
        ActivateRoom();
        
        //calls function to activate room specific actions (buttons)
        currentLocation.OnEnter(this);
        
        
    }
    
    //____________________BACKGROUND COLOR______________________________
    public void ChangeBackgroundColor() //calls a function in Location script that updates the background color
    {
        currentLocation.ChangeCameraColor();
    }
    public void ActivateRoom() //calls a function in Location script that updates the background color
    {
        if (currentLocation.name == "Living Room")
        {
            livingRoom.SetActive(true);
        }
        if (currentLocation.name == "Hall")
        {
            hallway.SetActive(true);
        }
        if (currentLocation.name == "Bathroom")
        {
            bathroom.SetActive(true);
        }
        if (currentLocation.name == "Bedroom")
        {
            bedroom.SetActive(true);
        }
        if (currentLocation.name == "Kitchen")
        {
            kitchen.SetActive(true);
        }
        if (currentLocation.name == "Foyer")
        {
            foyer.SetActive(true);
        }

        if (currentLocation.name == "Office")
        {
            office.SetActive(true);
            isDoubleLocation = true;
        }
        
        if (currentLocation.name == "Upstairs Hall")
        {
            upstairsHall.SetActive(true);
            isDoubleLocation = true;
        }
    }
    public void DeactivateRoom() //calls a function in Location script that updates the background color
    {
        hallway.SetActive(false);
        livingRoom.SetActive(false);
        bedroom.SetActive(false);
        bathroom.SetActive(false);
        kitchen.SetActive(false);
        foyer.SetActive(false);
        office.SetActive(false);
        upstairsHall.SetActive(false);
    }
    
   

   
   
    
    //______________________ITEM LOGIC________________________________________
    
    
   //calls grabitem to add item to dictionary
    public void TakeItem(string itemName) 
        //***this as a separate function probably wasn't necessary, but maybe it would help if the itemText changes
    {
        //calls grab item and sets the parameter itemText to You Have:
        GrabItem(itemName); 
        currentLocation.DestroyButton();
        
        
    } 
    
    //adds the item and its text to the dictionary
    public void GrabItem(string itemName) //adds
    {
        
        itemsOwned.Add(itemName); 
        //checks the item
        //Debug.Log(itemName); 
        //changes the description display to "You Have: <item>"
        if (itemName == "LETTER")
        {
            locationDescriptionDisplay.text = "BLAH BLAH BLAH";
        }
        else
        {
            locationDescriptionDisplay.text = "You have:" + itemName;
            // Debug.Log("You have" + itemName);
        }
      
        
        
    }
    
    
    public void CheckItems() //checks through the player inventory and sets certain conditions to true
    {
        //Debug.Log("checking items");
        //runs through itemsOwned to check each item in dictionary
      if(itemsOwned.Contains("PLIERS"))
      {
          hasPliers = true;
      }
      
      //runs through itemsOwned to check each item in dictionary
      if(itemsOwned.Contains("WATER"))
      {
          hasWater = true;
      }

      if (itemsOwned.Contains("ALBUM"))
      {
          
          hasAlbum = true;
          Debug.Log(hasAlbum);
      }
      
      if(itemsOwned.Contains("FLASHLIGHT"))
      {
          hasFlashlight = true;
      }
      
      if(itemsOwned.Contains("BUCKET"))
      {
          hasBucket = true;
      }
      
      if(itemsOwned.Contains("KEY"))
      {
          hasKey = true;
      }
      
      if(itemsOwned.Contains("LETTER"))
      {
          hasLetter = true;
      }
      
      if(itemsOwned.Contains("PAINTING"))
      {
          hasPainting = true;
      }
      
      if(itemsOwned.Contains("MAP"))
      {
          hasMap = true;
      }
    
      if(itemsOwned.Contains("LETTER2"))
      {
          hasLetter2 = true;
      }
      
      if(itemsOwned.Contains("BUCKET"))
      {
          hasBucket = true;
      }



    }

    public void UseItem(string usedItemText)
    {
        locationDescriptionDisplay.text = usedItemText; 
        currentLocation.DestroyUseItemButton();
        
        if (isDoubleLocation)
        {
            Debug.Log(isDoubleLocation);
            currentLocation.specialUseItem(this);
            currentLocation.DestroyUseItemButton();
        }
        
    }

    public void secondTakeItemReveal(string secondItemDescritpion, string neededItemName)
    {
        // if (neededItemName == "MAP")
        // {
        //     locationDescriptionDisplay.text = secondItemDescritpion;
        //     currentLocation.secondTakeItem(this);
        // }
        locationDescriptionDisplay.text = secondItemDescritpion;
        currentLocation.DestroyButton();
        TakeItem(neededItemName);
        
        
    }

    ///ODO: Create an ON Enter function in Location to get overridden
    //TO: Create extensions from Location that are specific 
    //TO: In SpecificLocation, overide the on etner and on exit
    //TOD0: Button Action function that handles the button behavior 


    // public void SpecialClick()
    // {
    //
    //     currentLocation.Special();
    // }

    // public void UpdateText();
    // {
    //
    // }

}
