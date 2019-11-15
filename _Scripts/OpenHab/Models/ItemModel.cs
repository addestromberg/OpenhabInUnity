using K.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace se.Studio13.OpenHabUnity
{
    public class StateDescription
    {
        public int step;
        public string pattern;
        public bool readOnly;
        public List<object> options;
    }

    public class ItemModel
    {
        public string link;
        public string state;
        public StateDescription stateDescription;
        public bool editable;
        public string type;
        public string name;
        public string label;
        public List<object> tags;
        public List<object> groupNames;

        /** For debugging. Library not included.
        public override string ToString()
        {
            return Dumper.Dump(this);
        }
        */
    }
}
