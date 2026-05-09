using UnityEngine;

public class ClickableObject : MonoBehaviour, IClickable


{
    //this lets me put the direction int on each door in scene
    public int direction;
    
    void Start()
    {
        //Debug.Log(GameManager.instance);
    }
    public void OnClick()
    {
        //grabs the singelton game manager and calls move direction
        //feeds the game manager the direction
        GameManager.instance.MoveDirection(direction);
        //Debug.Log(gameObject.name + " was clicked!");
        
    }
}