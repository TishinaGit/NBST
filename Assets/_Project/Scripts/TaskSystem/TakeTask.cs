using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class TakeTask : MonoBehaviour
{
    [SerializeField] private ListTaskSo _listTaskSo;
    [SerializeField] private List<UITaskData> _uiTaskData;

    public int _saveIndexCurrentTask;

    private void Awake()
    {
        RandomPotion();
        InitIndexTask();
        
    } 

    private void InitIndexTask()
    {
        for (int i = 0; i < _listTaskSo.listTasks.Count; i++)
        {
              _listTaskSo.listTasks[i].IndexTask(i);
        }
    }

    private void RandomPotion()
    {
       // int _saveIndex = PlayerPrefs.GetInt("_saveIndexCurrentTask");
        var task = _listTaskSo.listTasks[_saveIndexCurrentTask];
        for (int i = 0; i < task.listTasks.Count; i++)
        {
            _uiTaskData[i]._description.text = task.listTasks[i].DescriptionItem;
            _uiTaskData[i]._spritePotion.sprite = task.listTasks[i].AvatarItem;
            _uiTaskData[i]._spritePotionComplete.sprite = task.listTasks[i].AvatarItem;
            _uiTaskData[i]._itemType = task.listTasks[i].ItemTypeEnum;
            _uiTaskData[i]._countPotion.text = task.listTasks[i].Count.ToString(); 
        }
    }

    public void PlusIndexList(int index)
    {
        if (_saveIndexCurrentTask >= _listTaskSo.listTasks.Count)
        {
            return;
        }
        else
        {
            _saveIndexCurrentTask += index;
            PlayerPrefs.SetInt("_saveIndexCurrentTask", _saveIndexCurrentTask);
            RandomPotion();
        } 
        
    }

}
