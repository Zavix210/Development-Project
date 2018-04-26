using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimulationSystem;
using System;
using SceneBuilderWpf.DataModels;

public class ParticleController : SimulationComponentBase
{
    ParticlePlayer _particlePlayer;

    public ParticleController(SimulationController controller) : base(controller)
    {
        _particlePlayer = GameObject.FindObjectOfType<ParticlePlayer>();
        if (!_particlePlayer)
            Debug.LogError("Particle player not found on scene!");
    }

    public override bool IsMessageRouteValid(int route)
    {
        return route == (int)MessageDestination.SCENE_CHANGE || route == (int)MessageDestination.SIMULATION_PAUSE || route == (int)MessageDestination.SIMULATION_RESUME;
    }

    public override void OnInitialize()
    {
        //
    }
    public override void OnReceivedMessage(Message message)
    {
        // A scene was changed (next video?)
        if (message.Route == (int)MessageDestination.SCENE_CHANGE)
        {
            // Is the scene valid?
            if (message.Identifier == "VALID")
            {
                _particlePlayer.ClearParticle();
                // Get the scene node from the message
                SimulationScene scene = (SimulationScene)message.Data;

                string particleX, particleY, particleZ, particleType, particleIntensity;
                float x = 0.0f;
                float y = 0.0f;
                float z = 0.0f;
                float intensity = 0.0f;
                Actions action;

                if (scene.GetAttribute("PARTICLE_TYPE", out particleType))
                {
                    action = (Actions)Int32.Parse(particleType);
                }
                else
                {
                    //no particles
                    return;
                }

                if (scene.GetAttribute("PARTICLE_X", out particleX))
                {
                    x = float.Parse(particleX);
                }
                if (scene.GetAttribute("PARTICLE_Y", out particleY))
                {
                    y = float.Parse(particleY);
                }
                if (scene.GetAttribute("PARTICLE_Z", out particleZ))
                {
                    z = float.Parse(particleZ);
                }
                if (scene.GetAttribute("PARTICLE_INTENSITY", out particleIntensity))
                {
                    intensity = float.Parse(particleIntensity);
                }

                Vector3 particlePos = new Vector3(x, y, z);
                _particlePlayer.CreateParticle(action, particlePos, intensity);
            }
        }
    }
}
