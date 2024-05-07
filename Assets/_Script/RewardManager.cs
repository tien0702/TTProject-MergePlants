using System.Linq;
using UnityEngine;

public class RewardManager
{
    #region Singleton
    private static RewardManager instance;

    public RewardManager() 
    {
    }

    public static RewardManager GetInstance()
    {
        if(instance == null) instance = new RewardManager();
        return instance;
    }
    #endregion


}
