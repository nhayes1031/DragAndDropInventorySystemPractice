using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private AssetReference _lootItemHolderPrefab;
    private static LootSystem _instance;
    private static Queue<LootItemHolder> _lootItemHolders = new Queue<LootItemHolder>();

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public static void Drop(Item item, Transform droppingTransform)
    {
        if (_lootItemHolders.Any())
        {
            var lootItemHolder = _lootItemHolders.Dequeue();
            lootItemHolder.gameObject.SetActive(true);
            AssignItemToHolder(lootItemHolder, item, droppingTransform);
        }
        else
        {
            _instance.StartCoroutine(_instance.DropAsync(item, droppingTransform));
        }
    }

    private IEnumerator DropAsync(Item item, Transform droppingTransform)
    {
        var operation = _lootItemHolderPrefab.InstantiateAsync();
        yield return operation;

        var lootItemHolder = operation.Result.GetComponent<LootItemHolder>();
        AssignItemToHolder(lootItemHolder, item, droppingTransform);
    }

    private static void AssignItemToHolder(LootItemHolder lootItemHolder, Item item, Transform droppingTransform)
    {
        lootItemHolder.TakeItem(item);

        Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle * 3f;
        Vector3 randomPosition = droppingTransform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

        lootItemHolder.transform.position = randomPosition;
    }

    public static void AddToPool(LootItemHolder lootItemHolder)
    {
        lootItemHolder.gameObject.SetActive(false);
        _lootItemHolders.Enqueue(lootItemHolder);
    }
}