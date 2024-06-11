using System.Threading;
using Cysharp.Threading.Tasks;
using HGtest;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Startup : IAsyncStartable
{
    private const int MainSceneIndex = 1;

    public async UniTask StartAsync(CancellationToken cancellation)
    {
         await SceneManager.LoadSceneAsync(MainSceneIndex, LoadSceneMode.Single);

         var mainScene = SceneManager.GetSceneByBuildIndex(MainSceneIndex);

         foreach (var rootGameObject in mainScene.GetRootGameObjects())
         {
             rootGameObject.TryGetComponent(out MainLifetimeScope mainLifetimeScope);

             if (mainLifetimeScope == null) 
                 continue;

             mainLifetimeScope.Build();
             break;
         }
    }
}
