using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessList : MonoBehaviour
{
    public TextAsset demoList;

    public List<GroceryItem> groceryList;

    public string rawList;

    // Start is called before the first frame update
    void Start()
    {
        //ProcessIncomingList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProcessIncomingList()
    {
        string[] listItems = rawList.ToString().Split(',');
        foreach (var item in listItems)
        {
            if (item != null)
            {
                groceryList.Add(HandleItemConversion(item));
            }
        }
    }

    private GroceryItem HandleItemConversion(string rawItemName)
    {
        GroceryItem newItem = new GroceryItem();
        newItem.name = rawItemName;
        newItem.section_name = GetSectionName(rawItemName);
        newItem.section_index = GetSectionIndex(newItem.section_name);
        newItem.inCart = false;

        return newItem;

    }

    private int GetSectionIndex(string sectionName)
    {
        int sectionIndex = StoreManager.instance.activeStore.store_sections.ToList().FindIndex(s => s == sectionName);

        return sectionIndex;
    }


    private string GetSectionName(string itemName)
    {
        string sectionName = "Unknown";
        foreach (SectionItem sectionItem in StoreManager.instance.activeStore.sectionItems)
        {
            // if (sectionItem.items.Contains(itemName))
            foreach (string item in sectionItem.items)
                if (HandleItemCases(item.ToLower(), itemName.ToLower()))
                    sectionName = sectionItem.section_name;

        }

        return sectionName;
    }

    private bool HandleItemCases(string currentItemName, string compareItemName)
    {
        return string.Equals(currentItemName.Trim(), compareItemName.Trim());
    }

    public void SetRawList(string list) { rawList = list; }

}
