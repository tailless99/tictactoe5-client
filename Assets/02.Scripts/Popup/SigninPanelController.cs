using TMPro;
using UnityEngine;

public struct SigninData {
    public string username;
    public string password;
}

public struct SigninResult {
    public int result;
}

public class SigninPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;


    public override void Show() {
        base.Show();
    }

    public void OnClickConfirmButton() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
            // TODO : 누락된 값을 입력하도록 요청
            GameManager.Instance.OpenConfirmPanel("아이디 또는 비밀번호를 입력하지 않았습니다.", () => Hide());
            Shake();
            return;
        }

        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        StartCoroutine(NetworkManager.Instance.Signin(signinData, () => {
            GameManager.Instance.OpenConfirmPanel("로그인에 성공하였습니다.", () => Hide());
        },
        (result) => {
            if(result == 0) {
                GameManager.Instance.OpenConfirmPanel("아이디가 일치하지 않습니다.", null);
            }
            else if (result == 1) {
                GameManager.Instance.OpenConfirmPanel("비밀번호가 일치하지 않습니다.", null);
            }
        }));
    }

    public void OpenRegisterPanel() {
        GameManager.Instance.OpenRegisterPanel();
    }
}
