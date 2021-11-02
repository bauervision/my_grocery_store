using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Main Screens")]
    public GameObject StartScreen;
    public GameObject NewListScreen;
    public GameObject NewStoreScreen;
    public GameObject EditStoreScreen;
    public GameObject EditFoodScreen;
    public GameObject EditSectionsScreen;

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


    }

    private void InitUI()
    {
        if (!StartScreen.activeInHierarchy)
            StartScreen.SetActive(true);

        if (!NewListScreen.activeInHierarchy)
            NewListScreen.SetActive(true);

        if (!NewStoreScreen.activeInHierarchy)
            NewStoreScreen.SetActive(true);

        if (!EditStoreScreen.activeInHierarchy)
            EditStoreScreen.SetActive(true);

        if (!EditFoodScreen.activeInHierarchy)
            EditFoodScreen.SetActive(true);

        if (!EditSectionsScreen.activeInHierarchy)
            EditSectionsScreen.SetActive(true);

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

    #region Screen Handlers

    private void HideAllScreens()
    {
        StartScreen.SetActive(false);
        NewListScreen.SetActive(false);
        NewStoreScreen.SetActive(false);
        EditFoodScreen.SetActive(false);
        EditStoreScreen.SetActive(false);
        EditSectionsScreen.SetActive(false);

        PasteListPanel.SetActive(false);
        PasteListInput.SetActive(false);
        PasteListScrollView.SetActive(false);
        PasteListAddItem.SetActive(false);
    }

    public void ShowConvertedList()
    {
        PasteListInput.SetActive(false);
        PasteListScrollView.SetActive(true);
        PasteListAddItem.SetActive(true);
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