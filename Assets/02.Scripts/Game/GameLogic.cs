using System;
using UnityEngine;

public class GameLogic : IDisposable
{
    public BlockController BlockController;     // Block�� ó���� ��ü

    private Constants.PlayerType[,] _board;     // ������ ���� ����

    public BasePlayerState firstPlayerState;    // Player A
    public BasePlayerState secondPlayerState;   // Player B
    public BasePlayerState _currentPlayerState; // ���� ���� Player

    private MultiplayController _multiplayController; // Multiplay
    private string _roomId; // room ID

    public enum GameResult { None, Win, Lose, Draw }


    public GameLogic(BlockController blockController, Constants.GameType gameType) {
        BlockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // Game Type �ʱ�ȭ
        switch (gameType) {
            case Constants.GameType.SinglePlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new AIState();
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);
                break;
            case Constants.GameType.MultiPlay:
                _multiplayController = new MultiplayController((state, roomId) => {
                    _roomId = roomId;
                    switch (state) {
                        case Constants.MultiplayControllerState.CreateRoom:
                            Debug.Log("## Create Room ##");
                            // TODO: ��� ȭ�� UI ǥ��
                            break;
                        case Constants.MultiplayControllerState.JoinRoom:
                            Debug.Log("## Join Room ##");
                            firstPlayerState = new MultiplayerState(true, _multiplayController);
                            secondPlayerState = new PlayerState(false, _multiplayController, _roomId);
                            SetState(firstPlayerState);
                            break;
                        case Constants.MultiplayControllerState.StartGame:
                            Debug.Log("## Start Game ##");
                            firstPlayerState = new PlayerState(true, _multiplayController, _roomId);
                            secondPlayerState = new MultiplayerState(false, _multiplayController);
                            SetState(firstPlayerState);
                            break;
                        case Constants.MultiplayControllerState.ExitRoom:
                            Debug.Log("## Exit Room ##");
                            // TODO: �˾� ���� ����ȭ������ �̵�
                            break;
                        case Constants.MultiplayControllerState.EndGame:
                            Debug.Log("## End Game ##");
                            // TODO: �˾� ���� ����ȭ������ �̵�
                            break;
                    }
                });
                break;
        }
        
        // ���� ����
        SetState(firstPlayerState);
    }

    // �ܺο��� ���带 ������ �� �ֵ��� ��ȯ
    public Constants.PlayerType[,] GetBoard() {
        return _board;
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
        
        if (playerType == Constants.PlayerType.PlayerA) {
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

        // �������� Game Over ǥ��
        GameManager.Instance.OpenConfirmPanel("���� ����", () => {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    // ������ ��� Ȯ��
    public GameResult CheckGameResult() {
        
        if(TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerA, _board)) return GameResult.Win; // �÷��̾� A �¸� üũ
        if(TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerB, _board)) return GameResult.Lose; // �÷��̾� B �¸� üũ
        if(TicTacToeAI.CheckGameDraw(_board)) return GameResult.Draw; // ������ Ȯ��

        // �� �ƴ϶��, ���� �º����̹Ƿ� None ���� ��ȯ
        return GameResult.None;
    }

    public void Dispose() {
        _multiplayController?.LeaveRoom(_roomId);
        _multiplayController?.Dispose();
    }
}
