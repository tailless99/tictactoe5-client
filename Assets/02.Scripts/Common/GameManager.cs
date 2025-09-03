using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject confirmPanel;

    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas �Ҵ�
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;

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
        if(_canvas != null) {
            var confirPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
        }
    }

    // �� �ε�� ȣ��Ǵ� �Լ�
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        _canvas = FindFirstObjectByType<Canvas>();

        if(scene.name == "Game") {
            // Block �ʱ�ȭ
            var blockContoroller = FindFirstObjectByType<BlockController>();
            blockContoroller.InitBlocks();

            // GameLogic ����
            if(_gameLogic != null) {
                // TODO : ���� ���� ������ �Ҹ�
            }
            _gameLogic = new GameLogic(blockContoroller, _gameType);
        }
    }
}
