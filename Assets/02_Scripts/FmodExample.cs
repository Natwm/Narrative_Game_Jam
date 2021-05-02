using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
public class FmodExample : MonoBehaviour
{

    // Référence à l'event dans l'inspector.
    [FMODUnity.EventRef]
    public string EventName;

    // Instance de l'évent en question;
    FMOD.Studio.EventInstance myEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
