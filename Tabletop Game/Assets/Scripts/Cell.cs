using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
  public enum CellType { normal, skip, forward, backward}
  [SerializeField] public CellType cellType;
  [SerializeField] public Cell TeleportCell;
  public int CellId;

}
