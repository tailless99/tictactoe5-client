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
            // TODO : ������ ���� �Է��ϵ��� ��û
            GameManager.Instance.OpenConfirmPanel("���̵� �Ǵ� ��й�ȣ�� �Է����� �ʾҽ��ϴ�.", () => Hide());
            Shake();
            return;
        }

        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        StartCoroutine(NetworkManager.Instance.Signin(signinData, () => {
            GameManager.Instance.OpenConfirmPanel("�α��ο� �����Ͽ����ϴ�.", () => Hide());
        },
        (result) => {
            if(result == 0) {
                GameManager.Instance.OpenConfirmPanel("���̵� ��ġ���� �ʽ��ϴ�.", null);
            }
            else if (result == 1) {
                GameManager.Instance.OpenConfirmPanel("��й�ȣ�� ��ġ���� �ʽ��ϴ�.", null);
            }
        }));
    }

    public void OpenRegisterPanel() {
        GameManager.Instance.OpenRegisterPanel();
    }
}
