﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugFonc : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
    }
}