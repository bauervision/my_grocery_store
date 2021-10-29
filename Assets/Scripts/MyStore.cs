using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GroceryStore
{
    public string store_name;
    public string[] store_sections;
    public List<SectionItem> sectionItems;


}

[System.Serializable]
public class SectionItem
{
    public string section_name;
    public string[] items;

    public SectionItem(string name, string[] sectionItems)
    {
        this.section_name = name;
        this.items = sectionItems;
    }
}