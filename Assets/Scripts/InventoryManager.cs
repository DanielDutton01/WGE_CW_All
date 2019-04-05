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

    bool remove = true;
    bool quickSorted;
    bool SelSorted;
    bool MergeSorted;
    GameObject[] invItem;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
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
    /*
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
        SelSorted = true;
        quickSorted = false;
        MergeSorted = false;
    }

    public void StartQuickSort()
    {
        inventoryList = QuickSort(inventoryList);
        DisplayListInOrder();
        SelSorted = false;
        quickSorted = true;
        MergeSorted = false;
    }

    List<InventoryItemScript> QuickSort(List<InventoryItemScript> listIn)
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
    }*/

    public void AddObject(int blockType)
    {
        itemAmounts[blockType - 1]++;
    }


    public void StartMergeSort()
    {
        inventoryList = MergeSort(inventoryList);
        DisplayListInOrder();
        MergeSorted = true;
    }


    List<InventoryItemScript> MergeSort(List<InventoryItemScript> a)
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
            if(i < (a.Count/2))
            {
                left.Add(a[i]);
            }
            else
            {
                right.Add(a[i]);
            }
        }

        MergeSort(left);
        MergeSort(right);
        a = Merge(left, right);
        return a;
    }


    public List<InventoryItemScript> Merge(List<InventoryItemScript> l, List<InventoryItemScript> r)
    {
        List<InventoryItemScript> m = new List<InventoryItemScript>();
        int i = 0, j = 0;

        while (i < l.Count && j < r.Count)
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
        }        if (i < l.Count)
        {
            m.Add(l[i]);
        }
        else
        {
            m.Add(r[j]);
        }

            return m;
    }
}
