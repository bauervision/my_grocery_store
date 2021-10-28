using UnityEngine;

[System.Serializable]
public enum GroceryStoreSection { Produce, Frozen, Cereal, Sanitary }

[System.Serializable]
public class GroceryStore
{
    public string store_name;
    public string[] store_sections;


}