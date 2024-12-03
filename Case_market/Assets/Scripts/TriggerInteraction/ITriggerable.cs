


// using Scripts.GameScripts.CharacterModule;
using UnityEngine;

namespace Template
{
    public interface ITriggerable
    {
        void TriggerEnter(BaseCharacter character);
        void TriggerExit(BaseCharacter character);
    }
}