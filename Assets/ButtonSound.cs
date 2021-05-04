using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string clickSound;
    FMOD.Studio.EventInstance clickEvent;

    // Start is called before the first frame update
    void Start()
    {
        clickEvent = FMODUnity.RuntimeManager.CreateInstance(clickSound);
    }

    public void PlayButton()
    {
        clickEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
