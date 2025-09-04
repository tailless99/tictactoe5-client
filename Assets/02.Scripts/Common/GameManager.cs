using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject confirmPanel;

    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas �Ҵ�
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;

    // Game ���� UI�� ����ϴ� ��ü
    private GameUIController _gameUIController;


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
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked) {
        if (_canvas != null) {
            var confirPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// Game Scene���� ���� ǥ���ϴ� UI�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="type"></param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType type) {
        _gameUIController.SetGameTurnPanel(type);
    }

    // �� �ε�� ȣ��Ǵ� �Լ�
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game") {
            // Block �ʱ�ȭ
            var blockContoroller = FindFirstObjectByType<BlockController>();
            if (blockContoroller != null) {
                blockContoroller.InitBlocks();
            }

            // Game UI Controller �Ҵ� �� �ʱ�ȭ
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null) {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic ����
            _gameLogic = new GameLogic(blockContoroller, _gameType);
        }
    }
}
