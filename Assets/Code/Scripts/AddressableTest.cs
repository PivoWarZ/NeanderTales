
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.AddressableAssets;

public class AddressableTest : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] Transform _parent;
    [SerializeField] private List<AssetReference> _addressables;

    private void Awake()
    {
        _button.onClick.AddListener(DownloadImage);
    }

    [Button]
    private async void DownloadImage()
    {
        var addres = _addressables[0];
        var prefab = await Addressables.InstantiateAsync(addres, _parent);
    }
}
