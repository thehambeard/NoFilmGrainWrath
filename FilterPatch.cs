using Kingmaker.Visual;
using Owlcat.Runtime.Visual.RenderPipeline.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine.SceneManagement;
using static NoFilmGrainWrath.Main;
using UnityEngine;
using UnityEngine.Rendering;
using ModMaker;
using Kingmaker.PubSubSystem;
using Kingmaker;
using System.Reflection;
using System.Reflection.Emit;
using Owlcat.Runtime.Visual.RenderPipeline.PostProcess;

namespace NoFilmGrainWrath
{
    class FilterPatch
    {
        [HarmonyPatch(typeof(PostProcessUtils))]
        [HarmonyPatch("ConfigureFilmGrain")]
        [HarmonyPatch(new Type[] { typeof(PostProcessData), typeof(FilmGrain), typeof(Camera), typeof(Material) } )]
        static class FilmGrain_Patch
        {
            static void Postfix(PostProcessData data, FilmGrain settings, Camera camera, Material material)
            {
                if(Settings.disableFilmGrain)
                    material.SetVector(Shader.PropertyToID("_Grain_Params"), new Vector2(0f, 0f));
            }
        }
    }
}
