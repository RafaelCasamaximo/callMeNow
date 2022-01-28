using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class JSONLoader : MonoBehaviour
{
    public JSONLoader(string path){
        this.jsonFile = Resources.Load<TextAsset>(path);
    }
    TextAsset jsonFile;
    void Start()
    {

 
    }

    public void Load(){
        Characters charactersInJson = JsonUtility.FromJson<Characters>(jsonFile.text);

        foreach(Character character in charactersInJson.characters){
            Debug.Log(character);
        }

        
    }
}
