using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.HUD
{
    public class HUDPresenter : ITickable
    {
        private readonly HUDModel _model;
        private readonly HUDView _hudView;

        public HUDPresenter(HUDModel model, HUDView hudView)
        {
            _model = model;
            _hudView = hudView;
        }
        
        public void Tick()
        {
            UpdateView();
        }
        
        private void UpdateView()
        {
            _hudView.SetCoordinates(_model.ShipPosition);
            _hudView.SetRotation(_model.ShipRotation);
            _hudView.SetSpeed(_model.ShipSpeed);
            _hudView.SetLaserCharges(_model.LaserCharges);
            _hudView.SetNextLaserTime(_model.NextLaserTime);
        }
    }
}