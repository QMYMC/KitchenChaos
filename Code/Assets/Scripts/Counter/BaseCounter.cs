using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;
    
    public virtual void InterAction(Player player)
    {
        Debug.LogWarning("未对InterAction方法进行重写");
    }

    public virtual void InterActionOperate(Player player)
    {
        Debug.LogWarning("未对InterActionOperate方法进行重写");
    }
    
    public void SelectCounter()
    {
        selectedCounter.SetActive(true);
    }

    public void CancelSelect()
    {
        selectedCounter.SetActive(false);
    }
}
