using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject Visual;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI promptButtonText;

    private bool isActive { get { return Visual.activeSelf; } }

    private void Start()
    {
        Show();
        promptButtonText.text = InputHandler.Instance.GetInteractBindingString();
    }

    public void ChangeText(string text)
    {
        promptText.text = text;
    }

    public void Show()
    {
        Visual.SetActive(true);
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }
    public bool IsActive()
    {
        return isActive;
    }
}
