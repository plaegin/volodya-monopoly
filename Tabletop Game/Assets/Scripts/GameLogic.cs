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

  int CurrentGameState = 0;
  int DiceNumber;
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
    }
    CurrentPlayerID = 0;
    for(int i = 0; i < NumberOfPlayers; i++)
    {
      PlayerSprites[i].transform.position = CellArray[0].transform.position + new Vector3(i*0.5f-1, 0, 0);
    }
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      CurrentGameState++;
      if (CurrentGameState >= 4) CurrentGameState = 0;
      switch (CurrentGameState)
      {
        case 0:NextTurn(); break;
        case 1:RollDice(); break;
        case 2:MovePlayer();break;
        case 3: CheckCell(); break;
        default:NextTurn(); break;
      }
    }
    
  }
  public void NextTurn()
  {
    CurrentPlayerID++;
    if (CurrentPlayerID >= NumberOfPlayers) CurrentPlayerID = 0;
    GameCamera.transform.position = new Vector3(CellArray[PlayerArray[CurrentPlayerID].CellId].transform.position.x, 
      CellArray[PlayerArray[CurrentPlayerID].CellId].transform.position.y, GameCamera.transform.position.z);
  }
  public void RollDice()
  {
    GameCamera.transform.position = new Vector3(DiceObject.transform.position.x, DiceObject.transform.position.y, GameCamera.transform.position.z);
    DiceNumber = Random.Range(1, 7);
    Debug.Log(DiceNumber);
  }
  public void MovePlayer()
  {
    int tempCellId = PlayerArray[CurrentPlayerID].CellId + DiceNumber;
    if (tempCellId >= CellArray.Length) { tempCellId = CellArray.Length-1;  }
    PlayerArray[CurrentPlayerID].CellId = tempCellId;
    PlayerSprites[CurrentPlayerID].transform.position = CellArray[tempCellId].transform.position + new Vector3(CurrentPlayerID * 0.5f - 1, 0, 0);

    GameCamera.transform.position = new Vector3(PlayerSprites[CurrentPlayerID].transform.position.x,
      PlayerSprites[CurrentPlayerID].transform.position.y, GameCamera.transform.position.z);
  }
  public void CheckCell()
  {
    GameCamera.transform.position = new Vector3(CellArray[PlayerArray[CurrentPlayerID].CellId].transform.position.x,
      CellArray[PlayerArray[CurrentPlayerID].CellId].transform.position.y, GameCamera.transform.position.z);
  }
}
