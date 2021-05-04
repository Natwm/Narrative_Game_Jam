using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    public float moveAmount;
    public float edge;

    private Vector3 cameraPosition;
    public Vector2 heightMinMax;
    public Vector2 widthMinmax;
    GameManager instance;

    //Movement
    KeyCode UpInput;
    KeyCode DownInput;
    KeyCode LeftInput;
    KeyCode RightInput;



    // Start is called before the first frame update
    void Start()
    {
        instance = FindObjectOfType<GameManager>();
        if (instance.currentLanguage == GameManager.Language.Francais)
        {
            UpInput = KeyCode.Z;
            DownInput = KeyCode.S;
            LeftInput = KeyCode.Q;
            RightInput = KeyCode.D;
        }
        else
        {
            UpInput = KeyCode.W;
            DownInput = KeyCode.S;
            LeftInput = KeyCode.A;
            RightInput = KeyCode.D;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(UpInput))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z - moveAmount * Time.deltaTime, -2.5f, 2.3f);
            Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x,Camera.main.transform.position.y, value);
        }
        if (Input.GetKey(DownInput))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z + moveAmount * Time.deltaTime, -2.5f, 2.3f);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, value);
        }
        if (Input.GetKey(LeftInput))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.x + ((moveAmount/2) * Time.deltaTime), -0.6f, .7f);
            Camera.main.transform.position = new Vector3(value, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Input.GetKey(RightInput))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.x - (moveAmount /2)* Time.deltaTime, -0.6f, .7f);
            Camera.main.transform.position = new Vector3(value, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

        
        if(Input.mousePosition.x> Screen.width - edge)
        {
            float value = Mathf.Clamp(Camera.main.transform.position.x - (moveAmount / 2) * Time.deltaTime, heightMinMax.x, heightMinMax.y);
            Camera.main.transform.position = new Vector3(value, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Input.mousePosition.x < edge + 10)
        {
            float value = Mathf.Clamp(Camera.main.transform.position.x + (moveAmount / 2) * Time.deltaTime, heightMinMax.x, heightMinMax.y);
            Camera.main.transform.position = new Vector3(value, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

        if (Input.mousePosition.y > Screen.height - edge)
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z - moveAmount * Time.deltaTime, widthMinmax.x, widthMinmax.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, value);
        }
        
        if (Input.mousePosition.y < edge)
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z + moveAmount * Time.deltaTime, widthMinmax.x, widthMinmax.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, value);
        }
    }
}
