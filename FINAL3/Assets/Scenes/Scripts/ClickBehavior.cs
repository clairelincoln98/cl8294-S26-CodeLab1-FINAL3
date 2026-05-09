using UnityEngine;

public class ClickBehavior : MonoBehaviour
{
    public float maxDistance = 500f;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // calls the input function that recognizing the mouse being clicked
        {
            //ray casting (shooting a line through camera into world)
            //basically turns the mouse position into a 3D ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
           

            //variable that tests whether the raycast (the mouse 3d ray) hits something
            RaycastHit hit;
            
            //if the ray hits
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                //gets the object that the collider belongs to
                GameObject clickedObject = hit.collider.gameObject;
                //Debug.Log("HIT: " + hit.collider.name);
                
                //get the clickable interface on the object that was clicked
                IClickable clickable = clickedObject.GetComponent<IClickable>();
                
                //checks that the object clicked has a clickable interface
                if (clickable != null)
                {
                    //call on click function for that particular object
                    clickable.OnClick();
                }
            }
        }
        else
        {
            //Debug.Log("NO HIT");
        }
    }
}
