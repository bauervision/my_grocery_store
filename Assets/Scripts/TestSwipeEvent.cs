using UnityEngine;

public class TestSwipeEvent : MonoBehaviour
{
    public void SwipeEventCalled(string id)
    {
        Debug.Log(id + " swiped");
    }
}