using System.Linq;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class InventoryEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Inventory Editor")]
        private static void Open()
        {
            InventoryEditorWindow window = GetWindow<InventoryEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true)
            {
                DefaultMenuStyle =
                {
                    IconSize = 28.00f
                },
                Config =
                {
                    DrawSearchToolbar = true
                }
            };

            tree.AddAllAssetsAtPath("", "Assets/Data/Items", typeof(Item), true);
            
            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuItem selected = this.MenuTree.Selection.FirstOrDefault();
            int toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if(selected != null)
                    GUILayout.Label(selected.Name);

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
                {
                    ScriptableObjectCreator.ShowDialog<Item>("Assets/Data/Items", obj =>
                    {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj);
                    });
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Character")))
                {
                    
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}