using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject Visual;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI promptButtonText;

    private void Start()
    {
        Hide();
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
}
