using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

// 팝업의 공통된 부분을 담는 컨트롤러
public class PanelController : MonoBehaviour {
    // 팝업창 RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    // Panel이 Hide될 때 해야할 동작을 처리하는 대리자
    public delegate void PanelControllerHideDelegate();
    
    private void Awake() {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel 표시
    /// </summary>
    public virtual void Show() {
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
    public void Hide(PanelControllerHideDelegate hideDelegate = null) {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        // DoTween을 이용한 Fade
        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() => {
            hideDelegate?.Invoke();
            Destroy(gameObject);
        });
    }

    protected void Shake() {
        panelRectTransform.DOShakeAnchorPos(0.3f, 30f);
    }
}
