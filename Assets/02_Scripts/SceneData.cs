using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData 
{
    
}

[System.Serializable]
public class SceneItem
{
    public string name;
    bool isOptionnal;
    public int iterationNumber;

    public SceneItem(string _name,string optionnal,string _it)
    {
        name = _name;
        if (optionnal == "yes")
        {
            int.TryParse(_it, out iterationNumber);
            isOptionnal = true;
        }
        else
        {
            iterationNumber = 1;
        }
    }
}
