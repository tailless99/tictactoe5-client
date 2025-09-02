using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TextMeshProUGUI messageText;

    /// <summary>
    /// Confirm Panel을 표시하는 메서드
    /// </summary>
    /// <param name="message"></param>
    public void Show(string message) {
        messageText.text = message;
        base.Show();
    }

    /// <summary>
    /// 확인 버튼 클릭 시, 호출되는 메서드
    /// </summary>
    public void OnClickConfirmButton() {
        Hide();
    }

    /// <summary>
    /// X 버튼 클릭 시, 호출되는 메서드
    /// </summary>
    public void OnClickCloseButton() {
        Hide();
    }
}
