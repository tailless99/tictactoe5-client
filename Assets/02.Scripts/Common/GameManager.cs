using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject confirmPanel;

    private Constants.GameType _gameType;

    // Panel을 띄우기 위한 Canvas 할당
    private Canvas _canvas;

    /// <summary>
    /// Maint에서 Game Scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType) {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game에서 Main Scene으로 전환 시 호출될 메서드
    /// </summary>
    public void ChangeToMainScene() {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Confirm Panel을 띄우는 메서드
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message) {
        if(_canvas != null) {
            var confirPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        // TODO : 씬 전환시 처리할 함수
        _canvas = FindFirstObjectByType<Canvas>();
    }
}
