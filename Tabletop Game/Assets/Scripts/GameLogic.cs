using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
  public struct Player
  {
    public string Name;
    public int CellId;
    public int Skip;
    public Player(string _name, int _cellId) { Name = _name; CellId = _cellId; Skip = 0; }
  }
  [SerializeField] GameObject DiceObject;
  [SerializeField] int NumberOfPlayers = 1;
  [SerializeField] GameObject[] PlayerSprites;
  Player[] PlayerArray;
  [SerializeField] Cell[] CellArray;
  int CurrentPlayerID;
  [SerializeField] GameObject GameCamera;

  int CurrentGameState = -1;
  int DiceNumber;
  bool isPlayerSkip = false;
  Player CurrentPlayer;
  Cell CurrentCell;
  private void Start()
  {
    StartGame();
  }
  public void StartGame()
  {
    PlayerArray = new Player[NumberOfPlayers];
    for (int i = 0; i < NumberOfPlayers; i++)
    {
      PlayerArray[i] = new Player(i.ToString(), 0);
      PlayerSprites[i].transform.position = CellArray[0].transform.position + new Vector3(i * 0.5f - 1, 0, 0);
    }
    CurrentPlayerID = 0;

    for (int i = 0; i < CellArray.Length; i++)
    {
      CellArray[i].CellId = i;
    }
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      CurrentGameState++;
      if (CurrentGameState >= 3) CurrentGameState = 0;
      switch (CurrentGameState)
      {
        case 0:NextTurn(); break;
        case 1:if(!isPlayerSkip) RollDice(); break;
        case 2: if (!isPlayerSkip) MovePlayer();break;
        default:NextTurn(); break;
      }
    }
    
  }
  public void NextTurn()
  {
    isPlayerSkip = false;
    CurrentPlayerID++;
    if (CurrentPlayerID >= NumberOfPlayers) CurrentPlayerID = 0;
    CurrentPlayer = PlayerArray[CurrentPlayerID];
    Debug.Log("id = "+CurrentPlayer.CellId);
    CurrentCell = CellArray[CurrentPlayer.CellId];
    if (CurrentPlayer.Skip > 0)
    {
      isPlayerSkip = true;
      CurrentPlayer.Skip--;
      if (CurrentPlayer.Skip < 0) CurrentPlayer.Skip = 0;

    }
    GameCamera.transform.position = new Vector3(CurrentCell.transform.position.x, 
      CurrentCell.transform.position.y, GameCamera.transform.position.z);
  }
  public void RollDice()
  {
    GameCamera.transform.position = new Vector3(DiceObject.transform.position.x, DiceObject.transform.position.y, GameCamera.transform.position.z);
    DiceNumber = Random.Range(1, 7);
    Debug.Log(CurrentPlayerID +" "+ DiceNumber);
  }
  public void MovePlayer()
  {
    int tempCellId = CurrentPlayer.CellId + DiceNumber;
    if (tempCellId >= CellArray.Length) { tempCellId = CellArray.Length-1;  }
    MovePlayerOnCell(tempCellId, CurrentPlayerID);
    CheckCell();
  }
  public void CheckCell()
  {
    GameCamera.transform.position = new Vector3(CellArray[CurrentPlayer.CellId].transform.position.x,
      CellArray[CurrentPlayer.CellId].transform.position.y, GameCamera.transform.position.z);
    switch (CurrentCell.cellType)
    {
      case Cell.CellType.normal: break;
      case Cell.CellType.forward: MovePlayerOnCell(CurrentCell.TeleportCell.CellId, CurrentPlayerID); break;
      case Cell.CellType.skip: CurrentPlayer.Skip++;break;
    }
  }
  public void MovePlayerOnCell(int cellId, int playerId)
  {
    PlayerArray[playerId].CellId = cellId;
    PlayerSprites[playerId].transform.position = CellArray[PlayerArray[playerId].CellId].transform.position + new Vector3(playerId * 0.5f - 1, 0, 0);
    GameCamera.transform.position = new Vector3(PlayerSprites[playerId].transform.position.x,
      PlayerSprites[CurrentPlayerID].transform.position.y, GameCamera.transform.position.z);
  }
}
