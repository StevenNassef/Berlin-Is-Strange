using UnityEngine;

[System.Serializable]
public class InteractableObjectUI
{
    [SerializeField]
    private string ObjectName;
    [SerializeField]
    public Transform targetObject;
    public InteractableObjectUI()
    {
        ObjectName = "Object";
    }

    // public CreateObject(Transform targetObject)
    // {

    // }

    public string getName()
    {
        return ObjectName;
    }
}
