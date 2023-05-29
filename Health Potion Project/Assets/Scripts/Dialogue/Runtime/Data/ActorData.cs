using System;
using UnityEngine;

namespace Dlog.Runtime {
    [Serializable]
    public class ActorData {
        public string Name;
        public ActorScriptableObject CustomData;
        public Property Property;

        public ActorData(string name, ActorScriptableObject customData, Property property) {
            Name = name;
            CustomData = customData;
            Property = property;
        }
    }
}