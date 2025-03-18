using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Turret, IAttackable
{
    [BoxGroup("����"),LabelText("������Ʈ Ǯ")]
    [SerializeField] private ObjectPool _objectPool; // ������Ʈ Ǯ
    [BoxGroup("����"),LabelText("�Ѿ� ������")]
    [SerializeField] private GameObject _projectilePrefab;  // �߻��� �Ѿ� ������
    [BoxGroup("����"),LabelText("�ѱ� ��ġ")]
    [SerializeField] private Transform _firePoint;          // �߻� ��ġ

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();

        if (_objectPool == null)
        {
            _objectPool = GetComponentInChildren<ObjectPool>();
            if (_objectPool == null)
            {
                GameObject poolObject = new GameObject("ObjectPool");
                poolObject.transform.SetParent(transform); // �θ� ����
                _objectPool = poolObject.AddComponent<ObjectPool>();
            }
        }

        if (_projectilePrefab == null)
        {
            Debug.LogError("�Ѿ� �������� �����ϴ�.");
            return;
        }

        _objectPool.InitializePool(_projectilePrefab);
    }

    [Button]
    public virtual void Attack()
    {
        GameObject bullet = _objectPool.GetBullet();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
    }
}
