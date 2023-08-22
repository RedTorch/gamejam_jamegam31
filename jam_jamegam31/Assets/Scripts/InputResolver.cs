using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputResolver : MonoBehaviour
{
    [SerializeField] private SpongePropulsion mySp;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform mouseCursor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPos = Input.mousePosition;
        mPos.z -= -10f; // compensates for camera offset
        mPos.y -= 2f;
        Vector3 wPoint = Camera.main.ScreenToWorldPoint(mPos);
        mouseCursor.position = wPoint;
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z += 10f; // compensates for camera offset
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 delta = worldPoint - player.position;
            print("jump called:" + delta);
            mySp.Jump(delta);
        }
    }
}
