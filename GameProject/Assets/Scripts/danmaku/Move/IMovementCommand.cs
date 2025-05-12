using UnityEngine;

/// <summary>
/// 移动命令接口
/// </summary>
public interface IMovementCommand
{
    /// <summary>
    /// 执行移动命令
    /// </summary>
    /// <param name="enemy">要移动的敌人对象</param>
    void Execute(Enemy enemy);
}




