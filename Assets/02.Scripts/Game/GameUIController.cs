using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton() {
        GameManager.Instance.OpenConfirmPanel("������ �����Ͻðڽ��ϱ�?", () => {
            GameManager.Instance.ChangeToMainScene();
        });
    }
}
