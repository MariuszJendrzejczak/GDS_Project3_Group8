using UnityEngine;

public interface IRespawnAble
{
    public void RespawnMe();
    public void OnMyDeath();
    public void CancelRespawn(GameObject value);
}
