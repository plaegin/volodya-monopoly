using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
  public enum CellType { normal, skip, forward, backward}
  [SerializeField] CellType cellType;
  [SerializeField] GameObject TeleportCell;

}
