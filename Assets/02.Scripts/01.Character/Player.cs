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

        // �÷��̾� �׾��� �� �̺�Ʈ �߻�
        OnDeath?.Invoke();
      
    }
}
