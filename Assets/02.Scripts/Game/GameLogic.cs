using System.Diagnostics;

public class GameLogic
{
    public BlockController BlockController;     // Block�� ó���� ��ü

    private Constants.PlayerType[,] _board;     // ������ ���� ����

    public BasePlayerState firstPlayerState;    // Player A
    public BasePlayerState secondPlayerState;   // Player B
    public BasePlayerState _currentPlayerState; // ���� ���� Player

    public enum GameResult { None, Win, Lose, Draw }


    public GameLogic(BlockController blockController, Constants.GameType gameType) {
        BlockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // Game Type �ʱ�ȭ
        switch (gameType) {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // ���� ����
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    // ���� �ٲ� ��, ���� �����ϴ� ���¸� Exit�ϰ�
    // �̹� ���� ���¸� _currentPlayerState�� ����
    // 
    public void SetState(BasePlayerState state) {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    // _board �迭�� ���ο� Marker ���� �Ҵ�
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col) {
        if (_board[row, col] != Constants.PlayerType.None) return false;
        
        if(playerType == Constants.PlayerType.PlayerA) {
            _board[row, col] = playerType;
            BlockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if(playerType == Constants.PlayerType.PlayerB) {
            _board[row, col] = playerType;
            BlockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    // Game Over ó��
    public void EndGame(GameResult gameResult) {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // TODO : �������� Game Over ǥ��
        Debug.Write("### Game Over ###");
    }

    // ������ ��� Ȯ��
    public GameResult CheckGameResult() {
        // �÷��̾� A �¸� üũ
        if(CheckGameWin(Constants.PlayerType.PlayerA, _board)) return GameResult.Win;

        // �÷��̾� B �¸� üũ
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) return GameResult.Lose;

        // ������ Ȯ��
        if (CheckGameDraw(_board)) return GameResult.Draw;

        // �� �ƴ϶��, ���� �º����̹Ƿ� None ���� ��ȯ
        return GameResult.None;
    }

    // ������ Ȯ��
    public bool CheckGameDraw(Constants.PlayerType[,] board) {
        for(var row = 0; row < board.GetLength(0); row++) {
            for(var col = 0; col < board.GetLength(1); col++) {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }
        return true;
    }

    // ���� �¸� Ȯ��
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board) {
        // Col üũ �� ���ڸ� True
        for (var row = 0; row < board.GetLength(0); row++) {
            if (board[row, 0] == playerType && board[row, 1] == playerType && board[row, 2] == playerType)
                return true;
        }
        
        // Row üũ �� ���ڸ� True
        for (var col = 0; col < board.GetLength(1); col++) {
            if (board[0, col] == playerType && board[1, col] == playerType && board[2, col] == playerType)
                return true;
        }

        // �밢�� ���ڸ� True
        if (board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType)
            return true;

        if (board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
            return true;

        return false;
    }
}
