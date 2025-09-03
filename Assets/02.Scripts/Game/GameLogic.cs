using System.Diagnostics;

public class GameLogic
{
    public BlockController BlockController;     // Block을 처리할 객체

    private Constants.PlayerType[,] _board;     // 보드의 상태 정보

    public BasePlayerState firstPlayerState;    // Player A
    public BasePlayerState secondPlayerState;   // Player B
    public BasePlayerState _currentPlayerState; // 현재 턴의 Player

    public enum GameResult { None, Win, Lose, Draw }


    public GameLogic(BlockController blockController, Constants.GameType gameType) {
        BlockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // Game Type 초기화
        switch (gameType) {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // 게임 시작
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    // 턴이 바뀔 때, 기존 진행하던 상태를 Exit하고
    // 이번 턴의 상태를 _currentPlayerState로 변경
    // 
    public void SetState(BasePlayerState state) {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    // _board 배열에 새로운 Marker 값을 할당
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

    // Game Over 처리
    public void EndGame(GameResult gameResult) {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // TODO : 유저에게 Game Over 표시
        Debug.Write("### Game Over ###");
    }

    // 게임의 결과 확인
    public GameResult CheckGameResult() {
        // 플레이어 A 승리 체크
        if(CheckGameWin(Constants.PlayerType.PlayerA, _board)) return GameResult.Win;

        // 플레이어 B 승리 체크
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) return GameResult.Lose;

        // 비겼는지 확인
        if (CheckGameDraw(_board)) return GameResult.Draw;

        // 다 아니라면, 아직 승부중이므로 None 상태 반환
        return GameResult.None;
    }

    // 비겼는지 확인
    public bool CheckGameDraw(Constants.PlayerType[,] board) {
        for(var row = 0; row < board.GetLength(0); row++) {
            for(var col = 0; col < board.GetLength(1); col++) {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }
        return true;
    }

    // 게임 승리 확인
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board) {
        // Col 체크 후 일자면 True
        for (var row = 0; row < board.GetLength(0); row++) {
            if (board[row, 0] == playerType && board[row, 1] == playerType && board[row, 2] == playerType)
                return true;
        }
        
        // Row 체크 후 일자면 True
        for (var col = 0; col < board.GetLength(1); col++) {
            if (board[0, col] == playerType && board[1, col] == playerType && board[2, col] == playerType)
                return true;
        }

        // 대각선 일자면 True
        if (board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType)
            return true;

        if (board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
            return true;

        return false;
    }
}
