using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    [Header("UI Objects")]

    public Dropdown stores_dropdown;
    public GameObject sectionGrid;
    public GameObject editSectionGrid;
    public GameObject sectionPrefab;
    public GameObject draggablePrefab;
    public GameObject newSectionButton;
    public GameObject newSectionInput;
    public GameObject newSectionConfirmation;
    public List<Dropdown.OptionData> store_options;


    [Header("Data Elements")]
    public GroceryStore activeStore;
    public List<GroceryStore> my_stores;
    private List<GameObject> loadedSections = new List<GameObject>();
    private List<GameObject> editSections = new List<GameObject>();



    public int activeStoreID = -1;
    private string newSectionName = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DefaultData();
        newSectionConfirmation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DefaultData()
    {
        stores_dropdown.ClearOptions();
        AddNewStore("Harris Teeter", new string[] { "Produce", "Wine", "Bread", "Meats", "Coffee", "TP/PT" });
        AddNewStore("Walmart", new string[] { "Produce", "Frozen", "Coffee", "Cereal", "TP/PT", "Clothes" });
        AddNewStore("Target", new string[] { "Clothes", "Produce", "Coffee", "Frozen", "Cereal", "TP/PT", });

        stores_dropdown.AddOptions(store_options);

        // start with the first store as the active store that is loaded
        activeStore = my_stores[0];
        LoadStoreSections(activeStore);

    }

    private void AddNewStore(string storeName, string[] sections)
    {
        GroceryStore newStore = new GroceryStore();
        newStore.store_name = storeName;
        newStore.store_sections = sections;
        my_stores.Add(newStore);
        Dropdown.OptionData newOption = new Dropdown.OptionData();
        newOption.text = storeName;
        store_options.Add(newOption);
    }


    public void SetActiveStore(int storeID)
    {
        LoadStoreSections(my_stores[storeID]);
    }

    private void ClearAllCurrentSections(List<GameObject> currentList)
    {
        foreach (GameObject child in loadedSections)
            Destroy(child);

    }

    private void LoadStoreSections(GroceryStore store)
    {
        if (sectionGrid.transform.childCount > 0)
            ClearAllCurrentSections(loadedSections);

        foreach (string section in store.store_sections)
        {
            GameObject newSection = GameObject.Instantiate(sectionPrefab, sectionGrid.transform);
            newSection.transform.GetChild(0).GetComponent<Text>().text = section;
            loadedSections.Add(newSection);
        }

    }

    public void AddNewSection()
    {
        newSectionButton.SetActive(false);
        newSectionConfirmation.SetActive(true);
    }

    public void CancelNewSection()
    {
        newSectionButton.SetActive(true);
        newSectionConfirmation.SetActive(false);
    }

    public void ConfirmNewSection()
    {
        newSectionButton.SetActive(true);
        newSectionConfirmation.SetActive(false);

        GameObject draggable = GameObject.Instantiate(draggablePrefab, editSectionGrid.transform);
        draggable.transform.GetChild(0).GetComponent<Text>().text = newSectionName;
        editSections.Add(draggable);
    }

    public void SetNewSectionName(string newSection)
    {
        newSectionName = newSection;
    }

    public void LoadEditStoreSections()
    {
        if (editSectionGrid.transform.childCount > 0)
            ClearAllCurrentSections(editSections);

        if (activeStore != null)
        {
            foreach (string section in activeStore.store_sections)
            {
                GameObject draggable = GameObject.Instantiate(draggablePrefab, editSectionGrid.transform);
                draggable.transform.GetChild(0).GetComponent<Text>().text = section;
                editSections.Add(draggable);
            }
        }
        else
            print("Active store is null");

    }

    private List<string> GetCurrentSectionList()
    {
        List<string> newArrangement = new List<string>();

        for (int i = 0; i < editSectionGrid.transform.childCount; i++)
            newArrangement.Add(editSectionGrid.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text);

        return newArrangement;
    }

    public void SaveNewArrangement()
    {
        // now save the new arrangement of sections
        if (activeStore != null)
            activeStore.store_sections = GetCurrentSectionList().ToArray();

        print("New Arrangement: " + activeStore.store_sections);



    }
}
