using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableObjectScript : MonoBehaviour
{

    public int blockType;
    public float forceAmount;
    public GameObject target;
    Rigidbody rb;
    bool inRangeOfBlock;

    // delegate signature
    public delegate void EventBlockPickUp(int blockType);

    // event instances for EventBlockChanged
    public static event EventBlockPickUp OnEventBlockPickUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRangeOfBlock == true)
        {
            Debug.Log("ForceAdded");
            transform.LookAt(target.transform.position);
            rb.AddForce(transform.forward * 150f * forceAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            inRangeOfBlock = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Touch");
            if (blockType==1)
            {
                Debug.Log("G");
                OnEventBlockPickUp(blockType);
                Destroy(this.gameObject);
            }
            else if (blockType == 2)
            {
                Debug.Log("D");
                OnEventBlockPickUp(blockType);
                Destroy(this.gameObject);
            }
            else if (blockType == 3)
            {
                Debug.Log("Sa");
                OnEventBlockPickUp(blockType);
                Destroy(this.gameObject);
            }
            else if (blockType == 4)
            {
                Debug.Log("St");
                OnEventBlockPickUp(blockType);
                Destroy(this.gameObject);
            }
        }
    }

    

}
