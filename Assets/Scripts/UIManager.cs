using System;
using UnityEngine;



public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Main Screens")]
    public GameObject GroceryList;
    public GameObject StartScreen;
    public GameObject NewListScreen;
    public GameObject NewStoreScreen;
    public GameObject EditStoreScreen;
    public GameObject EditFoodScreen;
    public GameObject EditSectionsScreen;
    public GameObject EditStoresPanel;

    [Header("New List Items")]
    public GameObject NewListGrid;
    public GameObject NewListPanel;


    [Header("Paste List Items")]
    public GameObject PasteListPanel;
    public GameObject PasteListInput;
    public GameObject PasteListScrollView;
    public GameObject PasteListAddItem;



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

        if (!NewListScreen.activeInHierarchy)
            NewListScreen.SetActive(false);

        if (!NewStoreScreen.activeInHierarchy)
            NewStoreScreen.SetActive(false);

        if (!EditStoreScreen.activeInHierarchy)
            EditStoreScreen.SetActive(false);

        if (!EditFoodScreen.activeInHierarchy)
            EditFoodScreen.SetActive(false);

        if (!EditSectionsScreen.activeInHierarchy)
            EditSectionsScreen.SetActive(false);

        // if visible, hide these
        if (NewListPanel.activeInHierarchy)
            NewListPanel.SetActive(false);

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
        NewListScreen.SetActive(false);
        NewStoreScreen.SetActive(false);
        EditFoodScreen.SetActive(false);
        EditStoreScreen.SetActive(false);
        EditSectionsScreen.SetActive(false);

        PasteListPanel.SetActive(false);

    }

    public void ShowConvertedList()
    {
        PasteListPanel.SetActive(false);
        GroceryList.SetActive(true);
    }

    public void SetScreen_PasteList()
    {
        HideAllScreens();
        PasteListPanel.SetActive(true);
        PasteListInput.SetActive(true);

    }
    public void SetScreen_NewList()
    {
        HideAllScreens();
        NewListScreen.SetActive(true);
        NewListPanel.SetActive(false);
        NewListGrid.SetActive(true);
    }

    public void NewList_CreateNew()
    {
        NewListPanel.SetActive(true);
        NewListGrid.SetActive(false);

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