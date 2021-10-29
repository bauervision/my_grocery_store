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
    }

    #region Screen Handlers
    public void SetScreen_NewList()
    {
        HideAllScreens();
        NewListScreen.SetActive(true);

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

    private void HideAllScreens()
    {
        StartScreen.SetActive(false);
        NewListScreen.SetActive(false);
        NewStoreScreen.SetActive(false);
        EditFoodScreen.SetActive(false);
        EditStoreScreen.SetActive(false);
        EditSectionsScreen.SetActive(false);
    }

    #endregion



}