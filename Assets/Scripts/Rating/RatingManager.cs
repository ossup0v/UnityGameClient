using System;
using System.Collections.Generic;
using System.Linq;

public static class RatingManager
{
    public static Action RatingChaneged = delegate { };
    public static Dictionary<int, RatingEntity> Rating = new Dictionary<int, RatingEntity>();

    public static void Init(RatingEntity[] ratingEntities)
    {
        Rating = ratingEntities.ToDictionary(x => x.Id, x => x);
     
        RatingChaneged();
    }

    public static void Update(RatingEntity entity)
    {
        Rating[entity.Id] = entity;

        RatingChaneged();
    }

    public static void UpdateKillAndDeath(int killer, int died)
    {
        Rating[killer].Killed++;
        Rating[died].Died++;

        RatingChaneged();
    }

    public static void UpdateDeath(int died)
    {
        Rating[died].Died++;

        RatingChaneged();
    }
}
