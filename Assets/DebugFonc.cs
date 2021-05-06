using System.Collections;
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
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            GameManager.instance.DadMusique();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            GameManager.instance.EthanMusique();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            GameManager.instance.HinaMusique();
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            GameManager.instance.MotherMusique();
        }
    }
}
