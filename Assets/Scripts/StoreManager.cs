using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    [Header("UI Objects")]

    public Dropdown stores_dropdown;

    public Dropdown current_stores_dropdown;
    public Dropdown sections_dropdown;
    public GameObject sectionGrid;
    public GameObject editItemsGrid;
    public GameObject newListItemsGrid;
    public GameObject listItemPrefab;
    public GameObject sectionPrefab;
    public GameObject draggablePrefab;
    public GameObject newSectionButton;
    public GameObject newSectionInput;
    public GameObject newSectionConfirmation;

    public GameObject newItemButton;
    public GameObject newItemInput;
    public GameObject newItemConfirmation;
    public List<Dropdown.OptionData> store_options;
    public List<Dropdown.OptionData> section_options;


    [Header("Data Elements")]
    public GroceryStore activeStore = null;
    public SectionItem activeSection = null;

    public List<GroceryStore> my_stores;
    private List<GameObject> loadedSections = new List<GameObject>();
    private List<GameObject> editSections = new List<GameObject>();


    private UnityEvent onEndEvent = new UnityEvent();

    public int activeStoreID = -1;
    private string newSectionName = string.Empty;
    private string newItemName = string.Empty;

    // Default Sections
    private string[] defaultSections = new string[] { "Produce", "Alcohol", "Bread", "Meats", "Coffee", "Supplies", "Clothes", "Snacks", "Dairy" };

    //Deafult Items
    private string[] produce = new string[] { "Apples", "Bananas", "Lettuce", "Berries", "Veggies" };
    private string[] alcohol = new string[] { "Wine", "Beer" };
    private string[] breads = new string[] { "Bread", "Hot Dog buns", "Hamburger Buns", "Muffins" };
    private string[] meats = new string[] { "Meats", "Bacon", "Hot dogs", "Beef", "Chicken", "Turkey", "Pork", "Lunch Meat" };
    private string[] coffee = new string[] { "Coffee", "Starbucks", "Peets", "Creamer" };
    private string[] supplies = new string[] { "TP", "PT", "Trash Bags", "Sandwich Bags", "Paper Plates" };
    private string[] clothes = new string[] { "Socks", "Pants", "Shirts", "Underwear" };
    private string[] snacks = new string[] { "Snacks", "Cheeseits", "Pop Tarts", "Chips", "Popcorn", "Cereal" };
    private string[] dairy = new string[] { "Milk", "Yogurt", "Cheese", "Butter", "Sour Cream", "Cereal" };


    private int groceryListCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DefaultData();

        //UIManager.instance.HideEditStores();
        newSectionConfirmation.SetActive(false);
        newItemConfirmation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // monitor the lists
        if (groceryListCount != newListItemsGrid.transform.childCount)
        {
            groceryListCount = newListItemsGrid.transform.childCount;
            if (groceryListCount == 0)
            {
                print("List Complete!");
            }
        }

    }

    private void DefaultData()
    {
        stores_dropdown.ClearOptions();
        current_stores_dropdown.ClearOptions();

        AddNewStore("Harris Teeter", defaultSections);
        AddNewStore("Walmart", defaultSections);
        AddNewStore("Target", defaultSections);
        stores_dropdown.AddOptions(store_options);
        current_stores_dropdown.AddOptions(store_options);// for the paste list screen

        // start with the first store as the active store that is loaded
        SetActiveStore(0);
        activeSection = activeStore.sectionItems[0];
        LoadStoreSections(activeStore);

    }

    private void AddSectionItem(string sectionName, string itemName)
    {

    }

    private void AddNewStore(string storeName, string[] sections)
    {
        GroceryStore newStore = new GroceryStore();
        newStore.store_name = storeName;
        newStore.store_sections = sections;
        // populate some basic items
        newStore.sectionItems = new List<SectionItem>();
        newStore.sectionItems.Add(new SectionItem(sections[0], produce));
        newStore.sectionItems.Add(new SectionItem(sections[1], alcohol));
        newStore.sectionItems.Add(new SectionItem(sections[2], breads));
        newStore.sectionItems.Add(new SectionItem(sections[3], meats));
        newStore.sectionItems.Add(new SectionItem(sections[4], coffee));
        newStore.sectionItems.Add(new SectionItem(sections[5], supplies));
        newStore.sectionItems.Add(new SectionItem(sections[6], clothes));
        newStore.sectionItems.Add(new SectionItem(sections[7], snacks));
        newStore.sectionItems.Add(new SectionItem(sections[8], dairy));

        my_stores.Add(newStore);
        Dropdown.OptionData newOption = new Dropdown.OptionData();
        newOption.text = storeName;
        store_options.Add(newOption);
    }

    public void SetActiveStoreForPasteList(int storeID)
    {
        activeStore = my_stores[storeID];

    }

    public void SetActiveStore(int storeID)
    {
        activeStore = my_stores[storeID];
        LoadStoreSections(activeStore);
    }

    public void SetActiveSection(int id)
    {
        activeSection = activeStore.sectionItems[id];
        LoadEditStoreSectionItems();

    }

    private void ClearAllCurrentSections(List<GameObject> currentList)
    {
        foreach (GameObject child in currentList)
            Destroy(child);
    }

    public void LoadDefaultStoreSections()
    {
        if (activeStore == null)
            activeStore = my_stores[0];

        if (sectionGrid.transform.childCount > 0)
            ClearAllCurrentSections(loadedSections);

        foreach (string section in activeStore.store_sections)
            CreateNewSectionGameObject(section, loadedSections, draggablePrefab, sectionGrid);

    }

    private void LoadStoreSections(GroceryStore store)
    {
        if (sectionGrid.transform.childCount > 0)
            ClearAllCurrentSections(loadedSections);

        foreach (string section in store.store_sections)
            CreateNewSectionGameObject(section, loadedSections, draggablePrefab, sectionGrid);

    }

    public void AddNewSection()
    {
        // hide the add new section button to reveal the input
        newSectionButton.SetActive(false);
        // show the cancel / confirm buttons
        newSectionConfirmation.SetActive(true);
    }

    public void AddNewItem()
    {
        // hide the add new section button to reveal the input
        newItemButton.SetActive(false);
        // show the cancel / confirm buttons
        newItemConfirmation.SetActive(true);
    }

    public void CancelNewItem()
    {
        newItemButton.SetActive(true);
        newItemConfirmation.SetActive(false);
    }

    public void CancelNewSection()
    {
        newSectionButton.SetActive(true);
        newSectionConfirmation.SetActive(false);
    }

    public void ConfirmNewItem()
    {
        newItemButton.SetActive(true);
        newItemConfirmation.SetActive(false);

        CreateNewSectionGameObject(newItemName, editSections, sectionPrefab, editItemsGrid);
        // save the update
        activeSection.items = GetCurrentSectionList(editItemsGrid);

    }

    public void ConfirmNewSection()
    {
        newSectionButton.SetActive(true);
        newSectionConfirmation.SetActive(false);

        CreateNewSectionGameObject(newSectionName, loadedSections, draggablePrefab, sectionGrid);
        // save the update
        activeStore.store_sections = GetCurrentSectionList(sectionGrid);
    }

    private void CreateNewSectionGameObject(string name, List<GameObject> list, GameObject prefab, GameObject grid)
    {
        // add a new section
        GameObject section = GameObject.Instantiate(prefab, grid.transform);
        section.transform.GetChild(0).GetComponent<Text>().text = name;
        list.Add(section);
    }


    public void CreateNewListGameObject(string name)
    {
        // add a new section
        GameObject section = GameObject.Instantiate(listItemPrefab, newListItemsGrid.transform);
        //section.GetComponent<Lean.Gui.LeanDrag>().OnEnd.AddListener(() => HandleItemDragged(name, section));
        // set the first child
        section.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = name;


    }

    private void HandleItemDragged(string name, GameObject item)
    {
        Destroy(item);
        print(newListItemsGrid.transform.childCount + " list items reamining");

    }

    public void SetNewSectionName(string newSection)
    {
        newSectionName = newSection;
    }

    public void SetNewItemName(string newItem)
    {
        newItemName = newItem;
    }


    public void LoadEditStoreSectionItems()
    {
        if (activeSection == null)
            activeSection = activeStore.sectionItems[0];

        if (editItemsGrid.transform.childCount > 0)
            ClearAllCurrentSections(editSections);

        foreach (string item in activeSection.items)
            CreateNewSectionGameObject(item, editSections, sectionPrefab, editItemsGrid);


    }


    public void SetItemDropDown()
    {
        sections_dropdown.ClearOptions();
        section_options.Clear();

        foreach (SectionItem item in activeStore.sectionItems)
        {
            Dropdown.OptionData newOption = new Dropdown.OptionData();
            newOption.text = item.section_name;
            section_options.Add(newOption);
        }

        sections_dropdown.AddOptions(section_options);
    }

    private string[] GetCurrentSectionList(GameObject fromGameObject)
    {
        List<string> newArrangement = new List<string>();

        for (int i = 0; i < fromGameObject.transform.childCount; i++)
            newArrangement.Add(fromGameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text);

        return newArrangement.ToArray();
    }

    public void SaveNewArrangement()
    {
        // now save the new arrangement of sections
        if (activeStore != null)
            activeStore.store_sections = GetCurrentSectionList(sectionGrid);

    }


}

