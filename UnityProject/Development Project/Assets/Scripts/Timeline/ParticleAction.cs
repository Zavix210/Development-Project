using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneBuilderWpf.DataModels;
using SimulationSystem;
using System;

public class ParticleAction : TimelineActionBase
{
    private List<SceneBuilderWpf.DataModels.Action> _particleActions;

    public override void ExecuteAction()
    {
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;
        ParticleController particleController = controller.GetSimulationComponent<ParticleController>();
        particleController.CreateParticles(_particleActions);
    }

    public void AddParticleAction(SceneBuilderWpf.DataModels.Action action)
    {
        if (_particleActions == null)
            _particleActions = new List<SceneBuilderWpf.DataModels.Action>();
        _particleActions.Add(action);
    }
}
