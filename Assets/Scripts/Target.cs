using UnityEngine;

public class Target : MonoBehaviour
{
    public void OnCannonHit()
    {
        GameManager.Instance.OnEnemyHit();
        Destroy(gameObject);
    }
}