using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int buildBlockType = 1;
    public GameObject inventoryMenu;
    public GameObject gameMenu;

    int layerMask = 1 << 12;

    // delegate signature
    public delegate void EventBlockUse(Vector3 v, int blockType);

    // event instances for EventBlockChanged
    public static event EventBlockUse OnEventBlockUse;




    // Use this for initialization
    void Start () {
        InventoryInactive();
        MenuInactive();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, true))
            {
                OnEventBlockUse(v, 0);
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, false))
            {
                Debug.Log(v);
                OnEventBlockUse(v, buildBlockType);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildBlockType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildBlockType = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buildBlockType = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            buildBlockType = 4;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryMenu.activeInHierarchy == false)
            {              
                InventoryActive();
                
            }
            else
            {
                InventoryInactive();             
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameMenu.activeInHierarchy == false)
            {
                MenuActive();
            }
            else
            {
                MenuInactive();
            }
        }

    }
    
    bool PickBlock(out Vector3 v, float dist, bool blockOption)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new
        Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            if (inventoryMenu.activeInHierarchy == false)
            {
                v = (blockOption ? hit.point - hit.normal / 2 : hit.point + hit.normal / 2);
                // round down to get the index of the block hit
                v.x = Mathf.Floor(v.x);
                v.y = Mathf.Floor(v.y);
                v.z = Mathf.Floor(v.z);
                return true;
            }
        }

        return false;
    }
    
    public void InventoryActive()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inventoryMenu.SetActive (true);
    }

    public void InventoryInactive()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inventoryMenu.SetActive (false);
    }

    public void MenuActive()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameMenu.SetActive(true);
    }

    public void MenuInactive()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameMenu.SetActive(false);
    }

}
