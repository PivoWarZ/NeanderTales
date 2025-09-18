using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components
{
    public interface IRotateAsync: IRotatable
    {
        UniTask<UniTask> RotateAsync(Vector3 rotateDirecton, CancellationTokenSource cancell);
    }
}