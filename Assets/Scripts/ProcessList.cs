using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessList : MonoBehaviour
{
    public TextAsset demoList;

    public List<GroceryItem> groceryList;

    // Start is called before the first frame update
    void Start()
    {
        ProcessIncomingList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ProcessIncomingList()
    {
        string[] listItems = demoList.ToString().Split(',');
        Debug.Log("List contains: " + listItems.Length);
        foreach (var item in listItems)
        {
            groceryList.Add(new GroceryItem() { name = item });
        }
    }

}
