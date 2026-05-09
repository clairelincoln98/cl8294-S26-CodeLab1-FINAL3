using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonDef", menuName = "Scriptable Objects/ButtonDef")]
public class ButtonDef : ScriptableObject
{
    public string buttonName;
    public Vector2 position;
    private Dictionary<string, string> storyKeys = new Dictionary<string, string>();
    //this is a list of story tags that correlate to a button response

    public string storyItem;

    public string[] storyKeysArray;
    public string[] responseTextArray;
    public string[] itemGrantedArray;
 
    public void AddStoryKey(string key)
    {
        
    }
}
//TODO: CREATE AN ARRAY OF STORY KEYS AND AN ARAY OF REPLIES 
//TODO: CREATE A DICTIONARY MADE UP OF THE ABOVE ARRAYS ^
//T0DO: YOU STILL HAVE AN INVENTORY 