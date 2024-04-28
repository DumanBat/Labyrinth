using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public interface ITarget
    {
        public IHealth Health { get; }
        public IPosition Position { get; }
    }
}
