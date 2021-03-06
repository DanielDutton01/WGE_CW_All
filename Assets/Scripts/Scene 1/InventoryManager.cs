﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    // Parent object inventory item
    public Transform parentPanel;
    // Item info to build inventory items
    public List<Sprite> itemSprites;
    public List<string> itemNames;
    public List<int> itemAmounts;
    // Starting template item
    public GameObject startItem;

    public GameObject[] buttons;

    public InputField searchField;

    bool remove = true;
    GameObject[] invItem;

    List<InventoryItemScript> inventoryList;

    public delegate void EventBlockCreate(Vector3 v, int blockType);

    // event instances for EventBlockChanged
    public static event EventBlockCreate OnEventBlockCreate;


    void OnEnable()
    {
        CollectableObjectScript.OnEventBlockPickUp += AddObject;
        PlayerScript.OnEventBlockPlace += ClearBlock;
    }

    // When game object is disabled
    void OnDisable()
    {
        CollectableObjectScript.OnEventBlockPickUp -= AddObject;
        PlayerScript.OnEventBlockPlace -= ClearBlock;
    }

	public void InventoryGen()
    {
        inventoryList = new List<InventoryItemScript>();
        for (int i = 0; i < itemNames.Count; i++)
        {

            // Create a duplicate of the starter item
            GameObject inventoryItem = (GameObject)Instantiate(startItem);
            inventoryItem.gameObject.tag = ("TempInvObject");
            // UI items need to parented by the canvas or an object within the canvas
            inventoryItem.transform.SetParent(parentPanel);
            // Original start item is disabled – so the duplicate must be enabled
            inventoryItem.SetActive(true);
            // Get InventoryItemScript component so we can set the data
            InventoryItemScript iis = inventoryItem.GetComponent<InventoryItemScript>();
            iis.itemSprite.sprite = itemSprites[i];
            iis.itemNameText.text = itemNames[i];
            iis.itemName = itemNames[i];
            iis.itemAmountText.text = itemAmounts[i].ToString();
            iis.itemAmount = itemAmounts[i];
            // Keep a list of the inventory items
            inventoryList.Add(iis);
        }
        DisplayListInOrder();
    }

	// Update is called once per frame
	void Update () {
        //deletes and renews the inventory everytime it is opened
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
            invItem = GameObject.FindGameObjectsWithTag("TempInvObject");
            remove = !remove;
            if (remove)
            {
                foreach (GameObject item in invItem)
                {
                    Destroy(item);
                }
            }
            else
            {
                InventoryGen();
            }
        }
    }


    void DisplayListInOrder()
    {
        // Height of item plus space between each
        float yOffset = 55f;
        // Use the start position for the first item
        Vector3 startPosition = startItem.transform.position;
        foreach (InventoryItemScript iis in inventoryList)
        {
            iis.transform.position = startPosition;
            //set position of next item using offset
            startPosition.y -= yOffset;
        }      
    }


    public void AddObject(int blockType)
    {
        itemAmounts[blockType - 1]++;
    }

    public void ClearBlock(Vector3 v, int blockType)
    {
        if (itemAmounts[blockType - 1] != 0)
        {
            itemAmounts[blockType - 1]--;
            OnEventBlockCreate(v, blockType);
        }
    }

    public void StartMergeSort(int mergeMethod)
    {
        inventoryList = MergeSort(inventoryList, mergeMethod);
        DisplayListInOrder();
    }

    public void StartStringSearch()
    {
        inventoryList = Search(inventoryList);
        DisplayListInOrder();
    }

    List<InventoryItemScript> Search(List<InventoryItemScript> a)
    {
        List<InventoryItemScript> invSearch = new List<InventoryItemScript>(a.Count);
        string searchResults = searchField.text;

        bool[] searchCounter = new bool[a.Count];

        for (int i = 0; i < a.Count; i++)
        {
            if(searchResults.ToLower() == a[i].itemName.ToLower())
            {
                invSearch.Add(a[i]);
                searchCounter[i] = true;
            }
        }

        for(int i = 0; i < a.Count; i++)
        {
            if(searchCounter[i] == false)
            {
                invSearch.Add(a[i]);
            }
        }

        return invSearch;
    }

    List<InventoryItemScript> MergeSort(List<InventoryItemScript> a, int MergeMethod)
    {
        
        /* check if only one element */
        if (a.Count <= 1)
        {
            return a; /* no sorting needed */
        }

        List<InventoryItemScript> left = new List<InventoryItemScript>();
        List<InventoryItemScript> right = new List<InventoryItemScript>();
        
        for (int i = 0; i < a.Count; i++)
        {
            if(i < Mathf.CeilToInt(a.Count/2))
            {
                left.Add(a[i]);
            }
            else
            {
                right.Add(a[i]);
            }
        }


            a = Merge(MergeSort(left, MergeMethod), MergeSort(right, MergeMethod), MergeMethod);

        return a;
    }

    public List<InventoryItemScript> Merge(List<InventoryItemScript> l, List<InventoryItemScript> r, int method)
    {
        List<InventoryItemScript> m = new List<InventoryItemScript>();
        int i = 0;
        int j = 0;

        while (i < l.Count && j < r.Count && method == 0)
        {
            if (l[i].itemAmount < r[j].itemAmount)
            {
                m.Add(l[i]);
                i++;
            }
            else
            {
                m.Add(r[j]);
                j++;
            }
        }

        while (i < l.Count && j < r.Count && method == 1)
        {
            if (l[i].itemAmount > r[j].itemAmount)
            {
                m.Add(l[i]);
                i++;
            }
            else
            {
                m.Add(r[j]);
                j++;
            }
        }

        while (i < l.Count && j < r.Count && method == 2)
        {
            //String, String > Char, Char > Int, Int
            string name1 = l[i].itemName;
            string name2 = r[i].itemName;
            char[] charName1 = name1.ToCharArray();
            char[] charName2 = name2.ToCharArray();
            int intName1 = Convert.ToInt32(charName1[0]);
            int intName2 = Convert.ToInt32(charName2[0]);

            if (intName1 <= intName2)
            {
                Debug.Log(name1);
                m.Add(l[i]);
                i++;              
            }
            else
            {
                Debug.Log(name2);
                m.Add(r[j]);
                j++;
            }
        }

        while (i < l.Count && j < r.Count && method == 3)
        {
            //String, String > Char, Char > Int, Int
            string name1 = l[i].itemName;
            string name2 = r[i].itemName;
            char[] charName1 = name1.ToCharArray();
            char[] charName2 = name2.ToCharArray();
            int intName1 = Convert.ToInt32(charName1[0]);
            int intName2 = Convert.ToInt32(charName2[0]);

            if (intName1 >= intName2)
            {
                m.Add(l[i]);
                i++;
            }
            else
            {
                m.Add(r[j]);
                j++;
            }
        }

        while (i < l.Count)
        {
            m.Add(l[i]);
            i++;
        }
        while (j < r.Count)
        {
            m.Add(r[j]);
            j++;
        }
            return m;
    }

}
