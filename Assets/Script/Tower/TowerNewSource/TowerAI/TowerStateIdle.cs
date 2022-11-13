using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerStateIdle : TowerState
{
    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
	public override void OnStateEnter(TowerState previousState, TowerState newState)
    {
        // Stop to moving and turning
        if (towerBehaviour.navAgent != null)
        {
            towerBehaviour.navAgent.move = false;
            towerBehaviour.navAgent.turn = false;
        }
        // If unit has animator
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            // Search for clip
            foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "Idle")
                {
                    // Play animation
                    anim.SetTrigger("idle");
                    break;
                }
            }
        }
    }
}
