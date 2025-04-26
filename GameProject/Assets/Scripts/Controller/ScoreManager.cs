using YanGameFrameWork.ModelControlSystem;


/// <summary>
/// 游戏内部的分数，玩家不直接看到，用于计算fever模式和退出fever模式
/// </summary>
public class ScoreManager :YanModelBase
{
    public int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void ResetScore()
    {
        score = 0;
    }



    public void LoseScore(int amount)
    {
        score -= amount;
    }
    
}
