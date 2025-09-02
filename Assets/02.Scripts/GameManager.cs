using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    private Constants.GameType _gameType;

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

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        // TODO : �� ��ȣ���� ó���� �Լ�
    }
}
