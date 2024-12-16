using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camarselect : MonoBehaviour
{
  private CinemachineTargetGroup _targetGroup;
  
  private GameObject[] _player;
  
 private void Start()
  {
      _targetGroup = GetComponent<CinemachineTargetGroup>();
      _player = GameObject.FindGameObjectsWithTag("Player");

      foreach (GameObject player in _player)
      {
          _targetGroup.AddMember(player.transform, 1f, 1f);
      }
    
    
  }
    
}
