using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/StringEvent")]
public class StringEvent : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }
}
