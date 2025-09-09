using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

// �˾��� ����� �κ��� ��� ��Ʈ�ѷ�
public class PanelController : MonoBehaviour {
    // �˾�â RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    // Panel�� Hide�� �� �ؾ��� ������ ó���ϴ� �븮��
    public delegate void PanelControllerHideDelegate();
    
    private void Awake() {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel ǥ��
    /// </summary>
    public virtual void Show() {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        // DoTween�� �̿��� Fade
        _backgroundCanvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        // ��� Ȱ��ȭ
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Panel �����
    /// </summary>
    public void Hide(PanelControllerHideDelegate hideDelegate = null) {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        // DoTween�� �̿��� Fade
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
