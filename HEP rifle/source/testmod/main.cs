using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace HEP_rifle
{
    public class linker : DefModExtension
    {
        public HediffDef effect;
    }
    public class proj : Bullet
    {
        public linker props => def.GetModExtension<linker>(); //linking to XML def
        protected override void Impact(Thing hitThing, bool blockedByShield = false) 
        {
            base.Impact(hitThing, blockedByShield);
            if (props != null && hitThing != null && hitThing is Pawn HitPawn)
            {
                Hediff OnPawn = HitPawn.health?.hediffSet?.GetFirstHediffOfDef(props.effect);
                if (OnPawn == null)
                {
                    Hediff ToAdd = HediffMaker.MakeHediff(props.effect, HitPawn); //create hediff
                    HitPawn.health.AddHediff(ToAdd); //apply hediff 
                }
                else
                {
                    OnPawn.Severity += OnPawn.Severity / 2;
                }
            }
        }
    }
}
