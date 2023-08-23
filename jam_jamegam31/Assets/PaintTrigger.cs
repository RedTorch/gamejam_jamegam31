using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrigger : MonoBehaviour
{
    [SerializeField] private int paintType;
    private GameManager myGm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<SpongePropulsion>().OnPaintCollision(paintType);
            myGm.OnPaintDestroyed();
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetGameManager(GameManager newgm)
    {
        myGm = newgm;
    }
}
