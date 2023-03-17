using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dlog.Runtime
{
    [CreateAssetMenu(fileName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public Sprite angryIcon;
        public Sprite surprisedIcon;
        public Sprite confidentIcon;
        public Sprite loveIcon;
        public Sprite calmIcon;
        public Sprite tiredIcon;
        public Sprite smileIcon;
        public Sprite talkIcon;
        public Sprite confusedIcon;
    }
}

