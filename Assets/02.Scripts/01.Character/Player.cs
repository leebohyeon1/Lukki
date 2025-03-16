using Sirenix.OdinInspector;
using System;

public class Player : Character
{
    public static event Action OnDeath;

    void Update()
    {
        
    }

    [Button]
    protected override void Die()
    {
        base.Die();

        // 플레이어 죽었을 때 이벤트 발생
        OnDeath?.Invoke();
      
    }
}
