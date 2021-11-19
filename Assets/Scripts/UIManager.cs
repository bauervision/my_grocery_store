using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Main Screens")]
    public GameObject GroceryList;
    public GameObject StartScreen;

    public GameObject NewStoreScreen;
    public GameObject EditStoreScreen;
    public GameObject EditFoodScreen;
    public GameObject EditSectionsScreen;
    public GameObject EditStoresPanel;



    [Header("Paste List Items")]
    public GameObject PasteListPanel;
    public GameObject PasteListInput;
    public GameObject PasteListScrollView;
    public GameObject PasteListAddItem;

    [Header("Text Items")]
    public Text CurrentGroceryStore;


    private void Awake()
    {
        // always make sure the UI is in the right state before we launch
        InitUI();

        instance = this;
    }

    private void InitUI()
    {
        if (GroceryList.activeInHierarchy)
            GroceryList.SetActive(false);
        if (!StartScreen.activeInHierarchy)
            StartScreen.SetActive(true);

        if (!NewStoreScreen.activeInHierarchy)
            NewStoreScreen.SetActive(false);

        if (!EditStoreScreen.activeInHierarchy)
            EditStoreScreen.SetActive(false);

        if (!EditFoodScreen.activeInHierarchy)
            EditFoodScreen.SetActive(false);

        if (!EditSectionsScreen.activeInHierarchy)
            EditSectionsScreen.SetActive(false);

        // if visible, hide these
        if (PasteListPanel.activeInHierarchy)
            PasteListPanel.SetActive(false);

        if (PasteListInput.activeInHierarchy)
            PasteListInput.SetActive(false);

        if (PasteListAddItem.activeInHierarchy)
            PasteListAddItem.SetActive(false);

        if (PasteListScrollView.activeInHierarchy)
            PasteListScrollView.SetActive(false);



    }

    private void Start()
    {

    }

    #region Screen Handlers

    public void HideEditStores()
    {
        EditStoresPanel.SetActive(false);
    }

    public void ShowEditStores()
    {
        EditStoresPanel.SetActive(true);
    }

    private void HideAllScreens()
    {
        StartScreen.SetActive(false);
        NewStoreScreen.SetActive(false);
        EditFoodScreen.SetActive(false);
        EditStoreScreen.SetActive(false);
        EditSectionsScreen.SetActive(false);
        PasteListPanel.SetActive(false);
        GroceryList.SetActive(false);
    }

    ///<summary>Called from the UI whenever we process the incoming list. </summary>

    public void ShowConvertedList()
    {
        PasteListPanel.SetActive(false);
        GroceryList.SetActive(true);
        CurrentGroceryStore.text = StoreManager.instance.activeStore.store_name;

    }

    public void ReturnToList()
    {
        EditStoreScreen.SetActive(false);
        GroceryList.SetActive(true);
        StoreManager.instance.ClearOldGroceryList();

    }
    public void SetScreen_PasteList()
    {
        HideAllScreens();
        PasteListPanel.SetActive(true);
        PasteListInput.SetActive(true);

    }

    public void SetScreen_NewStore()
    {
        HideAllScreens();
        NewStoreScreen.SetActive(true);

    }

    public void SetScreen_EditStore()
    {
        HideAllScreens();
        EditStoreScreen.SetActive(true);
        StoreManager.instance.LoadDefaultStoreSections();

    }
    public void SetScreen_EditFood()
    {
        HideAllScreens();
        EditFoodScreen.SetActive(true);

    }

    public void SetScreen_EditSections()
    {
        HideAllScreens();
        EditSectionsScreen.SetActive(true);
        StoreManager.instance.LoadEditStoreSectionItems();
        StoreManager.instance.SetItemDropDown();
    }
    public void SetScreen_Start()
    {
        HideAllScreens();
        StartScreen.SetActive(true);

    }





    #endregion



}