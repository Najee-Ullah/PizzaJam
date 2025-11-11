using System.Collections;
using UnityEngine;

public class OptionsMenuUI : MonoBehaviour
{
    [Header("Local UI References")]
    [SerializeField] GameObject Visual;
    [SerializeField] MenuUI MenuUIRef;

    public bool IsActive { get { return Visual.activeSelf; } }

    private void Start()
    {
        InputHandler.Instance.OnPauseAction += Instance_OnPauseAction;
        Hide();
    }

    private void Instance_OnPauseAction(object sender, System.EventArgs e)
    {
        if (IsActive)
        {
            Hide();
            MenuUIRef.Show();
        }
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