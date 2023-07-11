using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTopBlockState
{
    public abstract void Enter(TopBlockStateManager context);         // Requested Methods with a passed component - a class "TopBlockStateManager" that contains Rigidbody, otheer inheritors from "BaseTopBlockSate" and other necessary components
    public abstract void BlockStateUpdate(TopBlockStateManager context); 
    public abstract void Exit(TopBlockStateManager context);
    public abstract void OnCollisonEnter2D(TopBlockStateManager context);


}
