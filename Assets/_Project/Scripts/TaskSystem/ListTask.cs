using System;
using System.Collections.Generic;

namespace Task
{
    [Serializable]
    public class ListTask
    {
        public int TaskIndex;
        public EnumPotionTask PotionTaskEnum;
        public List<ItemTaskConfig> listTasks;

        public void IndexTask(int index)
        {
            TaskIndex = index;
        }
    }

}
