using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mouse_Movement : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public GameObject objectToMove;
    public float objectOffset;
    public float scaleInc;
    bool held;
    bool hasbeenglued;
    Vector3 heldScale;
    public Texture2D[] hands;
   public int layerindex=1;

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

        if (Physics.Raycast(tryGrab,out hit) && !held)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Grab"))
            {
                
                if (hit.collider.gameObject.GetComponent<StickerBehaviours>().isSpawned == false)
                {
                    held = true;
                    hit.collider.attachedRigidbody.isKinematic = true;
                    objectToMove = hit.collider.gameObject;
                    objectToMove.GetComponent<SpriteRenderer>().sortingOrder = 10000;
                    objectToMove.GetComponent<BoxCollider>().isTrigger = true;
                    if (objectToMove != null && objectToMove.GetComponent<StickerBehaviours>() != null)
                    {
                        objectToMove.GetComponent<StickerBehaviours>().IsMoving = true;
                        objectToMove.GetComponent<StickerBehaviours>().move = this;
                        objectToMove.GetComponent<StickerBehaviours>().hasbeengrabbed = true;
                        
                    }
                }

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
            Cursor.SetCursor(hands[1],Vector2.zero ,CursorMode.Auto);

        }
        if (Input.GetMouseButtonUp(0))
        {
            held = false;
            Cursor.SetCursor(hands[0], Vector2.zero, CursorMode.Auto);
            if (objectToMove != null && objectToMove.GetComponent<StickerBehaviours>() != null)
            {
                
                objectToMove.GetComponent<Rigidbody>().isKinematic = false;
                objectToMove.GetComponent<BoxCollider>().isTrigger = false;
                objectToMove.GetComponent<StickerBehaviours>().IsMoving = false;
                objectToMove.transform.DOScale(1f, 0.05f);
            }
            objectToMove = null;
        }

        if (held)
        {
            Vector3 objectPos = new Vector3(ScreenToWorld(Input.mousePosition).x, objectOffset, ScreenToWorld(Input.mousePosition).z);
            objectToMove.transform.position = objectPos;
        }
    }
}
