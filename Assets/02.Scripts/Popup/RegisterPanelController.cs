using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;


public struct RegisterData {
    public string username;
    public string password;
    public string nickname;
}
public class RegisterPanelController : PanelController {
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TMP_InputField nickNameInputField;


    public override void Show() {
        base.Show();
    }

    public void OnClickRegisterButton() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string nickName = nickNameInputField.text;


        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(nickName) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)) {
            GameManager.Instance.OpenConfirmPanel("아이디나 비밀번호, 또는 닉네임을 입력하지 않았습니다.", () => Hide());
            Shake();
            return;
        }

        // Confirm Password 확인
        if (password.Equals(confirmPassword)) {
            var signinData = new RegisterData();
            signinData.username = username;
            signinData.password = password;
            signinData.nickname = nickName;

            StartCoroutine(NetworkManager.Instance.RegisterUser(signinData, () => {
                GameManager.Instance.OpenConfirmPanel("회원 가입에 성공하였습니다.", () => Hide());
            },
            (result) => {
                if (result == 0) {
                    GameManager.Instance.OpenConfirmPanel("중복된 아이디가 존재합니다.", () => {
                        usernameInputField.text = "";
                        passwordInputField.text = "";
                        confirmPasswordInputField.text = "";
                        nickNameInputField.text = "";
                    });
                }
                else if (result == 1) {
                    GameManager.Instance.OpenConfirmPanel("비밀번호가 유효하지 않습니다.", () => {
                        passwordInputField.text = "";
                        confirmPasswordInputField.text = "";
                    });
                }
            }));
        }
        else {
            return;
        }
    }

    /// <summary>
    /// X 버튼 클릭 시, 호출되는 메서드
    /// </summary>
    public void OnClickCloseButton() {
        Hide();
    }
}
