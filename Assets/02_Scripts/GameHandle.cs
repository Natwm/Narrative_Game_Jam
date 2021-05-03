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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z - moveAmount * Time.deltaTime, -2.5f, 2.3f);
            Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x,Camera.main.transform.position.y, value);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.z + moveAmount * Time.deltaTime, -2.5f, 2.3f);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, value);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            float value = Mathf.Clamp(Camera.main.transform.position.x + ((moveAmount/2) * Time.deltaTime), -0.6f, .7f);
            Camera.main.transform.position = new Vector3(value, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
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
