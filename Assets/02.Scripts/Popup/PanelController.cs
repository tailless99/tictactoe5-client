using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

// 팝업의 공통된 부분을 담는 컨트롤러
public class PanelController : MonoBehaviour {
    // 팝업창 RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    private void Awake() {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel 표시
    /// </summary>
    public void Show() {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        // DoTween을 이용한 Fade
        _backgroundCanvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        // 요소 활성화
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Panel 숨기기
    /// </summary>
    public void Hide() {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        // DoTween을 이용한 Fade
        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack);

        // 요소 비활성화
        // 안 보일 뿐, 잔여하고 있으면 상호작용이 안되기 때문에 비활성화
        gameObject.SetActive(false);
    }
}
