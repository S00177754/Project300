//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public class SteamVR_Input_ActionSet_MinimapSelector : Valve.VR.SteamVR_ActionSet
    {
        
        public virtual SteamVR_Action_Boolean Select
        {
            get
            {
                return SteamVR_Actions.minimapSelector_Select;
            }
        }
        
        public virtual SteamVR_Action_Boolean ActivateMenu
        {
            get
            {
                return SteamVR_Actions.minimapSelector_ActivateMenu;
            }
        }
        
        public virtual SteamVR_Action_Vector2 MenuSelectionPosition
        {
            get
            {
                return SteamVR_Actions.minimapSelector_MenuSelectionPosition;
            }
        }
        
        public virtual SteamVR_Action_Boolean InteractClick
        {
            get
            {
                return SteamVR_Actions.minimapSelector_InteractClick;
            }
        }
        
        public virtual SteamVR_Action_Boolean ReturnToCommandCenter
        {
            get
            {
                return SteamVR_Actions.minimapSelector_ReturnToCommandCenter;
            }
        }
        
        public virtual SteamVR_Action_Vibration Haptic
        {
            get
            {
                return SteamVR_Actions.minimapSelector_Haptic;
            }
        }
    }
}
