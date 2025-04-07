using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRocketMoveProvider
{
    public void MoveLeft();

    public void MoveRight();

    public void MoveLeftDouble();

    public void MoveRightDouble();
}
