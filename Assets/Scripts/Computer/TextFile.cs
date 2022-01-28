using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextFile
{
    public string filename;

    [TextArea(5, 20)]
    public string content;
}
