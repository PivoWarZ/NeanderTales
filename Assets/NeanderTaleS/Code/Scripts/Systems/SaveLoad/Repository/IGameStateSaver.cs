using System.Collections.Generic;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository
{
    public interface IGameStateSaver
    {
        void SaveState(Dictionary<string, string> data);
        Dictionary<string, string> LoadState();
    }
}