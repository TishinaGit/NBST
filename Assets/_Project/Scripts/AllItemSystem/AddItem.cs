using Inventory;
using System.Collections;
using UnityEngine; 

public class AddItem : MonoBehaviour
{  
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _dropItem;
    [SerializeField] private InventoryPanel _inventoryPanel;
    [SerializeField] private Sprite _sprite;

    private bool _checkDeathEnemy;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _inventoryPanel = (InventoryPanel)FindObjectOfType(typeof(InventoryPanel));
    }

    private void Update()
    {
        StartCoruoutineDeath(); 
    }
     

    private void StartCoruoutineDeath()
    {
        if (_checkDeathEnemy == true)
        {
            if (_dropItem != null)
            {
                StopCoroutine(MoveObject());
            }
            StartCoroutine(MoveObject());
        }

    }

    public void EnemyDeathBool()
    {
        _checkDeathEnemy = true;
    }

    public IEnumerator MoveObject()
    { 
        float currentMovementTime = 0.1f;
        _dropItem.transform.parent = null;
        while (Vector3.Distance(_dropItem.transform.position, _player.transform.position) > 0 && _dropItem != null)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_dropItem.transform.position, _player.transform.position, currentMovementTime * Time.deltaTime );
            yield return null;
        }
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddItems();
            Destroy(_dropItem);  
        }
    }

    private void AddItems()
    {
        _inventoryPanel.AddItem(ItemTypeEnum.Apple, 1, 14);
    }
}
