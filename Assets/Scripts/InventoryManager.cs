using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    bool remove;

    List<InventoryItemScript> inventoryList;

    void OnEnable()
    {
        CollectableObjectScript.OnEventBlockPickUp += AddObject;
    }

    // When game object is disabled
    void OnDisable()
    {
        CollectableObjectScript.OnEventBlockPickUp -= AddObject;
    }


    // Use this for initialization
    void Start () {
        //InventoryGen();
    }

	public void InventoryGen()
    {
        inventoryList = new List<InventoryItemScript>();
        for (int i = 0; i < itemNames.Count; i++)
        {

            // Create a duplicate of the starter item
            GameObject inventoryItem =
            (GameObject)Instantiate(startItem);
            inventoryItem.gameObject.name = ("InventoryItem_" + i);
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject invItem = GameObject.Find("InventoryItem_" + i);
                Debug.Log(invItem);
                if (invItem)
                {
                    Destroy(invItem);
                }
            }
            InventoryGen();
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

    public void SelectionSortInventory()
    {
        // iterate through every item in the list except last
        for (int i = 0; i < inventoryList.Count - 1; i++)
        {
            int minIndex = i;
            // iterate through unsorted portion of the list
            for (int j = i; j < inventoryList.Count; j++)
            {
                if (inventoryList[j].itemAmount <
                inventoryList[minIndex].itemAmount)
                {
                    minIndex = j;

                }
            }
            // Swap the minimum item into position
            if (minIndex != i)
            {
                InventoryItemScript iis = inventoryList[i];
                inventoryList[i] = inventoryList[minIndex];
                inventoryList[minIndex] = iis;
            }
        }
        // Display the list in the new correct order
        DisplayListInOrder();
    }

    public void StartQuickSort()
    {
        inventoryList = QuickSort(inventoryList);
        DisplayListInOrder();
    }

    List<InventoryItemScript> QuickSort(List<InventoryItemScript>
listIn)
    {
        if (listIn.Count <= 1)
        {
            return listIn;
        }
        // Naive pivot selection
        int pivotIndex = 0;
        // Left and right lists
        List<InventoryItemScript> leftList =
       new List<InventoryItemScript>();
        List<InventoryItemScript> rightList =
       new List<InventoryItemScript>();
        for (int i = 1; i < listIn.Count; i++)
        {
            // Compare amounts to determine list to add to
            if (listIn[i].itemAmount > listIn[pivotIndex].itemAmount)
            {
                // Greater than pivot to left list
                leftList.Add(listIn[i]);
            }
            else
            {
                // Smaller than pivot to right list
                rightList.Add(listIn[i]);
            }
        }

        // Recurse left list
        leftList = QuickSort(leftList);
        //Recurse right list
        rightList = QuickSort(rightList);
        // Concatenate lists: left + pivot + right
        leftList.Add(listIn[pivotIndex]);
        leftList.AddRange(rightList);
        return leftList;
    }

    public void AddObject(int blockType)
    {
        itemAmounts[blockType - 1]++;
    }

}
