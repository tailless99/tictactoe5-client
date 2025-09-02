using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TextMeshProUGUI messageText;

    /// <summary>
    /// Confirm Panel�� ǥ���ϴ� �޼���
    /// </summary>
    /// <param name="message"></param>
    public void Show(string message) {
        messageText.text = message;
        base.Show();
    }

    /// <summary>
    /// Ȯ�� ��ư Ŭ�� ��, ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickConfirmButton() {
        Hide();
    }

    /// <summary>
    /// X ��ư Ŭ�� ��, ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickCloseButton() {
        Hide();
    }
}
