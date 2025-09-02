using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject confirmPanel;

    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas �Ҵ�
    private Canvas _canvas;

    /// <summary>
    /// Maint���� Game Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType) {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game���� Main Scene���� ��ȯ �� ȣ��� �޼���
    /// </summary>
    public void ChangeToMainScene() {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Confirm Panel�� ���� �޼���
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message) {
        if(_canvas != null) {
            var confirPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        // TODO : �� ��ȯ�� ó���� �Լ�
        _canvas = FindFirstObjectByType<Canvas>();
    }
}
