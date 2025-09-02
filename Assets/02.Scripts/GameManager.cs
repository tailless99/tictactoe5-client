using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    private Constants.GameType _gameType;

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

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        // TODO : 씬 전호나시 처리할 함수
    }
}
