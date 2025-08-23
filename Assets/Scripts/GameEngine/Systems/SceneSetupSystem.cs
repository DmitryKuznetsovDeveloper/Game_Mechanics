using System.Linq;
using GameEngine.Objects;
using UnityEngine;
using Zenject;

namespace GameEngine.Systems
{
    public sealed class SceneSetupSystem : IInitializable
    {
        private readonly UnitManager _unitManager;
        private readonly ResourceService _resourceService;

        public SceneSetupSystem(
            UnitManager unitManager,
            ResourceService resourceService)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
        }

        public void Initialize()
        {
            _unitManager.SetupUnits(Object.FindObjectsByType<Unit>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            ).ToList());
            
            _resourceService.SetResources(Object.FindObjectsByType<Resource>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            ).ToList());
        }
    }
}