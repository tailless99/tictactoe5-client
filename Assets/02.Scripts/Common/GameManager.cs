using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject confirmPanel;   // 확인창 패널
    [SerializeField] private GameObject signInPanel;    // 로그인 패널
    [SerializeField] private GameObject registerPanel;  // 회원가입 패널

    private Constants.GameType _gameType;

    // Panel을 띄우기 위한 Canvas 할당
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;

    // Game 씬의 UI를 담당하는 객체
    private GameUIController _gameUIController;



    private void Start() {
        // sid
        var sid = PlayerPrefs.GetString("sid");
        if (!string.IsNullOrEmpty(sid)) {
            OpenSigninPanel();
        }
    }

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
        _gameLogic?.Dispose();
        _gameLogic = null;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Confirm Panel을 띄우는 메서드
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked) {
        if (_canvas != null) {
            var confirPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// 로그인 팝업 표시
    /// </summary>
    /// <param name="message"></param>
    /// <param name="onConfirmButtonClicked"></param>
    public void OpenSigninPanel() {
        if (_canvas != null) {
            var loginPanelObject = Instantiate(signInPanel, _canvas.transform);
            loginPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }

    /// <summary>
    /// 회원가입 팝업 표시
    /// </summary>
    /// <param name="message"></param>
    /// <param name="onConfirmButtonClicked"></param>
    public void OpenRegisterPanel() {
        if (_canvas != null) {
            var loginPanelObject = Instantiate(registerPanel, _canvas.transform);
            loginPanelObject.GetComponent<RegisterPanelController>().Show();
        }
    }

    /// <summary>
    /// Game Scene에서 턴을 표시하는 UI를 제어하는 함수
    /// </summary>
    /// <param name="type"></param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType type) {
        _gameUIController.SetGameTurnPanel(type);
    }

    // 씬 로드시 호출되는 함수
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game") {
            // Block 초기화
            var blockContoroller = FindFirstObjectByType<BlockController>();
            if (blockContoroller != null) {
                blockContoroller.InitBlocks();
            }

            // Game UI Controller 할당 및 초기화
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null) {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic 생성
            if (_gameLogic != null) _gameLogic.Dispose();
            _gameLogic = new GameLogic(blockContoroller, _gameType);
        }
    }

    private void OnApplicationQuit() {
        _gameLogic.Dispose();
        _gameLogic = null;
    }
}
