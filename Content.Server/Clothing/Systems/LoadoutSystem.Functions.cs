using JetBrains.Annotations;
using Robust.Shared.Serialization.Manager;
using Content.Shared.Clothing.Loadouts.Prototypes;
using Content.Server.NPC.Components;
using Content.Server.NPC.Systems;
using Content.Server.NPC.HTN;
using Content.Server.NPC;
using Robust.Shared.Map;
using System.Numerics;

namespace Content.Server.Clothing.Systems;

[UsedImplicitly]
public sealed partial class LoadoutMakeFollower : LoadoutFunction
{
    public override void OnPlayerSpawn(EntityUid character,
        EntityUid loadoutEntity,
        IComponentFactory factory,
        IEntityManager entityManager,
        ISerializationManager serializationManager)
    {
        var npc = entityManager.System<NPCSystem>();
        var htn = entityManager.System<HTNSystem>();
        if (!entityManager.TryGetComponent<HTNComponent>(loadoutEntity, out var hTNComponent))
            return;

        npc.SetBlackboard(loadoutEntity, NPCBlackboard.FollowTarget, new EntityCoordinates(character, Vector2.Zero), hTNComponent);
        htn.Replan(hTNComponent);
    }
}

// Corvax-Change-Start
/// <summary>
/// Данная функция позволяет перенести вещи из StratingGear.Storage в новый рюкзак.
/// </summary>
[UsedImplicitly]
public sealed partial class LoadoutFiledStorage : LoadoutFunction
{
    public override void OnPlayerSpawn(EntityUid uid,
        EntityUid loadout,
        IComponentFactory factory,
        IEntityManager entityManager,
        ISerializationManager serializationManager)
    {
        var loadoutSys = entityManager.System<LoadoutSystem>();
        loadoutSys.InsertBack(uid, loadout);
    }
}
// Corvax-Change-End
