using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }
    private PlayerMovementManager movementManager;
    private BaseCounter selectedCounter;
    private bool isWalking; // 重新添加 isWalking 属性

    private void Awake()
    {
        Instance = this;
        movementManager = GetComponent<PlayerMovementManager>();
    }

    private void Update()
    {
        HandleInteraction();

        Vector3 direction = movementManager.GetMovementDirection();
        isWalking = direction != Vector3.zero; // 更新 isWalking 状态
        movementManager.Move(direction);

        if (Input.GetKeyDown(KeyBindingManager.Instance.GetActionKey()))
        {
            selectedCounter?.InterAction(this);
        }
        if (Input.GetKeyDown(KeyBindingManager.Instance.GetOperateKey()))
        {
            selectedCounter?.InterActionOperate(this);
        }
    }

    private void HandleInteraction()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.5f; // 射线从玩家中心上方一点发出
        bool hitDetected = Physics.Raycast(rayStart, transform.forward, out RaycastHit hitInfo, 2.5f, LayerMask.GetMask("Counter"));

        if (!hitDetected)
        {
            // 若Raycast未命中，使用SphereCast作为备选方案
            hitDetected = Physics.SphereCast(rayStart, 0.3f, transform.forward, out hitInfo, 2.5f, LayerMask.GetMask("Counter"));
        }

        if (hitDetected && hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
        {
            SetSelectedCounter(counter);
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    public void SetSelectedCounter(BaseCounter counter)
    {
        if (counter != selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();
            selectedCounter = counter;
        }
    }

    public bool IsWalking // 公开的 IsWalking 属性
    {
        get { return isWalking; }
    }
}