﻿using System;

namespace SS14.Shared.GO.Component.Equippable
{
    [Serializable]
    public class EquippableComponentState : ComponentState
    {
        public EquipmentSlot WearLocation;
        public int? Holder;

        public EquippableComponentState(EquipmentSlot wearLocation, int? holder)
            : base(ComponentFamily.Equippable)
        {
            WearLocation = wearLocation;
            Holder = holder;
        }
    }
}
