using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Movement : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public GameObject objectToMove;
    public float objectOffset;
    bool held;
    Vector3 ScreenToWorld(Vector2 screenPos)
    {
        // Create a ray going into the scene starting 
        // from the screen position provided 
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        
        // ray hit an object, return intersection point
        if (Physics.Raycast(ray, out hit,~ignoreLayer))
            return hit.point;

        // ray didn't hit any solid object, so return the 
        // intersection point between the ray and 
        // the Y=0 plane (horizontal plane)
        float t = -ray.origin.y / ray.direction.y;
        return ray.GetPoint(t);
    }

    public void GetObject()
    {
        Ray tryGrab = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(tryGrab,out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Grab"))
            {
                Debug.Log("Hit Object");
                held = true;
                hit.collider.attachedRigidbody.isKinematic = true;
                objectToMove = hit.collider.gameObject;
                if(objectToMove != null && objectToMove.GetComponent<StickerBehaviours>() != null)
                    objectToMove.GetComponent<StickerBehaviours>().IsMoving = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetObject();
        /**/
        }
        if (Input.GetMouseButtonUp(0))
        {
            held = false;
            objectToMove.GetComponent<Rigidbody>().isKinematic = false;
            if (objectToMove != null && objectToMove.GetComponent<StickerBehaviours>() != null)
                objectToMove.GetComponent<StickerBehaviours>().IsMoving = false;
        }

        if (held)
        {
            Vector3 objectPos = new Vector3(ScreenToWorld(Input.mousePosition).x, objectOffset, ScreenToWorld(Input.mousePosition).z);
            objectToMove.transform.position = objectPos;
        }
    }
}
