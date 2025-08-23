using GameEngine.Systems;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public sealed class Test : MonoBehaviour
    {
        [SerializeField] private UnitManager _unitManager;
        [SerializeField] private ResourceService _resourceService;

        [Inject]
        public void Construct(UnitManager unitManager, ResourceService resourceService)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
        }
    }
}