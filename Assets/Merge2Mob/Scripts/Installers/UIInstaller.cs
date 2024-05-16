
using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Containers;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using MergeTwoMob.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MergeTwoMob.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [Inject] private UIContainer _uiContainer;
        [SerializeField] private Transform rootForms;
        
        private void InstallForms()
        {
            var forms = rootForms.GetComponentsInChildren<IForm>();

            foreach (var form in forms)
            {
                diContainer.Inject(form);
                _uiContainer.AddForm(form);
            }
            SceneManager.LoadSceneAsync(CONSTANTS.GAME_SCENE, LoadSceneMode.Additive);
        }

        public override void InstallBindings()
        {
            InstallForms();
        }
    }
}
