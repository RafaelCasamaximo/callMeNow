using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class JSONLoader
{
    public JSONLoader(string path){
        this.jsonFile = Resources.Load<TextAsset>(path);
    }
    TextAsset jsonFile;

    public Character[] Load(){
        Character[] charactersInJson = JsonUtility.FromJson<Character[]>(jsonFile.text);

        return charactersInJson;

    }
}
