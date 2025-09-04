using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public delegate void OnBlockClicked(int index);
    private OnBlockClicked _onBlockClicked;


    // ��Ŀ Ÿ��
    public enum MarkerType { None, O, X }

    // Block Index
    private int _blockIndex;

    // Block�� �� ������ ���� Block�� Sprite Renderer
    private SpriteRenderer _spriteRenderer;
    private Color _defaultBlockColor;


    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultBlockColor = _spriteRenderer.color;
    }


    // 1. �ʱ�ȭ
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked) {
        _blockIndex = blockIndex;
        Setmarker(MarkerType.None);
        SetBlockColor(_defaultBlockColor);
        _onBlockClicked = onBlockClicked;
    }

    // 2. ��Ŀ ����
    public void Setmarker(MarkerType type) {
        switch (type) {
            case MarkerType.None:
                markerSpriteRenderer.sprite = null;
                break;
            case MarkerType.O:
                markerSpriteRenderer.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSpriteRenderer.sprite = xSprite;
                break;
        }
    }

    // 3. �÷� ����
    public void SetBlockColor(Color color) {
        _spriteRenderer.color = color;
    }

    // 4. �� ��ġ
    private void OnMouseUpAsButton() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Debug.Log($"Selected Block : {_blockIndex}");
        _onBlockClicked?.Invoke(_blockIndex);
    }
}