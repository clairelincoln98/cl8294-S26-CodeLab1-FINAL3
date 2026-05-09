using TMPro;
using UnityEngine;

public class ButtonCreator : MonoBehaviour
{
    public static ButtonCreator instance;
    public GameObject buttonPrefab;

    public GameObject buttonParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    //a function that instantiates a button at a given location 
    //function also assigns text to the textMeshPro component 
    public GameObject CreateButton(string buttonText) 
    {
        //Debug.Log("Create Button Called");
        GameObject button = Instantiate<GameObject>(buttonPrefab, buttonParent.transform);
        TMP_Text buttonTextMesh = button.GetComponentInChildren<TMP_Text>();
        buttonTextMesh.text = buttonText;
        return button;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


