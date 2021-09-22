using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool CanBePlaced()
    {
        BoardCell overlappedBoardCell = SnapController.Instance.GetClosestSnapPointTransform(this);

        return overlappedBoardCell != null;
    }
}
