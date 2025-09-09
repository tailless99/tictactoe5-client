using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController {
    [SerializeField] private TextMeshProUGUI messageText;

    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked _onConfirmButtonClicked;



    /// <summary>
    /// Confirm Panel�� ǥ���ϴ� �޼���
    /// </summary>
    /// <param name="message"></param>
    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked) {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;

        base.Show();
    }

    /// <summary>
    /// Ȯ�� ��ư Ŭ�� ��, ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickConfirmButton() {
        Hide(() => {
            _onConfirmButtonClicked?.Invoke();
        });

    }

    /// <summary>
    /// X ��ư Ŭ�� ��, ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickCloseButton() {
        Hide();
    }
}
