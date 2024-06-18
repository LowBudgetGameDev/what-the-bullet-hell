using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    public void OnShoot(Transform bullet);

    public void OnAdded();

    public void SetIsCounter(bool isCounter);
}
