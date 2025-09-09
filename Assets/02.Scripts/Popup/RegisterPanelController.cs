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
            GameManager.Instance.OpenConfirmPanel("���̵� ��й�ȣ, �Ǵ� �г����� �Է����� �ʾҽ��ϴ�.", () => Hide());
            Shake();
            return;
        }

        // Confirm Password Ȯ��
        if (password.Equals(confirmPassword)) {
            var signinData = new RegisterData();
            signinData.username = username;
            signinData.password = password;
            signinData.nickname = nickName;

            StartCoroutine(NetworkManager.Instance.RegisterUser(signinData, () => {
                GameManager.Instance.OpenConfirmPanel("ȸ�� ���Կ� �����Ͽ����ϴ�.", () => Hide());
            },
            (result) => {
                if (result == 0) {
                    GameManager.Instance.OpenConfirmPanel("�ߺ��� ���̵� �����մϴ�.", () => {
                        usernameInputField.text = "";
                        passwordInputField.text = "";
                        confirmPasswordInputField.text = "";
                        nickNameInputField.text = "";
                    });
                }
                else if (result == 1) {
                    GameManager.Instance.OpenConfirmPanel("��й�ȣ�� ��ȿ���� �ʽ��ϴ�.", () => {
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
    /// X ��ư Ŭ�� ��, ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickCloseButton() {
        Hide();
    }
}
