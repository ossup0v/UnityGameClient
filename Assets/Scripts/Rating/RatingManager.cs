using System;
using System.Collections.Generic;
using System.Linq;

public static class RatingManager
{
    public static Action RatingChanged = delegate { };
    public static Dictionary<Guid, RatingEntity> Rating = new Dictionary<Guid, RatingEntity>();

    public static void Init(RatingEntity[] ratingEntities)
    {
        Rating = ratingEntities.ToDictionary(x => x.Id, x => x);
     
        RatingChanged();
    }

    public static void Update(RatingEntity entity)
    {
        Rating[entity.Id] = entity;

        RatingChanged();
    }

    public static void UpdateKillAndDeath(Guid killer, Guid died)
    {
        Rating[killer].Killed++;
        Rating[died].Died++;

        RatingChanged();
    }

    public static void UpdateDeath(Guid died)
    {
        Rating[died].Died++;

        RatingChanged();
    }

    public static void UpdateBotKills(Guid killerId, int killCount)
    {
        Rating[killerId].KilledBots = killCount;

        RatingChanged();
    }
}
