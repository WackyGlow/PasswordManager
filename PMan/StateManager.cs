using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMan.Entity;

namespace PMan
{
    public sealed class StateManager
    {
        public string hashedPassword { get; set; }
        private static readonly Lazy<StateManager> lazyInstance = new Lazy<StateManager>(() => new StateManager());

        public static StateManager Instance => lazyInstance.Value;

        // Add other properties and methods as needed

        private StateManager()
        {
            // Initialize the state manager if needed
        }

    }

}
