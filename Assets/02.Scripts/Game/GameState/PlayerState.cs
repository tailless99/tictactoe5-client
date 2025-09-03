using System.Net.NetworkInformation;

public class PlayerState : BasePlayerState {
    
    private bool _isFirstPlayer;
    private Constants.PlayerType _playerType;

    // ������ �ʱ�ȭ
    public PlayerState(bool isFirstPlayer) {
        _isFirstPlayer = isFirstPlayer;
        _playerType = _isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

#region �ʼ� �޼���
    public override void OnEnter(GameLogic gameLogic) {
        // 1. First Player���� Ȯ���ؼ� ���� UI�� ���� �� ǥ��
        // TODO : Game ���� �� ǥ�� UI ���� �� ���� ����

        // 2. Block Controller���� �ؾ� �� ���� ����
        gameLogic.BlockController.OnBlockClickedDelegate = (row, col) => {
            // Block�� ��ġ �� ������ ��ٷȴٰ�
            // ��ġ �Ǹ� ó���� ��
            HandleMove(gameLogic, row, col);
        };
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col) {
        ProcessMove(gameLogic, _playerType, row, col);
    }

    protected override void HandleNextTurn(GameLogic gameLogic) {
        if (_isFirstPlayer) {
            // TODO : ���� �������� Second Player�� ���¸� Ȱ��ȭ �϶�� ����
        }
        else {
            // TODO : ���� �������� First Player�� ���¸� Ȱ��ȭ �϶�� ����
        }
    }

    public override void OnExit(GameLogic gameLogic) {
        gameLogic.BlockController.OnBlockClickedDelegate = null;
    }
    #endregion
}
